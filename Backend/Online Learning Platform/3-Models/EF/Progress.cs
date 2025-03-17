using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class Progress
{
    
    [Required]
    [Key]
    [Column("ProgressID")]
    public Guid Id { get; set; } 



    public DateTime? WatchedAt { get; set; }


    [Required]
    [Column("UsersID")]
    public Guid UserId { get; set; }

    [Required]
    [Column("LessonID")]
    public Guid LessonId { get; set; }




    [ForeignKey("UserId")]
    [InverseProperty("Progresses")]
    public virtual User User { get; set; } = null!;




    [ForeignKey("LessonId")]
    [InverseProperty("Progresses")]
    public virtual Lesson Lesson { get; set; }=null!;

    
}
