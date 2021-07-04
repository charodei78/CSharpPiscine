using System;
using d04_ex02.Models;
using d04_ex02.ConsoleSetter;

namespace d04_ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new IdentityUser();
            ConsoleSetter.ConsoleSetter.SetValues(user);
            Console.WriteLine(user);
            
            Console.WriteLine();

            var role = new IdentityRole();
            ConsoleSetter.ConsoleSetter.SetValues(role);
            Console.WriteLine(role);

        }
    }
}