using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class Quiz
{
    [Key]
    public int QuizId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizAttendance> QuizAttendances { get; set; } = new List<QuizAttendance>();
}
