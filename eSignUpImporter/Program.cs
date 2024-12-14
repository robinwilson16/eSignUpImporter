using eSignUpImporter.Data;
using eSignUpImporter.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Web;

namespace eSignUpImporter
{
    class Program
    {
        static IConfiguration? _config { get; set; }
        private static ApplicationDbContext? _context;
        public static APIAccessToken? APIAccessToken { get; set; }
        public static IList<Candidate>? Candidates { get; set; }
        public static IList<EnrolmentRequest>? EnrolmentRequests { get; set; }
        public static int? MaxEnrolmentRequestID { get; set; }
        public static string? APIToken { get; set; }
        public static bool CanConnect { get; set; }
        public static bool LoginExpired { get; set; }
        public static int StatusCodeNumber { get; set; }
        public static string? StatusCodeString { get; set; }
        public static string? ErrorMessage { get; set; }

        public static string? DoImport { get; set; }

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("\neSignUp Importer");
            Console.WriteLine("=========================================\n");

            string? productVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            Console.WriteLine($"Version {productVersion}");
            Console.WriteLine($"Copyright Robin Wilson");

            string configFile = "appsettings.json";
            string? customConfigFile = null;
            if (args.Length >= 1)
            {
                customConfigFile = args[0];
            }

            if (!string.IsNullOrEmpty(customConfigFile))
            {
                configFile = customConfigFile;
            }

            Console.WriteLine($"\nUsing Config File {configFile}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFile, optional: false);

            try
            {
                _config = builder.Build();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return 1;
            }

            Console.WriteLine($"\nSetting Locale To {_config["Locale"]}");

            //Set locale to ensure dates and currency are correct
            CultureInfo culture = new CultureInfo(_config["Locale"] ?? "en-GB");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            var databaseConnection = _config.GetSection("DatabaseConnection");
            var databaseTable = _config.GetSection("DatabaseTable");
            var excelFile = _config.GetSection("ExcelFile");
            var ftpConnection = _config.GetSection("FTPConnection");
            var storedProcedure = _config.GetSection("StoredProcedure");
            string[]? filePaths = { @excelFile["Folder"] ?? "", excelFile["FileName"] ?? "" };
            string excelFilePath = Path.Combine(filePaths);
            string? excelFileNameNoExtension = excelFile["FileName"]?.Substring(0, excelFile["FileName"]!.LastIndexOf("."));

            var conStrBuilder = new SqlConnectionStringBuilder(
                _config.GetConnectionString("DefaultConnection2") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));

            var sqlConnection = new SqlConnectionStringBuilder
            {
                DataSource = databaseConnection["Server"],
                UserID = databaseConnection["Username"],
                Password = databaseConnection["Password"],
                IntegratedSecurity = databaseConnection.GetValue<bool>("UseWindowsAuth", false),
                InitialCatalog = databaseConnection["Database"],
                TrustServerCertificate = true
            };

            //If not using windows auth then need username and password values too
            if (sqlConnection.IntegratedSecurity == false)
            {
                sqlConnection.UserID = databaseConnection["Username"];
                sqlConnection.Password = databaseConnection["Password"];
            }

            var connectionString = conStrBuilder.ConnectionString;
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetService<ApplicationDbContext>();

            Console.WriteLine($"\nObtaining Access Token");
            
            HttpClient httpClient = new HttpClient();

            APIToken = await GetAPIToken(httpClient);

            if (APIToken != null) 
            {
                Console.WriteLine($"Access Token is: {APIToken}");
            }
            else
            {
                Console.WriteLine($"Could Not Obtain Access Token. Please Check the {configFile} Config File");
                return 1;
            }


            Console.WriteLine($"\nImporting Data from eSignUp");

            Console.WriteLine($"Importing Candidates from API");

            Console.WriteLine(await GetCandidates(httpClient));

            Console.WriteLine(GetCandidatesForImport());

            IList<Candidate>? deduplicatedCandidates = DeduplicateCandidates();

            if (deduplicatedCandidates?.Count() < Candidates?.Count())
            {
                Console.WriteLine($"Found {Candidates?.Count() - deduplicatedCandidates?.Count()} Duplicate Candidates Sent by API. Deduplicating...");

                Candidates = deduplicatedCandidates;
            }

            Console.WriteLine($"\nAccessing Enrolment Requests from ProSolution");

            Console.WriteLine($"Comparing {Candidates?.Count()} Candidates to ProSolution Enrolment Requests");

            Console.WriteLine(await GetEnrolmentRequests());

            Console.WriteLine(MatchCandidatesToEnrolmentRequests());

            Console.WriteLine($"Found {Candidates?.Where(c => c.CandidateEnrolmentRequest == null).Count()} New Learners to Import");

            Console.WriteLine($"Importing New Learners into ProSolution");

            Console.WriteLine(await GetMaxEnrolmentRequestID());

