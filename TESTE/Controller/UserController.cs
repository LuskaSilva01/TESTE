using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TESTE.Data.Entities;
using TESTE.Data;
using TESTE.Services;

namespace TESTE.Controller;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_userService.Get());

    [HttpGet("{id}")]
    public IActionResult GetById(string id) => Ok(_userService.Get(id));

    [HttpPost]
    public IActionResult Create(User user) => Ok(_userService.Create(user));

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _userService.Delete(id);
        return NoContent();
    }
    }