using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TESTE.Data.Entities;
using TESTE.Services;
using TESTE.Data;

namespace TESTE.Controller;

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
             _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
            return Ok(new
            {
                mensagem = "Cliente cadastrado com sucesso",
                dados = customer
            });
        }

    }
