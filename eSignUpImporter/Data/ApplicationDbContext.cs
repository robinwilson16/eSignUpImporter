using eSignUpImporter.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eSignUpImporter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //DB Classes
        public DbSet<APIAccessToken> APIAccessToken { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateDisabilityLearningDifficultyResult> CandidateDisabilityLearningDifficultyResult { get; set; }
        public DbSet<CandidateDocument> CandidateDocument { get; set; }
        public DbSet<CandidateExtraField> CandidateExtraField { get; set; }
        public DbSet<CandidateNote> CandidateNote { get; set; }
        public DbSet<CandidateQualification> CandidateQualification { get; set; }
        public DbSet<CustomFieldValue> CustomFieldValue { get; set; }
        public DbSet<EnrolmentRequest> EnrolmentRequest { get; set; }
    }
}
