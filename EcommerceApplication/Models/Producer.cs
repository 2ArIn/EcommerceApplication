using EcommerceApplication.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("تصویر")]
        
        public string? ProfilePictureURL { get; set; }
        [NotMapped]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public IFormFile PicUpload { get; set; }
        [DisplayName("نام کامل")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public string FullName { get; set; }
        [DisplayName("شرح حال")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public string Bio { get; set; }
        public List<MovieDetail>? MovieDetails { get; set; }
    }
}
