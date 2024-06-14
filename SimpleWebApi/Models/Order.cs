namespace SimpleWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<int> ArticleIds { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
