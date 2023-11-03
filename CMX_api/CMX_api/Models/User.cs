using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<QuizAttendance> QuizAttendances { get; set; } = new List<QuizAttendance>();

    public virtual ICollection<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
}
