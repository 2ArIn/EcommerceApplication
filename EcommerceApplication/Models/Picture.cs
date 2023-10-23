using EcommerceApplication.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class Picture:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string PicName { get; set; }
        public string? PicUrl { get; set; }
        //[NotMapped]
        //public IFormFile? PicUpload {set;get;}
        public bool MainPic { get; set; }
       
        public int MovieDetailId { get; set; }
        [ForeignKey("MovieDetailId")]
        public virtual MovieDetail MovieDetail { get; set; }
        
    }
}
