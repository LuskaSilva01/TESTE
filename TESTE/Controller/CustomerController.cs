using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TESTE.Data.Entities;
using TESTE.Data;

namespace TESTE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public CustomerController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _mongoDbService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _mongoDbService.AddCustomer(customer);
            return Ok(new
            {
                mensagem = "Cliente cadastrado com sucesso",
                dados = customer
            });
        }

            }
        } 