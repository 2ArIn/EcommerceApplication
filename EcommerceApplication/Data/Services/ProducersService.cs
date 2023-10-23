using EcommerceApplication.Data.Base;
using EcommerceApplication.Models;

namespace EcommerceApplication.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>,IProducersService
    {
        public ProducersService(AppDbContext context):base(context)
        {
            
        }
    }
}
