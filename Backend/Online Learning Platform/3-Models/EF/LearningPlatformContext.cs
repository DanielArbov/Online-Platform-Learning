using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public partial class LearningPlatformContext : DbContext
{
    public LearningPlatformContext()
    {
    }

    public LearningPlatformContext(DbContextOptions<LearningPlatformContext> options)
        : base(options)
    {
    }



    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<User> Users { get; set; }
   
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    
    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Progress>  Progresses { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
        optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
    }

    
}









    

   