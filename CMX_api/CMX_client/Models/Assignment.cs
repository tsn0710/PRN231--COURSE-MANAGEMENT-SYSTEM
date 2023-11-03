﻿using System;
using System.Collections.Generic;

namespace CMX_client.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool Display { get; set; }

    public DateTime? Deadline { get; set; }

    public string? File { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
}
