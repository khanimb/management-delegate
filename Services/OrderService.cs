using management_delegate.Models;

namespace management_delegate.Services
{
    public class OrderService
    {
        public void PlaceOrder(List<OrderItem> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("? S?b?tiniz bo?dur!");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("??????????????????????????????????");
            Console.WriteLine("?        Sifari?iniz             ?");
            Console.WriteLine("??????????????????????????????????");
            decimal total = 0;

            foreach (var item in cart)
            {
                var itemTotal = item.Product.Price * item.Quantity;
                Console.WriteLine($"?? {item.Product.Name} x{item.Quantity} = {itemTotal} AZN");
                total += itemTotal;
            }

            Console.WriteLine($"\n?? Ümumi m?bl??: {total} AZN");

            Console.Write("\n?? Çatd?r?lma ünvan?: ");
            var address = Console.ReadLine();

            Console.Write("?? Telefon nömr?si: ");
            var phone = Console.ReadLine();

            Console.WriteLine("\n? Sifari?iniz q?bul edildi!");
            Console.WriteLine($"Ümumi: {total} AZN");
            Console.WriteLine($"Ünvan: {address}");
            Console.WriteLine($"Telefon: {phone}");
            cart.Clear();
            Console.ReadKey();
        }
    }
}
