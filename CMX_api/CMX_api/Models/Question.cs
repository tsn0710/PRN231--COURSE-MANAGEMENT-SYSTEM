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
    public QuestionDTO GetQuestionDTO()
    {
        QuestionDTO qd = new QuestionDTO();
        qd.QuestionId = QuestionId;
        qd.QuizId = QuizId;
        qd.Question1 = Question1;
        qd.OptionA = OptionA;
        qd.OptionB = OptionB;
        qd.OptionC = OptionC;
        qd.OptionD = OptionD;
        qd.CorrectOption = CorrectOption;
        return qd;

    }
}
public class QuestionDTO
{
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public string? Question1 { get; set; }

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public string? CorrectOption { get; set; }
}
