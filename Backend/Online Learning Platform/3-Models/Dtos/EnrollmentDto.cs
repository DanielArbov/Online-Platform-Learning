using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Talent;
public class EnrollmentDto
{

    
    public Guid Id { get; set; } 


    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    
    public Guid UserId { get; set; }

    
    public Guid CourseId { get; set; }



}
