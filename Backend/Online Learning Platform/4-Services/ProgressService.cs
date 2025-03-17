using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class ProgressService : IDisposable
{
    private readonly LearningPlatformContext _db;
    private readonly IMapper _mapper;

    public ProgressService(LearningPlatformContext learningPlatformContext,IMapper mapper)
    {
        _db = learningPlatformContext;
        _mapper = mapper;
    }



    // Retrieves all progress records for a specific user
    public List<ProgressDto> GetAllProgressForUser(Guid userId)
    {
        return _db.Progresses.Where(p => p.UserId == userId).Select(p=>_mapper.Map<ProgressDto>(p)).ToList();
    }

    // Retrieves all progress records for a specific lesson
    public List<ProgressDto> GetAllProgressForLesson(Guid lessonId)
    {
        return _db.Progresses.Where(p => p.LessonId == lessonId).Select(p => _mapper.Map<ProgressDto>(p)).ToList();
    }
  



    // get all progrees of a user in specific course
    //public List<Progress> GetAllProgressForCourse(Guid courseId, Guid userId)
    //{
    //    Lesson? lesson = _db.Lessons.SingleOrDefault(l => l.CourseId == courseId);
    //    if (lesson == null) return new List<Progress>();

    //    return _db.Progresses.Where(p =>p.UserId==userId && p.LessonId == lesson.Id).ToList();
    //}


    //when we register a user to course(in the EnrollementService) we will call this function to conect all the lessons of this course to the user
    public void AddAllProgressForCourse(Guid courseId, Guid userId)
    {
        Console.WriteLine("adding");
        List<Lesson> lessons = _db.Lessons.Where(l => l.CourseId == courseId).ToList();
        if (!lessons.Any())
        {
            Console.WriteLine("No lessons found for this course.");
            return;
        }
        List<Progress> progresses = lessons.Select(l=> new Progress { LessonId = l.Id, UserId = userId, WatchedAt = null }).ToList();

        _db.Progresses.AddRange(progresses); 
        _db.SaveChanges(); 

        Console.WriteLine("Progress added successfully!");

    }


    //when we delete a user from course(in the EnrollementService) we will call this function to delete all the lessons of this course to the user
    public void DeleteProgressForCourse(Guid userId, Guid courseId)
    {
        Console.WriteLine("deleting");

        
        List<Progress> progresses = _db.Progresses
            .Include(p => p.Lesson)
            .Where(p => p.UserId == userId && p.Lesson.CourseId == courseId)
            .ToList();

        if (!progresses.Any())
        {
            Console.WriteLine("No progress found for this user in this course.");
            return;
        }

       
        _db.Progresses.RemoveRange(progresses);
        _db.SaveChanges();

        Console.WriteLine("Progress deleted successfully!");
    }


    // Adds a single progress entry
    public Progress AddProgress(Progress progress)
    {
        Console.WriteLine("added1");
        _db.Progresses.Add(progress);
        _db.SaveChanges();
        Console.WriteLine("added2");
        return progress;
    }


    // Updates a progress entry with a watched timestamp
    public ProgressDto? UpdateProgress(Guid userId, Guid lessonId, DateTime watchedAt)
    {
        Progress? progress = _db.Progresses.FirstOrDefault(p => p.UserId == userId && p.LessonId == lessonId);

        if (progress == null) return null;
        Console.WriteLine("aaaaaaa");
        progress.WatchedAt = watchedAt;
        _db.SaveChanges();
        Console.WriteLine("bbbbbbbbbbbb");
        ProgressDto progressDto = _mapper.Map<ProgressDto>(progress);   
        return progressDto;
    }





    // Deletes a specific progress entry
    public bool DeleteProgress(Guid progressId)
    {
        Progress? progress = _db.Progresses.FirstOrDefault(p => p.Id == progressId);
        if (progress == null) return false;

        _db.Progresses.Remove(progress);
        _db.SaveChanges();
        return true;
    }





    // Checks if a lesson has been watched by a user
    public bool CheckIfLessonWatched(Guid userId, Guid lessonId)
    {
        Progress? progress = _db.Progresses.FirstOrDefault(p => p.UserId == userId && p.LessonId == lessonId);

        if (progress == null) return false;
        return progress.WatchedAt.HasValue;
    }


    // Releases database resources
    public void Dispose()
    {
        _db.Dispose();
    }

}
