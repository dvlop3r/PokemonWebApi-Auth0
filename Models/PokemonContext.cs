using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonWebApi_Auth0.Models;

namespace PokemonWebApi_Auth0.Models
{
    public class PokemonContext : DbContext
    {
        public DbSet<Pokedex> Pokedexes { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<PokedexItem> PokemonItems{ get; set; }
        public DbSet<LatestPokemon> LatestPokemon { get; set; }
        public PokemonContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
