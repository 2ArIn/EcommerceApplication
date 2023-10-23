using EcommerceApplication.Data.Base;
using EcommerceApplication.Models;

namespace EcommerceApplication.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>,ICinemasService
    {
        public CinemasService(AppDbContext context):base(context)
        {
            
        }
    }
}
