using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using TSUS.Infrastructure.Services.contracts;

namespace TSUS.Infrastructure.Services;

public class MailService(IConfiguration configuration) : IMailService
{
    private readonly IConfiguration _configuration = configuration;
    public async Task<bool> SendVerifyMail(int verifyCode, string mail)
    {
        var apiKey = _configuration.GetSection("EmailAPI:APIKey").Value;
        var client = new SendGridClient(apiKey);
        var msg = CreateMessage(verifyCode, mail);
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }


    private SendGridMessage CreateMessage(int verifyCode, string mail)
    {
        var from = new EmailAddress(_configuration.GetSection("EmailAPI:Email").Value, "TSU Students Portal");
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
</html>"; ;
        return MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    }
}