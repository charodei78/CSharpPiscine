namespace DAY01.Models
{
    public struct ExchangeSum
    {
        private double _sum;
        private string _identifier;

        public ExchangeSum(double sum, string identifier)
        {
            _sum = sum;
            _identifier = identifier;
        }
        
        public override string ToString()
        {
            return $"Сумма в {_identifier}: {_sum:N2}";
        }
    }
}