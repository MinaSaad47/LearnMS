namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateCourseRequest(
    string Name
);

public sealed record CreateCourseCommand(
    string Title
);


public sealed record CreateCourseResult
{
    public Guid Id;
};

public sealed record CreateCourseResponse
{
    public Guid Id;
};