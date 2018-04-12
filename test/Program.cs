using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // PageInfo page = new PageInfo();

            Console.WriteLine("Hello World!");
            Console.WriteLine(SecurityHelper.HTMLEncode("<h3>content</h3>"));
            Console.Read();
        }
    }
}