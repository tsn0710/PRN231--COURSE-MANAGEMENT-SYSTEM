using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class QuizAttendance
{
    [Key]
    public int AttendanceId { get; set; }

    public int QuizId { get; set; }

    public int StudentId { get; set; }

    public int? Score { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
