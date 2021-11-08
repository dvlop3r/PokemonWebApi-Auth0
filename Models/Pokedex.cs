using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class Pokedex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<Type> Types { get; set; }
        public List<Ability> Ablilites { get; set; }

        public double Height { get; set; }
        public int Evolves_from { get; set; }
        public int Evolves_to{ get; set; }
        public string Image_link { get; set; }


        



    }
}
