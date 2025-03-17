using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Talent;


public  class Lesson
{
    

    [Required]
    [Key]
    [Column("LessonID")]
    public Guid Id { get; set; } 



    [Required]
    [StringLength(50, ErrorMessage = "title cant exceeds 50 chars")]
    public string Title { get; set; } = null!;

    [Required]
    [Url]
    public string VideoUrl { get; set; } = null!;



    [Required]
    [Column("CourseID")]
    public Guid CourseId { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Lessons")]
    public virtual Course Course { get; set; } = null!;


    [InverseProperty("Lesson")]
    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();


}
