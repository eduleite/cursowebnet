using BlogWeb.Infra;
using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWeb.DAO
{
    public class PostDaoEF
    {

        private BlogContext ctx;

        public PostDaoEF()
        {
            ctx = new BlogContext();
        }

        public IList<Post> Lista()
        {
            return ctx.Posts.ToList();
        }

        public void Incluir(Post post)
        {
            ctx.Posts.Add(post);
            ctx.SaveChanges();
        }

        public void Excluir(int id)
        {
            var post = ctx.Posts.First(p => p.Id == id);
            ctx.Posts.Remove(post);
            ctx.SaveChanges();
        }

        public void Atualizar(Post post)
        {
            ctx.Entry(post).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Post BuscaPorId(int id)
        {
            return ctx.Posts.FirstOrDefault(p => p.Id == id);
        }

        public IList<Post> Listar()
        {
            return ctx.Posts.ToList();
        }

    }
}