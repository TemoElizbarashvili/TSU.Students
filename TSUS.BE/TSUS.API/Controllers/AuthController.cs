using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.Dtos;
using TSUS.Infrastructure.Repositories;
using TSUS.Infrastructure.Services.contracts;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IUnitOfWork uow, IMailService mailService) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = uow;
    private readonly IMailService _mailService = mailService;

    [HttpPost("Registration")]
    public async Task<IActionResult> RegisterUserAsync(RegistrationDto model)
    {
        model.Password = Infrastructure.Repositories.UserRepository.HashPassword(model.Password);
        var user = Domain.Entities.User.Create(model);
        try
        {
            await _unitOfWork.UserRepository?.AddSingleAsync(user)!;
        }
        catch
        {
            return BadRequest();
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginDto model)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(model.Email);
        if (user == null)
            return BadRequest(error: "Email or password is incorrect!");
        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            return BadRequest(error: "Email or password is incorrect!");
        var token = _unitOfWork.UserRepository.CreateToken(user);

        return Ok(token);
    }

    [HttpPost("Email")]
    public async Task<IActionResult> SendEmail(string email)
    {
        var code = GetVerificationCode();
        var result = await _mailService.SendVerifyMail(code, email);
        if (result == false)
            return BadRequest("Something went wrong, please try again.");
        try
        {
            await _unitOfWork.VerifyCodeRepository.AddSingleAsync(
                new() { Email = email, VerifyCode = code, Attempt = 0 });
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
        return Ok();
    }

    [HttpPut("Verify")]
    public async Task<IActionResult> Verify(string email, int code)
    {
        try
        {
            if (await _unitOfWork.UserRepository.GetByEmailAsync(email) == null)
                return BadRequest("User with this email does not exists!");
            var response = await _unitOfWork.VerifyCodeRepository.VerifyAsync(email, code);
            if (response == false)
            {
                await _unitOfWork.VerifyCodeRepository.IncreaseAttemptAsync(email);
                await _unitOfWork.SaveChangesAsync();
                return BadRequest("Code is incorrect!");
            }

            await _unitOfWork.UserRepository.Verify(email);
            await _unitOfWork.VerifyCodeRepository.Remove(email, code);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("RequestPasswordReset")]
    public async Task<ActionResult<int>> RequestPasswordReset(string email)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
        if (user is null)
            return BadRequest($"User with email ({email}) does not exist!");
        try
        {
            var code = GetVerificationCode();
            var result = await _mailService.SendVerifyMail(code, email);
            if (result == false)
                return BadRequest("Something went wrong while sending mail, Please try again!");
            await _unitOfWork.VerifyCodeRepository.AddSingleAsync(new()
            { Attempt = 0, Email = email, VerifyCode = code });
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return user.UserId;
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword(int userId, string password, int code)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
            return NotFound("Given User does not exist");
        try
        {
            var result = await _unitOfWork.VerifyCodeRepository.VerifyAsync(user.Email, code);
            if (result == false)
            {
                await _unitOfWork.VerifyCodeRepository.IncreaseAttemptAsync(user.Email);
                await _unitOfWork.SaveChangesAsync();
                return BadRequest("Email or Code is Incorrect!");
            }
            await _unitOfWork.VerifyCodeRepository.Remove(user.Email, code);
            user.Password = UserRepository.HashPassword(password);
            _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    private int GetVerificationCode()
    {
        Random rand = new Random();
        return rand.Next(100000, 999999);
    }
}