namespace LearnMS.API.Features.CreditCodes;

public sealed class CodeGenerator : ICodeGenerator
{

    public async Task<string> GenerateAsync(int length, Func<string, Task<bool>>? isUnique)
    {
        string code;

        if (isUnique is null)
        {
            return RandomString(length);
        }

        while (!await isUnique(code = RandomString(length))) ;
        return code;
    }

    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}
