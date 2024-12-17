using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eSignUpImporter.ModelsExportCandidate
{
    public class EnglishMathsComponent
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string? FunctionalSkillLevel { get; set; }
        public string? DeliveryLead { get; set; }
        public string? Method { get; set; }
        public bool? Required { get; set; }
        public bool? StudyTowards { get; set; }
        public bool? TakeExam { get; set; }
        public decimal? TotalHours { get; set; }
        public string? FundingSource { get; set; }

        [JsonPropertyName("startDate")]
        public string? StartDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Start Date")]
        public DateTime? startDate
        {
            get { return StartDate == null ? null : DateTime.ParseExact(StartDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        [JsonPropertyName("endDate")]
        public string? EndDate { get; set; }

        [JsonIgnore]
        [Display(Name = "End Date")]
        public DateTime? endDate
        {
            get { return EndDate == null ? null : DateTime.ParseExact(EndDate ?? "", "yyyy-MM-dd", new CultureInfo("en-GB")); }
        }

        [JsonIgnore]
        public PlacedRecruitment? PlacedRecruitment { get; set; }
    }
}
