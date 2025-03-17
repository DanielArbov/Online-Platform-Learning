using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Talent;
public class Course
{
    [Required]
    [Key]
    [Column("CourseID")]
    public Guid Id { get; set; } 

    [Required]
    [StringLength(50, ErrorMessage = "title cant exceeds 50 chars")]
    public string Title { get; set; } = null!;




    [Required]
    [StringLength(50, ErrorMessage = "description cant exceeds 50 chars")]
    public string Description { get; set; }=null!;


    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [InverseProperty("Course")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    [InverseProperty("Course")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();


}
