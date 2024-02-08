﻿using Microsoft.AspNetCore.Mvc;
using TSUS.Domain.Dtos;
using TSUS.Domain.Entities;
using TSUS.Infrastructure.UOW.Contract;

namespace TSUS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class DepartmentsController(IUnitOfWork uow, IConfiguration configuration) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = uow;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public async Task<ActionResult<List<Department>>> GetAll()
        => Ok(await _unitOfWork.DepartmentRepository.GetAllAsync());

    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync(DepartmentDto model)
    {
        var department = Department.Create(model);
        try
        {
            await _unitOfWork.DepartmentRepository.AddSingleAsync(department);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
        return Ok();
    }

}

