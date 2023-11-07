using System;
using System.Collections.Generic;

namespace CMX_client.Models;

public partial class QuizAttendance
{
    public int AttendanceId { get; set; }

    public int QuizId { get; set; }

    public int StudentId { get; set; }

    public int? Score { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
    public QuizAttendanceDTO GetQuizAttendanceDTO()
    {
        QuizAttendanceDTO qad = new QuizAttendanceDTO();
        qad.AttendanceId = AttendanceId;
        qad.QuizId = QuizId;
        qad.StudentId = StudentId;
        qad.Score = Score;
        qad.Student = Student.GetUserDTO();
        return qad;
    }
}
public class QuizAttendanceDTO
{
    public int AttendanceId { get; set; }

    public int QuizId { get; set; }

    public int StudentId { get; set; }

    public int? Score { get; set; }
    public virtual UserDTO Student { get; set; } = null!;
}

