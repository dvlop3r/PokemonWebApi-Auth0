using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
