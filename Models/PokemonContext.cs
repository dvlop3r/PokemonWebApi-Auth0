using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Models
{
    public class PokemonContext
    {
        public List<Pokemon> pokemons;
        public PokemonContext()
        {
            pokemons = new List<Pokemon>
            {
                new Pokemon
                {
                    Id=1,
                    name="pokemon1",
                    type="typ1"
                },new Pokemon
                {
                    Id=1,
                    name="pokemon2",
                    type="typ2"
                },new Pokemon
                {
                    Id=1,
                    name="pokemon3",
                    type="typ1"
                }
            };
        }
    }
}
