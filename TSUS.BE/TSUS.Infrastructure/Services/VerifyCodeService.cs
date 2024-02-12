using Microsoft.EntityFrameworkCore;
using TSUS.Domain.DataBase;
using TSUS.Domain.Entities;

namespace TSUS.Infrastructure.Services;

public class VerifyCodeService(TsusDbContext context)
{
    private readonly TsusDbContext _context = context;

    public async Task AddSingleAsync(VerifyCodes model)
        => await _context.VerifyCodes.AddAsync(model);

    public async Task<bool> VerifyAsync(string mail, int code)
        => await _context.VerifyCodes.FirstOrDefaultAsync(c => c.Email.Equals(mail) && c.VerifyCode == code && c.Attempt <= 3) != null;

    public async Task Remove(string email, int code)
    {
        var verifyCode = await _context.VerifyCodes.FirstOrDefaultAsync(v => v.VerifyCode == code && v.Email == email);
        if (verifyCode != null)
            _context.VerifyCodes.Remove(verifyCode);
        else
            throw new Exception("Code on that email can not be found!");
    }

    public async Task IncreaseAttemptAsync(string email)
    {
        var verifyCode = await _context.VerifyCodes.FirstOrDefaultAsync(v => v.Email.Equals(email));
        if (verifyCode != null)
        {
            verifyCode.Attempt++;
            _context.VerifyCodes.Update(verifyCode);
        }
        else
            throw new Exception("SomethingWentWrong");
    }
}

