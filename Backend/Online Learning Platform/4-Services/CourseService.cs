using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class CourseService : IDisposable
{
    private readonly LearningPlatformContext _db;
    private readonly IMapper _mapper;

    public CourseService(LearningPlatformContext learningPlatformContext, IMapper mapper)
    {
        _db = learningPlatformContext;
        _mapper = mapper;
    }

    // Returns a list of all courses from the database
    public List<CourseDto> GetAllCourses()
    {
        return _db.Courses.AsNoTracking().Select(c=>_mapper.Map<CourseDto>(c)).ToList();
    }


    // Retrieves a course by its unique ID, or returns null if not found
    public CourseDto? GetCourseById(Guid id)
    {
        Course? course = _db.Courses.AsNoTracking().SingleOrDefault(s => s.Id == id);
        if (course == null) return null;
        CourseDto courseDto=_mapper.Map<CourseDto>(course);
        return courseDto;
    }

    // Adds a new course to the database and returns its details
    public CourseDto AddCourse(CourseDto courseDto)
    {
        Course course = _mapper.Map<Course>(courseDto);
        _db.Courses.Add(course);
        _db.SaveChanges();
        courseDto = _mapper.Map<CourseDto>(course);
        return courseDto;
    }

    // Deletes a course by its ID, including its lessons, progress, and enrollments
    public bool DeleteCourse(Guid id)
    {
        CourseDto? courseDto = GetCourseById(id);
        if (courseDto == null) return false;


        Course course = _mapper.Map<Course>(courseDto);



        List<Lesson> lessons = _db.Lessons.Where(l => l.CourseId == id).ToList();
        _db.Lessons.RemoveRange(lessons);

        List<Progress> progresses = _db.Progresses.Include(P=>P.Lesson).Where(P => P.Lesson.CourseId == id).ToList();
        _db.Progresses.RemoveRange(progresses);


        List<Enrollment> enrollments = _db.Enrollments.Where(l => l.CourseId ==id).ToList();
        _db.Enrollments.RemoveRange(enrollments);



        _db.Courses.Remove(course);
        _db.SaveChanges();
        return true;
    }



    // Updates an existing course and returns the updated details
    public CourseDto? UpdateCourse(CourseDto courseDto)
    {
        CourseDto? c= GetCourseById(courseDto.Id);
        if (c == null) return null;

        Course course = _mapper.Map<Course>(courseDto);

        _db.Courses.Attach(course);
        _db.Entry(course).State = EntityState.Modified;
        _db.SaveChanges();
        return courseDto;
    }

    // Releases database resources
    public void Dispose()
    {
        _db.Dispose();
    }
}
