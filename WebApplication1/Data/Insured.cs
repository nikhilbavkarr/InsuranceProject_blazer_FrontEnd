﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Data;

public partial class Insured
{
    public int InsuredId { get; set; }

    public int PolicyHolderId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<InsuredPolicy> InsuredPolicies { get; set; } = new List<InsuredPolicy>();

    public virtual PolicyHolder PolicyHolder { get; set; } = null!;
}
