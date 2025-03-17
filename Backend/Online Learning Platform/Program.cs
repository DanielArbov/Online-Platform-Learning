using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

// Install-Package Microsoft.EntityFrameworkCore.SqlServer
// Install-Package Microsoft.EntityFrameworkCore.Design
// Install-Package Microsoft.EntityFrameworkCore.Tools
// Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.0


public class Program
{
    public static void Main(string[] args)
    {
        //Building app
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<LearningPlatformContext>();

        // Registering database context (for working with the database)
        builder.Services.AddScoped<CourseService>();
        builder.Services.AddScoped<LessonService>();
        builder.Services.AddScoped<ProgressService>();
        builder.Services.AddScoped<EnrollmentService>();
        builder.Services.AddScoped<UserService>();

        // Adding MVC with a global filter (for exception handling)
        builder.Services.AddMvc(options => options.Filters.Add<CatchAllFilter>());

        // Configuring JWT authentication for API authorization
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtHelper.SetBearerOptions);

        builder.Services.Configure<ApiBehaviorOptions>(option => option.SuppressModelStateInvalidFilter = true);

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddControllers();


        // Adding CORS policy to allow requests from Angular client
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularClient", policy =>
            {
                policy.WithOrigins("http://localhost:4200") 
                      .AllowAnyMethod()  
                      .AllowAnyHeader(); 
            });
        });


        var app = builder.Build();

        // Configure and run app

        app.UseCors("AllowAngularClient");


        app.UseAuthorization();
        app.MapControllers();

        app.Run();

    }
}
