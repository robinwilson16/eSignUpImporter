using System;
using System.Collections.Generic;

namespace eSignUpImporter.Models;

public partial class EnrolmentRequestEmploymentHistory
{
    public int EnrolmentRequestEmploymentHistoryId { get; set; }

    public int EnrolmentRequestId { get; set; }

    public string? EmployerName { get; set; }

    public string? ContactTel { get; set; }

    public string? Email { get; set; }

    public string? JobTitle { get; set; }

    public bool IsFullTime { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public int? WblperiodOfTrainingId { get; set; }

    public string? EmploymentStatusId { get; set; }

    public int? OrganisationId { get; set; }

    public string? WorkplacePostcodeOut { get; set; }

    public string? WorkplacePostcodeIn { get; set; }

    public int? OrganisationContactId { get; set; }

    public bool? IsPlacement { get; set; }

    public DateTime? ExpectedPlacementEndDate { get; set; }

    public bool? IsSelfEmployed { get; set; }

    public string? EmploymentIntensityId { get; set; }

    public string? LengthOfUnemploymentId { get; set; }

    public string? BenefitStatusIndicatorId { get; set; }

    public bool? PreviouslyInFteducation { get; set; }

    public bool? Neetrisk { get; set; }

    public string? ReasonForUnemploymentId { get; set; }

    public string? EmploymentStatusType1112 { get; set; }

    public string? EmploymentStatusCode1112 { get; set; }

    public bool IncludeInReturn { get; set; }

    public string? LengthOfEmploymentId { get; set; }

    public virtual EnrolmentRequest EnrolmentRequest { get; set; } = null!;
}
