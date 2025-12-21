namespace management_delegate.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public decimal Price { get; set; }
    }
}
