using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonWebApi_Auth0.Models;

namespace PokemonWebApi_Auth0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokedexItemsController : ControllerBase
    {
        private readonly PokemonContext _context;

        public PokedexItemsController(PokemonContext context)
        {
            _context = context;
        }

        // GET: api/PokedexItems
        [HttpGet]
        public ActionResult<IEnumerable<Pokedex>> GetPokemonItems()
        {
            var items = _context.PokemonItems.Include(item => item.Pokedex).Select(item =>
                new PokedexItemsDto
                {
                    Id = item.Id,
                    //UserId=0,
                    PokedexId = item.PokedexId,
                    BreedName=item.Pokedex.Name,
                    Nickname = item.Nickname,
                    Birthdate = item.Birthdate,
                    Gender = item.Gender,
                    Image = item.Image,
                    State = item.State
                });
            return Ok(items);
        }

        // GET: api/PokedexItems/5
        [HttpGet("{id}")]
        public ActionResult<PokedexItem> GetPokedexItems(int id)
        {
            var item = _context.PokemonItems.Include(item => item.Pokedex).Select(item =>
                new PokedexItemsDto
                {
                    Id = item.Id,
                    //UserId=0,
                    PokedexId = item.PokedexId,
                    BreedName = item.Pokedex.Name,
                    Nickname = item.Nickname,
                    Birthdate = item.Birthdate,
                    Gender = item.Gender,
                    Image = item.Image,
                    State = item.State
                }).Where(x => x.Id == id).First();
            return Ok(item);
        }

        // PUT: api/PokedexItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokedexItems(int id, PokedexItem pokedexItems)
        {
            if (id != pokedexItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokedexItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokedexItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PokedexItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokedexItem>> PostPokedexItems(PokedexItemsDtoPost pokedexItemsDtoPost)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var pokedexItem = new PokedexItem
            {
                Id = pokedexItemsDtoPost.Id,
                UserId=userId,
                PokedexId = pokedexItemsDtoPost.PokedexId,
                Nickname = pokedexItemsDtoPost.Nickname,
                Birthdate = pokedexItemsDtoPost.Birthdate,
                Gender = pokedexItemsDtoPost.Gender,
                Image = pokedexItemsDtoPost.Image,
                State = pokedexItemsDtoPost.State
            };
            _context.PokemonItems.Add(pokedexItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokedexItems", new { id = pokedexItemsDtoPost.Id }, pokedexItemsDtoPost);
        }

        // DELETE: api/PokedexItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokedexItems(int id)
        {
            var pokedexItems = await _context.PokemonItems.FindAsync(id);
            if (pokedexItems == null)
            {
                return NotFound();
            }

            _context.PokemonItems.Remove(pokedexItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokedexItemsExists(int id)
        {
            return _context.PokemonItems.Any(e => e.Id == id);
        }
    }
}
