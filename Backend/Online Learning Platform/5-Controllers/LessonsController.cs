using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ApiController]
public class LessonsController : ControllerBase,IDisposable
{
    private readonly LessonService _lessonService;

    public LessonsController(LessonService lessonService)
    {
        _lessonService = lessonService;
    }

    //[HttpGet("api/lessons/{courseId}")]
    //public IActionResult GetAllLessonsForCourse([FromRoute] Guid courseId)
    //{

    //    return Ok(_lessonService.GetAllLessonsForCourse(courseId));
    //}


    // GET request to retrieve all lessons (only accessible by students)
    [Authorize(Roles = "Student")]
    [HttpGet("api/lessons")]
    public IActionResult GetAllLessons()
    {
        return Ok(_lessonService.GetAllLessons());
    }


    // GET request to retrieve a lesson by its ID (only accessible by students)
    [Authorize(Roles = "Student")]
    [HttpGet("api/lessons/{lessonId}")]
    public IActionResult GetLessonById([FromRoute] Guid lessonId)
    {
        LessonDto? lessonDto = _lessonService.GetLessoneById(lessonId);
        if (lessonDto == null) return NotFound(new ResourceNotFoundError());
        return Ok(lessonDto);
    }

    // POST request to add a new lesson (only accessible by instructors)
    [Authorize(Roles = "Instructor")]
    [HttpPost("api/lessons")]
    public IActionResult AddLesson([FromBody] LessonDto lessonDto)
    {     
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        LessonDto l = _lessonService.AddLesson(lessonDto);
        Console.WriteLine("hello"+l);
        return Created("api/lessons/" + l.Id, l);
    }


    // PUT request to update an existing lesson
    [HttpPut("api/lessons/{lessonId}")]
    public IActionResult UpdateLesson([FromRoute] Guid lessonId, [FromBody] LessonDto lessonDto)
    {
        lessonDto.Id = lessonId;
        
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        LessonDto? l = _lessonService.UpdateLesson(lessonDto);
        if (l == null) return NotFound(new ResourceNotFoundError());
        return Ok(l);
    }

    // DELETE request to remove a lesson by its ID
    [HttpDelete("api/lessons/{lessonId}")]
    public IActionResult DeleteLesson([FromRoute] Guid lessonId)
    {
        if (!_lessonService.DeleteLesson(lessonId)) return NotFound(new ResourceNotFoundError());
        return NoContent();
    }


    // Disposes of the lesson service to free up resources
    public void Dispose()
    {
        _lessonService.Dispose();
    }
}

