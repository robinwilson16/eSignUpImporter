using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace eSignUpImporter.ModelsCandidateGetAll
{
    public class Candidate
    {
        public int ID { get; set; }

        [Required]
        public string? StudentID { get; set; }

        [StringLength(10)]
        public string? ULN { get; set; }
        public string? Title { get; set; }
        public int? ListCandidateTitleID { get; set; }
        public string? Surname { get; set; }
        public string? PreviousSurname { get; set; }
        public string? FirstNames { get; set; }
        public string? PreferredName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(1)]
        public string? Sex { get; set; }

        public string? HomeAddress1 { get; set; }
        public string? HomeAddress2 { get; set; }
        public string? HomeAddress3 { get; set; }
        public string? HomeAddress4 { get; set; }
        public string? HomePostCode { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmergencyContactFullName { get; set; }
        public string? EmergencyContactTelNumber { get; set; }
        public string? EmergencyContactEmail { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public int? ListEmergencyContactRelationshipID { get; set; }
        public int? CandidateEthnicityID { get; set; }
        public string? NationalInsuranceNumber { get; set; }
        public int? CandidateRegistrationStatusID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LivingAtAddressSince { get; set; }
        public bool? LsNeedsDifficulty { get; set; }
        public bool? InCare { get; set; }
        public bool? LeftCareRecently { get; set; }
        public string? SchoolLastAttended { get; set; }
        public string? CandidateEthnicityIlrCode { get; set; }
        public string? ArcCardNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ArcCardIssueDate { get; set; }

        public string? LocalAuthorityReference { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LocalAuthorityIssueDate { get; set; }
        public string? PassportNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PassportIssueDate { get; set; }
        public string? FamilyMemberName { get; set; }
        public string? EndorsementReference { get; set; }
        public string? DrivingLicenceNumber { get; set; }
        public string? NasCandidateID { get; set; }
        public string? BirthCertificateNumber { get; set; }
        public bool? OfficialUseEuCitizen { get; set; }
        public string? OfficialUseOtherEvidence { get; set; }
        public string? OfficialUseEvidenceEmpEgPaye { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SignedUpDate { get; set; }
        public bool? StudyAtAnotherCollege { get; set; }
        public string? StudyAtAnotherCollegeName { get; set; }
        public int? HomeAddressCountryCodeID { get; set; }
        public string? HomeAddressCountry { get; set; }
        public string? SexualOrientation { get; set; }
        public string? SexualOrientationSelfDescribe { get; set; }
        public string? ParentGuardianName { get; set; }
        public string? ParentGuardianHomeAddress { get; set; }
        public string? ParentGuardianHomePostCode { get; set; }
        public string? ParentGuardianTelNumber { get; set; }
        public string? ParentGuardianEmail { get; set; }
        public int? CountryOfNationalityID { get; set; }
        public string? CountryOfNationalityName { get; set; }
        public int? CountryOfBirthID { get; set; }
        public string? CountryOfBirthName { get; set; }
        public string? CountryLivingPast3Years { get; set; }
        public int? CandidateReligiousIdentityID { get; set; }
        public string? CandidateReligiousIdentityName { get; set; }
        public string? ReligiousIdentityOther { get; set; }
        public int? CandidateSexualOrientationID { get; set; }
        public bool? LsNeedsSupport { get; set; }
        public bool? LsNeedsExtraHelp { get; set; }
        public string? LsNeedsExtraHelpWith { get; set; }
        public bool? LsNeedsMedicine { get; set; }
        public bool? LsNeedMedicineHelp { get; set; }
        public bool? LsNeedsPersonalCareAssistant { get; set; }
        public int? DisabilityLearningDifficultiesPrimaryID { get; set; }
        public string? DisabilityLearningDifficultiesPrimaryName { get; set; }
        public bool? SchoolingInterupted { get; set; }
        public bool? LivingInHostelOrResidentail { get; set; }
        public bool? OffenderInCommunity { get; set; }
        public bool? InCareEmployerPermission { get; set; }
        public bool? UnSpentConvictions { get; set; }
        public string? SchoolAt16 { get; set; }
        public bool? StudentSignatureObtained { get; set; }
        public int? QuestionSetID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StudentSignatureObtainedDate { get; set; }
        public string? TitleOther { get; set; }
        public int? CandidateHighestLevelID { get; set; }
        public string? Nationality { get; set; }
        public bool? PermanentUkResident { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfEntryUk { get; set; }
        public bool? LivedOutsideUk { get; set; }
        public bool? BornInUk { get; set; }
        public string? CountriesLived { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ArcCardNumberIssued { get; set; }
        public string? LaLetterRef { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LaLetterRefIssued { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PassportNumberIssued { get; set; }
        public string? EndrosementRef { get; set; }

        [DataType(DataType.Date)]
        public DateTime? VisaEndDate { get; set; }
        public string? EuSettledStatusShareCode { get; set; }
        public string? LsAdditionalComment { get; set; }
        public bool? HasEhcp { get; set; }
        public bool? EhcpEmployerPermission { get; set; }
        public int? ListGenderIdentityID { get; set; }

        public ICollection<CandidateNote>? CandidateNotes { get; set; }
        public ICollection<CustomFieldValue>? CustomFieldValues { get; set; }
        public CandidateExtraField? CandidateExtraFields { get; set; }
        public ICollection<CandidateDocument>? CandidateDocuments { get; set; }
        public ICollection<CandidateQualification>? CandidateQualifications { get; set; }
        public ICollection<CandidateDisabilityLearningDifficultyResult>? CandidateDisabilityLearningDifficultyResults { get; set; }

        //Used to match Candidate to Enrolment Request
        public CandidateEnrolmentRequest? CandidateEnrolmentRequest { get; set; }
    }
}
