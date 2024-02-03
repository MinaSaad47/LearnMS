using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LearnMS.API.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Permission
{
    ManageCourses,
}