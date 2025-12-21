using System.Text.Json;
using management_delegate.Models;

namespace management_delegate.Services
{
    public class DataManager
    {
        private const string UsersFile = "users.json";
        private const string ProductsFile = "products.json";

        public List<User> LoadUsers()
        {
            if (!File.Exists(UsersFile))
            {
                var defaultUsers = new List<User>();
                SaveUsers(defaultUsers);
                return defaultUsers;
            }
            var json = File.ReadAllText(UsersFile);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UsersFile, json);
        }

        public List<Product> LoadProducts()
        {
            if (!File.Exists(ProductsFile))
            {
                var defaultProducts = new List<Product>
                {
                    new Product { Id = 1, Name = "Marqarita", Ingredients = new List<string> { "Pomidor sousu", "Mozzarella pendiri", "Reyhan", "Zeytun ya??" }, Price = 12.50m },
                    new Product { Id = 2, Name = "Pepperoni", Ingredients = new List<string> { "Pomidor sousu", "Mozzarella", "Pepperoni kolbasa", "?dviyyat" }, Price = 15.00m },
                    new Product { Id = 3, Name = "Vegetarian", Ingredients = new List<string> { "Pomidor sousu", "Mozzarella", "Göb?l?k", "Ya??l bib?r", "Qara zeytun", "Pomidor" }, Price = 13.50m },
                    new Product { Id = 4, Name = "Quattro Formaggi", Ingredients = new List<string> { "A? sous", "Mozzarella", "Gorgonzola", "Parmesan", "Ricotta" }, Price = 16.00m },
                    new Product { Id = 5, Name = "Hawaiian", Ingredients = new List<string> { "Pomidor sousu", "Mozzarella", "Vetçina", "Ananas", "Qar??dal?" }, Price = 14.50m }
                };
                SaveProducts(defaultProducts);
                return defaultProducts;
            }
            var json = File.ReadAllText(ProductsFile);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ProductsFile, json);
        }
    }
}
