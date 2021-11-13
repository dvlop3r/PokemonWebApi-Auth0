using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class Pokedex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Height { get; set; }
        public string Imagelink { get; set; }
        public int? EvolvesFromId { get; set; }
        public int? EvolvesToId { get; set; }

        [ForeignKey("EvolvesFromId")]
        public Pokedex EvolvesFrom { get; set; }
        [ForeignKey("EvolvesToId")]
        public Pokedex EvolvesTo { get; set; }

        public List<Type> Types { get; set; }
        public List<Ability> Abilities { get; set; }
    }
}
