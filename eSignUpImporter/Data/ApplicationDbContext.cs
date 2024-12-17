using eSignUpImporter.Models;
using eSignUpImporter.ModelsCandidateGetAll;
using eSignUpImporter.ModelsExportCandidate;
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
    }
}
