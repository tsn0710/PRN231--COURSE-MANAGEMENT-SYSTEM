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
    public QuizDTO GetQuizDTO()
    {
        QuizDTO qd = new QuizDTO();
        qd.QuizId = QuizId;
        qd.CourseId = CourseId;
        qd.Title = Title;
        qd.Course = Course.GetCourseDTO();
        foreach (Question q in Questions)
        {
            qd.Questions.Add(q.GetQuestionDTO());
        }
        foreach(QuizAttendance aq in QuizAttendances)
        {
            qd.QuizAttendances.Add(aq.GetQuizAttendanceDTO());
        }
        return qd;
    }
}
public class QuizDTO
{
    public int QuizId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public virtual CourseDTO Course { get; set; } = null!;
    public virtual ICollection<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();

    public virtual ICollection<QuizAttendanceDTO> QuizAttendances { get; set; } = new List<QuizAttendanceDTO>();
}
