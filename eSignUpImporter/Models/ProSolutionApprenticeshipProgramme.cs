using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSignUpImporter.Models
{
    public class ProSolutionApprenticeshipProgramme
    {
		[Key]
		public int OfferingID { get; set; }
		public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public int? StandardCode { get; set; }
        public string? StandardName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
