namespace LearnMS.API.Features.Administration.Contracts;


public sealed record CreateTeacherCommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}