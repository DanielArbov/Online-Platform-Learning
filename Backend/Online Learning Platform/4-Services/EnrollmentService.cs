using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class EnrollmentService: IDisposable
{
    private readonly LearningPlatformContext _db;
    private readonly ProgressService _progressService;
    private readonly IMapper _mapper;

    public EnrollmentService(LearningPlatformContext learningPlatformContext ,ProgressService progressService, IMapper mapper)
    {
        _db = learningPlatformContext;
        _progressService = progressService;
        _mapper = mapper;
    }




    // Registers a user for a course and initializes their progress
    public EnrollmentDto? RegisterUserToCourse(Guid userId, Guid courseId)
    {
        if(_db.Courses.FirstOrDefault(c=>c.Id==courseId)==null)
            return null;

        Enrollment enrollment = new Enrollment {  UserId = userId,  CourseId = courseId, EnrolledAt = DateTime.UtcNow};

        _db.Enrollments.Add(enrollment);
        _db.SaveChanges();

        _progressService.AddAllProgressForCourse( courseId,userId);

        EnrollmentDto enrollmentDto = _mapper.Map<EnrollmentDto>(enrollment);
        return enrollmentDto;
    }


    // Unregisters a user from a course and removes their progress
    public bool UnregisterUserFromCourse(Guid userId, Guid courseId)
    {
        if (_db.Courses.FirstOrDefault(c => c.Id == courseId) == null )
            return false;
        
    
        Enrollment? enrollment = _db.Enrollments.FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId);

        if (enrollment == null) return false; 

        _progressService.DeleteProgressForCourse(userId, courseId);

        _db.Enrollments.Remove(enrollment);
        _db.SaveChanges();
        return true;
    }


    // Retrieves all courses a user is enrolled in
    public List<CourseDto> GetCoursesOfUser(Guid userId)
    {
        return _db.Enrollments.Where(e => e.UserId == userId).Select(e =>_mapper.Map<CourseDto>(e.Course)).ToList();
    }



    // Retrieves all users enrolled in a specific course
    public List<UserDto> GetUsersInCourse(Guid courseId)
    {
        return _db.Enrollments.Where(e => e.CourseId == courseId).Select(e => _mapper.Map <UserDto>(e.User)).ToList();
    }

    // Checks if a user is enrolled in a specific course
    public bool IsUserEnrolled(Guid userId, Guid courseId)
    {
        return _db.Enrollments.Any(e => e.UserId == userId && e.CourseId == courseId);
    }



    // Releases database resources
    public void Dispose()
    {
        _db.Dispose();
    }
}
