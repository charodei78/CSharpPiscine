using System;
using System.Collections.Generic;
using System.IO;
using DAY01.Models;

namespace DAY01
{
    public class Exchanger
    {
        private List<ExchangeRate> _exchangeRates;

        public Exchanger(string dirPath)
        {
            _exchangeRates = new List<ExchangeRate>();

            foreach (string filePath in Directory.GetFiles(dirPath))
            {
                string[] fileLines = File.ReadAllLines(filePath);
                foreach (string line in fileLines)
                {
                    _exchangeRates.Add(new ExchangeRate(filePath[^7..^4], line));
                }
            }
        }

        public List<ExchangeSum> Calculate(double amount, string currency)
        {
            var result = new List<ExchangeSum>();

            currency = currency.ToUpper();

            foreach (var exchangeRate in _exchangeRates)
            {
                if (exchangeRate.From == currency)
                    result.Add(new ExchangeSum(amount * exchangeRate.Rate, exchangeRate.To));
            }
            
            return result;
        }

        public List<ExchangeSum> Calculate(string input)
        {
            string[] values = input.Split(" ");
            if (values.Length != 2)
                throw new Exception();

            return Calculate(double.Parse(values[0]), values[1]);
        }
    }
}