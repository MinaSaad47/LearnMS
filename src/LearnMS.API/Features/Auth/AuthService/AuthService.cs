
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Auth.Contracts;
using LearnMS.API.Security.JwtBearer;
using LearnMS.API.Security.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearnMS.API.Features.Auth;

public sealed class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtBearerConfig _jwtConfig;


    public AuthService(AppDbContext dbContext, IPasswordHasher passwordHasher, IOptions<JwtBearerConfig> jwtOptions)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtConfig = jwtOptions.Value;
    }


    public async Task<RegisterResult> ExecuteAsync(RegisterStudentCommand command)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(x => x.Email == command.Email);

        if (account != null)
        {
            throw new ApiException(AuthErrors.EmailAlreadyExists);
        }

        var passwordHash = _passwordHasher.Hash(command.Password);

        var student = Student.Register(new Account
        {
            Email = command.Email,
            PasswordHash = passwordHash,
            ProviderType = ProviderType.Local,
        });

        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();

        return new RegisterResult(student.Id);
    }

    public Task<RegisterResult> ExecuteAsync(RegisterStudentExternalCommand command)
    {
        throw new ApiException(ServerErrors.NotImplemented);
    }

    public async Task<LoginResult> ExecuteAsync(LoginCommand command)
    {
        var account = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Email == command.Email);

        if (account is null || account.PasswordHash is null)
        {
            throw new ApiException(AuthErrors.InvalidCredentials);
        }

        if (!_passwordHasher.Verify(account.PasswordHash, command.Password))
        {
            throw new ApiException(AuthErrors.InvalidCredentials);
        }

        var token = GetToken(GetClaims(account.User));

        return new LoginResult(account.Id, token);
    }

    public Task<LoginResult> ExecuteAsync(LoginExternalCommand command)
    {
        throw new NotImplementedException();
    }

    private List<Claim> GetClaims(User user)
    {
        return new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
    }


    private string GetToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtConfig.Secret
            )
        );

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.TokenExpirationInMinutes),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}