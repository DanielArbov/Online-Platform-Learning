using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;


[ApiController]
public class CourseController : ControllerBase,IDisposable
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }



    // Retrieves all courses (requires authorization)
    [Authorize]
    [HttpGet("api/courses")]
    public IActionResult GetAllCourses()
    {
        return Ok(_courseService.GetAllCourses());
    }

    // Retrieves a course by ID (requires authorization)
    [Authorize]
    [HttpGet("api/courses/{courseId}")]
    public IActionResult GetcourseById([FromRoute] Guid courseId)
    {
        CourseDto? courseDto = _courseService.GetCourseById(courseId);
        if (courseDto == null) return NotFound(new ResourceNotFoundError());
        return Ok(courseDto);
    }


    // Adds a new course (only accessible by users with the "Instructor" role)
    [Authorize(Roles = "Instructor")]
    [HttpPost("api/courses")]
    public IActionResult AddCourse([FromBody] CourseDto courseDto)
    {
        Console.WriteLine("add course");
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        CourseDto c = _courseService.AddCourse(courseDto);
        return Created("api/courses/" + c.Id, c);
    }


    // Updates an existing course
    [HttpPut("api/courses/{courseId}")]
    public IActionResult UpdateCourse([FromRoute] Guid courseId, [FromBody] CourseDto courseDto)
    {
        courseDto.Id = courseId;

        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        CourseDto? c = _courseService.UpdateCourse(courseDto);
        if (c == null) return NotFound(new ResourceNotFoundError());
        return Ok(c);
    }

    // Deletes a course by ID
    [HttpDelete("api/courses/{courseId}")]
    public IActionResult DeleteCourse([FromRoute] Guid courseId)
    {
        if (!_courseService.DeleteCourse(courseId)) return NotFound(new ResourceNotFoundError());
        return NoContent();
    }

    // Disposes of the course service to free resources
    public void Dispose()
    {
        _courseService.Dispose();
    }
}

