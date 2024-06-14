using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Repositories;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers
{
    /// <summary>
    /// Controller for managing orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Retrieves the list of all orders.
        /// </summary>
        /// <returns>A list of orders.</returns>
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(StaticRepository.Orders);
        }

        /// <summary>
        /// Adds a new order to the list.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>The created order with its ID.</returns>
        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            if (order.ArticleIds == null || !order.ArticleIds.Any())
            {
                return BadRequest("Order must contain at least one article.");
            }

            foreach (var articleId in order.ArticleIds)
            {
                if (!StaticRepository.Articles.Any(a => a.Id == articleId))
                {
                    return BadRequest($"Article with ID {articleId} does not exist.");
                }
            }

            order.Id = StaticRepository.Orders.Count > 0 ? StaticRepository.Orders.Max(o => o.Id) + 1 : 1;
            order.OrderDate = DateTime.Now;
            StaticRepository.Orders.Add(order);
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        /// <summary>
        /// Deletes an order by ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>No content if successful; otherwise, not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = StaticRepository.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            StaticRepository.Orders.Remove(order);
            return NoContent();
        }
    }
}