            if (MaxEnrolmentRequestID > 0)
            {
                Console.WriteLine(ImportNewLearners());

                //Console.WriteLine(await InsertEnrolmentRequests());
            }

            return 0;
        }

        public static async Task<string> GetAPIToken(HttpClient httpClient)
        {
            string? apiToken = APIToken;

            var apiSettings = _config?.GetSection("APIClient");
            string endpointLogin = apiSettings?["Endpoint"] + $"Login/GetAccessToken?Client={apiSettings?["Client"]}&Secret={apiSettings?["Secret"]}";

            try
            {
                APIAccessToken = await httpClient.GetFromJsonAsync<APIAccessToken>(endpointLogin);
                apiToken = APIAccessToken?.Token;
            }
            catch (HttpRequestException e)
            {
                
                Console.WriteLine(EndpointException(e, null));
                return "";
            }

            return apiToken ?? "";
        }

        public static async Task<string> GetCandidates(HttpClient httpClient)
        {
            var apiSettings = _config?.GetSection("APIClient");
            string endpointCandidates = apiSettings?["Endpoint"] + "Candidates/GetAll";
            string importResult = "";

            if (string.IsNullOrEmpty(APIToken))
            {
                Console.WriteLine($"\nAPI Token Missing Or Invalid. Please Try Again");
            }
            else
            {
                try
                {
                    //Add API Token to API GET and POST Requests
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIToken);

                    //Candidates = await httpClient.GetFromJsonAsync<List<Candidate>>(endpointCandidates); // Does not allow GET command so must use POST
                    var response = await httpClient.PostAsJsonAsync(endpointCandidates, DoImport);

                    if (response.IsSuccessStatusCode)
                    {
                        CanConnect = true;
                        Candidates = await response.Content.ReadFromJsonAsync<List<Candidate>>();

                        importResult = $"Imported {Candidates?.Count ?? 0} Candidates from eSignUp";
                    }
                    else
                    {
                        CanConnect = false;

                        StatusCodeNumber = (int)response.StatusCode;
                        StatusCodeString = response.StatusCode.ToString();

                        if (StatusCodeNumber == 401)
                        {
                            //Login Expired Attempt Login Again
                            LoginExpired = true;

                            importResult = $"Could not import any candidates due to an error";
                            ErrorMessage = $"Login Expired During Request. Please Try Again";
                        }
                        else
                        {
                            importResult = $"Could not import any candidates due to an error";
                            ErrorMessage = $"Sorry There Was An Error ({StatusCodeString}). Please Try Again";
                        }
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(EndpointException(e, null));
                    return "";
                }
            }

            return importResult;
        }

        public static string GetCandidatesForImport()
        {
            var candidateImportSettings = _config?.GetSection("CandidateImport");
            int registrationStatusToImport = int.Parse(candidateImportSettings?["RegistrationStatusToImport"] ?? "");

            string returnMsg;
            IList<Candidate>? importableCandidates = new List<Candidate>();
            importableCandidates = Candidates?.Where(c => c.CandidateRegistrationStatusID == registrationStatusToImport).ToList();

            returnMsg = $"{importableCandidates?.Count ?? 0} Candidates Set To Be Imported with Registration Status {registrationStatusToImport.ToString()}";
            Candidates = importableCandidates;

            return returnMsg;
        }

        public static IList<Candidate>? DeduplicateCandidates()
        {
            IList<Candidate>? distinctCandidates = new List<Candidate>();

            //candidates = Candidates?.Distinct().ToList();

            if (Candidates != null)
            {
                foreach (var candidate in Candidates)
                {
                    var existingCandidate = distinctCandidates.FirstOrDefault(c => c.ID == candidate.ID);

                    if (existingCandidate == null)
                    {
                        distinctCandidates.Add(candidate);
                    }
                }
            }


            return distinctCandidates;
        }

        public static async Task<string> GetEnrolmentRequests()
        {
            var enrolmentRequestSettings = _config?.GetSection("EnrolmentRequests");
            string? academicYearID = enrolmentRequestSettings?["AcademicYearID"];

            string miSystemResult;

            try
            {
                EnrolmentRequests = await _context?.EnrolmentRequest
                    .Where(e => e.AcademicYearId == academicYearID)
                    .ToListAsync()!;

                miSystemResult = $"Found {EnrolmentRequests?.Count ?? 0} Enrolment Requests in ProSolution in {academicYearID}";


            }
            catch (Exception ex)
            {
                miSystemResult = $"Could not find any Enrolment Requests from ProSolution due to an error";
                Console.WriteLine($"Error Connecting to ProSolution ({ex.Message}). Please Try Again");
            }

            return miSystemResult;
        }

