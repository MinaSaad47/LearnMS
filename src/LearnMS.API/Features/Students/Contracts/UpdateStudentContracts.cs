using System.ComponentModel.DataAnnotations;
using LearnMS.API.Entities;

namespace LearnMS.API.Features.Students.Contracts;

public sealed record UpdateStudentRequest
{
    [MinLength(3)]
    public string? FullName { get; set; }
    [Length(11, 11)]
    public string? ParentPhoneNumber { get; set; }
    [Length(1, 8)]
    public string? StudentCode { get; set; }
    [Length(11, 11)]
    public string? PhoneNumber { get; set; }
    [MinLength(3)]
    public string? SchoolName { get; set; }
    public StudentLevel? Level { get; set; }
}

public sealed record UpdateStudentCommand
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? ParentPhoneNumber { get; set; }
    public string? StudentCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SchoolName { get; set; }
    public StudentLevel? Level { get; set; }
}