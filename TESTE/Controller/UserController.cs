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
    public IActionResult GetAll()
    {
        var users = _userService.Get();
        if (users == null || users.Count == 0)
            return NotFound("Nenhum usuário ativo encontrado.");
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var user = _userService.Get(id);
        if (user == null)
            return NotFound($"Usuário com Id {id} não encontrado.");
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            return BadRequest("Usuário inválido.");

        // Verificar se já existe um usuário com o mesmo email
        var existingUser = _userService.GetByEmail(user.Email);
        if (existingUser != null)
            return BadRequest("Já existe um usuário cadastrado com este email.");

        var createdUser = _userService.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }
    [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] User user)
        {
            try
            {
                _userService.Update(id, user);
                return Ok("Usuário atualizado com sucesso.");
            }
            catch
            {
                return NotFound("Usuário não encontrado.");
            }
        }

        [HttpPatch("{id}/status")]
        public IActionResult SetActiveStatus(string id, [FromQuery] bool isActive)
        {
            // Busca ativo ou inativo
            var user = _userService.GetIncludeInactive(id);
            if (user == null)
                return NotFound($"Usuário com Id {id} não encontrado.");

            _userService.SetActiveStatus(id, isActive);
            return Ok($"Status do usuário atualizado para {(isActive ? "ativo" : "inativo")}.");
        }
    }