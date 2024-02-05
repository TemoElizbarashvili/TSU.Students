﻿using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TSUS.Domain.DataBase;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.Repositories.Contracts;

namespace TSUS.Infrastructure.Repositories;

public class UserRepository(TsusDbContext dbContext, IConfiguration configuration) : IRepository<User>
{
    private readonly TsusDbContext _dbContext = dbContext;
    private readonly IConfiguration _configuration = configuration;
    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddSingleAsync(User model)
    {
        await _dbContext.Users.AddAsync(model);
        await _dbContext.SaveChangesAsync();
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
        => await _dbContext.Users.FirstOrDefaultAsync(user => user.UserId == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

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
}