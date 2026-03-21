using System.Security.Claims;
using BMS.Application.DTOs;
using BMS.Application.Interfaces;
using BMS.Web.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS.UnitTests.Controllers;

public class BuildingsControllerFactory {

    public static BuildingsController Create(
        IBuildingService service,
        IValidator<SaveBuildingDTO> validator,
        long? userId = null)
    {
        var controller = new BuildingsController(service, validator);
        var claims = userId.HasValue
            ? new[] { new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()) }
            : Array.Empty<Claim>();
        
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims))
            }
        };
        return controller;
    }

}