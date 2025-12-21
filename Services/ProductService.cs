using management_delegate.Models;

namespace management_delegate.Services
{
    public class ProductService
    {
        private readonly DataManager _dataManager;
        private List<Product> _products;

        public ProductService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _products = _dataManager.LoadProducts();
        }

        public void ShowProducts(List<OrderItem> cart)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("??????????????????????????????????");
                Console.WriteLine("?      Products Menyusu          ?");
                Console.WriteLine("??????????????????????????????????");
                _products = _dataManager.LoadProducts();

                foreach (var product in _products)
                {
                    Console.WriteLine($"{product.Id}. {product.Name} - {product.Price} AZN");
                }

                Console.WriteLine("\n0. Geri");
                Console.Write("\nPizzan?n ID-sini daxil edin: ");
                var input = Console.ReadLine();

                if (input == "0")
                    return;

                if (int.TryParse(input, out int id))
                {
                    var product = _products.FirstOrDefault(p => p.Id == id);
                    if (product != null)
                    {
                        ShowProductDetails(product, cart);
                    }
                    else
                    {
                        Console.WriteLine("? Bel? bir pizza tap?lmad?!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("? Düzgün ID daxil edin!");
                    Console.ReadKey();
                }
            }
        }

        private void ShowProductDetails(Product product, List<OrderItem> cart)
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????????????");
            Console.WriteLine($"?  {product.Name}");
            Console.WriteLine("??????????????????????????????????????????");
            Console.WriteLine("\n?? ?nqredientl?r:");
            for (int i = 0; i < product.Ingredients.Count; i++)
            {
                Console.WriteLine($"   {i + 1}. {product.Ingredients[i]}");
            }
            Console.WriteLine($"\n?? Qiym?t: {product.Price} AZN");

            Console.WriteLine("\n[S] - S?b?t? ?lav? et");
            Console.WriteLine("[G] - Geri");
            Console.Write("\nSeçim: ");

            var choice = Console.ReadLine()?.ToUpper();

            if (choice == "S")
            {
                Console.Write("Neç? ?d?d: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    var existingItem = cart.FirstOrDefault(c => c.Product.Id == product.Id);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        cart.Add(new OrderItem { Product = product, Quantity = quantity });
                    }
                    Console.WriteLine("? S?b?t? ?lav? edildi!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("? Düzgün say daxil edin!");
                    Console.ReadKey();
                }
            }
        }

        public void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????????????");
            Console.WriteLine("?        Bütün Pizzalar                  ?");
            Console.WriteLine("??????????????????????????????????????????");
            _products = _dataManager.LoadProducts();

            foreach (var product in _products)
            {
                Console.WriteLine($"\n?? ID: {product.Id}");
                Console.WriteLine($"?? Ad: {product.Name}");
                Console.WriteLine($"?? Qiym?t: {product.Price} AZN");
                Console.WriteLine($"?? ?nqredientl?r: {string.Join(", ", product.Ingredients)}");
                Console.WriteLine("?????????????????????????????????");
            }

            Console.ReadKey();
        }

        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?      Pizza ?lav? Et            ?");
            Console.WriteLine("??????????????????????????????????");

            Console.Write("Pizza ad?: ");
            var name = Console.ReadLine();

            Console.Write("Qiym?t (AZN): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("? Yanl?? qiym?t!");
                Console.ReadKey();
                return;
            }

            var ingredients = new List<string>();
            Console.WriteLine("\n?nqredientl?r (bo? saxlamaq üçün Enter bas?n):");
            while (true)
            {
                Console.Write($"?nqredient {ingredients.Count + 1}: ");
                var ingredient = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingredient))
                    break;
                ingredients.Add(ingredient);
            }

            if (ingredients.Count == 0)
            {
                Console.WriteLine("? ?n az? 1 inqredient ?lav? edin!");
                Console.ReadKey();
                return;
            }

            var newProduct = new Product
            {
                Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1,
                Name = name,
                Price = price,
                Ingredients = ingredients
            };

            _products.Add(newProduct);
            _dataManager.SaveProducts(_products);

            Console.WriteLine("\n? Pizza ?lav? edildi!");
            Console.ReadKey();
        }

        public void EditProduct()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?      Pizza Düz?li? Et          ?");
            Console.WriteLine("??????????????????????????????????");
            Console.Write("Pizza ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    Console.Write($"Yeni ad [{product.Name}]: ");
                    var name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                        product.Name = name;

                    Console.Write($"Yeni qiym?t [{product.Price}]: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                        product.Price = price;

                    _dataManager.SaveProducts(_products);
                    Console.WriteLine("\n? D?yi?iklikl?r yadda saxlan?ld?!");
                }
                else
                {
                    Console.WriteLine("? Pizza tap?lmad?!");
                }
            }
            else
            {
                Console.WriteLine("? Düzgün ID daxil edin!");
            }

            Console.ReadKey();
        }

        public void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?         Pizza Sil              ?");
            Console.WriteLine("??????????????????????????????????");
            Console.Write("Pizza ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    _products.Remove(product);
                    _dataManager.SaveProducts(_products);
                    Console.WriteLine("\n? Pizza silindi!");
                }
                else
                {
                    Console.WriteLine("? Pizza tap?lmad?!");
                }
            }
            else
            {
                Console.WriteLine("? Düzgün ID daxil edin!");
            }

            Console.ReadKey();
        }
    }
}
