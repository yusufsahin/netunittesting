using System.Reflection.Metadata.Ecma335;

namespace HelloWorldNUnit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GetHelloWorld());
        }

        public static string GetHelloWorld()
        {
            return "Hello, World!";
        }
    }
}
