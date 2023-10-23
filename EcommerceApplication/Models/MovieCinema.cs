using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class MovieCinema
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }
    }
}