        public static string MatchCandidatesToEnrolmentRequests()
        {
            if (Candidates != null)
            {
                foreach (var candidate in Candidates)
                {

                    //Check if learner already imported into ProSolution
                    var foundEnrolmentRequest = EnrolmentRequests?.Where(e => e.Email == candidate.EmailAddress).FirstOrDefault();

                    if (foundEnrolmentRequest != null)
                    {
                        CandidateEnrolmentRequest candidateEnrolmentRequest = new CandidateEnrolmentRequest()
                        {
                            CandidateID = candidate.ID,
                            Email = candidate.EmailAddress,
                            EnrolmentRequestID = foundEnrolmentRequest.EnrolmentRequestId
                        };

                        //Add found record to list
                        //CandidateEnrolmentRequests?.Add(candidateEnrolmentRequest);

                        Candidates
                            .FirstOrDefault(c => c.ID == candidate.ID)!
                            .CandidateEnrolmentRequest = candidateEnrolmentRequest;

                    }
                }
            }

            return $"{Candidates?.Where(c => c.CandidateEnrolmentRequest != null).Count()} Learners Already Imported into ProSolution";
        }

        public static async Task<string> GetMaxEnrolmentRequestID()
        {
            string miSystemResult;

            try
            {
                //Models.EnrolmentRequest? maxEnrolRequest;
                //maxEnrolRequest = await _context.EnrolmentRequest.OrderByDescending(e => e.EnrolmentRequestId).FirstOrDefaultAsync();

                MaxEnrolmentRequestID = await _context?.EnrolmentRequest!.MaxAsync(e => (int?)e.EnrolmentRequestId)!;

                miSystemResult = $"Highest Enrolment Request ID in Database is {MaxEnrolmentRequestID}";


            }
            catch (Exception ex)
            {
                miSystemResult = $"Could not find any Enrolment Requests from ProSolution due to an error";
                Console.WriteLine($"Error Connecting to ProSolution ({ex.Message}). Please Try Again");
            }

            return miSystemResult;
        }

