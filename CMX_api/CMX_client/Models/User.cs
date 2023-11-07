using System;
using System.Collections.Generic;

namespace CMX_client.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<QuizAttendance> QuizAttendances { get; set; } = new List<QuizAttendance>();

    public virtual ICollection<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
    public UserDTO GetUserDTO() { return new UserDTO { UserId = UserId, Email = Email, Username = Username }; }
}
public class UserDTO
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }
}
