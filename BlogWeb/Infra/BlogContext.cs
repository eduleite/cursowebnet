using BlogWeb.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace BlogWeb.Infra
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogContext() : base("name=blog")
        {
            Database.Log = PrintLogMessage;
        }

        private void PrintLogMessage(string message)
        {
            Debug.WriteLine(message);
        }

    }

}