        public static string ImportNewLearners()
        {
            var enrolmentRequestSettings = _config?.GetSection("EnrolmentRequests");
            string? academicYearID = enrolmentRequestSettings?["AcademicYearID"];

            Models.EnrolmentRequest? newEnrolmentRequest;
            IList<Candidate>? candidatesToImport = Candidates?.Where(c => c.CandidateEnrolmentRequest == null).ToList();
            EnrolmentRequests = new List<Models.EnrolmentRequest>();
            int? currentEnrolmentRequestID = MaxEnrolmentRequestID;

            if (candidatesToImport != null)
            {
                foreach (var candidate in candidatesToImport)
                {
                    //Add 1 to the current max enrolment request ID
                    currentEnrolmentRequestID += 1;

                    newEnrolmentRequest = new Models.EnrolmentRequest()
                    {
                        //EnrolmentRequestId = currentEnrolmentRequestID ?? default, //Better to use an auto id so as not to clash with records imported from the web toolkit so start at 1
                        RequestDate = DateTime.Now,
                        RequestStatus = "To Do",
                        Surname = candidate.Surname,
                        FirstForename = candidate.FirstNames?.TrimEnd().Split(" ").FirstOrDefault() ?? candidate.FirstNames?.TrimEnd(),
                        Title = candidate.Title,
                        DateOfBirth = candidate.DateOfBirth,
                        Sex = candidate.Sex,
                        Address1 = candidate.HomeAddress1,
                        Address2 = candidate.HomeAddress2,
                        Address3 = candidate.HomeAddress3,
                        Address4 = candidate.HomeAddress4,
                        PostcodeOut = candidate.HomePostCode?.Length > 3 ? candidate.HomePostCode.TrimEnd().Substring(0, candidate.HomePostCode.TrimEnd().Length - 3).TrimEnd() : "",
                        PostcodeIn = candidate.HomePostCode?.Length > 3 ? candidate.HomePostCode?.TrimEnd().Substring(candidate.HomePostCode.TrimEnd().Length - 3) : "",
                        Tel = candidate.TelephoneNumber,
                        Contact1 = candidate.EmegencyContactFullName,
                        Contact1Tel = candidate.EmergencyContactTelNumber,
                        DisabilityId = null,
                        NationalityId = candidate.CountryOfNationalityName?.TrimEnd() == "GB" ? "XF" : candidate.CountryOfNationalityName?.TrimEnd(),
                        EthnicGroupId = candidate.CandidateEthnicityIlrCode,
                        Email = candidate.EmailAddress,
                        OfferingId = null,
                        PaymentStatus = "Pending",
                        RestrictedUseIndicator = null,
                        SentMarketingInfo = false,
                        AcademicYearId = academicYearID ?? "24/25",
                        Include = false,
                        PriorAttainmentLevelId =
                            candidate.CandidateHighestLevelID == 1 ? "09" :
                            candidate.CandidateHighestLevelID == 2 ? "07" :
                            candidate.CandidateHighestLevelID == 3 ? "01" :
                            candidate.CandidateHighestLevelID == 4 ? "02" :
                            candidate.CandidateHighestLevelID == 5 ? "03" :
                            candidate.CandidateHighestLevelID == 6 ? "04" :
                            candidate.CandidateHighestLevelID == 7 ? "05" :
                            candidate.CandidateHighestLevelID == 8 ? "12" :
                            candidate.CandidateHighestLevelID == 9 ? "13" :
                            candidate.CandidateHighestLevelID == 10 ? "97" :
                            candidate.CandidateHighestLevelID == 11 ? "98" :
                            candidate.CandidateHighestLevelID == 12 ? "99" :
                            "98",
                        EmployerRelease = false,
                        EmployerPaying = false,
                        EmployerName = null,
                        EmployerAddress1 = null,
                        EmployerAddress2 = null,
                        EmployerAddress3 = null,
                        EmployerAddress4 = null,
                        EmployerPostcodeOut = null,
                        EmployerPostcodeIn = null,
                        EmployerTel = null,
                        Md5 = null,
                        PassDetailsTutor = false,
                        LearningDifficultyId = null,
                        MobileTel = candidate.MobileNumber,
                        EmployerSizeTypeId = null,
                        EmploymentStatusBeforeEsfid = null,
                        UnemploymentDurationId = null,
                        Contact2 = candidate.ParentGuardianName,
                        Contact2Tel = candidate.ParentGuardianTelNumber,
                        EuroResidentId = candidate.OfficialUseEuCitizen ?? false,
                        CountryId = candidate.HomeAddressCountry?.TrimEnd() == "GB" ? "XF" : candidate.HomeAddressCountry?.TrimEnd(),
                        StudyElsewhere = false,
                        SchoolId = null,
                        Ni = candidate.NationalInsuranceNumber,
                        EmployerId = null,
                        AltAddress1 = null,
                        AltAddress2 = null,
                        AltAddress3 = null,
                        AltAddress4 = null,
                        AltPostcodeOut = null,
                        AltPostcodeIn = null,
                        AltTel1 = null,
                        FeeExemptionReasonId = null,
                        WebSiteSearchTexts = null,
                        WebPaymentId = null,
                        HeardAboutCollegeId = null,
                        CarReg = null,
                        CarMake = null,
                        CarModel = null,
                        SchoolAttendedFrom = null,
                        SchoolAttendedTo = null,
                        TutorName = null,
                        NextOfKin = null,
                        IsFullTime = false,
                        DisabilityNotes = null,
                        IsRegisteredDisabled = false,
                        Notes = null,
                        EmploymentStatusBeforeId = null,
                        OtherForenames =
                            candidate.FirstNames?.TrimEnd().Split(" ").LastOrDefault() != candidate.FirstNames?.TrimEnd().Split(" ").FirstOrDefault() ?
                                candidate.FirstNames?.TrimEnd().Split(" ").LastOrDefault() :
                                null,
                        RefNo = null,
                        Emareceipt = false,
                        Emanumber = null,
                        Emaconfirmed = false,
                        BritishNationality = false,
                        CountryOfResidence = null,
                        DateOfEntryUk = candidate.DateOfEntryUk,
                        Refugee = false,
                        AsylumSeeker = false,
                        Permit = false,
                        PermitDate = null,
                        AttendedBefore = false,
                        AddressLast3Yrs = false,
                        DiscussNeeds = false,
                        ExaminationArrangements = false,
                        PreviousSchool = candidate.SchoolLastAttended,
                        EducationLastYear = false,
                        Homeless = false,
                        InCare = false,
                        FulltimeCarer = false,
                        ShareInformation = false,
                        FefcexemptionReasonId = null,
                        FeebenefitDependent = false,
                        FeenoLevel2 = false,
                        FeenoLevel3 = false,
                        FeenoLevel2or3 = false,
                        KnownAs = candidate.PreferredName,
                        PreviousStudentRefNo = null,
                        UniqueLearnerNo = candidate.ULN,
                        WebApplicationId = candidate.ID,
                        AbilityToShareId = 1,
                        Alsrequired = candidate.LsNeedsSupport ?? false,
                        EmploymentStatusOnCompletionId = null,
                        NumberOfYearsAtCurrentAddress = null,
                        WblplacementOrgId = null,
                        EmployerEmail = null,
                        AdditionalLearningNeedsId = null,
                        GovernmentInitiative1Id = null,
                        GovernmentInitiative2Id = null,
                        SectorId = null,
                        ContractingOrgCode = null,
                        CompletionStatusId = null,
                        EmploymentStatusAfterStartingId = null,
                        ProgrammeTypeId = null,
                        ProgrammeEntryRouteId = null,
                        FranchisingPartnerId = null,
                        PlannedGroupBasedHours = null,
                        PlannedOneToOneHours = null,
                        NationalSkillsAcademyId = null,
                        LengthOfUnemploymentBeforeEsfid = null,
                        ProviderSpecified1 = null,
                        ProviderSpecified2 = null,
                        StartDate = null,
                        ExpectedEndDate = null,
                        ExpectedGlh = null,
                        ProportionFundingRemaining = null,
                        DeliveryPostcodeOut = null,
                        DeliveryPostcodeIn = null,
                        ErfundingEligibilityId = null,
                        L34a = null,
                        StudentProviderSpecified1 = null,
                        StudentProviderSpecified2 = null,
                        SpecialProjectId = null,
                        ProjectDossierNumber = null,
                        LocalProjectNumber = null,
                        Contact1RelationshipId =
                            candidate.ListEmergencyContactRelationshipID == 8 ? 10 :
                            candidate.ListEmergencyContactRelationshipID == 7 ? 8 :
                            candidate.ListEmergencyContactRelationshipID == 12 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 4 ? 16 :
                            candidate.ListEmergencyContactRelationshipID == 3 ? 2 :
                            candidate.ListEmergencyContactRelationshipID == 21 ? 14 :
                            candidate.ListEmergencyContactRelationshipID == 13 ? 24 :
                            candidate.ListEmergencyContactRelationshipID == 20 ? 13 :
                            candidate.ListEmergencyContactRelationshipID == 2 ? 1 :
                            candidate.ListEmergencyContactRelationshipID == 11 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 10 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 1 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 18 ? 5 :
                            candidate.ListEmergencyContactRelationshipID == 6 ? 9 :
                            candidate.ListEmergencyContactRelationshipID == 5 ? 15 :
                            candidate.ListEmergencyContactRelationshipID == 15 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 16 ? 19 :
                            candidate.ListEmergencyContactRelationshipID == 17 ? 26 :
                            candidate.ListEmergencyContactRelationshipID == 14 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 9 ? 11 :
                            candidate.ListEmergencyContactRelationshipID == 19 ? 12 :
                            candidate.ListEmergencyContactRelationshipID == 22 ? 29 :
                            29,
                        Contact2RelationshipId = null,
                        Tel2 = null,
                        StudentDeclaration = null,
                        RequestSource = "eSignUp",
                        CriminalConvictionId = candidate.UnSpentConvictions == true ? 2 : 3,
                        UcasapplicationCode = null,
                        HighestQualId = null,
                        LastInstitutionId = null,
                        SococcupationId = null,
                        SocioEconomicClassId = null,
                        PassportExpiryDate = null,
                        PassportIssueDate = candidate.PassportIssueDate,
                        PassportNumber = candidate.PassportNumber,
                        StudentVisaRequirementId = null,
                        Overseas = false,
                        RestrictedUseAllowContactByPost = false,
                        RestrictedUseAllowContactByTelephone = false,
                        RestrictedUseAllowContactByEmail = false,
                        UcaspersonalId = null,
                        HequalsOnEntryId = null,
                        DoNotEnrol = false,
                        T2gpartnerId = null,
                        Photo = null,
                        InstitutionChoiceNumber = null,
                        AltCountry = null,
                        PnbookingId = null,
                        MajorFundingSourceId = null,
                        HasAdditionalLearningNeeds = candidate.LsNeedsSupport ?? false,
                        HasAdditionalSocialNeeds = false,
                        ApprenticeshipPathwayId = null,
                        Country = candidate.HomeAddressCountry,
                        RestrictedUseAllowResearch = false,
                        RestrictedUseAllowLearningOpportunities = false,
                        RestrictedUseNoContactIllness = false,
                        ParentFirstName = candidate.ParentGuardianName?.TrimEnd().Split(" ").FirstOrDefault() ?? candidate.ParentGuardianName?.TrimEnd(),
                        ParentSurname =
                            candidate.ParentGuardianName?.TrimEnd().Split(" ").LastOrDefault() != candidate.ParentGuardianName?.TrimEnd().Split(" ").FirstOrDefault() ?
                                candidate.ParentGuardianName?.TrimEnd().Split(" ").LastOrDefault() :
                                null,
                        ParentTitle = null,
                        ParentAddress1 = candidate.ParentGuardianHomeAddress,
                        ParentAddress2 = null,
                        ParentAddress3 = null,
                        ParentAddress4 = null,
                        ParentPostCodeOut = candidate.ParentGuardianHomePostCode?.Length > 3 ? candidate.ParentGuardianHomePostCode?.TrimEnd().Substring(0, candidate.ParentGuardianHomePostCode.TrimEnd().Length - 3).TrimEnd() : "",
                        ParentPostCodeIn = candidate.ParentGuardianHomePostCode?.Length > 3 ? candidate.ParentGuardianHomePostCode?.TrimEnd().Substring(candidate.ParentGuardianHomePostCode.TrimEnd().Length - 3) : "",
                        ParentCountry = null,
                        ParentPhoneNumber = null,
                        ParentMobileTel = candidate.ParentGuardianTelNumber,
                        ParentWorkTel = null,
                        ParentEmailAddress = candidate.ParentGuardianEmail,
                        IsLivingWithParent = false,
                        ParentContactAllowedByPost = null,
                        ParentContactAllowedByEmail = null,
                        ParentContactAllowedByTelephone = null,
                        Parent2FirstName = null,
                        Parent2Surname = null,
                        Parent2Title = null,
                        Parent2Address1 = null,
                        Parent2Address2 = null,
                        Parent2Address3 = null,
                        Parent2Address4 = null,
                        Parent2PostCodeOut = null,
                        Parent2PostCodeIn = null,
                        Parent2Country = null,
                        Parent2PhoneNumber = null,
                        Parent2MobileTel = null,
                        Parent2WorkTel = null,
                        Parent2EmailAddress = null,
                        IsLivingWithParent2 = false,
                        Parent2ContactAllowedByPost = null,
                        Parent2ContactAllowedByEmail = null,
                        Parent2ContactAllowedByTelephone = null,
                        PaymentMethodId = null,
                        VerificationTypeId = "999",
                        VerificationOtherDescription = null,
                        LookedAfter = candidate.InCare,
                        CareLeaver = candidate.LeftCareRecently,
                        LearningSupportFundingDateFrom = null,
                        LearningSupportFundingDateTo = null,
                        OriginalLearningStartDate = null,
                        ReferencesNotes = null,
                        EmployerAddress5 = null,
                        EmployerAddress6 = null,
                        EmployerAddress7 = null,
                        PebookingLearnerId = null,
                        WorkProgrammeParticipation = false,
                        Contact1EmailAddress = candidate.EmergencyContactEmail,
                        Contact2EmailAddress = null,
                        GcsemathsAchievementId = null,
                        GcseenglishAchievementId = null,
                        CollegeChoice = null,
                        StudentFirstLanguageId = null,
                        YoungParent = false,
                        YoungCarer = false,
                        HighNeedsStudent = false,
                        StillAtLastSchool = false,
                        Alsrequested = candidate.LsNeedsExtraHelp ?? false,
                        AccommodationTypeId = null,
                        FreeMealsEligibilityId = null,
                        HasLearningDifficultyAssessment = candidate.LsNeedsDifficulty ?? false,
                        HasEducationHealthCarePlan = candidate.HasEhcp ?? false,
                        PreviousUklearningProvider = null,
                        DisabilityCategory1Id = //Make first one the primary 1 if a primary has been selected otherwise make it first in the list
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 1 ? 4 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 2 ? 5 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 3 ? 6 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 4 ? 7 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 5 ? 8 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 6 ? 9 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 7 ? 10 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 8 ? 11 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 9 ? 12 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 10 ? 13 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 11 ? 14 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 12 ? 15 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 13 ? 16 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 14 ? 17 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 15 ? 93 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 16 ? 94 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 17 ? 95 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 18 ? 96 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 19 ? 97 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 20 ? 98 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 21 ? 99 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID == candidate.DisabilityLearningDifficultiesPrimaryID).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(0)?.CandidateDisabilityLearningDifficultiesID) == 22 ? 18 :
                            null,
                        DisabilityCategory2Id = //Make the second one anything not selected as primary or if no primary then just the second one in the list (if any)
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 1 ? 4 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 2 ? 5 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 3 ? 6 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 4 ? 7 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 5 ? 8 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 6 ? 9 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 7 ? 10 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 8 ? 11 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 9 ? 12 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 10 ? 13 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 11 ? 14 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 12 ? 15 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 13 ? 16 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 14 ? 17 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 15 ? 93 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 16 ? 94 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 17 ? 95 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 18 ? 96 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 19 ? 97 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 20 ? 98 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 21 ? 99 :
                            (candidate.CandidateDisabilityLearningDifficultyResults?.Where(d => d.CandidateDisabilityLearningDifficultiesID != candidate.DisabilityLearningDifficultiesPrimaryID && candidate.DisabilityLearningDifficultiesPrimaryID != null).FirstOrDefault()?.CandidateDisabilityLearningDifficultiesID ?? candidate.CandidateDisabilityLearningDifficultyResults?.ElementAtOrDefault(1)?.CandidateDisabilityLearningDifficultiesID) == 22 ? 18 :
                            null,
                        HouseholdSituation1Id = null,
                        HouseholdSituation2Id = null,
                        CarColour = null,
                        HasDisabledStudentsAllowance = false,
                        VisaTypeId = null,
                        VisaStartDate = null,
                        VisaExpiryDate = candidate.VisaEndDate,
                        Casid = null,
                        CasissueDate = null,
                        IdcardNumber = candidate.DrivingLicenceNumber,
                        GcsemathsConditionOfFundingId = null,
                        GcseenglishConditionOfFundingId = null,
                        AchievedEnglishGcsebyEndOfYear11 = null,
                        AchievedMathsGcsebyEndOfYear11 = null,
                        SupportNeed1Id = null,
                        SupportNeed2Id = null,
                        SupportNeed3Id = null,
                        LearningDiffOrDisId = candidate.LsNeedsDifficulty == true ? "1" : candidate.LsNeedsDifficulty == false ? "2" : "9",
                        SurnameAtBirth = candidate.PreviousSurname,
                        HasSpecialEducationNeeds = null,
                        SupportRef = null,
                        StudentSignature = null,
                        CanBeContactBySms = false,
                        CanBeContactBySocialMedia = false,
                        AcceptMarketingConsent = false,
                        AcceptShareInfoConsent = false,
                        CanBeSharedByEmail = false,
                        CanBeSharedByWebSite = false,
                        CanBeSharedBySocialMedia = false,
                        GdprallowContactByPhone = null,
                        GdprallowContactByPost = null,
                        GdprallowContactByEmail = null,
                        IsWheelChairUser = null,
                        ReceivedFreeSchoolMeals = null,
                        TargetOtjhours = null,
                        ParentRelationshipId =
                            candidate.ListEmergencyContactRelationshipID == 8 ? 10 :
                            candidate.ListEmergencyContactRelationshipID == 7 ? 8 :
                            candidate.ListEmergencyContactRelationshipID == 12 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 4 ? 16 :
                            candidate.ListEmergencyContactRelationshipID == 3 ? 2 :
                            candidate.ListEmergencyContactRelationshipID == 21 ? 14 :
                            candidate.ListEmergencyContactRelationshipID == 13 ? 24 :
                            candidate.ListEmergencyContactRelationshipID == 20 ? 13 :
                            candidate.ListEmergencyContactRelationshipID == 2 ? 1 :
                            candidate.ListEmergencyContactRelationshipID == 11 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 10 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 1 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 18 ? 5 :
                            candidate.ListEmergencyContactRelationshipID == 6 ? 9 :
                            candidate.ListEmergencyContactRelationshipID == 5 ? 15 :
                            candidate.ListEmergencyContactRelationshipID == 15 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 16 ? 19 :
                            candidate.ListEmergencyContactRelationshipID == 17 ? 26 :
                            candidate.ListEmergencyContactRelationshipID == 14 ? 29 :
                            candidate.ListEmergencyContactRelationshipID == 9 ? 11 :
                            candidate.ListEmergencyContactRelationshipID == 19 ? 12 :
                            candidate.ListEmergencyContactRelationshipID == 22 ? 29 :
                            29,
                        ParentCanBeContactedBySocialMedia = false,
                        ParentCanBeContactedBySms = false,
                        ParentAcceptMarketingConsent = false,
                        ParentAcceptShareInfoConsent = false,
                        ParentConsentGivenDate = null,
                        ParentConsentGiven = false,
                        ParentCanBeSharedByEmail = false,
                        ParentCanBeSharedBySocialMedia = false,
                        ParentCanBeSharedByWebSite = false,
                        Parent2RelationshipId = null,
                        Parent2CanBeContactedBySocialMedia = false,
                        Parent2CanBeContactedBySms = false,
                        Parent2AcceptMarketingConsent = false,
                        Parent2AcceptShareInfoConsent = false,
                        Parent2ConsentGivenDate = null,
                        Parent2ConsentGiven = false,
                        Parent2CanBeSharedByEmail = false,
                        Parent2CanBeSharedBySocialMedia = false,
                        Parent2CanBeSharedByWebSite = false,
                        Contact1ContactTypeId = 1,
                        Contact1Title = null,
                        Contact1Address1 = candidate.ParentGuardianHomeAddress,
                        Contact1Address2 = null,
                        Contact1Address3 = null,
                        Contact1Address4 = null,
                        Contact1PostCodeOut = candidate.ParentGuardianHomePostCode?.Length > 3 ? candidate.ParentGuardianHomePostCode?.TrimEnd().Substring(0, candidate.ParentGuardianHomePostCode.TrimEnd().Length - 3).TrimEnd() : "",
                        Contact1PostCodeIn = candidate.ParentGuardianHomePostCode?.Length > 3 ? candidate.ParentGuardianHomePostCode?.TrimEnd().Substring(candidate.ParentGuardianHomePostCode.TrimEnd().Length - 3) : "",
                        Contact1Country = null,
                        Contact1MobileTel = candidate.ParentGuardianTelNumber,
                        Contact1WorkTel = null,
                        IsLivingWithContact1 = false,
                        Contact1ContactAllowedByPost = null,
                        Contact1ContactAllowedByEmail = null,
                        Contact1ContactAllowedByTelephone = null,
                        Contact1CanBeContactedBySocialMedia = false,
                        Contact1CanBeContactedBySms = false,
                        Contact1AcceptMarketingConsent = false,
                        Contact1AcceptShareInfoConsent = false,
                        Contact1ConsentGivenDate = null,
                        Contact1ConsentGiven = false,
                        Contact1CanBeSharedByEmail = false,
                        Contact1CanBeSharedBySocialMedia = false,
                        Contact1CanBeSharedByWebSite = false,
                        Contact2ContactTypeId = 1,
                        Contact2Title = null,
                        Contact2Address1 = null,
                        Contact2Address2 = null,
                        Contact2Address3 = null,
                        Contact2Address4 = null,
                        Contact2PostCodeOut = null,
                        Contact2PostCodeIn = null,
                        Contact2Country = null,
                        Contact2MobileTel = null,
                        Contact2WorkTel = null,
                        IsLivingWithContact2 = false,
                        Contact2ContactAllowedByPost = null,
                        Contact2ContactAllowedByEmail = null,
                        Contact2ContactAllowedByTelephone = null,
                        Contact2CanBeContactedBySocialMedia = false,
                        Contact2CanBeContactedBySms = false,
                        Contact2AcceptMarketingConsent = false,
                        Contact2AcceptShareInfoConsent = false,
                        Contact2ConsentGivenDate = null,
                        Contact2ConsentGiven = false,
                        Contact2CanBeSharedByEmail = false,
                        Contact2CanBeSharedBySocialMedia = false,
                        Contact2CanBeSharedByWebSite = false,
                        IsPurged = false,
                        PreferredPronounId = null,
                        IsHomeFees = null,
                        HomeFeeEligibilityId = null,
                        IsIncomplete = false,
                        WebUserId = null,
                        AsylumSeekerRef = null,
                        GenderType = null,
                        GenderIdentityCode = null,
                        GenderExpressionOfStudentCode = null,
                        SexualOrientationCode =
                            candidate.CandidateSexualOrientationID == 1 ? "4" :
                            candidate.CandidateSexualOrientationID == 2 && candidate.Sex == "M" ? "2" :
                            candidate.CandidateSexualOrientationID == 2 ? "3" :
                            candidate.CandidateSexualOrientationID == 4 ? "1" :
                            candidate.CandidateSexualOrientationID == 5 ? "6" :
                            candidate.CandidateSexualOrientationID == 6 ? "5" :
                            candidate.CandidateSexualOrientationID == 7 ? "5" :
                            candidate.CandidateSexualOrientationID == 8 ? "4" :
                            "5",
                        EstrangedFromParents = null,
                        ParentCarerInArmedForces = null,
                        ServedInArmedForces = null,
                        InCareDuration = null,
                        RefugeeAsylumSeekerStatus = null,
                        VisaDocumentNumber = null,
                        AclprovisionTypeId = null,
                        AflprovisionTypeId = null
                    };

                    //Add record to be inserted
                    EnrolmentRequests.Add(newEnrolmentRequest);
                }
            }

            return $"{EnrolmentRequests?.Count()} New Enrolment Request Records Prepared To Be Inserted";
        }

        public static async Task<string> InsertEnrolmentRequests()
        {
            string insertMsg;

            try
            {
                if (EnrolmentRequests != null)
                {
                    _context?.EnrolmentRequest.AddRange(EnrolmentRequests);
                    await _context?.SaveChangesAsync()!;

                    //Need to do it this way to be able to insert a specific Enrolment Request ID -- May need to be careful not to clash with the Web Toolkit so pick a range not in use
                    /*
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.EnrolmentRequest.AddRange(EnrolmentRequests);
                        await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT ProSolution.dbo.EnrolmentRequest ON;");
                        await _context.SaveChangesAsync();
                        await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT ProSolution.dbo.EnrolmentRequest OFF;");
                        transaction.Commit();
                    }
                    */

                    insertMsg = $"{EnrolmentRequests?.Count()} New Enrolment Request Records Added to ProSolution";
                }
                else
                {
                    insertMsg = $"No New Enrolment Request Records To Add to ProSolution";
                }
            }
            catch (Exception ex)
            {
                insertMsg = $"Could not insert the new Enrolment Request Records Due to An Error";
                Console.WriteLine($"Error Inserting Enrolment Request Records into ProSolution ({ex.InnerException?.Message ?? ex.Message}). Please Try Again");
            }

            return insertMsg;
        }

        private static string EndpointException(Exception ex, int? recordID)
        {
            string errorMsg = "";
            if (ex.Message.Contains("The input does not contain any JSON tokens"))
            {
                //This is valid and the API returns 204 No Content which is eroneously logged as an error when it is not
            }
            else
            {
                CanConnect = false;

                if (ex.Message.Contains(HttpStatusCode.Unauthorized.ToString()))
                {
                    errorMsg = $"You are not authorised to view this page";
                }
                else if (ex.Message.Contains("404 (Not Found)"))
                {
                    if (recordID != null)
                    {
                        errorMsg = $"The record \"{recordID}\" requested does not exist";
                    }
                    else
                    {
                        errorMsg = $"The record does not exist";
                    }
                }
                else if (ex.Message.Contains("400 (Bad Request)"))
                {
                    if (recordID != null)
                    {
                        errorMsg = $"The record \"{recordID}\" requested is invalid";
                    }
                    else
                    {
                        errorMsg = $"The record does not exist";
                    }
                }
                else errorMsg = $"Error: {ex.Message}";
            }

            return errorMsg;
        }
    }
}