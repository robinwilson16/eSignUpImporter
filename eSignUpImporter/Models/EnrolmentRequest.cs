namespace eSignUpImporter.Models
{
    public partial class EnrolmentRequest
    {
        public int EnrolmentRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequestStatus { get; set; } = null!;

        public string? Surname { get; set; }

        public string? FirstForename { get; set; }

        public string? Title { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Sex { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Address3 { get; set; }

        public string? Address4 { get; set; }

        public string? PostcodeOut { get; set; }

        public string? PostcodeIn { get; set; }

        public string? Tel { get; set; }

        public string? Contact1 { get; set; }

        public string? Contact1Tel { get; set; }

        public string? DisabilityId { get; set; }

        public string? NationalityId { get; set; }

        public string? EthnicGroupId { get; set; }

        public string? Email { get; set; }

        public int? OfferingId { get; set; }

        public string? PaymentStatus { get; set; }

        public string? RestrictedUseIndicator { get; set; }

        public bool SentMarketingInfo { get; set; }

        public string AcademicYearId { get; set; } = null!;

        public bool Include { get; set; }

        public string? PriorAttainmentLevelId { get; set; }

        public bool EmployerRelease { get; set; }

        public bool EmployerPaying { get; set; }

        public string? EmployerName { get; set; }

        public string? EmployerAddress1 { get; set; }

        public string? EmployerAddress2 { get; set; }

        public string? EmployerAddress3 { get; set; }

        public string? EmployerAddress4 { get; set; }

        public string? EmployerPostcodeOut { get; set; }

        public string? EmployerPostcodeIn { get; set; }

        public string? EmployerTel { get; set; }

        public string? Md5 { get; set; }

        public bool PassDetailsTutor { get; set; }

        public string? LearningDifficultyId { get; set; }

        public string? MobileTel { get; set; }

        public string? EmployerSizeTypeId { get; set; }

        public string? EmploymentStatusBeforeEsfid { get; set; }

        public string? UnemploymentDurationId { get; set; }

        public string? Contact2 { get; set; }

        public string? Contact2Tel { get; set; }

        public bool EuroResidentId { get; set; }

        public string? CountryId { get; set; }

        public bool StudyElsewhere { get; set; }

        public int? SchoolId { get; set; }

        public string? Ni { get; set; }

        public int? EmployerId { get; set; }

        public string? AltAddress1 { get; set; }

        public string? AltAddress2 { get; set; }

        public string? AltAddress3 { get; set; }

        public string? AltAddress4 { get; set; }

        public string? AltPostcodeOut { get; set; }

        public string? AltPostcodeIn { get; set; }

        public string? AltTel1 { get; set; }

        public int? FeeExemptionReasonId { get; set; }

        public string? WebSiteSearchTexts { get; set; }

        public long? WebPaymentId { get; set; }

        public string? EnrolmentUserDefined1 { get; set; }

        public string? EnrolmentUserDefined2 { get; set; }

        public string? EnrolmentUserDefined3 { get; set; }

        public string? EnrolmentUserDefined4 { get; set; }

        public string? EnrolmentUserDefined5 { get; set; }

        public string? EnrolmentUserDefined6 { get; set; }

        public string? EnrolmentUserDefined7 { get; set; }

        public string? EnrolmentUserDefined8 { get; set; }

        public string? EnrolmentUserDefined9 { get; set; }

        public int? HeardAboutCollegeId { get; set; }

        public string? CarReg { get; set; }

        public string? CarMake { get; set; }

        public string? CarModel { get; set; }

        public DateTime? SchoolAttendedFrom { get; set; }

        public DateTime? SchoolAttendedTo { get; set; }

        public string? TutorName { get; set; }

        public string? NextOfKin { get; set; }

        public bool IsFullTime { get; set; }

        public string? DisabilityNotes { get; set; }

        public bool IsRegisteredDisabled { get; set; }

        public string? Notes { get; set; }

        public string? EmploymentStatusBeforeId { get; set; }

        public string? OtherForenames { get; set; }

        public string? RefNo { get; set; }

        public bool Emareceipt { get; set; }

        public string? Emanumber { get; set; }

        public bool Emaconfirmed { get; set; }

        public bool BritishNationality { get; set; }

        public string? CountryOfResidence { get; set; }

        public DateTime? DateOfEntryUk { get; set; }

        public bool Refugee { get; set; }

        public bool AsylumSeeker { get; set; }

        public bool Permit { get; set; }

        public DateTime? PermitDate { get; set; }

        public bool AttendedBefore { get; set; }

        public bool AddressLast3Yrs { get; set; }

        public bool DiscussNeeds { get; set; }

        public bool ExaminationArrangements { get; set; }

        public string? PreviousSchool { get; set; }

        public bool EducationLastYear { get; set; }

        public bool Homeless { get; set; }

        public bool InCare { get; set; }

        public bool FulltimeCarer { get; set; }

        public bool ShareInformation { get; set; }

        public string? FefcexemptionReasonId { get; set; }

        public bool FeebenefitDependent { get; set; }

        public bool FeenoLevel2 { get; set; }

        public bool FeenoLevel3 { get; set; }

        public bool FeenoLevel2or3 { get; set; }

        public string? KnownAs { get; set; }

        public string? PreviousStudentRefNo { get; set; }

        public string? UniqueLearnerNo { get; set; }

        public int? WebApplicationId { get; set; }

        public byte? AbilityToShareId { get; set; }

        public bool Alsrequired { get; set; }

        public string? EmploymentStatusOnCompletionId { get; set; }

        public string? EnrolmentUserDefined10 { get; set; }

        public string? EnrolmentUserDefined11 { get; set; }

        public string? EnrolmentUserDefined12 { get; set; }

        public string? EnrolmentUserDefined13 { get; set; }

        public string? EnrolmentUserDefined14 { get; set; }

        public string? EnrolmentUserDefined15 { get; set; }

        public string? StudentDetailUserDefined1 { get; set; }

        public string? StudentDetailUserDefined2 { get; set; }

        public string? StudentDetailUserDefined3 { get; set; }

        public string? StudentDetailUserDefined4 { get; set; }

        public string? StudentDetailUserDefined5 { get; set; }

        public string? StudentDetailUserDefined6 { get; set; }

        public string? StudentDetailUserDefined7 { get; set; }

        public string? StudentDetailUserDefined8 { get; set; }

        public string? StudentDetailUserDefined9 { get; set; }

        public string? StudentDetailUserDefined10 { get; set; }

        public string? StudentDetailUserDefined11 { get; set; }

        public string? StudentDetailUserDefined12 { get; set; }

        public string? StudentDetailUserDefined13 { get; set; }

        public string? StudentDetailUserDefined14 { get; set; }

        public string? StudentDetailUserDefined15 { get; set; }

        public int? NumberOfYearsAtCurrentAddress { get; set; }

        public int? WblplacementOrgId { get; set; }

        public string? EmployerEmail { get; set; }

        public string? AdditionalLearningNeedsId { get; set; }

        public string? GovernmentInitiative1Id { get; set; }

        public string? GovernmentInitiative2Id { get; set; }

        public string? SectorId { get; set; }

        public string? ContractingOrgCode { get; set; }

        public string? CompletionStatusId { get; set; }

        public string? EmploymentStatusAfterStartingId { get; set; }

        public string? ProgrammeTypeId { get; set; }

        public string? ProgrammeEntryRouteId { get; set; }

        public int? FranchisingPartnerId { get; set; }

        public int? PlannedGroupBasedHours { get; set; }

        public int? PlannedOneToOneHours { get; set; }

        public string? NationalSkillsAcademyId { get; set; }

        public string? LengthOfUnemploymentBeforeEsfid { get; set; }

        public string? ProviderSpecified1 { get; set; }

        public string? ProviderSpecified2 { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpectedEndDate { get; set; }

        public decimal? ExpectedGlh { get; set; }

        public int? ProportionFundingRemaining { get; set; }

        public string? DeliveryPostcodeOut { get; set; }

        public string? DeliveryPostcodeIn { get; set; }

        public string? ErfundingEligibilityId { get; set; }

        public string? L34a { get; set; }

        public string? StudentProviderSpecified1 { get; set; }

        public string? StudentProviderSpecified2 { get; set; }

        public string? SpecialProjectId { get; set; }

        public string? ProjectDossierNumber { get; set; }

        public string? LocalProjectNumber { get; set; }

        public int? Contact1RelationshipId { get; set; }

        public int? Contact2RelationshipId { get; set; }

        public string? Tel2 { get; set; }

        public string? StudentDeclaration { get; set; }

        public string? RequestSource { get; set; }

        public int? CriminalConvictionId { get; set; }

        public string? UcasapplicationCode { get; set; }

        public string? HighestQualId { get; set; }

        public string? LastInstitutionId { get; set; }

        public string? SococcupationId { get; set; }

        public string? SocioEconomicClassId { get; set; }

        public DateTime? PassportExpiryDate { get; set; }

        public DateTime? PassportIssueDate { get; set; }

        public string? PassportNumber { get; set; }

        public int? StudentVisaRequirementId { get; set; }

        public bool Overseas { get; set; }

        public bool RestrictedUseAllowContactByPost { get; set; }

        public bool RestrictedUseAllowContactByTelephone { get; set; }

        public bool RestrictedUseAllowContactByEmail { get; set; }

        public string? UcaspersonalId { get; set; }

        public string? HequalsOnEntryId { get; set; }

        public bool DoNotEnrol { get; set; }

        public int? T2gpartnerId { get; set; }

        public byte[]? Photo { get; set; }

        public byte? InstitutionChoiceNumber { get; set; }

        public string? AltCountry { get; set; }

        public string? StudentInternationalDetailUserDefined1 { get; set; }

        public string? StudentInternationalDetailUserDefined2 { get; set; }

        public int? PnbookingId { get; set; }

        public string? MajorFundingSourceId { get; set; }

        public bool HasAdditionalLearningNeeds { get; set; }

        public bool HasAdditionalSocialNeeds { get; set; }

        public string? ApprenticeshipPathwayId { get; set; }

        public string? Country { get; set; }

        public bool? RestrictedUseAllowResearch { get; set; }

        public bool? RestrictedUseAllowLearningOpportunities { get; set; }

        public bool? RestrictedUseNoContactIllness { get; set; }

        public string? ParentFirstName { get; set; }

        public string? ParentSurname { get; set; }

        public string? ParentTitle { get; set; }

        public string? ParentAddress1 { get; set; }

        public string? ParentAddress2 { get; set; }

        public string? ParentAddress3 { get; set; }

        public string? ParentAddress4 { get; set; }

        public string? ParentPostCodeOut { get; set; }

        public string? ParentPostCodeIn { get; set; }

        public string? ParentCountry { get; set; }

        public string? ParentPhoneNumber { get; set; }

        public string? ParentMobileTel { get; set; }

        public string? ParentWorkTel { get; set; }

        public string? ParentEmailAddress { get; set; }

        public bool IsLivingWithParent { get; set; }

        public bool? ParentContactAllowedByPost { get; set; }

        public bool? ParentContactAllowedByEmail { get; set; }

        public bool? ParentContactAllowedByTelephone { get; set; }

        public string? Parent2FirstName { get; set; }

        public string? Parent2Surname { get; set; }

        public string? Parent2Title { get; set; }

        public string? Parent2Address1 { get; set; }

        public string? Parent2Address2 { get; set; }

        public string? Parent2Address3 { get; set; }

        public string? Parent2Address4 { get; set; }

        public string? Parent2PostCodeOut { get; set; }

        public string? Parent2PostCodeIn { get; set; }

        public string? Parent2Country { get; set; }

        public string? Parent2PhoneNumber { get; set; }

        public string? Parent2MobileTel { get; set; }

        public string? Parent2WorkTel { get; set; }

        public string? Parent2EmailAddress { get; set; }

        public bool IsLivingWithParent2 { get; set; }

        public bool? Parent2ContactAllowedByPost { get; set; }

        public bool? Parent2ContactAllowedByEmail { get; set; }

        public bool? Parent2ContactAllowedByTelephone { get; set; }

        public int? PaymentMethodId { get; set; }

        public string? VerificationTypeId { get; set; }

        public string? VerificationOtherDescription { get; set; }

        public string? StudentDetailUserDefined16 { get; set; }

        public string? StudentDetailUserDefined17 { get; set; }

        public string? StudentDetailUserDefined18 { get; set; }

        public string? StudentDetailUserDefined19 { get; set; }

        public string? StudentDetailUserDefined20 { get; set; }

        public string? StudentDetailUserDefined21 { get; set; }

        public string? StudentDetailUserDefined22 { get; set; }

        public string? StudentDetailUserDefined23 { get; set; }

        public string? StudentDetailUserDefined24 { get; set; }

        public string? StudentDetailUserDefined25 { get; set; }

        public string? StudentDetailUserDefined26 { get; set; }

        public string? StudentDetailUserDefined27 { get; set; }

        public string? StudentDetailUserDefined28 { get; set; }

        public string? StudentDetailUserDefined29 { get; set; }

        public string? StudentDetailUserDefined30 { get; set; }

        public bool? LookedAfter { get; set; }

        public bool? CareLeaver { get; set; }

        public DateTime? LearningSupportFundingDateFrom { get; set; }

        public DateTime? LearningSupportFundingDateTo { get; set; }

        public DateTime? OriginalLearningStartDate { get; set; }

        public string? ReferencesNotes { get; set; }

        public string? EmployerAddress5 { get; set; }

        public string? EmployerAddress6 { get; set; }

        public string? EmployerAddress7 { get; set; }

        public int? PebookingLearnerId { get; set; }

        public bool WorkProgrammeParticipation { get; set; }

        public string? Contact1EmailAddress { get; set; }

        public string? Contact2EmailAddress { get; set; }

        public string? GcsemathsAchievementId { get; set; }

        public string? GcseenglishAchievementId { get; set; }

        public int? CollegeChoice { get; set; }

        public int? StudentFirstLanguageId { get; set; }

        public bool YoungParent { get; set; }

        public bool YoungCarer { get; set; }

        public bool HighNeedsStudent { get; set; }

        public bool StillAtLastSchool { get; set; }

        public bool Alsrequested { get; set; }

        public string? AccommodationTypeId { get; set; }

        public int? FreeMealsEligibilityId { get; set; }

        public bool HasLearningDifficultyAssessment { get; set; }

        public bool HasEducationHealthCarePlan { get; set; }

        public int? PreviousUklearningProvider { get; set; }

        public int? DisabilityCategory1Id { get; set; }

        public int? DisabilityCategory2Id { get; set; }

        public int? HouseholdSituation1Id { get; set; }

        public int? HouseholdSituation2Id { get; set; }

        public string? CarColour { get; set; }

        public bool HasDisabledStudentsAllowance { get; set; }

        public int? VisaTypeId { get; set; }

        public DateTime? VisaStartDate { get; set; }

        public DateTime? VisaExpiryDate { get; set; }

        public string? Casid { get; set; }

        public DateTime? CasissueDate { get; set; }

        public string? IdcardNumber { get; set; }

        public int? GcsemathsConditionOfFundingId { get; set; }

        public int? GcseenglishConditionOfFundingId { get; set; }

        public bool? AchievedEnglishGcsebyEndOfYear11 { get; set; }

        public bool? AchievedMathsGcsebyEndOfYear11 { get; set; }

        public int? SupportNeed1Id { get; set; }

        public int? SupportNeed2Id { get; set; }

        public int? SupportNeed3Id { get; set; }

        public string? LearningDiffOrDisId { get; set; }

        public string? EnrolmentUserDefined16 { get; set; }

        public string? EnrolmentUserDefined17 { get; set; }

        public string? EnrolmentUserDefined18 { get; set; }

        public string? EnrolmentUserDefined19 { get; set; }

        public string? EnrolmentUserDefined20 { get; set; }

        public string? EnrolmentUserDefined21 { get; set; }

        public string? EnrolmentUserDefined22 { get; set; }

        public string? EnrolmentUserDefined23 { get; set; }

        public string? EnrolmentUserDefined24 { get; set; }

        public string? EnrolmentUserDefined25 { get; set; }

        public string? EnrolmentUserDefined26 { get; set; }

        public string? EnrolmentUserDefined27 { get; set; }

        public string? EnrolmentUserDefined28 { get; set; }

        public string? EnrolmentUserDefined29 { get; set; }

        public string? EnrolmentUserDefined30 { get; set; }

        public string? StudentDetailUserDefined31 { get; set; }

        public string? StudentDetailUserDefined32 { get; set; }

        public string? StudentDetailUserDefined33 { get; set; }

        public string? StudentDetailUserDefined34 { get; set; }

        public string? StudentDetailUserDefined35 { get; set; }

        public string? StudentDetailUserDefined36 { get; set; }

        public string? StudentDetailUserDefined37 { get; set; }

        public string? StudentDetailUserDefined38 { get; set; }

        public string? StudentDetailUserDefined39 { get; set; }

        public string? StudentDetailUserDefined40 { get; set; }

        public string? StudentDetailUserDefined41 { get; set; }

        public string? StudentDetailUserDefined42 { get; set; }

        public string? StudentDetailUserDefined43 { get; set; }

        public string? StudentDetailUserDefined44 { get; set; }

        public string? StudentDetailUserDefined45 { get; set; }

        public string? SurnameAtBirth { get; set; }

        public bool? HasSpecialEducationNeeds { get; set; }

        public string? SupportRef { get; set; }

        public byte[]? StudentSignature { get; set; }

        public bool CanBeContactBySms { get; set; }

        public bool CanBeContactBySocialMedia { get; set; }

        public bool AcceptMarketingConsent { get; set; }

        public bool AcceptShareInfoConsent { get; set; }

        public bool CanBeSharedByEmail { get; set; }

        public bool CanBeSharedByWebSite { get; set; }

        public bool CanBeSharedBySocialMedia { get; set; }

        public bool? GdprallowContactByPhone { get; set; }

        public bool? GdprallowContactByPost { get; set; }

        public bool? GdprallowContactByEmail { get; set; }

        public bool? IsWheelChairUser { get; set; }

        public bool? ReceivedFreeSchoolMeals { get; set; }

        public int? TargetOtjhours { get; set; }

        public int? ParentRelationshipId { get; set; }

        public bool ParentCanBeContactedBySocialMedia { get; set; }

        public bool ParentCanBeContactedBySms { get; set; }

        public bool ParentAcceptMarketingConsent { get; set; }

        public bool ParentAcceptShareInfoConsent { get; set; }

        public DateOnly? ParentConsentGivenDate { get; set; }

        public bool ParentConsentGiven { get; set; }

        public bool ParentCanBeSharedByEmail { get; set; }

        public bool ParentCanBeSharedBySocialMedia { get; set; }

        public bool ParentCanBeSharedByWebSite { get; set; }

        public int? Parent2RelationshipId { get; set; }

        public bool Parent2CanBeContactedBySocialMedia { get; set; }

        public bool Parent2CanBeContactedBySms { get; set; }

        public bool Parent2AcceptMarketingConsent { get; set; }

        public bool Parent2AcceptShareInfoConsent { get; set; }

        public DateOnly? Parent2ConsentGivenDate { get; set; }

        public bool Parent2ConsentGiven { get; set; }

        public bool Parent2CanBeSharedByEmail { get; set; }

        public bool Parent2CanBeSharedBySocialMedia { get; set; }

        public bool Parent2CanBeSharedByWebSite { get; set; }

        public int Contact1ContactTypeId { get; set; }

        public string? Contact1Title { get; set; }

        public string? Contact1Address1 { get; set; }

        public string? Contact1Address2 { get; set; }

        public string? Contact1Address3 { get; set; }

        public string? Contact1Address4 { get; set; }

        public string? Contact1PostCodeOut { get; set; }

        public string? Contact1PostCodeIn { get; set; }

        public string? Contact1Country { get; set; }

        public string? Contact1MobileTel { get; set; }

        public string? Contact1WorkTel { get; set; }

        public bool IsLivingWithContact1 { get; set; }

        public bool? Contact1ContactAllowedByPost { get; set; }

        public bool? Contact1ContactAllowedByEmail { get; set; }

        public bool? Contact1ContactAllowedByTelephone { get; set; }

        public bool Contact1CanBeContactedBySocialMedia { get; set; }

        public bool Contact1CanBeContactedBySms { get; set; }

        public bool Contact1AcceptMarketingConsent { get; set; }

        public bool Contact1AcceptShareInfoConsent { get; set; }

        public DateOnly? Contact1ConsentGivenDate { get; set; }

        public bool Contact1ConsentGiven { get; set; }

        public bool Contact1CanBeSharedByEmail { get; set; }

        public bool Contact1CanBeSharedBySocialMedia { get; set; }

        public bool Contact1CanBeSharedByWebSite { get; set; }

        public int Contact2ContactTypeId { get; set; }

        public string? Contact2Title { get; set; }

        public string? Contact2Address1 { get; set; }

        public string? Contact2Address2 { get; set; }

        public string? Contact2Address3 { get; set; }

        public string? Contact2Address4 { get; set; }

        public string? Contact2PostCodeOut { get; set; }

        public string? Contact2PostCodeIn { get; set; }

        public string? Contact2Country { get; set; }

        public string? Contact2MobileTel { get; set; }

        public string? Contact2WorkTel { get; set; }

        public bool IsLivingWithContact2 { get; set; }

        public bool? Contact2ContactAllowedByPost { get; set; }

        public bool? Contact2ContactAllowedByEmail { get; set; }

        public bool? Contact2ContactAllowedByTelephone { get; set; }

        public bool Contact2CanBeContactedBySocialMedia { get; set; }

        public bool Contact2CanBeContactedBySms { get; set; }

        public bool Contact2AcceptMarketingConsent { get; set; }

        public bool Contact2AcceptShareInfoConsent { get; set; }

        public DateOnly? Contact2ConsentGivenDate { get; set; }

        public bool Contact2ConsentGiven { get; set; }

        public bool Contact2CanBeSharedByEmail { get; set; }

        public bool Contact2CanBeSharedBySocialMedia { get; set; }

        public bool Contact2CanBeSharedByWebSite { get; set; }

        public bool IsPurged { get; set; }

        public string? PreferredPronounId { get; set; }

        public bool? IsHomeFees { get; set; }

        public int? HomeFeeEligibilityId { get; set; }

        public bool IsIncomplete { get; set; }

        public string? StudentDetailUserDefined46 { get; set; }

        public string? StudentDetailUserDefined47 { get; set; }

        public string? StudentDetailUserDefined48 { get; set; }

        public string? StudentDetailUserDefined49 { get; set; }

        public string? StudentDetailUserDefined50 { get; set; }

        public string? StudentDetailUserDefined51 { get; set; }

        public string? StudentDetailUserDefined52 { get; set; }

        public string? StudentDetailUserDefined53 { get; set; }

        public string? StudentDetailUserDefined54 { get; set; }

        public string? StudentDetailUserDefined55 { get; set; }

        public string? StudentDetailUserDefined56 { get; set; }

        public string? StudentDetailUserDefined57 { get; set; }

        public string? StudentDetailUserDefined58 { get; set; }

        public string? StudentDetailUserDefined59 { get; set; }

        public string? StudentDetailUserDefined60 { get; set; }

        public string? StudentDetailUserDefinedHealth1 { get; set; }

        public string? StudentDetailUserDefinedHealth2 { get; set; }

        public string? StudentDetailUserDefinedHealth3 { get; set; }

        public string? StudentDetailUserDefinedHealth4 { get; set; }

        public string? StudentDetailUserDefinedHealth5 { get; set; }

        public string? StudentDetailUserDefinedHealth6 { get; set; }

        public string? StudentDetailUserDefinedHealth7 { get; set; }

        public string? StudentDetailUserDefinedHealth8 { get; set; }

        public string? StudentDetailUserDefinedHealth9 { get; set; }

        public string? StudentDetailUserDefinedHealth10 { get; set; }

        public string? StudentDetailUserDefinedHealth11 { get; set; }

        public string? StudentDetailUserDefinedHealth12 { get; set; }

        public string? StudentDetailUserDefinedHealth13 { get; set; }

        public string? StudentDetailUserDefinedHealth14 { get; set; }

        public string? StudentDetailUserDefinedHealth15 { get; set; }

        public Guid? WebUserId { get; set; }

        public string? AsylumSeekerRef { get; set; }

        public string? GenderType { get; set; }

        public string? GenderIdentityCode { get; set; }

        public string? GenderExpressionOfStudentCode { get; set; }

        public string? SexualOrientationCode { get; set; }

        public bool? EstrangedFromParents { get; set; }

        public bool? ParentCarerInArmedForces { get; set; }

        public bool? ServedInArmedForces { get; set; }

        public string? InCareDuration { get; set; }

        public string? RefugeeAsylumSeekerStatus { get; set; }

        public string? VisaDocumentNumber { get; set; }

        public string? AclprovisionTypeId { get; set; }

        public string? AflprovisionTypeId { get; set; }
    }
}
