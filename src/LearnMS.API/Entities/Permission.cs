using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum Permission
{
    ManageCourses,
    ManageStudents,
    ManageCreditCodes,
    GenerateCreditCodes,
    ManageAssistants,
    ManageFiles,
}