using BlazorMovieTracker.Data;
using BlazorMovieTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovieTracker.Services
{
    public class ActorService
    {
        private readonly IDbContextFactory<MovieContext> _contextFactory;

        public ActorService(IDbContextFactory<MovieContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Actor>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Actors
                .Include(a => a.Movies)
                .ToListAsync();
        }

        public async Task<Actor?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Actor actor)
        {
            using var context = _contextFactory.CreateDbContext();

            foreach (var movie in actor.Movies)
            {
                context.Attach(movie);
            }

            context.Actors.Add(actor);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Actor actor)
        {
            using var context = _contextFactory.CreateDbContext();

            var existingActor = await context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == actor.Id);

            if (existingActor != null)
            {
                existingActor.Name = actor.Name;
                existingActor.DateOfBirth = actor.DateOfBirth;
                existingActor.Biography = actor.Biography;

                existingActor.Movies.Clear();

                foreach (var movie in actor.Movies)
                {
                    var trackedMovie = await context.Movies.FindAsync(movie.Id);
                    if (trackedMovie != null)
                        existingActor.Movies.Add(trackedMovie);
                }
                await context.SaveChangesAsync();
            }

        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var actor = await context.Actors
                .Include (a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor != null)
            {
                context.Actors.Remove(actor);
                await context.SaveChangesAsync();
            }
        }
    }
}
