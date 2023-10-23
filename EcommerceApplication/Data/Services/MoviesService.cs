using EcommerceApplication.Data.Base;
using EcommerceApplication.Data.enums;
using EcommerceApplication.Data.ViewModels;
using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Data.Services
{
    public class MoviesService:EntityBaseRepository<Movie>,IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context):base(context)
        { 
            _context = context;
        }

        public async Task AddNewMovie(NewMovieViewModel data)
        {
            var newMovieDetail = new MovieDetail()
            {
                //Id = newMovie.Id,
                Rank = data.Rank,
                ProducerId = data.ProducerId,
                TrailerPath =  data.TrailerPath,

            };
            await _context.MovieDetails.AddAsync(newMovieDetail);
            await _context.SaveChangesAsync();
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                MovieDetailId = newMovieDetail.Id
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach(var actorId in data.ActorIds)
            {
                var newMovieActor = new MovieActor()
                {
                    ActorId = actorId,
                    MovieId = newMovie.Id
                };
                await _context.MovieActors.AddAsync(newMovieActor);
            }
            await _context.SaveChangesAsync();

            foreach(var cinemaId in data.CinemaIds)
            {
                var newMovieCinema = new MovieCinema()
                {
                    CinemaId = cinemaId,
                    MovieId = newMovie.Id
                };
                await _context.MovieCinemas.AddAsync(newMovieCinema);
            }
            await _context.SaveChangesAsync();

           
            
            //List<Picture> MyPics = new List<Picture>();
            if (data.Galleries.Sum(f=>f.Length) > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "galleries\\");
                //Picture MyPic = new Picture();
                foreach (var pic in data.Galleries)
                {
                    var newPic = new Picture();
                    newPic.MovieDetailId = newMovieDetail.Id;
                    string fileName = Guid.NewGuid().ToString() + "_" + pic.FileName;
                    newPic.PicName = fileName;
                    string path = filePath + fileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        pic.CopyTo(stream);
                    }
                    newPic.PicUrl = "/images/galleries/" + fileName;
                    await _context.Pictures.AddAsync(newPic);
                    await _context.SaveChangesAsync();
                    newMovieDetail.Pictures.Add(newPic);
                }
            }

        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c=>c.MovieCinemas).ThenInclude(c=>c.Cinema)
                .Include(am => am.MovieActors).ThenInclude(a => a.Actor)
                .Include(md=>md.MovieDetail).ThenInclude(p => p.Producer)
                .FirstOrDefaultAsync(n => n.Id == id);
            return movieDetails;
        }
        public async Task<NewMovieDropdownsViewModel> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsViewModel()
            {
                Actors= await _context.Actors.OrderBy(n=>n.FullName).ToListAsync(),
                Cinemas= await _context.Cinemas.OrderBy(n=>n.Name).ToListAsync(),
                Producers= await _context.Producers.OrderBy(n=>n.FullName).ToListAsync(),
            };
            return response;
        }

        public async Task<List<Picture>> GetPictureByMovieDetail(int movieDetailId)
        {
            List<Picture> pics = await _context.Pictures.Where(n => n.MovieDetailId == movieDetailId).ToListAsync();
            return pics;

        }

        public async Task UpdateMovieAsync(NewMovieViewModel data)
        {
            var dbMovieDetails = await _context.MovieDetails.FirstOrDefaultAsync(n => n.Id == data.MovieDetailId);
            if(dbMovieDetails != null) 
            {
                dbMovieDetails.Rank = data.Rank;
                dbMovieDetails.ProducerId = data.ProducerId;
                dbMovieDetails.TrailerPath = data.TrailerPath;
                await _context.SaveChangesAsync();
            }

            var dbMovies = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbMovies != null)
            {
                dbMovies.Name = data.Name;
                dbMovies.Description = data.Description;
                dbMovies.Price = data.Price;
                dbMovies.ImageURL = data.ImageURL;
                dbMovies.StartDate = data.StartDate;
                dbMovies.EndDate = data.EndDate;
                dbMovies.MovieCategory = data.MovieCategory;
                //dbMovies.MovieDetailId = newMovieDetail.Id;
                await _context.SaveChangesAsync();
            }

            var existingActorsDb = _context.MovieActors.Where(n=>n.MovieId == data.Id).ToList();
            _context.MovieActors.RemoveRange(existingActorsDb);
            foreach (var actorId in data.ActorIds)
            {
                var newMovieActor = new MovieActor()
                {
                    ActorId = actorId,
                    MovieId = data.Id
                };
                await _context.MovieActors.AddAsync(newMovieActor);
            }
            await _context.SaveChangesAsync();
            
            var existingCinemasDb = _context.MovieCinemas.Where(n=>n.MovieId == data.Id).ToList();
            _context.MovieCinemas.RemoveRange(existingCinemasDb);
            foreach (var cinemaId in data.CinemaIds)
            {
                var newMovieCinema = new MovieCinema()
                {
                    CinemaId = cinemaId,
                    MovieId = data.Id
                };
                await _context.MovieCinemas.AddAsync(newMovieCinema);
            }
            await _context.SaveChangesAsync();






            //var newMovieDetail = new MovieDetail()
            //{
            //    //Id = newMovie.Id,
            //    Rank = data.Rank,
            //    ProducerId = data.ProducerId,
            //    TrailerPath = data.TrailerPath,

            //};
            //await _context.MovieDetails.AddAsync(newMovieDetail);
            //await _context.SaveChangesAsync();
            //var newMovie = new Movie()
            //{
            //    Name = data.Name,
            //    Description = data.Description,
            //    Price = data.Price,
            //    ImageURL = data.ImageURL,
            //    StartDate = data.StartDate,
            //    EndDate = data.EndDate,
            //    MovieCategory = data.MovieCategory,
            //    MovieDetailId = newMovieDetail.Id
            //};
            //await _context.Movies.AddAsync(newMovie);
            //await _context.SaveChangesAsync();

            //foreach (var actorId in data.ActorIds)
            //{
            //    var newMovieActor = new MovieActor()
            //    {
            //        ActorId = actorId,
            //        MovieId = newMovie.Id
            //    };
            //    await _context.MovieActors.AddAsync(newMovieActor);
            //}
            //await _context.SaveChangesAsync();

            //foreach (var cinemaId in data.CinemaIds)
            //{
            //    var newMovieCinema = new MovieCinema()
            //    {
            //        CinemaId = cinemaId,
            //        MovieId = newMovie.Id
            //    };
            //    await _context.MovieCinemas.AddAsync(newMovieCinema);
            //}
            //await _context.SaveChangesAsync();



            //List<Picture> MyPics = new List<Picture>();
            if (data.Galleries?.Sum(f => f.Length) > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "galleries\\");

                foreach(var movie in dbMovieDetails.Pictures)
                {
                    string imgUrl = movie.PicUrl;
                    string fileDel = filePath + imgUrl;
                    if (System.IO.File.Exists(fileDel))
                    {
                        System.IO.File.Delete(fileDel);
                    }
                }


                //Picture MyPic = new Picture();
                foreach (var pic in data.Galleries)
                {
                    var newPic = new Picture();
                    newPic.MovieDetailId = dbMovieDetails.Id;
                    string fileName = Guid.NewGuid().ToString() + "_" + pic.FileName;
                    newPic.PicName = fileName;
                    string path = filePath + fileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        pic.CopyTo(stream);
                    }
                    newPic.PicUrl = "/images/galleries/" + fileName;
                    await _context.Pictures.AddAsync(newPic);
                    await _context.SaveChangesAsync();
                    dbMovieDetails.Pictures.Add(newPic);
                }
            }
        }
    }
}
