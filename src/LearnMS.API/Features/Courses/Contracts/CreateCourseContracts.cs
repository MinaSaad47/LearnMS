namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateCourseRequest(
    string Name
);

public sealed record CreateCourseCommand(
    string Title
);

