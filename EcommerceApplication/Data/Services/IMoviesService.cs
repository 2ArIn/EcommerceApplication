using EcommerceApplication.Data.Base;
using EcommerceApplication.Data.ViewModels;
using EcommerceApplication.Models;

namespace EcommerceApplication.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieById(int id);
        Task<NewMovieDropdownsViewModel> GetNewMovieDropdownsValues();
        Task AddNewMovie(NewMovieViewModel data);

        Task<List<Picture>> GetPictureByMovieDetail(int movieDetailId);

        Task UpdateMovieAsync(NewMovieViewModel data);
    }
}
