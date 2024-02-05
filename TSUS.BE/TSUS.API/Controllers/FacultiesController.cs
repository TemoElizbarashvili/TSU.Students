using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.Dtos;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class FacultiesController(IUnitOfWork uow) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = uow;

    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync(FacultyDto model)
    {
        var faculty = Faculty.Create(model);
        try
        {
            await _unitOfWork.FacultyRepository.AddSingleAsync(faculty);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return BadRequest("model can not be added to database. please try again");
        }
        return Ok();
    }
}
