using LearnMS.API.Features.Courses.Contracts;

namespace LearnMS.API.Features.Courses;

public interface ICoursesService
{
    // commands
    public Task<CreateCourseResult> ExecuteAsync(CreateCourseCommand command);
    public Task ExecuteAsync(UpdateCourseCommand command);
    public Task ExecuteAsync(PublishCourseCommand command);
    public Task ExecuteAsync(UnPublishCourseCommand command);
    public Task ExecuteAsync(CreateLectureCommand command);
    public Task ExecuteAsync(UpdateLectureCommand command);
    public Task ExecuteAsync(DeleteCourseCommand command);
    public Task ExecuteAsync(DeleteLectureCommand command);
    public Task ExecuteAsync(DeleteLessonCommand command);
    public Task ExecuteAsync(PublishLectureCommand command);
    public Task ExecuteAsync(UnPublishLectureCommand command);
    public Task ExecuteAsync(CreateLessonCommand command);
    public Task ExecuteAsync(UpdateLessonCommand command);
    public Task ExecuteAsync(UploadLessonVideoCommand command);
    public Task ExecuteAsync(BuyCourseCommand command);
    public Task ExecuteAsync(BuyLectureCommand command);
    public Task ExecuteAsync(RenewLessonExpirationCommand command);
    public Task ExecuteAsync(StartLessonCommand command);
    public Task<ValidateLessonVideoStatusResult> ExecuteAsync(ValidateLessonVideoStatusCommand command);


    // queries

    public Task<GetCoursesResult> QueryAsync(GetCoursesQuery query);
    public Task<GetStudentCoursesResult> QueryAsync(GetStudentCoursesQuery query);
    public Task<GetCourseResult> QueryAsync(GetCourseQuery query);
    public Task<GetStudentCourseResult> QueryAsync(GetStudentCourseQuery query);
    public Task<GetLectureResult> QueryAsync(GetLectureQuery query);
    public Task<GetStudentLectureResult> QueryAsync(GetStudentLectureQuery query);
    public Task<GetLessonResult> QueryAsync(GetLessonQuery query);
    public Task<GetStudentLessonResult> QueryAsync(GetStudentLessonQuery query);
}