using System;
using System.Collections.Generic;

namespace CMX_client.Models;

public partial class Quiz
{
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
        foreach (QuizAttendance aq in QuizAttendances)
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
    public QuizView GetQuizView()
    {
        QuizView qv = new QuizView();
        qv.QuizId= QuizId;
        qv.CourseId= CourseId;
        qv.Title = Title;
        qv.Course = Course;
        qv.Questions = Questions;
        qv.NoOfQuestion=Questions.Count;
        qv.QuizAttendances=QuizAttendances;
        return qv;
    }
}

public class QuizView
{
    public int QuizId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public virtual CourseDTO Course { get; set; } = null!;
    public virtual ICollection<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();

    public int NoOfQuestion { get; set; }

    public virtual ICollection<QuizAttendanceDTO> QuizAttendances { get; set; } = new List<QuizAttendanceDTO>();
}
