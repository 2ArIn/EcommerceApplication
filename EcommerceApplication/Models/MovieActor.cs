using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
