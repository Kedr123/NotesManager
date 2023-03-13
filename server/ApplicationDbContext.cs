using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Models.File> Files { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Models.List> Lists { get; set; }

        public DbSet<Column> Columns { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteFile> NoteFiles { get; set; }


    }
}
