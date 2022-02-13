using Microsoft.EntityFrameworkCore;
using todolist.Models;
namespace todolist.Data
{
    public class todolistDBContext : DbContext
    {
        //we'll setup the database from the Program.cs (i also pass options to the inherited class constructor)
        public todolistDBContext(DbContextOptions<todolistDBContext> options) : base(options){}

        //i setup a table of activities
        public DbSet<Activity> Activities {get; set;}

        
    }
}