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
    public class LocacaoController : ControllerBase
    {
        private ILocacaoRepository LocacaoRepository { get; set; }

        public LocacaoController()
        {
            LocacaoRepository = new LocacaoRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(LocacaoRepository.GetLocacoes());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{idLocacao}")]
        public IActionResult GetClienteChave(int idLocacao)
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(LocacaoRepository.GetLocacaoById(idLocacao));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        public IActionResult Post(Locacao locacao)
        {
            try
            {             
                string mRetorno = LocacaoRepository.Post(locacao);
                return Ok(mRetorno);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{idLocacao}")]
        public IActionResult Put(int idLocacao)
        {
            try
            {
                string mRetorno = LocacaoRepository.PutRealizarEntrega(idLocacao);
                return Ok(mRetorno);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}