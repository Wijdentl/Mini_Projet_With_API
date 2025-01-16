using Mini_Projet.Context;
using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.EntityFrameworkCore;

namespace Mini_Projet.Models.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly AppDbContext appDbContext;
        public ArticleRepository(AppDbContext myappDbContext)
        {
            this.appDbContext = myappDbContext;
        }
        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await appDbContext.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            return await appDbContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        }

        public async Task<Article> AddArticle(Article article)
        {
            var result = await appDbContext.Articles.AddAsync(article);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            var existingArticle = await appDbContext.Articles.FirstOrDefaultAsync(a => a.Id == article.Id);
            if (existingArticle != null)
            {
                existingArticle.Nom = article.Nom;
                existingArticle.Type = article.Type;
                existingArticle.Prix = article.Prix;
                existingArticle.Garantie = article.Garantie;

                await appDbContext.SaveChangesAsync();
                return existingArticle;
            }
            return null;
        }

        public async Task<Article> DeleteArticle(int articleId)
        {
            var article = await appDbContext.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
            if (article != null)
            {
                appDbContext.Articles.Remove(article);
                await appDbContext.SaveChangesAsync();
                return article;
            }
            return null;
        }

        public async Task<Article> GetArticleByName(string name)
        {
            return await appDbContext.Articles.FirstOrDefaultAsync(a => a.Nom == name);
        }
    }
}