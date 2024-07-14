using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly GenericLogic<User> _genericLogic;

    public AuthController(GenericLogic<User> genericLogic)
    {
        _genericLogic = genericLogic;
    }
    
}