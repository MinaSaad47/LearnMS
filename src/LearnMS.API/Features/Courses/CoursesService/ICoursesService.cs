using LearnMS.API.Features.Courses.Contracts;

namespace LearnMS.API.Features.Courses;

public interface ICoursesService
{
    public Task ExecuteAsync(CreateCourseCommand command);

    public Task ExecuteAsync(UpdateCourseCommand command);
    public Task ExecuteAsync(PublishCourseCommand command);
    public Task ExecuteAsync(UnPublishCourseCommand command);

    public Task Execute(CreateLectureCommand command);
    public Task Execute(UpdateLectureCommand command);
    public Task Execute(PublishLectureCommand command);
    public Task Execute(UnPublishLectureCommand command);

    public Task Execute(CreateLessonCommand command);
    public Task Execute(UpdateLessonCommand command);

    public Task<GetCoursesResult> QueryAsync(GetCoursesQuery query);
    public Task<GetStudentCoursesResult> QueryAsync(GetStudentCoursesQuery query);

    public Task<GetCourseResult> QueryAsync(GetCourseQuery query);
    public Task<GetStudentCourseResult> QueryAsync(GetStudentCourseQuery query);


    public Task<GetLectureResult> QueryAsync(GetLectureQuery query);
    public Task<GetStudentLectureResult> QueryAsync(GetStudentLectureQuery query);

    public Task<GetLessonResult> QueryAsync(GetLessonQuery query);
    public Task<GetStudentLessonResult> QueryAsync(GetStudentLessonQuery query);

}