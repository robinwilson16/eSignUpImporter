using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eSignUpImporter.Models
{
    public class CustomFieldValue
    {
        [Key]
        public string? Value { get; set; }

        [Required]
        public string? Label { get; set; }

        [JsonIgnore]
        public int? CandidateID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
