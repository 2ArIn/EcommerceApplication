using EcommerceApplication.Data;
using EcommerceApplication.Data.Services;
using EcommerceApplication.Data.ViewModels;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EcommerceApplication.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAll(includeProperties: "MovieCinemas.Cinema");
            //var allMovieDetails = await _context.MovieDetails.ToListAsync();
            
            //List<MovieDetailsViewModel> MDetailVMs = new List<MovieDetailsViewModel>();
            //List< MovieDetailsViewModel> lists = new List<MovieDetailsViewModel>();
            //for (int i = 0; i < allMovies.Count; i++)
            //{

            //    lists.Add(new MovieDetailsViewModel()
            //    {
            //        Name = allMovies[i].Name,
            //        Description = allMovies[i].Description,
            //        Price = allMovieDetails[i].Price,
            //        Rank = allMovieDetails[i].Rank,
            //        StartDate = allMovieDetails[i].StartDate,
            //        EndDate = allMovieDetails[i].EndDate,
            //        Pictures = allMovies[i].Pictures,
            //        TrailerPath = allMovieDetails[i].TrailerPath,
            //        ImageURL = allMovieDetails[i].ImageURL
            //    });
            //}
            //MDetailVMs.AddRange(lists);
            return View(allMovies);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAll(includeProperties: "MovieCinemas.Cinema");
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n=>n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();
                return View("Index",filteredResult);
            }
            return View("Index",allMovies);
        }

            public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieById(id);
            var moviePictures = await _service.GetPictureByMovieDetail(movieDetail.MovieDetailId);
            ViewBag.Pictures = moviePictures;
            return View(movieDetail);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Image,Galleries,Trailer,Name,Description,ActorIds,CinemaIds,Price,Rank,StartDate,EndDate,MovieCategory,ProducerId")]NewMovieViewModel movie)
        {
            
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            
            if (movie.Image?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "movies\\");
                string fileName = Guid.NewGuid().ToString() + "_" + movie.Image.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    movie.Image.CopyTo(stream);
                }
                movie.ImageURL = "/images/movies/" + fileName;
            }
            if (movie.Trailer?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", "MoviesTrailer\\");
                string fileName = Guid.NewGuid().ToString() + "_" + movie.Trailer.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    movie.Trailer.CopyTo(stream);
                }
                movie.TrailerPath = "/Videos/MoviesTrailer/" + fileName;
            }
           // if(movie.Galleries)

            await _service.AddNewMovie(movie);
            

            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieById(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieViewModel()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaIds = movieDetails.MovieCinemas.Select(mc => mc.CinemaId).ToList(),
                ActorIds = movieDetails.MovieActors.Select(ma => ma.ActorId).ToList(),
                ProducerId = movieDetails.MovieDetail.ProducerId,
                TrailerPath = movieDetails.MovieDetail.TrailerPath,
                Pictures = movieDetails.MovieDetail.Pictures,
                Rank = movieDetails.MovieDetail.Rank,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieDetailId = movieDetails.MovieDetailId
            };



            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");


            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,string trailerPath,string imgURL,NewMovieViewModel movie)
        {
            if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }

            if (movie.Image?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "movies\\");

                string imgDel = imgURL;
                string fileDel = filePath + imgDel;
                if(System.IO.File.Exists(fileDel))
                {
                    System.IO.File.Delete(fileDel);
                }


                string fileName = Guid.NewGuid().ToString() + "_" + movie.Image.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    movie.Image.CopyTo(stream);
                }
                movie.ImageURL = "/images/movies/" + fileName;
            }
            if (movie.Trailer?.Length > 0 )
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", "MoviesTrailer\\");

                string trailerDel = trailerPath;
                string fileDel = filePath + trailerDel;
                if (System.IO.File.Exists(fileDel))
                {
                    System.IO.File.Delete(fileDel);
                }


                
                string fileName = Guid.NewGuid().ToString() + "_" + movie.Trailer.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    movie.Trailer.CopyTo(stream);
                }
                movie.TrailerPath = "/Videos/MoviesTrailer/" + fileName;
            }
            else
            {
                movie.TrailerPath = trailerPath;
            }
            // if(movie.Galleries)

            await _service.UpdateMovieAsync(movie);


            return RedirectToAction("Index");


        }
    }    
}
