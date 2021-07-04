using System;
using d04_ex02.Models;
using d04_ex03.Models;

namespace d04_ex03
{
    class Program
    {
        static void Main(string[] args)
        {
            var user1 = TypeFactory.CreateWithActivator<IdentityUser>();
            var user2 = TypeFactory.CreateWithConstructor<IdentityUser>();

            var result = user1 == user2 ? "user1 == user2" : "user1 != user2";
            Console.WriteLine($"{result}");
            
            var role1 = TypeFactory.CreateWithActivator<IdentityRole>();
            var role2 = TypeFactory.CreateWithConstructor<IdentityRole>();

            result = user1 == user2 ? "role1 == role2" : "role1 != role2";
            Console.WriteLine($"{result}");

            Console.WriteLine("User name: ");
            var userName = Console.ReadLine();
            
            var userWithParam = TypeFactory.CreateWithParameters<IdentityUser>(new Object[] {userName});
            
            Console.WriteLine($"userWithParam {userWithParam.UserName}");

        }
    }
}