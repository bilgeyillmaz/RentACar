using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class RentACarDBContext: DbContext
    {
        public RentACarDBContext(DbContextOptions<RentACarDBContext> options) : base(options){ }
        public DbSet<Deneme> Denemeler { get; set; }   
        
    }
}
