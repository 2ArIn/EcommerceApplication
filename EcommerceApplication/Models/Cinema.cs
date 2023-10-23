using EcommerceApplication.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("لوگو")]
        public string? Logo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public IFormFile? PicUpload { get; set; }
        [DisplayName("نام سینما")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]

        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        public ICollection<MovieCinema>? MovieCinemas { get; set; }
    }
}
