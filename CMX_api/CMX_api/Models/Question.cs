using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class Question
{
    [Key]
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public string? Question1 { get; set; }

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public string? CorrectOption { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;
}
