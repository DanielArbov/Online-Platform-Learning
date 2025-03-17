using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Talent;


public  class LessonDto
{
    
   
    public Guid Id { get; set; } 

   
    public string Title { get; set; } = null!;

    
    public string VideoUrl { get; set; } = null!;

   
    public Guid CourseId { get; set; }

    

}
