using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.Wipro.Domains;
using Locadora.Wipro.Interfaces;
using Locadora.Wipro.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Wipro.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteRepository ClienteRepository { get; set; }

        public ClienteController()
        {
            ClienteRepository = new ClienteRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(ClienteRepository.GetClientes());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{idCliente}")]
        public IActionResult GetClienteChave(int idCliente)
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(ClienteRepository.GetClienteById(idCliente));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            try
            {
                ClienteRepository.Post(cliente);
                return Ok("Cliente cadastrado!");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}