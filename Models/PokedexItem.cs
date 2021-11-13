using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class PokedexItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public string Birthdate { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string State { get; set; }
        public int PokedexId { get; set; }
        public Pokedex Pokedex{ get; set; }

    }
}
