using System.ComponentModel.DataAnnotations;

namespace BlazorMovieTracker.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public string Director { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }

        public bool Watched { get; set; }

        public string? Description { get; set; }

        public ICollection<Actor>? Actors { get; set; }
    }
}
