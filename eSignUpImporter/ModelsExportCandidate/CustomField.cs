using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class CustomField
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? FormSection { get; set; }
        public string? FieldName { get; set; }
        public string? FieldValue { get; set; }
        
        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
