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
    public class CandidateDocument
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? QualifictionType { get; set; }
        public string? FileName { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public string? LastUpdatedDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Last Updated Date")]
        public DateTime? lastUpdatedDate
        {
            get { return LastUpdatedDate == null ? null : DateTime.ParseExact(LastUpdatedDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? DocumentURL { get; set; }
        public string? DocumentID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
