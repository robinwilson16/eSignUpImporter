using System;
using System.Collections.Generic;
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
        public string? LLDDCat { get; set; }
        public string? PrimaryLLDD { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
