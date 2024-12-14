using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eSignUpImporter.Models
{
    public class CandidateDocument
    {
        public string? QualificationType { get; set; }
        public string? FileName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedDate { get; set; }
        public string? DocumentURL { get; set; }
        public int? ID { get; set; }

        [JsonIgnore]
        public int? CandidateID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
