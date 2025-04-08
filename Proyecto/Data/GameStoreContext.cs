﻿using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Proyecto.Entities;

namespace Proyecto.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext (options)
    {
        public DbSet<GameEntity> Games => Set<GameEntity>();

        public DbSet<GenreEntity> Genres => Set<GenreEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreEntity>().HasData(
                new {Id = 1, Name = "Fighting"},
                new {Id = 2, Name = "Roleplaying"},
                new {Id = 3, Name = "Sports"},
                new {Id = 4, Name = "Racing"},
                new {Id = 5, Name = "Kids and Family"}
            );
        }
    }
}
