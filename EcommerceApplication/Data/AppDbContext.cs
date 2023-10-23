using EcommerceApplication.Data.enums;
using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(ma => new
            {
                ma.MovieId,
                ma.ActorId
            });
            modelBuilder.Entity<MovieActor>().HasOne(m => m.Movie).WithMany(ma => ma.MovieActors).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieActor>().HasOne(m => m.Actor).WithMany(ma => ma.MovieActors).HasForeignKey(m => m.ActorId);

            modelBuilder.Entity<MovieCinema>().HasKey(mc => new
            {
                mc.MovieId,
                mc.CinemaId,
            });
            modelBuilder.Entity<MovieCinema>().HasOne(m => m.Movie).WithMany(mc => mc.MovieCinemas).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieCinema>().HasOne(m => m.Cinema).WithMany(mc => mc.MovieCinemas).HasForeignKey(m => m.CinemaId);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ErrorMessage>().HasNoKey();


        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCinema> MovieCinemas { get; set; }
        public DbSet<MovieDetail> MovieDetails { get; set; }
        public DbSet<Picture> Pictures { set; get; }
        //public DbSet<ErrorMessage> Errors { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
