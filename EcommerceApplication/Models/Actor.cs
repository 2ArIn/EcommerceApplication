using EcommerceApplication.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("تصویر")]
        public string? ProfilePictureURL { get; set; }
        [NotMapped]
        public IFormFile? PicUpload {  get; set; }
        [Required(ErrorMessage =ErrorMsg.RequiredMsg)]
        [DisplayName("نام کامل")]
        //[StringLength(50,MinimumLength = 6,ErrorMessage = ErrorMsg.StringLength)]

        public string FullName { get; set; }
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        [DisplayName("شرح حال")]
        public string Bio { get; set; }
        public List<MovieActor>? MovieActors { get; set; }
        
    }
}
