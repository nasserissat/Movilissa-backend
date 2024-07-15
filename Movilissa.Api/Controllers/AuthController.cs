using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IGenericService<User> _genericService;

    public AuthController(IGenericService<User> genericService)
    {
        _genericService = genericService;
    }
    
}