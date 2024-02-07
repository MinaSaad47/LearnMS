using LearnMS.API.Common;
using LearnMS.API.Features.Students.Contracts;

namespace LearnMS.API.Features.Students;


public interface IStudentsService
{
    public Task ExecuteAsync(CreateStudentCommand command);

    public Task<PageList<SingleStudent>> QueryAsync(GetStudentsQuery query);
}

