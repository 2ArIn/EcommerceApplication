using EcommerceApplication.Controllers;
using EcommerceApplication.Models;

namespace EcommerceApplication.Data.ViewModels
{
    public class NewMovieDropdownsViewModel
    {
        public NewMovieDropdownsViewModel()
        {
            Producers = new List<Producer>();
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
        }
        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
