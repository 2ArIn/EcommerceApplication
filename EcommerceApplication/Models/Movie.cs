using EcommerceApplication.Data.Base;
using EcommerceApplication.Data.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class Movie:IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
     
        public MovieCategory MovieCategory { get; set; }
        public string? ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieCinema> MovieCinemas { get; set; }

        public int MovieDetailId { get; set; }
        [ForeignKey("MovieDetailId")]
        public MovieDetail MovieDetail { get; set; }
        
    }
}
