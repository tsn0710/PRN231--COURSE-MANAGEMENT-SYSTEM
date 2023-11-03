﻿using System;
using System.Collections.Generic;

namespace CMX_client.Models;

public partial class CourseEnrollment
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
