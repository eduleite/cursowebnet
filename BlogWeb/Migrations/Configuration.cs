namespace BlogWeb.Migrations
{
    using BlogWeb.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogWeb.Infra.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogWeb.Infra.BlogContext context)
        {
            /*
            var posts = new List<Post>
            {
                new Post
                {
                    Titulo = "Harry Potter 1",
                    Resumo = "Pedra Filosofal",
                    Categoria = "Filmes"
                },
                new Post
                {
                    Titulo = "Harry Potter 2",
                    Resumo = "A Camara Secreta",
                    Categoria = "Filmes"
                }
            };

            posts.ForEach(post => context.Posts.Add(post));
            */
        }
    }
}
