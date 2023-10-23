using EcommerceApplication.Data.Base;
using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>,IActorService
    {
        
        public ActorService(AppDbContext context) : base(context) { }

    }
}
