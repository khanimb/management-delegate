namespace management_delegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var app = new ManagementApp();
            app.Run();
        }
    }
}
