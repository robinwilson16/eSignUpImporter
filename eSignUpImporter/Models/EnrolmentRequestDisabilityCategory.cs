using System;
using System.Collections.Generic;

namespace eSignUpImporter.Models;

public partial class EnrolmentRequestDisabilityCategory
{
    public int EnrolmentRequestDisabilityCategoryId { get; set; }

    public int EnrolmentRequestId { get; set; }

    public int DisabilityCategoryId { get; set; }

    public bool? IsPrimary { get; set; }

    public virtual EnrolmentRequest EnrolmentRequest { get; set; } = null!;
}
