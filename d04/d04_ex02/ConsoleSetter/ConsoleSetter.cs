using System;
using System.ComponentModel;
using System.Reflection;
using d04_ex02.Attributes;

namespace d04_ex02.ConsoleSetter
{
    public class ConsoleSetter
    {
        public static void SetValues<T>(T item) where T : class
        {
            Type itemType = typeof(T);
            
            Console.WriteLine($"Let's set {itemType.Name}");
            var propertyInfos = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                if (Attribute.GetCustomAttribute(propertyInfo, typeof(NoDisplayAttribute)) != null)
                    continue;

                var description = Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
                Console.Write($"Set {description?.Description ?? propertyInfo.Name}: ");
                
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    var defaultValue = Attribute.GetCustomAttribute(propertyInfo, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                    input = defaultValue?.Value?.ToString() ?? "";
                }
                
                propertyInfo.SetValue(item, input);
            }

            Console.WriteLine($"We've set our instance!");
        }
    }
}