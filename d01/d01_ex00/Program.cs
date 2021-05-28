using System;

namespace DAY01
{
    class Program
    {
        static void onError()
        {
            Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
        }
        
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                onError();
                return 1;
            }

            string[] values = args[0].Split(" ");

            try
            {
                var exchanger = new Exchanger(args[1]);
                var amounts = exchanger.Calculate(args[0]);
                
                if (amounts.Count == 0)
                    Console.WriteLine("Нет доступных курсов для конвертации из этой валюты");
                
                Console.WriteLine($"Сумма в исходной валюте: {double.Parse(values[0]):N2} {values[1].ToUpper()}");

                foreach (var exchangeSum in amounts)
                {
                    Console.WriteLine(exchangeSum.ToString());
                }
            }
            catch (Exception)
            {
                onError();
                return 1;
            }

            return 0;
        }
    }
}
