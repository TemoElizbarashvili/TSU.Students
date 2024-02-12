using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.Dtos;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IUnitOfWork uow) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = uow;

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
        Random rand = new Random();
        int verificationCode = rand.Next(100000, 999999);

        var result = await _unitOfWork.UserRepository.SendVerifyMail(verificationCode, email);
        if (result == false)
            return BadRequest("Something went wrong, please try again.");
        try
        {
            await _unitOfWork.VerifyCodeRepository.AddSingleAsync(
                new() { Email = email, VerifyCode = verificationCode, Attempt = 0 });
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
            if (!response)
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
}