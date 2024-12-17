using eSignUpImporter.Models;
using eSignUpImporter.ModelsCandidateGetAll;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class Candidate
    {
        public int ID { get; set; }
        public string? StudentID { get; set; }
        public string? LearnRefNumber { get; set; }
        public string? ULN { get; set; }
        public string? Title { get; set; }
        
        private string? _FamilyName;
        public string? FamilyName
        {
            get => _FamilyName?.Trim();
            set => _FamilyName = value;
        }

        private string? _GivenNames;
        public string? GivenNames
        {
            get => _GivenNames?.Trim();
            set => _GivenNames = value;
        }

        private string? _PreferredName;
        public string? PreferredName
        {
            get => _PreferredName?.Trim();
            set => _PreferredName = value;
        }

        [JsonPropertyName("dateOfBirth")]
        public string? DateOfBirth { get; set; }

        [JsonIgnore]
        [Display(Name = "Date Of Birth")]
        public DateTime? dateOfBirth
        {
            get { return DateOfBirth == null ? null : DateTime.ParseExact(DateOfBirth ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? CandidateStatus { get; set; }
        public string? Ethnicity { get; set; }
        public string? EthnicityILRCode { get; set; }
        public string? Sex { get; set; }
        public string? GenderDifferentToSex { get; set; }
        public string? GenderIdentity { get; set; }
        public string? LLDDHealthProb { get; set; }
        public string? NINumber { get; set; }
        public string? PriorAttain { get; set; }
        public int? PriorAttainCode
        {
            get => PriorAttain?.Split(":")?.FirstOrDefault() == null ? null : int.Parse(PriorAttain?.Split(":")?.FirstOrDefault() ?? "0");

        }
        public string? PostCode { get; set; }
        public string? AddLine1 { get; set; }
        public string? AddLine2 { get; set; }
        public string? AddLine3 { get; set; }
        public string? AddLine4 { get; set; }
        public string? TelNo { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }

        [JsonPropertyName("candidateCreatedDate")]
        public string? CandidateCreatedDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Candidate Created Date")]
        public DateTime? candidateCreatedDate
        {
            get { return CandidateCreatedDate == null ? null : DateTime.ParseExact(CandidateCreatedDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? CandidateID { get; set; }
        public bool? HasEHCPEmployerPermission { get; set; }
        public bool? HasEHCP { get; set; }
        public string? NASCandidateID { get; set; }
        public string? LSAdditionalComment { get; set; }

        [JsonPropertyName("livingAtAddressSince")]
        public string? LivingAtAddressSince { get; set; }

        [JsonIgnore]
        [Display(Name = "Living At Address Since")]
        public DateTime? livingAtAddressSince
        {
            get { return LivingAtAddressSince == null ? null : DateTime.ParseExact(LivingAtAddressSince?.Replace("T", " ") ?? "", "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-GB")); }
        }

        public bool? LSNeedsDifficulty { get; set; }
        public bool? InCare { get; set; }
        public bool? LeftCareRecently { get; set; }
        public string? SchoolLastAttended { get; set; }
        public string? EmergencyContactFullName { get; set; }
        public string? EmergencyContactTelNumber { get; set; }
        public string? EmergencyContactEmail { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public string? EmergencyContactRelationshipCode { get; set; }
        public string? ARCCardNumber { get; set; }
        public string? ARCCardIssueDate { get; set; }
        public string? LocalAuthorityReference { get; set; }
        public string? LocalAuthorityIssueDate { get; set; }
        public string? PassportNumber { get; set; }

        [JsonPropertyName("passportIssueDate")]
        public string? PassportIssueDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Passport Issue Date")]
        public DateTime? passportIssueDate
        {
            get { return PassportIssueDate == null ? null : DateTime.ParseExact(PassportIssueDate?.Replace("T", " ") ?? "", "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-GB")); }
        }

        public string? FamilyMemberName { get; set; }
        public string? EndorsementReference { get; set; }
        public string? DrivingLicenceNumber { get; set; }
        public string? BirthCertificateNumber { get; set; }
        public bool? OfficialUseEUCitizen { get; set; }
        public string? OfficialUseOtherEvidence { get; set; }
        public string? OfficialUseEvidenceEmpEgPAYE { get; set; }
        public string? CandidateRegistrationStatus { get; set; }
        public bool? StudyAtAnotherCollege { get; set; }
        public string? StudyAtAnotherCollegeName { get; set; }
        public string? HomeAddressCountry { get; set; }
        public string? SexualOrientation { get; set; }
        public string? SexualOrientationSelfDescribe { get; set; }
        public string? ParentGuardianName { get; set; }
        public string? ParentGuardianHomeAddress { get; set; }
        public string? ParentGuardianHomePostCode { get; set; }
        public string? ParentGuardianTelephoneNumber { get; set; }
        public string? ParentGuardianEmail { get; set; }
        public string? CountryOfNationality { get; set; }
        public string? CountryOfBirth { get; set; }
        public string? CountryLivingPast3Years { get; set; }
        public string? CandidateReligiousIdentity { get; set; }
        public bool? LSNeedsExtraHelp { get; set; }
        public string? LSNeedsExtraHelpWith { get; set; }
        public bool? InCareEmployerPermission { get; set; }
        public bool? SchoolingInterupted { get; set; } //Typo in their API
        public bool? OffenderInCommunity { get; set; }
        public bool? UnspentConvictions { get; set; }
        public string? SchoolAt16 { get; set; }
        public bool? StudentSignatureObtained { get; set; }
        public string? StudentSignatureObtainedDate { get; set; }
        public int? QuestionSetID { get; set; }
        public ICollection<CustomField>? CustomFields { get; set; }
        public ICollection<ContactPreference>? ContactPreferences { get; set; }
        public ICollection<LLDDAndHealthProblem>? LLDDAndHealthProblems { get; set; }
        public ICollection<LLDDAndHealthProblemPeopleSoft>? LLDDAndHealthProblemsPeopleSoft { get; set; }
        public ICollection<LearnerEmploymentStatus>? LearnerEmploymentStatuses { get; set; }
        public ICollection<string>? CandidateEligibilities { get; set; }
        public ICollection<CandidateDocument>? CandidateDocuments { get; set; }
        public ICollection<PlacedRecruitment>? PlacedRecruitments { get; set; }
        public ICollection<EnglishAndMathsQualification>? EnglishAndMathsQualifications { get; set; }
        public ICollection<CandidateNote>? CandidateNotes { get; set; }
        public CandidateExtraFields? CandidateExtraFields { get; set; }
        public ICollection<CandidateQualification>? CandidateQualifications { get; set; }

        //Used to match Candidate to Enrolment Request
        public CandidateEnrolmentRequest? CandidateEnrolmentRequest { get; set; }
    }
}
