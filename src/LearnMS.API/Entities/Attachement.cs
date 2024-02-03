namespace LearnMS.API.Entities;


public sealed class Attachment
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}