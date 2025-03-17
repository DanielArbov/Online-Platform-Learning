using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Talent;
public class Enrollment
{

    [Required]
    [Key]
    [Column("EnrollmentID")]
    public Guid Id { get; set; } 


    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("UsersID")]
    public Guid UserId { get; set; }

    [Required]
    [Column("CourseID")]
    public Guid CourseId { get; set; }


    [ForeignKey("UserId")]
    [InverseProperty("Enrollments")]
    public virtual User User { get; set; } = null!;

    

    [ForeignKey("CourseId")]
    [InverseProperty("Enrollments")]
    public virtual Course Course { get; set; }=null!;   

}
