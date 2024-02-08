using Microsoft.AspNetCore.Mvc;
using TSUS.Infrastructure.Repositories;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(UserRepository userRepository) : ControllerBase
{
    private UserRepository _userRepository = userRepository;
}

