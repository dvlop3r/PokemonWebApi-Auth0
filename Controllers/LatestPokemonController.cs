using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonWebApi_Auth0.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PokemonWebApi_Auth0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatestPokemonController : ControllerBase
    {
        private readonly PokemonContext _context;
        public LatestPokemonController(PokemonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            string userId = CurrentUser();
            var latest = _context.LatestPokemon.Where(x => x.UserId == userId).First();
            var items = _context.PokemonItems.Where(x => x.Id == latest.PokemonId)
                .Include(item => item.Pokedex).Select(item =>
                new PokedexItemsDto
                {
                    Id = item.Id,
                    PokedexId = item.PokedexId,
                    BreedName = item.Pokedex.Name,
                    Nickname = item.Nickname,
                    Birthdate = item.Birthdate,
                    Gender = item.Gender,
                    Image = item.Image,
                    State = item.State
                });
            return Ok(items);
        }
        private string CurrentUser()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
