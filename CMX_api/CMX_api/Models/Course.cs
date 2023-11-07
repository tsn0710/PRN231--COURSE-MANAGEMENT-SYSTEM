using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMX_api.Models;

public partial class Course
{
    [Key]
    public int CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int LecturerId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual User Lecturer { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public CourseDTO GetCourseDTO()
    {
        CourseDTO cd = new CourseDTO();
        cd.CourseId = this.CourseId;
        cd.Title = this.Title;
        cd.Description = this.Description;
        cd.LecturerId = this.LecturerId;
        return cd;
    }
}
public class CourseDTO
{
    public int CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int LecturerId { get; set; }
}
