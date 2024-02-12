using LearnMS.API.Features.Students.Contracts;

namespace LearnMS.API.Features.Students;


public interface IStudentsService
{
    public Task ExecuteAsync(CreateStudentCommand command);
    public Task ExecuteAsync(AddStudentCreditCommand command);
    public Task ExecuteAsync(DeleteStudentCommand command);
    public Task ExecuteAsync(UpdateStudentCommand command);

    public Task<PageList<SingleStudent>> QueryAsync(GetStudentsQuery query);
    public Task<GetStudentResult> QueryAsync(GetStudentQuery query);
}

