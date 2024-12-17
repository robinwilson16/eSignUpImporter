using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class EmploymentStatusMonitoring
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? ESMTypeDesc { get; set; }
        public string? ESMCodeDesc { get; set; }
        public string? ESMType { get; set; }
        public string? ESMCode { get; set; }

        [JsonIgnore]
        public LearnerEmploymentStatus? LearnerEmploymentStatus { get; set; }
    }
}
