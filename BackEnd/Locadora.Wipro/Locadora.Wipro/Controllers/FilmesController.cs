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
    public class FilmesController : ControllerBase
    {
        private IFilmeRepository FilmeRepository { get; set; }

        public FilmesController()
        {
            FilmeRepository = new FilmeRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(FilmeRepository.GetFilmes());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{idFilme}")]
        public IActionResult GetClienteChave(int idFilme)
        {
            try
            {
                //Retornando o resultado (Lista de todos os clientes) 
                return Ok(FilmeRepository.GetFilmeById(idFilme));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        public IActionResult Post(Filme filme)
        {
            try
            {
                FilmeRepository.Post(filme);
                return Ok("Filme cadastrado!");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{idFilme}")]
        public IActionResult Put (Filme filme)
        {
            try
            {
                FilmeRepository.Put(filme);
                return Ok("Filme Alterado com sucesso!");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}