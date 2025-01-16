using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Mini_Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult> GetArticles()
        {
            try
            {
                var articles = await articleRepository.GetArticles();
                return Ok(articles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET: api/Article/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            try
            {
                var article = await articleRepository.GetArticleById(id);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(article);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // POST: api/Article
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            try
            {
                if (article == null)
                {
                    return BadRequest();
                }

                var createdArticle = await articleRepository.AddArticle(article);
                return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database");
            }
        }

        // PUT: api/Article/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            try
            {
                if (id != article.Id)
                    return BadRequest();

                var updatedArticle = await articleRepository.UpdateArticle(article);
                if (updatedArticle == null)
                {
                    return NotFound();
                }

                return Ok(updatedArticle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database");
            }
        }

        // DELETE: api/Article/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            try
            {
                var articleToDelete = await articleRepository.DeleteArticle(id);
                if (articleToDelete == null)
                {
                    return NotFound();
                }

                return Ok(articleToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        // GET: api/Article/name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Article>> GetArticleByName(string name)
        {
            try
            {
                var article = await articleRepository.GetArticleByName(name);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(article);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
