using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class UserDto
{
    
   
    public int Id { get; set; }

    
    public string Name { get; set; } = null!;

   
    public string Email { get; set; } = null!;

    
   

}





