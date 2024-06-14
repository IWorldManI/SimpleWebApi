using SimpleWebApi.Models;

namespace SimpleWebApi.Repositories
{
    /// <summary>
    /// Static repository for storing articles and orders.
    /// </summary>
    public static class StaticRepository
    {
        /// <summary>
        /// Gets or sets the list of articles.
        /// </summary>
        public static List<Article> Articles { get; set; } = new List<Article>();

        /// <summary>
        /// Gets or sets the list of orders.
        /// </summary>
        public static List<Order> Orders { get; set; } = new List<Order>();
    }
}
