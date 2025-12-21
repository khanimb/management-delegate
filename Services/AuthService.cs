using management_delegate.Helpers;
using management_delegate.Models;

namespace management_delegate.Services
{
    public class AuthService
    {
        private readonly DataManager _dataManager;
        private List<User> _users;

        public AuthService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _users = _dataManager.LoadUsers();
        }

        public User Register()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?         Qeydiyyat              ?");
            Console.WriteLine("??????????????????????????????????");

            Console.Write("Ad?n?z: ");
            var name = Console.ReadLine();

            Console.Write("Soyad?n?z: ");
            var surname = Console.ReadLine();

            string username;
            do
            {
                Console.Write("Login (3-16 simvol): ");
                username = Console.ReadLine();
                if (!Validator.ValidateUsername(username))
                {
                    Console.WriteLine("? Login 3-16 uzunluqda olmal?d?r!");
                    username = null;
                }
                else if (_users.Any(u => u.Username == username))
                {
                    Console.WriteLine("? Bu login art?q mövcuddur!");
                    username = null;
                }
            } while (string.IsNullOrEmpty(username));

            string password;
            do
            {
                Console.Write("Parol (6-16 simvol, ?n az? 1 böyük, 1 kiçik h?rf v? 1 r?q?m): ");
                password = Console.ReadLine();
                if (!Validator.ValidatePassword(password))
                {
                    Console.WriteLine("? Parol t?l?bl?r? uy?un deyil!");
                    password = null;
                }
            } while (string.IsNullOrEmpty(password));

            var newUser = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Name = name,
                Surname = surname,
                Username = username,
                Password = password,
                IsAdmin = false
            };

            _users.Add(newUser);
            _dataManager.SaveUsers(_users);

            Console.WriteLine("\n? Qeydiyyat u?urla tamamland?!");
            Console.ReadKey();
            return null;
        }

        public User Login()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?           Login                ?");
            Console.WriteLine("??????????????????????????????????");

            Console.Write("Username: ");
            var username = Console.ReadLine();

            Console.Write("Parol: ");
            var password = Console.ReadLine();

            _users = _dataManager.LoadUsers();
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"\n? Xo? g?ldiniz, {user.Name} {user.Surname}");
                Console.ReadKey();
                return user;
            }
            else
            {
                Console.WriteLine("\n? Username v? ya parol yanl??d?r!");
                Console.ReadKey();
                return null;
            }
        }
    }
}
