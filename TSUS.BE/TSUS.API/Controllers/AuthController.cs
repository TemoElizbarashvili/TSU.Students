using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.Dtos;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IUnitOfWork uow) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = uow;

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUserAsync(RegistrationDto model)
        {
            model.Password = _unitOfWork.UserRepository.HashPassword(model.Password);
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
    }
}
