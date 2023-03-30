using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class CSharpBookDbContext : DbContext  
    {  
        public CSharpBookDbContext(DbContextOptions<CSharpBookDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<CSharpBook> CSharpBooks { get; set; }  
    }  
}  