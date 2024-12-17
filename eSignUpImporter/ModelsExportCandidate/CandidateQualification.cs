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
    public class CandidateQualification
    {
        public int ID { get; set; }
        public string? QualificationTitle { get; set; }
        public string? QualificationReference { get; set; }
        public string? QualificationTypeName { get; set; }
        public string? OrganisationName { get; set; }
        public string? Grade { get; set; }

        [JsonPropertyName("dateOfAward")]
        public string? DateOfAward { get; set; }

        [JsonIgnore]
        [Display(Name = "Date of Award")]
        public DateTime? dateOfAward
        {
            get { return DateOfAward == null ? null : DateTime.ParseExact(DateOfAward ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        public string? HighestAward { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
