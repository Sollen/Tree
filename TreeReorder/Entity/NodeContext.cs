using Microsoft.EntityFrameworkCore;
using TreeReorder.Models;

namespace TreeReorder.Entity
{
    public class NodeContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if(!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=node;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<FileNode> FileNodes { get; set; }
        public DbSet<FolderNode> FolderNodes { get; set; }
        

    }
}
