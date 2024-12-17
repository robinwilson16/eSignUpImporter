using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class ContactPreference
    {
        public string? ContactPreferenceID { get; set; }
        public string? ContPrefDesc { get; set; }
        public string? ContPrefType { get; set; }
        public string? ContPrefCode { get; set; }
        
        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
