using management_delegate.Helpers;
using management_delegate.Models;

namespace management_delegate.Services
{
    public class UserService
    {
        private readonly DataManager _dataManager;
        private List<User> _users;

        public UserService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _users = _dataManager.LoadUsers();
        }

        public void ViewAllUsers()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????????????");
            Console.WriteLine("?      Bütün ?stifad?çil?r               ?");
            Console.WriteLine("??????????????????????????????????????????");
            _users = _dataManager.LoadUsers();

            foreach (var user in _users)
            {
                Console.WriteLine($"\n?? ID: {user.Id}");
                Console.WriteLine($"?? Ad Soyad: {user.Name} {user.Surname}");
                Console.WriteLine($"?? Username: {user.Username}");
                Console.WriteLine($"?? Rol: {(user.IsAdmin ? "Admin" : "?stifad?çi")}");
                Console.WriteLine("?????????????????????????????????");
            }

            Console.ReadKey();
        }

        public void AddAdmin()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?       Admin ?lav? Et           ?");
            Console.WriteLine("??????????????????????????????????");

            Console.Write("Ad?: ");
            var name = Console.ReadLine();

            Console.Write("Soyad?: ");
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
                Console.Write("Parol (6-16 simvol, 1 böyük, 1 kiçik h?rf, 1 r?q?m): ");
                password = Console.ReadLine();
                if (!Validator.ValidatePassword(password))
                {
                    Console.WriteLine("? Parol t?l?bl?r? uy?un deyil!");
                    password = null;
                }
            } while (string.IsNullOrEmpty(password));

            var newAdmin = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Name = name,
                Surname = surname,
                Username = username,
                Password = password,
                IsAdmin = true
            };

            _users.Add(newAdmin);
            _dataManager.SaveUsers(_users);

            Console.WriteLine("\n? Admin ?lav? edildi!");
            Console.ReadKey();
        }

        public void ChangeUserRole()
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?   ?stifad?çi Rolu D?yi?        ?");
            Console.WriteLine("??????????????????????????????????");
            Console.Write("?stifad?çi ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.IsAdmin = !user.IsAdmin;
                    _dataManager.SaveUsers(_users);
                    Console.WriteLine($"\n? {user.Name} {user.Surname} indi {(user.IsAdmin ? "Admin" : "?stifad?çi")} rolundad?r!");
                }
                else
                {
                    Console.WriteLine("? ?stifad?çi tap?lmad?!");
                }
            }
            else
            {
                Console.WriteLine("? Düzgün ID daxil edin!");
            }

            Console.ReadKey();
        }

        public void DeleteUser(int currentUserId)
        {
            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?      ?stifad?çi Sil            ?");
            Console.WriteLine("??????????????????????????????????");
            Console.Write("?stifad?çi ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    if (user.Id == currentUserId)
                    {
                        Console.WriteLine("? Özünüzü sil? bilm?zsiniz!");
                    }
                    else
                    {
                        _users.Remove(user);
                        _dataManager.SaveUsers(_users);
                        Console.WriteLine("\n? ?stifad?çi silindi!");
                    }
                }
                else
                {
                    Console.WriteLine("? ?stifad?çi tap?lmad?!");
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
