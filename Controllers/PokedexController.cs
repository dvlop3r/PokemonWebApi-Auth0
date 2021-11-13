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

        [HttpGet("TestAction")]
        public ActionResult TestAction()
        {
            //Get a valid token first
            var client = new RestClient("https://dev-pn3w6qi6.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"LM5DLDrn2ayNqGEsptBY5KSLLDMpqHrt\",\"client_secret\":\"MAEm0NHsp2kCd4IyZtMtlYSZF8P3bouhSf_peSg98WpU0wbk1TvO6O61yjbo_26M\",\"audience\":\"https://dev-pn3w6qi6.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var jc = JsonSerializer.Deserialize<JsonClass>(response.Content);
            
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string sampleToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik9HQmpiTWZ3OXRWNk0tR3dVby1tdSJ9.eyJpc3MiOiJodHRwczovL2Rldi1wbjN3NnFpNi51cy5hdXRoMC5jb20vIiwic3ViIjoiTE01RExEcm4yYXlOcUdFc3B0Qlk1S1NMTERNcHFIcnRAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGV2LXBuM3c2cWk2LnVzLmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjM2ODEzNDkyLCJleHAiOjE2MzY4OTk4OTIsImF6cCI6IkxNNURMRHJuMmF5TnFHRXNwdEJZNUtTTExETXBxSHJ0Iiwic2NvcGUiOiJyZWFkOmNsaWVudF9ncmFudHMgY3JlYXRlOmNsaWVudF9ncmFudHMgZGVsZXRlOmNsaWVudF9ncmFudHMgdXBkYXRlOmNsaWVudF9ncmFudHMgcmVhZDp1c2VycyB1cGRhdGU6dXNlcnMgZGVsZXRlOnVzZXJzIGNyZWF0ZTp1c2VycyByZWFkOnVzZXJzX2FwcF9tZXRhZGF0YSB1cGRhdGU6dXNlcnNfYXBwX21ldGFkYXRhIGRlbGV0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgY3JlYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSByZWFkOnVzZXJfY3VzdG9tX2Jsb2NrcyBjcmVhdGU6dXNlcl9jdXN0b21fYmxvY2tzIGRlbGV0ZTp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfdGlja2V0cyByZWFkOmNsaWVudHMgdXBkYXRlOmNsaWVudHMgZGVsZXRlOmNsaWVudHMgY3JlYXRlOmNsaWVudHMgcmVhZDpjbGllbnRfa2V5cyB1cGRhdGU6Y2xpZW50X2tleXMgZGVsZXRlOmNsaWVudF9rZXlzIGNyZWF0ZTpjbGllbnRfa2V5cyByZWFkOmNvbm5lY3Rpb25zIHVwZGF0ZTpjb25uZWN0aW9ucyBkZWxldGU6Y29ubmVjdGlvbnMgY3JlYXRlOmNvbm5lY3Rpb25zIHJlYWQ6cmVzb3VyY2Vfc2VydmVycyB1cGRhdGU6cmVzb3VyY2Vfc2VydmVycyBkZWxldGU6cmVzb3VyY2Vfc2VydmVycyBjcmVhdGU6cmVzb3VyY2Vfc2VydmVycyByZWFkOmRldmljZV9jcmVkZW50aWFscyB1cGRhdGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGRlbGV0ZTpkZXZpY2VfY3JlZGVudGlhbHMgY3JlYXRlOmRldmljZV9jcmVkZW50aWFscyByZWFkOnJ1bGVzIHVwZGF0ZTpydWxlcyBkZWxldGU6cnVsZXMgY3JlYXRlOnJ1bGVzIHJlYWQ6cnVsZXNfY29uZmlncyB1cGRhdGU6cnVsZXNfY29uZmlncyBkZWxldGU6cnVsZXNfY29uZmlncyByZWFkOmhvb2tzIHVwZGF0ZTpob29rcyBkZWxldGU6aG9va3MgY3JlYXRlOmhvb2tzIHJlYWQ6YWN0aW9ucyB1cGRhdGU6YWN0aW9ucyBkZWxldGU6YWN0aW9ucyBjcmVhdGU6YWN0aW9ucyByZWFkOmVtYWlsX3Byb3ZpZGVyIHVwZGF0ZTplbWFpbF9wcm92aWRlciBkZWxldGU6ZW1haWxfcHJvdmlkZXIgY3JlYXRlOmVtYWlsX3Byb3ZpZGVyIGJsYWNrbGlzdDp0b2tlbnMgcmVhZDpzdGF0cyByZWFkOmluc2lnaHRzIHJlYWQ6dGVuYW50X3NldHRpbmdzIHVwZGF0ZTp0ZW5hbnRfc2V0dGluZ3MgcmVhZDpsb2dzIHJlYWQ6bG9nc191c2VycyByZWFkOnNoaWVsZHMgY3JlYXRlOnNoaWVsZHMgdXBkYXRlOnNoaWVsZHMgZGVsZXRlOnNoaWVsZHMgcmVhZDphbm9tYWx5X2Jsb2NrcyBkZWxldGU6YW5vbWFseV9ibG9ja3MgdXBkYXRlOnRyaWdnZXJzIHJlYWQ6dHJpZ2dlcnMgcmVhZDpncmFudHMgZGVsZXRlOmdyYW50cyByZWFkOmd1YXJkaWFuX2ZhY3RvcnMgdXBkYXRlOmd1YXJkaWFuX2ZhY3RvcnMgcmVhZDpndWFyZGlhbl9lbnJvbGxtZW50cyBkZWxldGU6Z3VhcmRpYW5fZW5yb2xsbWVudHMgY3JlYXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRfdGlja2V0cyByZWFkOnVzZXJfaWRwX3Rva2VucyBjcmVhdGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiBkZWxldGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiByZWFkOmN1c3RvbV9kb21haW5zIGRlbGV0ZTpjdXN0b21fZG9tYWlucyBjcmVhdGU6Y3VzdG9tX2RvbWFpbnMgdXBkYXRlOmN1c3RvbV9kb21haW5zIHJlYWQ6ZW1haWxfdGVtcGxhdGVzIGNyZWF0ZTplbWFpbF90ZW1wbGF0ZXMgdXBkYXRlOmVtYWlsX3RlbXBsYXRlcyByZWFkOm1mYV9wb2xpY2llcyB1cGRhdGU6bWZhX3BvbGljaWVzIHJlYWQ6cm9sZXMgY3JlYXRlOnJvbGVzIGRlbGV0ZTpyb2xlcyB1cGRhdGU6cm9sZXMgcmVhZDpwcm9tcHRzIHVwZGF0ZTpwcm9tcHRzIHJlYWQ6YnJhbmRpbmcgdXBkYXRlOmJyYW5kaW5nIGRlbGV0ZTpicmFuZGluZyByZWFkOmxvZ19zdHJlYW1zIGNyZWF0ZTpsb2dfc3RyZWFtcyBkZWxldGU6bG9nX3N0cmVhbXMgdXBkYXRlOmxvZ19zdHJlYW1zIGNyZWF0ZTpzaWduaW5nX2tleXMgcmVhZDpzaWduaW5nX2tleXMgdXBkYXRlOnNpZ25pbmdfa2V5cyByZWFkOmxpbWl0cyB1cGRhdGU6bGltaXRzIGNyZWF0ZTpyb2xlX21lbWJlcnMgcmVhZDpyb2xlX21lbWJlcnMgZGVsZXRlOnJvbGVfbWVtYmVycyByZWFkOmVudGl0bGVtZW50cyByZWFkOmF0dGFja19wcm90ZWN0aW9uIHVwZGF0ZTphdHRhY2tfcHJvdGVjdGlvbiByZWFkOm9yZ2FuaXphdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGNyZWF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgcmVhZDpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVyX3JvbGVzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBkZWxldGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBjcmVhdGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25faW52aXRhdGlvbnMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.OJBfOXblxYxoPCYYHtmDJ5rgcds5D--iv8YysNAnK3Iadi6L9K7kAiU2-HhJLtvsEwQcxrg8TAz-7fsafUtQ4s4HiDy7MiU1v0rTFFVfJNUIa10lAdmQUu9Rzix_VeXZV5IHx5fTfP2utIyNmV_7A7iR8mMQ47Mklo7YYK33u2NJlDvOl_2OIGJAsLe3BjE5YjSSqZxFLzLy7MWFx5-elTGh8ctanzEmw9GB9cRl0chvmYHgT0raX2zgC0BjWiRablbKz1YXRWzl_5Bo8Fmir-PhPe8muYQ4_TFQSx2mGwPUrLRpwdhaVXaNlM_OFKwp34SaI5RpCNmglGW714qswg";

            string url = $"https://dev-pn3w6qi6.us.auth0.com/api/v2/users/{userId}?include_fields=true";

            //Request user's info using generated token and the above API endpoint
            var client1 = new RestClient(url);
            var request1 = new RestRequest(Method.GET);  
            request1.AddHeader("authorization", "Bearer "+jc.access_token);
            IRestResponse response1 = client1.Execute(request1); 

            return Ok(response1.Content);
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

        // POST api/<PokedexController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(PokedexDto))]
        //[ResponseType(typeof(PokedexDto))]
        public async Task<ActionResult> Post([FromBody] Pokedex pokedex)
        {
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

