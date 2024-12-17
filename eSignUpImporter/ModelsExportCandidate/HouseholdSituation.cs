using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class HouseholdSituation
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? HouseholdSituationDesc { get; set; }
        public string? LearnDelFAMType { get; set; }
        public string? LearnDelFAMCode { get; set; }

        [JsonIgnore]
        public PlacedRecruitment? PlacedRecruitment { get; set; }
    }
}
