namespace eSignUpImporter.Models
{
    public class CandidateEnrolmentRequest
    {
        public int CandidateEnrolmentRequestID { get; set; }
        public int CandidateID { get; set; }
        public string? Email { get; set; }
        public int EnrolmentRequestID { get; set; }

        public Candidate? Candidate { get; set; }
        public EnrolmentRequest? EnrolmentRequest { get; set; }
    }
}
