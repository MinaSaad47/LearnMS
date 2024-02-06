using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Courses.Contracts;
using LearnMS.API.Features.Profile;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Features.Courses;

public sealed class CoursesService : ICoursesService
{
    private readonly AppDbContext _dbContext;

    public CoursesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(CreateLectureCommand command)
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

    public async Task ExecuteAsync(UpdateLectureCommand command)
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


        if (command.ExpirationDays is not null)
        {
            lecture.ExpirationDays = command.ExpirationDays;
        }


        _dbContext.Update(lecture);

        await _dbContext.SaveChangesAsync();

    }

    public async Task ExecuteAsync(PublishLectureCommand command)
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

        item.IsPublished = true;

        _dbContext.Update(course);

        _dbContext.Update(lecture);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(UnPublishLectureCommand command)
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

        item.IsPublished = false;

        _dbContext.Update(course);

        _dbContext.Update(lecture);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(CreateLessonCommand command)
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
            VideoSrc = command.VideoSrc

        };

        lecture.AddLesson(lesson, out var addedLesson);

        _dbContext.Lectures.Update(lecture);

        await _dbContext.Lessons.AddAsync(addedLesson);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(UpdateLessonCommand command)
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

        if (!string.IsNullOrWhiteSpace(command.VideoSrc))
        {
            lesson.VideoSrc = command.VideoSrc;
        }

        _dbContext.Lessons.Update(lesson);

        await _dbContext.SaveChangesAsync();
    }


    public async Task<CreateCourseResult> ExecuteAsync(CreateCourseCommand command)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
        };

        await _dbContext.Courses.AddAsync(course);
        await _dbContext.SaveChangesAsync();

        return new()
        {
            Id = course.Id
        };
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

        course.IsPublished = true;

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

        course.IsPublished = false;

        _dbContext.Courses.Update(course);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(BuyCourseCommand command)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == command.StudentId);

        if (student is null)
        {
            throw new ApiException(ProfileErrors.NoStudentFound);
        }

        var studentCourse = await _dbContext.Set<StudentCourse>().FirstOrDefaultAsync(x => x.CourseId == command.CourseId && x.StudentId == command.StudentId);

        if (studentCourse is not null && studentCourse.ExpirationDate > DateTime.UtcNow)
        {
            throw new ApiException(CoursesErrors.AlreadyPurchased);
        }

        var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == command.CourseId && x.IsPublished) ?? throw new ApiException(CoursesErrors.NotFound);


        if (studentCourse is not null)
        {
            if (student.Credit < course.RenewalPrice) throw new ApiException(ProfileErrors.InsufficientCredits);

            studentCourse.ExpirationDate = DateTime.UtcNow.AddDays(course.ExpirationDays!.Value);
            student.Credit -= course.RenewalPrice ?? 0;

            _dbContext.Update(student);
            _dbContext.Update(studentCourse);
            await _dbContext.SaveChangesAsync();
            return;
        }


        if (student.Credit < course.Price) throw new ApiException(ProfileErrors.InsufficientCredits);

        student.Credit -= course.Price ?? 0;

        _dbContext.Update(student);
        _dbContext.Add(new StudentCourse
        {
            CourseId = command.CourseId,
            StudentId = command.StudentId,
            ExpirationDate = DateTime.UtcNow.AddDays(course.ExpirationDays!.Value)
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(BuyLectureCommand command)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == command.StudentId);

        if (student is null)
        {
            throw new ApiException(ProfileErrors.NoStudentFound);
        }

        var studentLecture = await _dbContext.Set<StudentLecture>().FirstOrDefaultAsync(x => x.LectureId == command.LectureId && x.StudentId == command.StudentId);

        if (studentLecture is not null && studentLecture.ExpirationDate > DateTime.UtcNow)
        {
            throw new ApiException(LecturesErrors.AlreadyPurchased);
        }

        var courseLecture = await _dbContext.Set<CourseItem>()
            .FirstOrDefaultAsync(x => x.CourseId == command.CourseId && x.IsPublished && x.Id == command.LectureId) ?? throw new ApiException(LecturesErrors.NotFound);

        var lecture = await _dbContext.Set<Lecture>().FirstOrDefaultAsync(x => x.Id == command.LectureId) ?? throw new ApiException(LecturesErrors.NotFound);

        if (studentLecture is not null)
        {
            if (student.Credit < lecture.RenewalPrice) throw new ApiException(ProfileErrors.InsufficientCredits);

            studentLecture.ExpirationDate = DateTime.UtcNow.AddDays(lecture.ExpirationDays!.Value);
            student.Credit -= lecture.RenewalPrice ?? 0;

            _dbContext.Update(student);
            _dbContext.Update(studentLecture);
            await _dbContext.SaveChangesAsync();
            return;
        }


        if (student.Credit < lecture.Price) throw new ApiException(ProfileErrors.InsufficientCredits);

        studentLecture = new StudentLecture
        {
            LectureId = command.LectureId,
            StudentId = command.StudentId,
            ExpirationDate = DateTime.UtcNow.AddDays(lecture.ExpirationDays!.Value)
        };

        student.Credit -= lecture.Price ?? 0;

        _dbContext.Update(student);
        _dbContext.Add(studentLecture);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<GetStudentCoursesResult> QueryAsync(GetStudentCoursesQuery query)
    {
        var result = from courses in _dbContext.Courses
                     join studentCourse in _dbContext.Set<StudentCourse>() on new { CourseId = courses.Id, query.StudentId } equals new { studentCourse.CourseId, studentCourse.StudentId } into groupedStudentCourse
                     from gsc in groupedStudentCourse.DefaultIfEmpty()
                     where courses.IsPublished
                     select new SingleStudentCourse
                     {
                         Id = courses.Id,
                         Title = courses.Title,
                         Description = courses.Description,
                         ExpiresAt = gsc != null ? gsc.ExpirationDate : null,
                         Enrollment = gsc != null ? (gsc.ExpirationDate > DateTime.UtcNow ? "Active" : "Expired") : "NotEnrolled",
                         RenewalPrice = courses.RenewalPrice,
                         Price = courses.Price,
                         ImageUrl = courses.ImageUrl,
                     };

        return new()
        {
            Items = await result.ToListAsync()
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
                          IsPublished = c.IsPublished,
                          Price = c.Price,
                          RenewalPrice = c.RenewalPrice
                      };

        if (query.IsPublished is not null)
        {
            courses = courses.Where(x => x.IsPublished);
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
                          ExpirationDays = c.ExpirationDays,
                          Id = c.Id,
                          IsPublished = c.IsPublished,
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
                              where (query.IsCourseItemPublished == null || ci.IsPublished == query.IsCourseItemPublished) && ci.CourseId == query.Id
                              select new SingleCourseItem
                              {
                                  Id = l.Id,
                                  ImageUrl = l.ImageUrl,
                                  Order = ci.Order,
                                  Title = l.Title,
                                  Type = "Lecture",
                              }).ToArrayAsync();
        var exams = await (from ci in _dbContext.Set<CourseItem>()
                           join e in _dbContext.Set<Exam>() on ci.Id equals e.Id
                           where (query.IsCourseItemPublished == null || ci.IsPublished == query.IsCourseItemPublished) && ci.CourseId == query.Id
                           select new SingleCourseItem
                           {
                               Id = e.Id,
                               Order = ci.Order,
                               Title = e.Title,
                               Type = "Exam",
                           })
                                  .ToListAsync();


        course.Items = lectures.Union(exams).OrderBy(x => x.Order);

        return course!;
    }

    public async Task<GetStudentCourseResult> QueryAsync(GetStudentCourseQuery query)
    {
        var courses = from c in _dbContext.Courses
                      join sc in _dbContext.Set<StudentCourse>() on new { CourseId = c.Id, StudentId = query.StudentId } equals new { CourseId = sc.CourseId, StudentId = sc.StudentId } into groupedCourseItems
                      from gci in groupedCourseItems.DefaultIfEmpty()
                      where c.IsPublished && c.Id == query.Id
                      select new GetStudentCourseResult
                      {
                          ExpirationDays = c.ExpirationDays,
                          Id = c.Id,
                          ExpiresAt = gci != null ? gci.ExpirationDate : null,
                          Enrollment = gci != null ? (gci.ExpirationDate > DateTime.UtcNow ? "Active" : "Expired") : "NotEnrolled",
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
                              join sl in _dbContext.Set<StudentLecture>() on l.Id equals sl.LectureId into groupedStudentLectures
                              from gsl in groupedStudentLectures.DefaultIfEmpty()
                              where ci.CourseId == query.Id && (query.IsCourseItemPublished == null || ci.IsPublished == query.IsCourseItemPublished)
                              select new SingleStudentCourseItem
                              {
                                  Id = l.Id,
                                  Price = l.Price,
                                  RenewalPrice = l.RenewalPrice,
                                  ImageUrl = l.ImageUrl,
                                  ExpiresAt = gsl != null ? gsl.ExpirationDate : null,
                                  Enrollment = gsl != null ? (gsl.ExpirationDate > DateTime.UtcNow ? "Active" : "Expired") : "NotEnrolled",
                                  Order = ci.Order,
                                  Title = l.Title,
                                  Type = "Lecture",
                              }).ToArrayAsync();

        var exams = await (from ci in _dbContext.Set<CourseItem>()
                           join e in _dbContext.Set<Exam>() on ci.Id equals e.Id
                           join se in _dbContext.Set<StudentExam>() on e.Id equals se.ExamId into groupedStudentExams
                           from gse in groupedStudentExams.DefaultIfEmpty()
                           where ci.CourseId == query.Id && (query.IsCourseItemPublished == null || ci.IsPublished == query.IsCourseItemPublished)
                           select new SingleStudentCourseItem
                           {
                               Id = e.Id,
                               Order = ci.Order,
                               ExpiresAt = gse != null ? gse.ExpirationDate : null,
                               Enrollment = gse != null ? (gse.ExpirationDate > DateTime.UtcNow ? "Active" : "Expired") : "NotEnrolled",
                               Title = e.Title,
                               Type = "Exam",
                           })
                                  .ToArrayAsync();


        course.Items = lectures.Union(exams).OrderBy(x => x.Order);

        return course;
    }

    public async Task<GetLectureResult> QueryAsync(GetLectureQuery query)
    {

        var lectures = from course in _dbContext.Set<Course>()
                       join courseItem in _dbContext.Set<CourseItem>() on course.Id equals courseItem.CourseId
                       join lecture in _dbContext.Set<Lecture>() on courseItem.Id equals lecture.Id
                       where
                       (query.IsCoursePublished != null ? course.IsPublished == query.IsCoursePublished : true) &&
                       (query.IsPublished != null ? courseItem.IsPublished == query.IsPublished : true) &&
                       course.Id == query.CourseId &&
                       lecture.Id == query.LectureId
                       select new GetLectureResult
                       {
                           Id = lecture.Id,
                           ExpirationDays = lecture.ExpirationDays,
                           Title = lecture.Title,
                           Description = lecture.Description,
                           ImageUrl = lecture.ImageUrl,
                           IsPublished = courseItem.IsPublished,
                           Price = lecture.Price,
                           RenewalPrice = lecture.RenewalPrice,
                       };


        var result = await lectures.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lessons = await (from li in _dbContext.Set<LectureItem>()
                             join l in _dbContext.Set<Lesson>() on li.Id equals l.Id
                             where li.LectureId == query.LectureId
                             select new SingleLectureItem
                             {
                                 Id = l.Id,
                                 Title = l.Title,
                                 Type = "Lesson",
                                 Description = l.Description,
                             }).ToListAsync();

        result.Items = lessons;

        return result;
    }

    public async Task<GetStudentLectureResult> QueryAsync(GetStudentLectureQuery query)
    {
        var lectures = from course in _dbContext.Set<Course>()
                       join courseItem in _dbContext.Set<CourseItem>() on course.Id equals courseItem.CourseId
                       join lecture in _dbContext.Set<Lecture>() on courseItem.Id equals lecture.Id
                       join studentLecture in _dbContext.Set<StudentLecture>() on lecture.Id equals studentLecture.LectureId into groupedStudentLectures
                       from gsl in groupedStudentLectures.DefaultIfEmpty()
                       where
                       course.IsPublished &&
                       courseItem.IsPublished &&
                       course.Id == query.CourseId &&
                       lecture.Id == query.LectureId
                       select new GetStudentLectureResult
                       {
                           Id = lecture.Id,
                           Title = lecture.Title,
                           Description = lecture.Description,
                           ExpiresAt = gsl != null ? gsl.ExpirationDate : null,
                           Enrollment = gsl != null ? (gsl.ExpirationDate > DateTime.UtcNow ? "Active" : "Expired") : "NotEnrolled",
                           ImageUrl = lecture.ImageUrl,
                           Price = lecture.Price,
                           RenewalPrice = lecture.RenewalPrice,
                       };


        var result = await lectures.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LecturesErrors.NotFound);
        }

        var lessons = await (from li in _dbContext.Set<LectureItem>()
                             join l in _dbContext.Set<Lesson>() on li.Id equals l.Id
                             where li.LectureId == query.LectureId
                             select new SingleLectureItem
                             {
                                 Id = l.Id,
                                 Title = l.Title,
                                 Type = "Lesson",
                                 Description = l.Description,
                             }).ToListAsync();

        result.Items = lessons;

        return result;
    }

    public async Task<GetStudentLessonResult> QueryAsync(GetStudentLessonQuery query)
    {
        var courseResult = from course in _dbContext.Set<Course>()
                           join studentCourse in _dbContext.Set<StudentCourse>() on course.Id equals studentCourse.CourseId
                           where
                           studentCourse.StudentId == query.StudentId &&
                           course.Id == query.CourseId &&
                           studentCourse.ExpirationDate > DateTime.UtcNow
                           select new
                           {
                               courseId = course.Id
                           };

        var lectureResult = from lecture in _dbContext.Set<Lecture>()
                            join studentLecture in _dbContext.Set<StudentLecture>() on lecture.Id equals studentLecture.LectureId
                            where
                            studentLecture.StudentId == query.StudentId &&
                            studentLecture.ExpirationDate > DateTime.UtcNow
                            select new
                            {
                                lectureId = lecture.Id
                            };



        if (courseResult.Count() == 0 && lectureResult.Count() == 0)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        var result = from courseItem in _dbContext.Set<CourseItem>()
                     join lectureItem in _dbContext.Set<LectureItem>() on courseItem.Id equals lectureItem.LectureId
                     join lesson in _dbContext.Set<Lesson>() on lectureItem.Id equals lesson.Id
                     where lesson.Id == query.LessonId
                     select new GetStudentLessonResult
                     {
                         Id = lesson.Id,
                         Title = lesson.Title,
                         Description = lesson.Description,
                         VideoSrc = lesson.VideoSrc
                     };


        return await result.FirstOrDefaultAsync() ?? throw new ApiException(LessonsErrors.NotFound);
    }

    public async Task<GetLessonResult> QueryAsync(GetLessonQuery query)
    {
        var lessons = from course in _dbContext.Set<Course>()
                      join courseItem in _dbContext.Set<CourseItem>() on course.Id equals courseItem.CourseId
                      join lectureItem in _dbContext.Set<LectureItem>() on courseItem.Id equals lectureItem.LectureId
                      join lesson in _dbContext.Set<Lesson>() on lectureItem.Id equals lesson.Id
                      where
                      course.Id == query.CourseId &&
                      courseItem.Id == query.LectureId &&
                      lectureItem.Id == query.LessonId
                      select new GetLessonResult
                      {
                          Id = lesson.Id,
                          Title = lesson.Title,
                          Description = lesson.Description,
                          VideoSrc = lesson.VideoSrc
                      };


        var result = await lessons.FirstOrDefaultAsync();

        if (result is null)
        {
            throw new ApiException(LessonsErrors.NotFound);
        }

        return result;
    }
}
