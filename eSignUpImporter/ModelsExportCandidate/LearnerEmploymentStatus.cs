using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class LearnerEmploymentStatus
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? EmpStat { get; set; }
        public ICollection<EmploymentStatusMonitoring>? EmploymentStatusMonitoring { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
    }
}
