using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Courses.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Features.Courses;

public sealed class CoursesService : ICoursesService
{
    private readonly AppDbContext _dbContext;

    public CoursesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Execute(CreateLectureCommand command)
    {
        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course == null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }


        var lecture = new Lecture
        {
            Title = command.Title,

        };

        course.AddLecture(lecture, out var addedLecture);

        _dbContext.Courses.Update(course);

        _dbContext.Lectures.Add(addedLecture);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Execute(UpdateLectureCommand command)
    {
        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        if (!course.Items.Any(x => x.Id == command.Id))
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lecture = await _dbContext.Lectures.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (lecture is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }


        if (!string.IsNullOrEmpty(command.Title))
        {
            lecture.Title = command.Title;
        }

        if (!string.IsNullOrEmpty(command.Description))
        {
            lecture.Description = command.Description;
        }

        if (!string.IsNullOrEmpty(command.ImageUrl))
        {
            lecture.ImageUrl = command.ImageUrl;
        }

        if (command.Price is not null)
        {
            lecture.Price = command.Price.Value;
        }

        if (command.RenewalPrice is not null)
        {
            lecture.RenewalPrice = command.RenewalPrice.Value;
        }


        _dbContext.Update(lecture);

        await _dbContext.SaveChangesAsync();

    }

    public async Task Execute(PublishLectureCommand command)
    {
        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        var item = course.Items.FirstOrDefault(x => x.Id == command.Id);

        if (item is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lecture = _dbContext.Lectures.FirstOrDefault(x => x.Id == command.Id);

        if (lecture is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        if (!lecture.IsPublishable)
        {
            throw new ApiException(LecturesErrors.NotPublishable);
        }

        item.Status = CourseItemStatus.Published;

        _dbContext.Update(course);

        _dbContext.Update(lecture);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Execute(UnPublishLectureCommand command)
    {

        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        var item = course.Items.FirstOrDefault(x => x.Id == command.Id);

        if (item is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lecture = _dbContext.Lectures.FirstOrDefault(x => x.Id == command.Id);

        if (lecture is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        item.Status = CourseItemStatus.Hidden;

        _dbContext.Update(course);

        _dbContext.Update(lecture);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Execute(CreateLessonCommand command)
    {
        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        if (!course.Items.Any(x => x.Id == command.LectureId))
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lecture = await _dbContext.Lectures.FirstOrDefaultAsync(x => x.Id == command.LectureId);

        if (lecture is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lesson = new Lesson
        {
            Title = command.Title,
            Description = command.Description,
            VideoEmbed = command.VideoEmbed

        };

        lecture.AddLesson(lesson, out var addedLesson);

        _dbContext.Lectures.Update(lecture);

        await _dbContext.Lessons.AddAsync(addedLesson);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Execute(UpdateLessonCommand command)
    {
        var course = await _dbContext.Courses.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.CourseId);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        if (!course.Items.Any(x => x.Id == command.LectureId))
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lecture = await _dbContext.Lectures.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == command.LectureId);

        if (lecture is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        if (!lecture.Items.Any(x => x.Id == command.Id))
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (lesson is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        if (!string.IsNullOrWhiteSpace(command.Title))
        {
            lesson.Title = command.Title;
        }

        if (!string.IsNullOrWhiteSpace(command.Description))
        {
            lesson.Description = command.Description;
        }

        if (!string.IsNullOrWhiteSpace(command.VideoEmbed))
        {
            lesson.VideoEmbed = command.VideoEmbed;
        }

        _dbContext.Lessons.Update(lesson);

        await _dbContext.SaveChangesAsync();
    }


    public async Task ExecuteAsync(CreateCourseCommand command)
    {
        var course = new Course
        {
            Title = command.Title,
        };

        await _dbContext.Courses.AddAsync(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(UpdateCourseCommand command)
    {
        var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        if (!string.IsNullOrWhiteSpace(command.Title))
        {
            course.Title = command.Title;
        }

        if (!string.IsNullOrWhiteSpace(command.Description))
        {
            course.Description = command.Description;
        }

        if (command.Price is not null)
        {
            course.Price = command.Price;
        }

        if (command.RenewalPrice is not null)
        {
            course.RenewalPrice = command.RenewalPrice;
        }

        if (command.ExpirationDays is not null)
        {
            course.ExpirationDays = command.ExpirationDays;
        }


        _dbContext.Courses.Update(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(PublishCourseCommand command)
    {
        var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        if (!course.IsPublishable)
        {
            throw new ApiException(CoursesErrors.NotPublishable);
        }

        course.Status = CourseStatus.Published;

        _dbContext.Courses.Update(course);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(UnPublishCourseCommand command)
    {
        var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        course.Status = CourseStatus.Hidden;

        _dbContext.Courses.Update(course);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<GetStudentCoursesResult> QueryAsync(GetStudentCoursesQuery query)
    {
        var courses = from c in _dbContext.Courses
                      join ci in _dbContext.Set<CourseItem>() on c.Id equals ci.CourseId
                      join sc in _dbContext.Set<StudentCourse>() on c.Id equals sc.CourseId
                      where sc.StudentId == query.StudentId
                      select new SingleStudentCourse
                      {
                          Id = c.Id,
                          Title = c.Title,
                          Description = c.Description,
                          ExpiresAt = sc.ExpirationDate,
                          RenewalPrice = c.RenewalPrice,
                          Price = c.Price,
                          ImageUrl = c.ImageUrl,
                          Status = CourseStatus.Published,
                      };

        return new()
        {
            Items = await courses.ToListAsync()
        };
    }

    public async Task<GetCoursesResult> QueryAsync(GetCoursesQuery query)
    {
        var courses = from c in _dbContext.Set<Course>()
                      select new SingleCourse
                      {
                          Id = c.Id,
                          Title = c.Title,
                          Description = c.Description,
                          ImageUrl = c.ImageUrl,
                          Status = c.Status,
                          Price = c.Price,
                          RenewalPrice = c.RenewalPrice
                      };

        if (query.Status is not null)
        {
            courses = courses.Where(x => x.Status == query.Status);
        }

        return new()
        {
            Items = await courses.ToListAsync()
        };
    }

    public async Task<GetCourseResult> QueryAsync(GetCourseQuery query)
    {

        var courses = from c in _dbContext.Courses
                      where c.Id == query.Id
                      select new GetCourseResult
                      {
                          Id = c.Id,
                          Status = c.Status,
                          Title = c.Title,
                          Description = c.Description,
                          ImageUrl = c.ImageUrl,
                          Price = c.Price,
                          RenewalPrice = c.RenewalPrice,

                      };

        var course = await courses.FirstOrDefaultAsync();


        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        var lectures = await (from ci in _dbContext.Set<CourseItem>()
                              join l in _dbContext.Set<Lecture>() on ci.Id equals l.Id
                              where query.ItemStatus != null && ci.Status == query.ItemStatus && ci.CourseId == query.Id
                              select new SingleCourseItem
                              {
                                  Id = l.Id,
                                  ImageUrl = l.ImageUrl,
                                  Order = ci.Order,
                                  Title = l.Title,
                                  Type = CourseItemType.Lecture,
                              }).ToArrayAsync();
        var exams = await (from ci in _dbContext.Set<CourseItem>()
                           join e in _dbContext.Set<Exam>() on ci.Id equals e.Id
                           where query.ItemStatus != null && ci.Status == query.ItemStatus && ci.CourseId == query.Id
                           select new SingleCourseItem
                           {
                               Id = e.Id,
                               Order = ci.Order,
                               Title = e.Title,
                               Type = CourseItemType.Exam,
                           })
                                  .ToListAsync();


        course.Items = lectures.Union(exams).OrderBy(x => x.Order);

        return course!;
    }

    public async Task<GetStudentCourseResult> QueryAsync(GetStudentCourseQuery query)
    {
        var courses = from c in _dbContext.Courses
                      join sc in _dbContext.Set<StudentCourse>() on c.Id equals sc.CourseId
                      where c.Id == query.Id && sc.StudentId == query.StudentId
                      select new GetStudentCourseResult
                      {
                          Id = c.Id,
                          Status = c.Status,
                          ExpiresAt = sc.ExpirationDate,
                          Title = c.Title,
                          Description = c.Description,
                          ImageUrl = c.ImageUrl,
                          Price = c.Price,
                          RenewalPrice = c.RenewalPrice,
                      };

        var course = await courses.FirstOrDefaultAsync();

        if (course is null)
        {
            throw new ApiException(CoursesErrors.NotFound);
        }

        var lectures = await (from ci in _dbContext.Set<CourseItem>()
                              join l in _dbContext.Set<Lecture>() on ci.Id equals l.Id
                              join sl in _dbContext.Set<StudentLecture>() on l.Id equals sl.LectureId
                              where query.ItemStatus != null && ci.Status == query.ItemStatus
                              where ci.CourseId == query.Id
                              select new SingleStudentCourseItem
                              {
                                  Id = l.Id,
                                  ImageUrl = l.ImageUrl,
                                  ExpiresAt = sl.ExpirationDate,
                                  Order = ci.Order,
                                  Title = l.Title,
                                  Type = CourseItemType.Lecture,
                              }).ToArrayAsync();

        var exams = await (from ci in _dbContext.Set<CourseItem>()
                           join e in _dbContext.Set<Exam>() on ci.Id equals e.Id
                           join se in _dbContext.Set<StudentExam>() on e.Id equals se.ExamId
                           where query.ItemStatus != null && ci.Status == query.ItemStatus
                           where ci.CourseId == query.Id
                           select new SingleStudentCourseItem
                           {
                               Id = e.Id,
                               ExpiresAt = se.ExpirationDate,
                               Order = ci.Order,
                               Title = e.Title,
                               Type = CourseItemType.Exam,
                           })
                                  .ToArrayAsync();


        course.Items = lectures.Union(exams).OrderBy(x => x.Order);

        return course;
    }

    public async Task<GetLectureResult> QueryAsync(GetLectureQuery query)
    {

        var lectures = from course in _dbContext.Set<Course>()
                       join studentCourse in _dbContext.Set<StudentCourse>() on course.Id equals studentCourse.CourseId
                       join courseItem in _dbContext.Set<CourseItem>() on studentCourse.CourseId equals courseItem.CourseId
                       join lecture in _dbContext.Set<Lecture>() on courseItem.Id equals lecture.Id
                       join studentLecture in _dbContext.Set<StudentLecture>() on lecture.Id equals studentLecture.LectureId
                       where
                       (query.CourseStatus == null || course.Status == CourseStatus.Published) &&
                       (query.LectureStatus == null || courseItem.Status == CourseItemStatus.Published) &&
                       courseItem.Status == CourseItemStatus.Published &&
                       course.Id == query.CourseId &&
                       lecture.Id == query.LectureId
                       select new GetLectureResult
                       {
                           Id = lecture.Id,
                           Title = lecture.Title,
                           Description = lecture.Description,
                           ImageUrl = lecture.ImageUrl,
                           Status = courseItem.Status,
                           Price = lecture.Price,
                           RenewalPrice = lecture.RenewalPrice
                       };


        var result = await lectures.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        return result;
    }

    public async Task<GetStudentLectureResult> QueryAsync(GetStudentLectureQuery query)
    {
        var lectures = from course in _dbContext.Set<Course>()
                       join studentCourse in _dbContext.Set<StudentCourse>() on course.Id equals studentCourse.CourseId
                       join courseItem in _dbContext.Set<CourseItem>() on studentCourse.CourseId equals courseItem.CourseId
                       join lecture in _dbContext.Set<Lecture>() on courseItem.Id equals lecture.Id
                       join studentLecture in _dbContext.Set<StudentLecture>() on lecture.Id equals studentLecture.LectureId
                       where
                       (
                         (studentCourse.StudentId == query.StudentId && studentCourse.ExpirationDate > DateTime.UtcNow)
                       ||
                         (studentLecture.LectureId == query.LectureId && studentLecture.ExpirationDate > DateTime.UtcNow)
                       ) &&
                       course.Status == CourseStatus.Published &&
                       courseItem.Status == CourseItemStatus.Published &&
                       course.Id == query.CourseId &&
                       lecture.Id == query.LectureId
                       select new GetStudentLectureResult
                       {
                           Id = lecture.Id,
                           Title = lecture.Title,
                           Description = lecture.Description,
                           ExpiresAt = studentLecture.ExpirationDate,
                           ImageUrl = lecture.ImageUrl,
                           Price = lecture.Price,
                           RenewalPrice = lecture.RenewalPrice,
                           Status = courseItem.Status
                       };


        var result = await lectures.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        return result;
    }

    public async Task<GetStudentLessonResult> QueryAsync(GetStudentLessonQuery query)
    {
        var lessons = from course in _dbContext.Set<Course>()
                      join studentCourse in _dbContext.Set<StudentCourse>() on course.Id equals studentCourse.CourseId
                      join courseItem in _dbContext.Set<CourseItem>() on studentCourse.CourseId equals courseItem.CourseId
                      join studentLecture in _dbContext.Set<StudentLecture>() on courseItem.Id equals studentLecture.LectureId
                      join lectureItem in _dbContext.Set<LectureItem>() on studentLecture.LectureId equals lectureItem.LectureId
                      join lesson in _dbContext.Set<Lesson>() on lectureItem.Id equals lesson.Id
                      where
                      lesson.Id == query.LessonId &&
                      (
                        (studentCourse.StudentId == query.StudentId && studentCourse.ExpirationDate > DateTime.UtcNow)
                      ||
                        (studentLecture.LectureId == query.LectureId && studentLecture.ExpirationDate > DateTime.UtcNow)
                      ) &&
                      courseItem.Status == CourseItemStatus.Published &&
                      course.Status == CourseStatus.Published &&
                      course.Id == query.CourseId &&
                      lectureItem.Id == query.LectureId
                      select new GetStudentLessonResult
                      {
                          Id = lesson.Id,
                          Title = lesson.Title,
                          Description = lesson.Description,
                          VideoEmbed = lesson.VideoEmbed
                      };


        var result = await lessons.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        return result;
    }

    public async Task<GetLessonResult> QueryAsync(GetLessonQuery query)
    {
        var lessons = from course in _dbContext.Set<Course>()
                      join courseItem in _dbContext.Set<CourseItem>() on course.Id equals courseItem.CourseId
                      join lectureItem in _dbContext.Set<LectureItem>() on courseItem.Id equals lectureItem.LectureId
                      join lesson in _dbContext.Set<Lesson>() on lectureItem.Id equals lesson.Id
                      where
                      lesson.Id == query.LessonId &&
                      course.Id == query.CourseId &&
                      lectureItem.Id == query.LectureId
                      select new GetLessonResult
                      {
                          Id = lesson.Id,
                          Title = lesson.Title,
                          Description = lesson.Description,
                          VideoEmbed = lesson.VideoEmbed
                      };


        var result = await lessons.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        return result;
    }
}
