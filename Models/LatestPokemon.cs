using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class LatestPokemon
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public string UserId { get; set; }
    }
}
