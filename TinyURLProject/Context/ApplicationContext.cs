using Microsoft.EntityFrameworkCore;
using TinyURLProject.Model;

namespace TinyURLProject.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) 
        {
            
        }
        public DbSet<UrlMapping>UrlMappings { get; set; }

    }
}
