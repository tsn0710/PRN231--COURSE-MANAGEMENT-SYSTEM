using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class StudentAssignment
{
    [Key]
    public int SubmissionId { get; set; }

    public int AssignmentId { get; set; }

    public int StudentId { get; set; }

    public string? File { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
