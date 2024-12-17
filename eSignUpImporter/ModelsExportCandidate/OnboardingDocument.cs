using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class OnboardingDocument
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? DocumentType { get; set; }
        public string? SignaturesCollected { get; set; }
        public string? SignaturesRequired { get; set; }
        public string? DocumentURL { get; set; }
        public string? RecruitmentID { get; set; }
        public string? DocumentTypeID { get; set; }

        [JsonIgnore]
        public PlacedRecruitment? PlacedRecruitment { get; set; }
    }
}
