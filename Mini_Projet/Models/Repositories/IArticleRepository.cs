namespace Mini_Projet.Models.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticleById(int articleId);
        Task<Article> AddArticle(Article article);
        Task<Article> UpdateArticle(Article article);
        Task<Article> DeleteArticle(int articleId);
        Task<Article> GetArticleByName(string name);
    }
}
