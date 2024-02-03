namespace LearnMS.API.Security.Storage;

public sealed class StorageConfig
{
    public const string Section = "Storage";

    public required string PdfsPath { get; init; }
    public required string ImagesPath { get; init; }
}