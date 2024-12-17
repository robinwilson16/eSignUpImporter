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
    public class CandidateNote
    {
        public int ID { get; set; }
        public string? Notes { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public string? LastUpdatedDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Last Updated Date")]
        public DateTime? lastUpdatedDate
        {
            get { return LastUpdatedDate == null ? null : DateTime.ParseExact(LastUpdatedDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        [JsonPropertyName("createdOn")]
        public string? CreatedOn { get; set; }

        [JsonIgnore]
        [Display(Name = "Created On")]
        public DateTime? createdOn
        {
            get { return CreatedOn == null ? null : DateTime.ParseExact(CreatedOn ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? CreatedBy { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
