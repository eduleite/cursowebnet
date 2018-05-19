using BlogWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Diagnostics;

namespace BlogWeb.Infra
{
    public class BlogContext : IdentityDbContext
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