using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Talent;


[ApiController]
public class LearningController : ControllerBase, IDisposable
{
    private readonly EnrollmentService _enrollmentService;
    private readonly ProgressService _progressService;

    public LearningController(EnrollmentService enrollmentService, ProgressService progressService)
    {
        _enrollmentService = enrollmentService;
        _progressService = progressService;
    }


    // Retrieves all courses of a user (only accessible by students)
    [Authorize(Roles = "Student")]
    [HttpGet("api/enrollments-by-user/{userId}")]
    public IActionResult GetCoursesOfUser([FromRoute] Guid userId)
    {
        return Ok(_enrollmentService.GetCoursesOfUser(userId));
    }


    // Retrieves all users enrolled in a specific course (only accessible by instructors)
    [Authorize(Roles = "Instructor")]
    [HttpGet("api/enrollments-by-course/{courseId}")]
    public IActionResult GetUsersInCourse([FromRoute] Guid courseId) 
    {
        return Ok(_enrollmentService.GetUsersInCourse(courseId));
    }


    // Registers a user to a course (only accessible by students)
    [Authorize(Roles = "Student")]
    [HttpPost("api/enrollments/{userId}")]
    public IActionResult RegisterUserToCourse([FromRoute] Guid userId, [FromBody] string courseIdS)
    {
        Guid courseId;
        if (!Guid.TryParse(courseIdS, out courseId)) return BadRequest(new { message = "Invalid courseId format" });
        
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        if (_enrollmentService.IsUserEnrolled(userId, courseId)) return Conflict(new { message = "You are already enrolled in this course." });
        EnrollmentDto? enrollmentDto = _enrollmentService.RegisterUserToCourse(userId, courseId);
        Console.WriteLine(enrollmentDto?.CourseId);
        if (enrollmentDto == null) return NotFound(new ResourceNotFoundError());
        return Created("api/enrollments/"+enrollmentDto.Id,enrollmentDto);
    }



    // Unregisters a user from a course (only accessible by students)
    [Authorize(Roles = "Student")]
    [HttpDelete("api/enrollments/{userId}")]
    public IActionResult UnregisterUserFromCourse([FromRoute] Guid userId, [FromQuery] Guid courseId)
    {
        if (!_enrollmentService.UnregisterUserFromCourse(userId,courseId)) return NotFound(new ResourceNotFoundError());
        return NoContent();
    }


    // Checks if a student has watched a specific lesson
    [Authorize(Roles = "Student")]
    [HttpGet("api/progresses/{userId}/lesson")]
    public IActionResult CheckIfLessonWatched([FromRoute] Guid userId, [FromQuery] Guid lessonId)
    {
        Console.WriteLine("check if viewd");

        return Ok(_progressService.CheckIfLessonWatched(userId, lessonId));
    }




    // Updates the student's progress (marks a lesson as watched) 
    [Authorize(Roles = "Student")]
    [HttpPut("api/progresses/{userId}")]
    public IActionResult UpdateProgress([FromRoute] Guid userId, [FromBody] string lessonIdS)
    {
        
        Guid lessonId;
        if (!Guid.TryParse(lessonIdS, out lessonId)) return BadRequest(new { message = "Invalid courseId format" });     
        ProgressDto? progressDto = _progressService.UpdateProgress(userId, lessonId, DateTime.UtcNow);
        if (progressDto == null) return NotFound(new ResourceNotFoundError());
        return Ok(progressDto);
    }


    // Deletes a specific progress record by its ID
    [HttpDelete("api/progresses/{progressId}")]
    public IActionResult DeleteProgress([FromRoute] Guid progressId)
    {
        if (!_progressService.DeleteProgress(progressId)) return NotFound(new ResourceNotFoundError());
        return NoContent();
    }

    // Disposes the services to free resources
    public void Dispose()
    {
        _enrollmentService.Dispose();
        _progressService.Dispose();
    }


}
