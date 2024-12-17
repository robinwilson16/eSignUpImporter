using eSignUpImporter.Models;
using System.Text.Json.Serialization;

namespace eSignUpImporter.ModelsCandidateGetAll
{
    public class CandidateEnrolmentRequest
    {
        public int CandidateEnrolmentRequestID { get; set; }
        public int CandidateID { get; set; }
        public string? Email { get; set; }
        public int EnrolmentRequestID { get; set; }

        [JsonIgnore]
        public Candidate? Candidate { get; set; }
        public EnrolmentRequest? EnrolmentRequest { get; set; }
    }
}
