using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace DAY01.Models
{
    public struct ExchangeRate
    {
        public  string From;
        public  string To;
        public double Rate;

        public ExchangeRate(string from, string to, double rate)
        {
            From  = from;
            To = to;
            Rate = rate;
        }

        private static void OnError(string message)
        {
            throw new Exception(message);
        }

        private static KeyValuePair<string, double> ParseString(string input)
        {
            string[] data = input.Split(":");
            double rate = 0;
            
            if (data.Length != 2)
                OnError("Не верный формат данных");
            try
            {
                rate = double.Parse(data[1]);
                if (rate < 0)
                    OnError("Сумма меньше 0");
            }
            catch (Exception e)
            {
                OnError("Не верный формат данных");
            }
            
            return new KeyValuePair<string, double>(data[0].ToUpper(), rate);
        }
        

        // <example>
        //  <code>
        //      ExchangeRate("EUR", "USD:1.2")
        //  </code>
        // </example>
        public ExchangeRate(string currencyForm, string currencyTo)
        {
            KeyValuePair<string, double> tmp;

            tmp = ParseString(currencyTo);
            To = tmp.Key;

            Rate = tmp.Value;

            From = currencyForm;
        }
    }
}