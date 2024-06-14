using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Models;
using SimpleWebApi.Repositories;

namespace SimpleWebApi.Controllers
{
    /// <summary>
    /// Controller for managing articles.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        /// <summary>
        /// Retrieves the list of all articles.
        /// </summary>
        /// <returns>A list of articles.</returns>
        [HttpGet]
        public IActionResult GetArticles()
        {
            return Ok(StaticRepository.Articles);
        }

        /// <summary>
        /// Adds a new article to the list.
        /// </summary>
        /// <param name="article">The article to add.</param>
        /// <returns>The created article with its ID.</returns>
        [HttpPost]
        public IActionResult AddArticle([FromBody] Article article)
        {
            article.Id = StaticRepository.Articles.Count > 0 ? StaticRepository.Articles.Max(a => a.Id) + 1 : 1;
            StaticRepository.Articles.Add(article);
            return CreatedAtAction(nameof(GetArticles), new { id = article.Id }, article);
        }

        /// <summary>
        /// Deletes an article by ID.
        /// </summary>
        /// <param name="id">The ID of the article to delete.</param>
        /// <returns>No content if successful; otherwise, not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            var article = StaticRepository.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
                return NotFound();

            StaticRepository.Articles.Remove(article);
            return NoContent();
        }
    }
}
