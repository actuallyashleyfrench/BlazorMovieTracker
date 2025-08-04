using BlazorMovieTracker.Data;
using BlazorMovieTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovieTracker.Services
{
    public class MovieService
    {
        private readonly IDbContextFactory<MovieContext> _contextFactory;

        public MovieService(IDbContextFactory<MovieContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Movies.Include(m => m.Actors).ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Movie movie)
        {
            using var context = _contextFactory.CreateDbContext();
            foreach (var actor in movie.Actors)
            {
                context.Attach(actor);
            }
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            using var context = _contextFactory.CreateDbContext();

            var existingMovie = await context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.Genre = movie.Genre;
                existingMovie.Director = movie.Director;
                existingMovie.ReleaseYear = movie.ReleaseYear;
                existingMovie.Description = movie.Description;
                existingMovie.Watched = movie.Watched;

                existingMovie.Actors.Clear();

                foreach (var actor in movie.Actors)
                {
                    var trackedActor = await context.Actors.FindAsync(actor.Id);
                    if (trackedActor != null)
                    {
                        existingMovie.Actors.Add(trackedActor);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var movie = await context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie != null)
            {
                movie.Actors.Clear();
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
            }
        }
    }
}
