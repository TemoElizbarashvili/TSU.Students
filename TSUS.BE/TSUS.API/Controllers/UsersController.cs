using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.ReadModels;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController() : ControllerBase
{

    [HttpGet("User-info")]
    [Authorize]
    public async Task<ActionResult<UserInfoRm>> GetInfo()
    {
        var userInfo = new UserInfoRm();
        var claims = HttpContext.User.Claims;
        foreach (var claim in claims)
        {
            var claimType = claim.Type.Split('/').LastOrDefault();
            switch (claimType)
            {
                case "name":
                    userInfo.UserName = claim.Value; 
                    break;
                case "emailaddress":
                    userInfo.Mail = claim.Value;
                    break;
                case "nameidentifier":
                    userInfo.Id = int.Parse(claim.Value);
                    break;
                case "authenticationinstant":
                    userInfo.IsVerified = bool.Parse(claim.Value);
                    break;
            }
        }
        return await Task.FromResult(userInfo);
    } 
}

