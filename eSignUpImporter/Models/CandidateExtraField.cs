using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eSignUpImporter.Models
{
    public class CandidateExtraField
    {
        public int? ID { get; set; }
        public bool? PreviouslyStudiedInUK { get; set; }
        public bool? PreviouslyStudiedAtCollege { get; set; }
        public string? PreviousCollegeIdNumber { get; set; }
        public bool? PriorLearningRecognition { get; set; }
        public bool? HomeEducated { get; set; }
        public bool? NoQualification { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastSchoolStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastSchoolLeavingDate { get; set; }
        public string? EstimatedGrade { get; set; }
        public string? ActualGrade { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfExam { get; set; }

        [JsonIgnore]
        public int? CandidateID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
