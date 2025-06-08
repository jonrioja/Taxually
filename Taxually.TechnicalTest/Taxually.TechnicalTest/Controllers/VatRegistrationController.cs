﻿using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController : ControllerBase
{
    private readonly IVatRegistrationService _vatRegistrationService;

    public VatRegistrationController(IVatRegistrationService vatRegistrationService)
    {
     _vatRegistrationService = vatRegistrationService;   
    }

    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
    {
        await _vatRegistrationService.VatRegisterAsync(request);
        return Ok();
    }
}
