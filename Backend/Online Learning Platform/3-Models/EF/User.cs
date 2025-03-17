using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Talent;
public enum UserRole
{
    Student,
    Instructor
}

public class User
{
    
    [Key]
    [Column("UsersID")]
    public Guid Id { get; set; }


    [Required(ErrorMessage = "Missing user first name")]
    [MinLength(2, ErrorMessage = "user name cant be less than 2 chars")]
    [MaxLength(15, ErrorMessage = "user name cant be more than 15 chars")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Missing user email")]
    [MinLength(2, ErrorMessage = "email cant be less than 2 chars")]
    [MaxLength(30, ErrorMessage = "email cant be more than 30 chars")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    
    
    [Required(ErrorMessage = "Missing user password")]
    [MinLength(2, ErrorMessage = "password cant be less than 2 chars")]
    [StringLength(1000)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage ="Missing role for user")] 
    public UserRole Role { get; set; }


    [InverseProperty("User")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    [InverseProperty("User")]
    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

}





