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
    public class LLDDAndHealthProblem
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? LLDDDesc { get; set; }

        [JsonPropertyName("llddCat")]
        public string? LLDDCat { get; set; }

        [JsonIgnore]
        [Display(Name = "LLDD Cat")]
        public int? llddCat
        {
            get { return LLDDCat == null ? null : int.Parse(LLDDCat ?? "", new CultureInfo("en-GB")); }
        }

        [JsonPropertyName("primaryLLDD")]
        public string? PrimaryLLDD { get; set; }

        [JsonIgnore]
        [Display(Name = "Primary LLDD")]
        public bool? primaryLLDD
        {
            get { return PrimaryLLDD == null ? null : PrimaryLLDD == "1" ? true : false; }
        }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
