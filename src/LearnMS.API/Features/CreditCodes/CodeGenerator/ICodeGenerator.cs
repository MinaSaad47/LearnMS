using System.Security.Cryptography;

namespace LearnMS.API.Features.CreditCodes;

public interface ICodeGenerator
{
    Task<string> GenerateAsync(int length, Func<string, Task<bool>>? isUnique = null);
}
