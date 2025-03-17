using AutoMapper;

namespace Talent;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Lesson, LessonDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<Progress, ProgressDto>().ReverseMap();
    }
}