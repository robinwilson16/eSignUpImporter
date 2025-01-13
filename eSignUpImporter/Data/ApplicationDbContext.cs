using eSignUpImporter.Models;
using eSignUpImporter.ModelsCandidateGetAll;
using eSignUpImporter.ModelsExportCandidate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace eSignUpImporter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //DB Classes
        public DbSet<APIAccessToken> APIAccessToken { get; set; }
        public DbSet<EnrolmentRequest> EnrolmentRequest { get; set; }
        public DbSet<ProSolutionApprenticeshipProgramme> ProSolutionApprenticeshipProgramme { get; set; }

        //ModelsCandidateGetAll
        public DbSet<ModelsCandidateGetAll.Candidate> GACandidate { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateDisabilityLearningDifficultyResult> GACandidateDisabilityLearningDifficultyResult { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateDocument> GACandidateDocument { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateEnrolmentRequest> GACandidateEnrolmentRequest { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateExtraField> GACandidateExtraField { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateNote> GACandidateNote { get; set; }
        public DbSet<ModelsCandidateGetAll.CandidateQualification> GACandidateQualification { get; set; }
        public DbSet<ModelsCandidateGetAll.CustomFieldValue> GACustomFieldValue { get; set; }

        //ModelsExportCandidate
        public DbSet<ModelsExportCandidate.ApprenticeshipEmployer> ECApprenticeshipEmployer { get; set; }
        public DbSet<ModelsExportCandidate.Candidate> ECCandidate { get; set; }
        public DbSet<ModelsExportCandidate.CandidateDocument> ECCandidateDocument { get; set; }
        public DbSet<ModelsExportCandidate.CandidateEnrolmentRequest> ECCandidateEnrolmentRequest { get; set; }
        public DbSet<ModelsExportCandidate.CandidateExtraFields> ECCandidateExtraFields { get; set; }
        public DbSet<ModelsExportCandidate.CandidateNote> ECCandidateNote { get; set; }
        public DbSet<ModelsExportCandidate.CandidateQualification> ECCandidateQualification { get; set; }
        public DbSet<ModelsExportCandidate.ContactPreference> ECContactPreference { get; set; }
        public DbSet<ModelsExportCandidate.CustomField> ECCustomField { get; set; }
        public DbSet<ModelsExportCandidate.EmploymentStatusMonitoring> ECEmploymentStatusMonitoring { get; set; }
        public DbSet<ModelsExportCandidate.EnglishAndMathsQualification> ECEnglishAndMathsQualification { get; set; }
        public DbSet<ModelsExportCandidate.EnglishMathsComponent> ECEnglishMathsComponent { get; set; }
        public DbSet<ModelsExportCandidate.HouseholdSituation> ECHouseholdSituation { get; set; }
        public DbSet<ModelsExportCandidate.LearnerEmploymentStatus> ECLearnerEmploymentStatus { get; set; }
        public DbSet<ModelsExportCandidate.LLDDAndHealthProblem> ECLLDDAndHealthProblem { get; set; }
        public DbSet<ModelsExportCandidate.LLDDAndHealthProblemPeopleSoft> ECLLDDAndHealthProblemPeopleSoft { get; set; }
        public DbSet<ModelsExportCandidate.OnboardingDocument> ECOnboardingDocument { get; set; }
        public DbSet<ModelsExportCandidate.PlacedRecruitment> ECPlacedRecruitment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Needed to avoid error:
            //The dependent side could not be determined for the one-to-one relationship between 'ApprenticeshipEmployer.PlacedRecruitment' and 'PlacedRecruitment.ApprenticeshipEmployer'.
            //To identify the dependent side of the relationship, configure the foreign key property.
            //If these navigations should not be part of the same relationship, configure them independently via separate method chains in 'OnModelCreating'.
            modelBuilder.Entity<PlacedRecruitment>()
            .HasOne(a => a.ApprenticeshipEmployer)
            .WithOne(a => a.PlacedRecruitment)
            .HasForeignKey<ApprenticeshipEmployer>(c => c.VacancyID);
        }
    }
}
