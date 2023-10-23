using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class MovieDetail
    {

        public MovieDetail()
        {
            Pictures = new List<Picture>();   
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        
        public float? Rank { get; set; }
        public virtual List<Picture>? Pictures { get; set; }
        public string? TrailerPath { get; set; }
        
        public Movie Movie { get; set; }
        public Producer Producer { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
    }
}
