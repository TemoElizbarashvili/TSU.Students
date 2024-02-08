using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TSUS.Domain.DataBase;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.Repositories.Contracts;
using TSUS.Infrastructure.ControlFlags;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TSUS.Infrastructure.Repositories;

public class UserRepository(TsusDbContext dbContext, IConfiguration configuration) : IRepository<User>
{
    private readonly TsusDbContext _context = dbContext;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<User>> GetAllAsync(BaseControlFlags flags = BaseControlFlags.Basic)
        => flags switch
        {
            BaseControlFlags.Basic => await _context.Users.ToListAsync(),
            BaseControlFlags.All => await _context.Users.Include(u => u.Department).ToListAsync(),
            _ => await _context.Users.ToListAsync()
        };

    public async Task AddSingleAsync(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public Task AddMultipleAsync(IEnumerable<User> models)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(User model)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

    public string CreateToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, Enum.GetName(user.Role.GetType(), user.Role)!),
            new Claim(ClaimTypes.Email, user.Email)
        ];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.MaxValue,
            signingCredentials: credentials);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }

    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public async Task<bool> SendVerifyMail(int verifyCode, string mail)
    {
        var apiKey = _configuration.GetSection("EmailAPI:APIKey").Value;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("projectsmaildev@gmail.com", "TSU Students Portal");
        var subject = "Verification code";
        var to = new EmailAddress($"{mail}", $"{mail}");
        var plainTextContent = $"Your Verification Code is {verifyCode}";
        var htmlContent = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Verification</title>
</head>
<body>
    <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f4f4f4;"">
        <h2 style=""color: #333;"">Email Verification</h2>
        <p style=""margin-bottom: 20px; line-height: 1.6; color: #666;"">Hello,</p>
        <p style=""margin-bottom: 20px; line-height: 1.6; color: #666;"">Your Verification Code is <strong style=""font-size: 1.5rem;"">{verifyCode}<strong></p>
        <p style=""margin-top: 20px; margin-bottom: 20px; line-height: 1.6; color: #666;"">If you did not request this verification, please ignore this email.</p>
    </div>
</body>
</html>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }

    public async Task Verify(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        if (user == null)
            throw new Exception("Provided email is incorrect");
        user.IsVerified = true;
        _context.Users.Update(user);
    }
}