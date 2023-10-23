using EcommerceApplication.Data.Base;
using EcommerceApplication.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApplication.Models
{
    public class NewMovieViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام فیلم")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public string Name { get; set; }
        [Display(Name = "توضیحات فیلم")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public string Description { get; set; }

        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "تصویر فیلم")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public IFormFile? Image { get; set; }

        public string? ImageURL { get; set; }

        [Display(Name = "تاریخ شروع اکران")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ پایان اکران")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public DateTime EndDate { get; set; }
        [Display(Name = "قیمت به تومان")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public decimal Price { get; set; }

        [Display(Name = "انتخاب یک یا چند بازیگر")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public ICollection<int> ActorIds { get; set; }

        [Display(Name = "انتخاب یک یا چند سینما")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public ICollection<int> CinemaIds { get; set; }

         public int MovieDetailId { get; set; }

        [Display(Name = "گالری")]
        public List<IFormFile>? Galleries { get; set; }
        public List<Picture>? Pictures { get; set; }

        [Display(Name = "انتخاب تهیه کننده")]
        [Required(ErrorMessage = ErrorMsg.RequiredMsg)]
        public int ProducerId { get; set; }

        [Display(Name = "امتیاز فیلم")]
        public float? Rank { get; set; }

        [Display(Name = "تریلر فیلم")]
        public IFormFile? Trailer { get; set; }

        public string? TrailerPath { get; set; }
    }
}
