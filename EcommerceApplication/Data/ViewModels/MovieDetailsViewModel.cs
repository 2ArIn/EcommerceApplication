using EcommerceApplication.Models;

namespace EcommerceApplication.Data.ViewModels
{
    public class MovieDetailsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public float Rank { get; set; }
        public List<Picture> Pictures { get; set; }
        public string TrailerPath { get; set; }
        public string ImageURL { get; set; }
    }
}
