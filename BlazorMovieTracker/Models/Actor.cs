using System.ComponentModel.DataAnnotations;

namespace BlazorMovieTracker.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Biography { get; set; } = string.Empty;
        public ICollection<Movie>? Movies { get; set; }
    }
}
