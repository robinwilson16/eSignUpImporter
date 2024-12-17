using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class EnglishAndMathsQualification
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? Type { get; set; }
        public string? Level { get; set; }
        public string? Score { get; set; }
        public string? BKSBResult { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
