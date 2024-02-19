
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using LearnMS.API.Common;
using LearnMS.API.Common.EmailService;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Auth.Contracts;
using LearnMS.API.Features.CreditCodes;
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
    private readonly ICodeGenerator _codeGenerator;
    private readonly IEmailService _emailService;
    private readonly JwtBearerConfig _jwtConfig;


    public AuthService(AppDbContext dbContext, IPasswordHasher passwordHasher, IOptions<JwtBearerConfig> jwtOptions, ICodeGenerator codeGenerator, IEmailService emailService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtConfig = jwtOptions.Value;
        _codeGenerator = codeGenerator;
        _emailService = emailService;
    }


    public async Task<RegisterResult> ExecuteAsync(RegisterStudentCommand command)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(x => x.Email.ToLower() == command.Email.ToLower());

        if (account != null)
        {
            throw new ApiException(AuthErrors.EmailAlreadyExists);
        }

        var passwordHash = _passwordHasher.Hash(command.Password.Trim());

        var token = await _codeGenerator.GenerateAsync(20, async (token) =>
        {
            return await _dbContext.Accounts.CountAsync(a => a.VerificationToken == token || a.PasswordResetToken == token) == 0;
        });

        var student = Student.Register(new Account
        {
            Email = command.Email.ToLower(),
            PasswordHash = passwordHash,
            VerificationToken = token,
            ProviderType = ProviderType.Local,
        }, command.FullName, command.PhoneNumber, command.ParentPhoneNumber,command.StudentCode, command.School, command.Level);


        await _dbContext.Students.AddAsync(student);

        SendEmailRequest SendEmailRequest = new()
        {
            To = command.Email,
            Subject = "Email Verification",
            Body = $"<a href='{_jwtConfig.BaseUrl}/api/auth/verify-email?token={token}'>Click here to verify your email</a>"
        };

        await _emailService.SendAsync(SendEmailRequest);

        await _dbContext.SaveChangesAsync();

        return new RegisterResult(student.Id);
    }

    public Task<RegisterResult> ExecuteAsync(RegisterStudentExternalCommand command)
    {
        throw new ApiException(ServerErrors.NotImplemented);
    }

    public async Task<LoginResult> ExecuteAsync(LoginCommand command)
    {
        var account = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Email.ToLower() == command.Email.ToLower());

        if (account is null || account.PasswordHash is null)
        {
            throw new ApiException(AuthErrors.InvalidCredentials);
        }

        if (!_passwordHasher.Verify(account.PasswordHash, command.Password.Trim()))
        {
            throw new ApiException(AuthErrors.InvalidCredentials);
        }

        if (account.VerifiedAt is null)
        {
            throw new ApiException(AuthErrors.NotVerifiedEmail);
        }

        var token = GetToken(GetClaims(account.User));

        return new LoginResult(account.Id, token);
    }

    public Task<LoginResult> ExecuteAsync(LoginExternalCommand command)
    {
        throw new ApiException(ServerErrors.NotImplemented);
    }

    public async Task ExecuteAsync(VerifyEmailCommand command)
    {
        var account = _dbContext.Accounts.Where(x => x.VerificationToken == command.Token).FirstOrDefault();

        if (account is null)
        {
            throw new ApiException(AuthErrors.InvalidToken);
        }

        account.VerifiedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(ForgotPasswordCommand command)
    {
        var account = await _dbContext.Accounts
           .FirstOrDefaultAsync(x => x.Email.ToLower() == command.Email.ToLower());

        if (account is null)
        {
            return;
        }

        account.PasswordResetToken = await _codeGenerator.GenerateAsync(20, async (token) =>
        {
            return await _dbContext.Accounts.CountAsync(a => a.PasswordResetToken == token || a.VerificationToken == token) == 0;
        });

        account.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddHours(1);

        await _emailService.SendAsync(
            new SendEmailRequest
            {
                To = command.Email,
                Body = $"<a href='{_jwtConfig.BaseUrl}/auth/reset-password?token={account.PasswordResetToken}'>Click here to reset your password</a>",
                Subject = "Password Reset"
            }
        );

        _dbContext.Update(account);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(ResetPasswordCommand command)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.PasswordResetToken == command.Token && x.PasswordResetTokenExpiresAt > DateTime.UtcNow) ?? throw new ApiException(AuthErrors.InvalidToken);

        account.PasswordHash = _passwordHasher.Hash(command.Password);

        account.PasswordResetToken = null;
        account.PasswordResetTokenExpiresAt = null;

        _dbContext.Update(account);

        await _dbContext.SaveChangesAsync();
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