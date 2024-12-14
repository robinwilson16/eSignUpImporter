using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eSignUpImporter.Models
{
    public class CandidateDisabilityLearningDifficultyResult
    {
        public int? ID { get; set; }

        [Required]
        public int? CandidateID { get; set; } //Include this ID field but not in the other related tables

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
        public int? CandidateDisabilityLearningDifficultiesID { get; set; }
        public string? Name { get; set; }
    }
}
