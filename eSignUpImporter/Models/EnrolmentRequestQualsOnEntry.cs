using System;
using System.Collections.Generic;

namespace eSignUpImporter.Models;

public partial class EnrolmentRequestQualsOnEntry
{
    public int EnrolmentRequestQualsOnEntryId { get; set; }

    public int EnrolmentRequestId { get; set; }

    public string? QualId { get; set; }

    public string? Grade { get; set; }

    public DateTime? DateAwarded { get; set; }

    public string? Subject { get; set; }

    public string? Level { get; set; }

    public string? PlaceOfStudy { get; set; }

    public string? PredictedGrade { get; set; }

    public bool AchievedAtCollege { get; set; }

    public virtual EnrolmentRequest EnrolmentRequest { get; set; } = null!;
}
