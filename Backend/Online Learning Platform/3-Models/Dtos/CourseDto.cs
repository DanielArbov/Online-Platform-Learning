using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Talent;
public class CourseDto
{
   
    public Guid Id { get; set; } 

    
    public string Title { get; set; } = null!;


    public string Description { get; set; }=null!;


    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 

}
