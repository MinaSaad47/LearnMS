using LearnMS.API.Features.Administration.Contracts;
using LearnMS.API.Features.Assistants.Contracts;

namespace LearnMS.API.Features.Administration;

public interface IAdministrationService
{

    public Task ExecuteAsync(UpdateAssistantCommand command);
    public Task ExecuteAsync(CreateAssistantCommand command);
    public Task ExecuteAsync(DeleteAssistantCommand command);
    public Task ExecuteAsync(CreateTeacherCommand command);
    public Task ExecuteAsync(ClaimAssistantIncomesCommand command);


    public Task<GetAssistantsResult> QueryAsync(GetAssistantsQuery query);
    public Task<GetAssistantResult> QueryAsync(GetAssistantQuery query);
    public Task<GetAssistantIncomeResult> QueryAsync(GetAssistantIncomeQuery query);
}

