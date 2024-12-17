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
    public class CandidateExtraFields
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? ApprenticeshipVacancy { get; set; }
        public string? PreviouslyStudiedInUK { get; set; }
        public string? PreviouslyStudiedAtCollege { get; set; }
        public string? PreviousCollegeIDNumber { get; set; }
        public string? PriorLearningRecognition { get; set; }
        public string? HomeEducated { get; set; }
        public string? NoQualification { get; set; }

        [JsonPropertyName("lastSchoolStartDate")]
        public string? LastSchoolStartDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Last School Start Date")]
        public DateTime? lastSchoolStartDate
        {
            get { return LastSchoolStartDate == null ? null : DateTime.ParseExact(LastSchoolStartDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        [JsonPropertyName("lastSchoolLeavingDate")]
        public string? LastSchoolLeavingDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Last School Leaving Date")]
        public DateTime? lastSchoolLeavingDate
        {
            get { return LastSchoolLeavingDate == null ? null : DateTime.ParseExact(LastSchoolLeavingDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? EstimatedGrade { get; set; }
        public string? ActualGrade { get; set; }

        [JsonPropertyName("dateOfExam")]
        public string? DateOfExam { get; set; }

        [JsonIgnore]
        [Display(Name = "Date Of Exam")]
        public DateTime? dateOfExam
        {
            get { return DateOfExam == null ? null : DateTime.ParseExact(DateOfExam ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? WorkExperience { get; set; }
    }
}
