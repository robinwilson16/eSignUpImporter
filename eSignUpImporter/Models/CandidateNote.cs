using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eSignUpImporter.Models
{
    public class CandidateNote
    {
        public int ID { get; set; }
        public string? Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }

        public int? CreatedNoteUserID { get; set; }

        [JsonIgnore]
        public int? CandidateID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
