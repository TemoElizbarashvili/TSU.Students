using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TSUS.Domain.DataBase;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.Repositories.Contracts;
using TSUS.Infrastructure.ControlFlags;
using TSUS.Domain.Dtos;
using TSUS.Infrastructure.Services.contracts;

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

    public void UpdateAsync(User model)
    {
        try
        {
            _context.Users.Update(model);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<bool> DeleteAsync(User model)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

    public Task<PagedListDto<User>> PagedListAsync(int limit, int lastEntityId)
    {
        throw new NotImplementedException();
    }

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

    public static string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public async Task Verify(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        if (user == null)
            throw new Exception("Provided email is incorrect");
        user.IsVerified = true;
        _context.Users.Update(user);
    }

}