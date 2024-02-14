namespace TSUS.Infrastructure.Services.contracts;

public interface IMailService
{
    public Task<bool> SendVerifyMail(int verifyCode, string mail);
}

