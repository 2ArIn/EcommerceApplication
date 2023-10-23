using EcommerceApplication.Models;

namespace EcommerceApplication.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                
                //Cinema
                if(!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name="Cinema 1",
                            Description = "Cinema 1 Description",
                            Logo = "/Images/Cinemas/1.jpg"
                        },
                        new Cinema()
                        {
                            Name="Cinema 2",
                            Description = "Cinema 2 Description",
                            Logo = "/Images/Cinemas/2.jpg"
                        },
                        new Cinema()
                        {
                            Name="Cinema 3",
                            Description = "Cinema 3 Description",
                            Logo = "/Images/Cinemas/3.jpg"
                        }, 
                        new Cinema()
                        {
                            Name="Cinema 4",
                            Description = "Cinema 4 Description",
                            Logo = "/Images/Cinemas/4.jpg"
                        }
                    });
                    context.SaveChanges();
                }
                //Actors
                if(!context.Actors.Any())
                {
                    context.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Ahmad Mehranfar",
                            Bio = "This is Bio ",
                            ProfilePictureURL = "/images/Actors/ahmad.jpg",

                        }
                        ,new Actor()
                        {
                            FullName = "Mohammad Reza Golzar",
                            Bio = "This is Bio ",
                            ProfilePictureURL = "/images/Actors/mreza.jpg",

                        }
                        ,new Actor()
                        {
                            FullName = "Shahab Hosseini",
                            Bio = "This is Bio ",
                            ProfilePictureURL = "/images/Actors/shahab.jpg",

                        }
                        ,new Actor()
                        {
                            FullName = "Leila Hatami",
                            Bio = "This is Bio ",
                            ProfilePictureURL = "/images/Actors/leila.jpg",

                        }
                        ,new Actor()
                        {
                            FullName = "Amin Haiaiee",
                            Bio = "This is Bio ",
                            ProfilePictureURL = "/images/Actors/amin.jpg",

                        }
                    });
                    context.SaveChanges();

                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Esmail Affifeh",
                            Bio = "This is Bio",
                            ProfilePictureURL = "/images/producers/esmail.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Javad Norouzbeigi",
                            Bio = "This is Bio",
                            ProfilePictureURL = "/images/Producers/javad.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Martin Scorsissi",
                            Bio = "Thisi is Bio",
                            ProfilePictureURL = "/images/Producers/martin.jpg"
                        }

                    });
                    context.SaveChanges();

                }
                //if (!context.Pictures.Any())
                //{
                //    context.AddRange(new List<Picture>()
                //    {
                //        new Picture()
                //        {
                //            PicName = "f1",
                //            PicUrl = "Images/Movies/Gallery/f1.jpg",
                //            MainPic = true,
                //            MovieDetailId = 1,

                //        }
                //        ,new Picture()
                //        {
                //            PicName = "f2",
                //            PicUrl = "Images/Movies/Gallery/f2.jpg",
                //            MainPic = false,
                //            MovieDetailId = 1

                //        }
                //        ,new Picture()
                //        {
                //            PicName = "f3",
                //            PicUrl = "Images/Movies/Gallery/f3.jpg",
                //            MainPic = false,
                //            MovieDetailId = 1

                //        },
                //    });
                //    context.SaveChanges();
                //}
                //MovieDetail
                if (!context.MovieDetails.Any())
                {
                    context.AddRange(new List<MovieDetail>()
                    {
                        new MovieDetail()
                        {
                            //Id = 1,
                            Rank = 9.8f,
                            TrailerPath = "/Videos/MoviesTrailer/1.mp4",
                            ProducerId = 1,
                            Pictures = new List<Picture>()
                            {
                                new Picture()
                                {
                                    PicName = "f1",
                                    PicUrl = "Images/Movies/Gallery/f1.jpg",
                                    MainPic = true,
                                    MovieDetailId = 1,
                                },
                                new Picture()
                                {
                                    PicName = "f2",
                                    PicUrl = "Images/Movies/Gallery/f2.jpg",
                                    MainPic = false,
                                    MovieDetailId = 1
                                },
                                new Picture()
                                {
                                    PicName = "f3",
                                    PicUrl = "Images/Movies/Gallery/f3.jpg",
                                    MainPic = false,
                                    MovieDetailId = 1
                                }
                            }
                        }

                        ,new MovieDetail()
                        {
                            //Id = 2,
                            Rank = 4.5f,
                            TrailerPath = "/Videos/MoviesTrailer/2.mp4",
                            ProducerId=1,

                        }
                        ,new MovieDetail()
                        {
                            //Id = 3,
                            Rank = 5.6f,
                            TrailerPath = "/Videos/MoviesTrailer/3.mp4",
                            ProducerId = 1
                        }
                        ,new MovieDetail()
                        {
                            //Id = 4,
                            Rank = 6f,
                            TrailerPath = "/Videos/MoviesTrailer/4.mp4",
                            ProducerId=2

                        }
                        ,new MovieDetail()
                        {
                            //Id =5,
                            Rank = 7.8f,
                            TrailerPath = "/Videos/MoviesTrailer/5.mp4",
                            ProducerId=3

                        }
                    });   
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any()) {
                    context.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name="Fosil",
                            Description = "Fosil",
                            MovieDetailId = 1,
                            MovieCategory = enums.MovieCategory.Comedy,
                            StartDate = DateTime.Now,
                            EndDate= DateTime.Parse("2023/12/29"),
                            Price = 30000.0M,
                            ImageURL = "/Images/Movies/fosil.jpg"
                            
                        },
                        new Movie()
                        {
                            Name="Ghost",
                            Description = "Ghost",
                            MovieDetailId = 2,
                            MovieCategory = enums.MovieCategory.Drama,
                            StartDate = DateTime.Now,
                            EndDate= DateTime.Parse("2024/06/29"),
                            ImageURL = "/Images/Movies/ghost.jpg",
                            Price = 30000.0M,
                        },
                        new Movie()
                        {
                            Name="I'm Legend",
                            Description = "Legend",
                            MovieDetailId = 3,
                            MovieCategory = enums.MovieCategory.Action,
                            StartDate = DateTime.Parse("2021/12/29"),
                            EndDate= DateTime.Parse("2022/12/29"),
                            ImageURL = "/Images/Movies/legend.jpg",
                            Price = 30000.0M,
                        },
                        new Movie()
                        {
                            Name="Seperation",
                            Description = "Seperation",
                            MovieDetailId = 4,
                            MovieCategory = enums.MovieCategory.Drama,                            
                            StartDate = DateTime.Parse("2021/12/29"),
                            EndDate= DateTime.Parse("2024/12/29"),
                            Price = 30000.0M,
                            ImageURL = "/Images/Movies/seperation.jpg"
                        },
                        new Movie()
                        {
                            Name="UP",
                            Description = "UP",
                            MovieDetailId = 5,
                            MovieCategory = enums.MovieCategory.Anime,                           
                            StartDate = DateTime.Now,
                            EndDate= DateTime.Parse("2023/12/29"),
                            Price = 30000.0M,
                            ImageURL = "/Images/Movies/up.jpg"
                        },
                    });
                    context.SaveChanges();

                }
                
                //MovieActor
                
                if (!context.MovieActors.Any())
                {
                    context.AddRange(new List<MovieActor>()
                    {
                        new MovieActor()
                        {
                            MovieId = 1,
                            ActorId = 1
                        }
                        ,new MovieActor()
                        {
                            MovieId = 1,
                            ActorId =2 
                        }
                        ,new MovieActor()
                        {
                            MovieId = 1,
                            ActorId = 3
                        }
                        ,new MovieActor()
                        {
                            MovieId = 2,
                            ActorId = 4
                        }
                        ,new MovieActor()
                        {
                            MovieId =3 ,
                            ActorId = 5
                        }
                        ,new MovieActor()
                        {
                            MovieId = 4,
                            ActorId =4 
                        }
                        ,new MovieActor()
                        {
                            MovieId = 4,
                            ActorId = 5
                        }
                        ,new MovieActor()
                        {
                            MovieId =5 ,
                            ActorId = 1
                        }
                        ,new MovieActor()
                        {
                            MovieId = 5,
                            ActorId = 2
                        }
                        ,new MovieActor()
                        {
                            MovieId = 5,
                            ActorId = 5
                        }
                    });
                    context.SaveChanges();

                }
                if (!context.MovieCinemas.Any())
                {
                    context.AddRange(new List<MovieCinema>()
                    {
                        new MovieCinema()
                        {
                            MovieId = 1,
                            CinemaId = 1
                        }
                        , new MovieCinema()
                        {
                            MovieId =1 ,
                            CinemaId = 2
                        }
                        , new MovieCinema()
                        {
                            MovieId =2 ,
                            CinemaId =2 
                        }
                        , new MovieCinema()
                        {
                            MovieId = 2,
                            CinemaId = 3
                        }
                        , new MovieCinema()
                        {
                            MovieId =2 ,
                            CinemaId = 4
                        }
                        , new MovieCinema()
                        {
                            MovieId = 3,
                            CinemaId = 1
                        }
                        , new MovieCinema()
                        {
                            MovieId = 3,
                            CinemaId = 2
                        }
                        , new MovieCinema()
                        {
                            MovieId = 4,
                            CinemaId = 4
                        }
                        , new MovieCinema()
                        {
                            MovieId = 5,
                            CinemaId = 2
                        }
                        , new MovieCinema()
                        {
                            MovieId = 5,
                            CinemaId = 3
                        }
                    });
                    context.SaveChanges();

                }
               

                

            }
        }
    }
}
