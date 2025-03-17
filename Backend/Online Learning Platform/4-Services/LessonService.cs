using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class LessonService : IDisposable
{
    private readonly LearningPlatformContext _db;
    private readonly IMapper _mapper;

    public LessonService(LearningPlatformContext learningPlatformContext,IMapper mapper)
    {
        _db = learningPlatformContext;
        _mapper=mapper;
    }

    // Retrieves all lessons for a specific course
    public List<LessonDto> GetAllLessonsForCourse(Guid CourseId)
    {
        return _db.Lessons.AsNoTracking().Where(l => l.CourseId == CourseId).Select(l=>_mapper.Map<LessonDto>(l)).ToList();
    }


    // Retrieves all lessons in the system
    public List<LessonDto> GetAllLessons()
    {
        return _db.Lessons.AsNoTracking().Select(l => _mapper.Map<LessonDto>(l)).ToList();
    }


    // Retrieves a lesson by its ID
    public LessonDto? GetLessoneById(Guid lessonId)
    {
        Lesson? lesson = _db.Lessons.SingleOrDefault(l => l.Id == lessonId);
        if (lesson == null) return null;
        LessonDto lessonDto=_mapper.Map<LessonDto>(lesson);
        return lessonDto;
    }


    // Adds a new lesson to the database
    public LessonDto AddLesson(LessonDto lessonDto)
    {
        Lesson lesson = _mapper.Map<Lesson>(lessonDto);
        _db.Lessons.Add(lesson);
        _db.SaveChanges();
        lessonDto = _mapper.Map<LessonDto>(lesson);
        return lessonDto;
    }


    // Deletes a lesson and removes related progress records
    public bool DeleteLesson(Guid lessonId)
    {
        LessonDto? lessonDto = GetLessoneById(lessonId);
        if (lessonDto == null) return false;

        Lesson lesson=_mapper.Map<Lesson>(lessonDto);

        List<Progress> progresses = _db.Progresses.Where(P => P.LessonId == lessonId).ToList();
        _db.Progresses.RemoveRange(progresses);


        _db.Lessons.Remove(lesson);
        _db.SaveChanges();
        return true;
    }



    // Updates an existing lesson
    public LessonDto? UpdateLesson(LessonDto lessonDto)
    {
        LessonDto? l = GetLessoneById(lessonDto.Id);
        if (l == null) return null;

        Lesson lesson = _mapper.Map<Lesson>(lessonDto);

        _db.Lessons.Attach(lesson);
        _db.Entry(lesson).State = EntityState.Modified;
        _db.SaveChanges();
        return lessonDto;
    }

    // Releases database resources
    public void Dispose()
    {
        _db.Dispose();
    }
}
