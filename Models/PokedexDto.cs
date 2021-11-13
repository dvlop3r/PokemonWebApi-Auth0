using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class PokedexDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Height { get; set; }
        public string Imagelink { get; set; }
        public int? EvolvesFromId { get; set; }
        public int? EvolvesToId { get; set; }
        public List<Type> Types{ get; set; }
        public List<Ability> Abilities { get; set; }
    }
}
