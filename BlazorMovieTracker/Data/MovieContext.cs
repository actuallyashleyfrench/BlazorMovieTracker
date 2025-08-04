using BlazorMovieTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovieTracker.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie 
                { 
                    Id = 1, 
                    Title = "Avatar", 
                    Genre = "Sci-Fi", 
                    Director = "James Cameron", 
                    ReleaseYear = 2000, 
                    Watched = true,
                    Description = "A paraplegic marine is sent to the alien world of Pandora on a unique mission " +
                                  "but becomes torn between following orders and protecting an indigenous civilization."
                },
                new Movie 
                { 
                    Id = 2, 
                    Title = "The Shawshank Redemption", 
                    Genre = "Drama", 
                    Director = "Frank Darabont", 
                    ReleaseYear = 1994, 
                    Watched = false,
                    Description = "Two imprisoned men develop a deep friendship and find hope and redemption " +
                                  "while serving long sentences in Shawshank State Penitentiary."
                },
                new Movie 
                { 
                    Id = 3, 
                    Title = "Inception", 
                    Genre = "Sci-Fi", 
                    Director = "Christopher Nolan", 
                    ReleaseYear = 2010, 
                    Watched = true,
                    Description = "A skilled thief who steals secrets by infiltrating dreams is offered a chance to erase " +
                                  "his criminal record by performing an impossible 'inception'—planting an idea into a target's subconscious."
                },
                new Movie 
                { 
                    Id = 4, 
                    Title = "Spirited Away", 
                    Genre = "Animation", 
                    Director = "Hayao Miyazaki", 
                    ReleaseYear = 2001, 
                    Watched = true,
                    Description = "A young girl enters a magical world of spirits and gods where she must find her " +
                                  "courage to save her parents and return home."
                },
                new Movie 
                { 
                    Id = 5, 
                    Title = "Across the Universe", 
                    Genre = "Musical", 
                    Director = "Julie Taymor", 
                    ReleaseYear = 2007, 
                    Watched = true,
                    Description = "Set in the turbulent 1960s, a young man travels from Liverpool to New York " +
                                  "in search of his father, falling in love and confronting the era's social changes, " +
                                  "all set to Beatles music."
                },
                new Movie 
                { 
                    Id = 6, 
                    Title = "The Godfather", 
                    Genre = "Crime", 
                    Director = "Francis Ford Coppola", 
                    ReleaseYear = 1972, 
                    Watched = false,
                    Description = "The aging patriarch of a crime family transfers control of his empire to his " +
                                  "reluctant son, unraveling a saga of power, loyalty, and betrayal."
                }
                );

            modelBuilder.Entity<Actor>().HasData(
                new Actor 
                { 
                    Id = 1, 
                    Name = "Sam Worthington",
                    DateOfBirth = new DateTime(1976, 8, 2),
                    Biography = "Austrailian actor known for his role as Jake Sully in Avatar."
                },
                new Actor
                {
                    Id = 2,
                    Name = "Zoe Saldana",
                    DateOfBirth = new DateTime(1978, 6, 19),
                    Biography = "American actress who portrayed Neytiri in Avatar and Gamora in the Marvel Cinematic Universe."
                },
                new Actor
                {
                    Id = 3,
                    Name = "Sigourney Weaver",
                    DateOfBirth = new DateTime(1949, 10, 8),
                    Biography = "Veteran actress known for the Alien franchise and Avatar."
                },
                new Actor
                {
                    Id = 4,
                    Name = "Leonardo DiCaprio",
                    DateOfBirth = new DateTime(1974, 11, 11),
                    Biography = "Oscar-winning actor best known for Inception, Titanic, and The Revenant."
                },
                new Actor
                {
                    Id = 5,
                    Name = "Joseph Gordon-Levitt",
                    DateOfBirth = new DateTime(1981, 2, 17),
                    Biography = "American actor who starred in Inception, 500 Days of Summer, and Looper."
                }
            );

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(j => j.HasData(
                    new { MoviesId = 1, ActorsId = 1 }, // Avatar - Sam Worthington
                    new { MoviesId = 1, ActorsId = 2 }, // Avatar - Zoe Saldana
                    new { MoviesId = 3, ActorsId = 4 }, // Inception - Leonardo DiCaprio
                    new { MoviesId = 3, ActorsId = 5 }  // Inception - Joseph Gordon-Levitt
                ));
        }
    }
}
