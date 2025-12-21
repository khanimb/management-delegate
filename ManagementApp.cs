using management_delegate.Models;
using management_delegate.Services;

namespace management_delegate
{
    public class ManagementApp
    {
        private readonly DataManager _dataManager;
        private readonly AuthService _authService;
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly UserService _userService;
        private User _currentUser;
        private readonly List<OrderItem> _cart = new();

        public ManagementApp()
        {
            _dataManager = new DataManager();
            _authService = new AuthService(_dataManager);
            _productService = new ProductService(_dataManager);
            _orderService = new OrderService();
            _userService = new UserService(_dataManager);
        }

        public void Run()
        {
            while (true)
            {
                ShowLoginMenu();
            }
        }

        private void ShowLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?      Login Menyusu             ?");
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Qeydiyyat");
            Console.WriteLine("0. Ç?x??");
            Console.Write("\nSeçim: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _currentUser = _authService.Login();
                    if (_currentUser != null)
                        ShowUserMenu();
                    break;
                case "2":
                    _authService.Register();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("? Yanl?? seçim!");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowUserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("??????????????????????????????????????????");
                Console.WriteLine($"?  User Menyusu - {_currentUser.Name} {_currentUser.Surname}");
                Console.WriteLine("??????????????????????????????????????????");
                Console.WriteLine("1. Pizzalara bax");
                Console.WriteLine("2. Sifari? ver");
                Console.WriteLine("3. Ç?x??");

                if (_currentUser.IsAdmin)
                {
                    Console.WriteLine("\n--- Admin Menyusu ---");
                    Console.WriteLine("4. Pizzalar (CRUD)");
                    Console.WriteLine("5. Userl?r (CRUD)");
                }

                Console.Write("\nSeçim: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _productService.ShowProducts(_cart);
                        break;
                    case "2":
                        _orderService.PlaceOrder(_cart);
                        break;
                    case "3":
                        _cart.Clear();
                        _currentUser = null;
                        return;
                    case "4" when _currentUser.IsAdmin:
                        ManageProducts();
                        break;
                    case "5" when _currentUser.IsAdmin:
                        ManageUsers();
                        break;
                    default:
                        Console.WriteLine("? Yanl?? seçim!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageProducts()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("??????????????????????????????????");
                Console.WriteLine("?      Pizzalar CRUD             ?");
                Console.WriteLine("??????????????????????????????????");
                Console.WriteLine("1. Ham?s?na bax");
                Console.WriteLine("2. ?lav? et");
                Console.WriteLine("3. Düz?li? et (Id-? gör?)");
                Console.WriteLine("4. Sil (Id-? gör?)");
                Console.WriteLine("0. Geri");
                Console.Write("\nSeçim: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _productService.ViewAllProducts();
                        break;
                    case "2":
                        _productService.AddProduct();
                        break;
                    case "3":
                        _productService.EditProduct();
                        break;
                    case "4":
                        _productService.DeleteProduct();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("? Yanl?? seçim!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("??????????????????????????????????");
                Console.WriteLine("?       Userl?r CRUD             ?");
                Console.WriteLine("??????????????????????????????????");
                Console.WriteLine("1. Ham?s?na bax");
                Console.WriteLine("2. ?lav? et (Admin)");
                Console.WriteLine("3. Düz?li? et (Rol d?yi?)");
                Console.WriteLine("4. Sil (Id-? gör?)");
                Console.WriteLine("0. Geri");
                Console.Write("\nSeçim: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _userService.ViewAllUsers();
                        break;
                    case "2":
                        _userService.AddAdmin();
                        break;
                    case "3":
                        _userService.ChangeUserRole();
                        break;
                    case "4":
                        _userService.DeleteUser(_currentUser.Id);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("? Yanl?? seçim!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
