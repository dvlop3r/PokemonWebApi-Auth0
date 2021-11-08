using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonWebApi_Auth0.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonWebApi_Auth0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _context;
        public PokemonController(PokemonContext context)
        {
            _context = context;
        }

        // GET: api/<PokemonController>
        [HttpGet]
        public IEnumerable<Pokedex> Get()
        {
            return _context.Pokedexes.ToList();
        }

        // GET api/<PokemonController>/5
        [HttpGet("{id}")]
        public Pokedex Get(int id)
        {
            return _context.Pokedexes.Where(p => p.Id == id).First();
        }

        // POST api/<PokemonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PokemonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PokemonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    
}

