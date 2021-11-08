using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Pokedex> Pokedexes { get; set; }
    }
}
