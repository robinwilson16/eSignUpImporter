using Microsoft.EntityFrameworkCore;
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
    public class ApprenticeshipEmployer
    {
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public int VacancyID { get; set; }

        [JsonPropertyName("employerID")]
        public string? EmployerID { get; set; }

        [JsonIgnore]
        [Display(Name = "Employer ID")]
        public int? employerID
        {
            get { return EmployerID == null ? null : int.Parse(EmployerID ?? "", new CultureInfo("en-GB")); }
        }

        public string? Name { get; set; }

        [JsonPropertyName("edrsNumber")]
        public string? EDRSNumber { get; set; }

        [JsonIgnore]
        [Display(Name = "EDRS Number")]
        public int? eDRSNumber
        {
            get { return EDRSNumber == null ? null : EDRSNumber == "" ? null : int.Parse(EDRSNumber ?? "", new CultureInfo("en-GB")); }
        }

        public string? VacancyEmployerSiteName { get; set; }
        public string? VacancyEmployerSiteAddress1 { get; set; }
        public string? VacancyEmployerSiteAddress2 { get; set; }
        public string? VacancyEmployerSiteTown { get; set; }
        public string? VacancyEmployerSitePostCode { get; set; }

        [JsonIgnore]
        public PlacedRecruitment? PlacedRecruitment { get; set; }
    }
}
