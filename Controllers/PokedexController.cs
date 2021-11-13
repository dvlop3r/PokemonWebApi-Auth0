using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using PokemonWebApi_Auth0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Web.Http.Description;
using System.Security.Claims;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using PokemonWebApi_Auth0.Authentication;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonWebApi_Auth0.Controllers
{
    public class JsonClass
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PokedexController : ControllerBase
    {
        private readonly PokemonContext _context;
        public PokedexController(PokemonContext context)
        {
            _context = context;
        }

        // GET: api/<PokedexController>
        [HttpGet]
        public ActionResult<IQueryable<PokedexDto>> Get()
        {
            var pokedexes = _context.Pokedexes.Include(p => p.Types).Include(p => p.Abilities).Select(p =>
                 new PokedexDto
                 {
                     Id=p.Id,
                     Name=p.Name,
                     Types=p.Types,
                     Abilities=p.Abilities,
                     Desc=p.Desc,
                     Height=p.Height,
                     Imagelink=p.Imagelink,
                     EvolvesFromId=p.EvolvesFromId,
                     EvolvesToId=p.EvolvesToId
                 });
            return Ok(pokedexes);
        }

        // GET api/<PokedexController>/5
        [HttpGet("{id}")]
        public ActionResult<Pokedex> Get(int id)
        {
            var pokedex = _context.Pokedexes.Include(p => p.Types).Include(p => p.Abilities).Select(p =>
                    new PokedexDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Types = p.Types,
                        Abilities = p.Abilities,
                        Desc = p.Desc,
                        Height = p.Height,
                        Imagelink = p.Imagelink,
                        EvolvesFromId = p.EvolvesFromId,
                        EvolvesToId = p.EvolvesToId
                    }).Where(x => x.Id == id).First();

            if (pokedex == null)
                return BadRequest();

            return Ok(pokedex);
        }
        // GET api/<PokedexController>/PokedexName
        [HttpGet("SearchByName")]
        public ActionResult<Pokedex> Get(string name)
        {
            var pokedexes = _context.Pokedexes.Include(p => p.Types).Include(p => p.Abilities).Select(p =>
                    new PokedexDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Types = p.Types,
                        Abilities = p.Abilities,
                        Desc = p.Desc,
                        Height = p.Height,
                        Imagelink = p.Imagelink,
                        EvolvesFromId = p.EvolvesFromId,
                        EvolvesToId = p.EvolvesToId
                    }).Where(p => p.Name==name);

            if (pokedexes == null)
                return NotFound();

            return Ok(pokedexes);
        }
        // GET api/<PokedexController>/PokedexType
        [HttpGet("SearchByType")]
        public ActionResult<Pokedex> SearchByType(string name)
        {
            var pokedexes = _context.Pokedexes.Include(p => p.Types).Include(p => p.Abilities).Select(p =>
                    new PokedexDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Types = p.Types,
                        Abilities = p.Abilities,
                        Desc = p.Desc,
                        Height = p.Height,
                        Imagelink = p.Imagelink,
                        EvolvesFromId = p.EvolvesFromId,
                        EvolvesToId = p.EvolvesToId
                    }).Where(p => p.Types.Any(t => t.Name == name));

            if (pokedexes == null)
                return NotFound();

            return Ok(pokedexes);
        }
        // GET api/<PokedexController>/PokedexAbility
        [HttpGet("SearchByAbility")]
        public ActionResult<Pokedex> SearchByAbility(string name)
        {
            var pokedexes = _context.Pokedexes.Include(p => p.Types).Include(p => p.Abilities).Select(p =>
                    new PokedexDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Types = p.Types,
                        Abilities = p.Abilities,
                        Desc = p.Desc,
                        Height = p.Height,
                        Imagelink = p.Imagelink,
                        EvolvesFromId = p.EvolvesFromId,
                        EvolvesToId = p.EvolvesToId
                    }).Where(p => p.Abilities.Any(t => t.Name == name));

            if (pokedexes == null)
                return NotFound();

            return Ok(pokedexes);
        }
        // POST api/<PokedexController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(PokedexDto))]
        //[ResponseType(typeof(PokedexDto))]
        public async Task<ActionResult> Post([FromBody] Pokedex pokedex)
        {
            TokenGenerator tokenGenerator = new TokenGenerator();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            UserValidation userValidation = new UserValidation();

            if (!userValidation.ValidateUser(userId, tokenGenerator.GetToken()))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Pokedexes.Add(pokedex);
            await _context.SaveChangesAsync();

            var pokedexDto = new PokedexDto
            {
                Id = pokedex.Id,
                Name = pokedex.Name,
                Desc = pokedex.Desc,
                Height = pokedex.Height,
                Imagelink = pokedex.Imagelink,
                EvolvesFromId = pokedex.EvolvesFromId,
                EvolvesToId = pokedex.EvolvesToId,
                Types = pokedex.Types,
                Abilities = pokedex.Abilities
            };

            return CreatedAtAction("Get", new { id = pokedexDto.Id}, pokedexDto);
        }

        // PUT api/<PokedexController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pokedex pokedex)
        {
            TokenGenerator tokenGenerator = new TokenGenerator();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            UserValidation userValidation = new UserValidation();

            if (!userValidation.ValidateUser(userId, tokenGenerator.GetToken()))
                return Unauthorized();

            if (id != pokedex.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokedex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<PokedexController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            TokenGenerator tokenGenerator = new TokenGenerator();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            UserValidation userValidation = new UserValidation();

            if (!userValidation.ValidateUser(userId, tokenGenerator.GetToken()))
                return Unauthorized();

            var book = await _context.Pokedexes.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Pokedexes.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    
}

