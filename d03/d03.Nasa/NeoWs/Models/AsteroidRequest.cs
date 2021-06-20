using System;
using Newtonsoft.Json;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidRequest
    {
        public AsteroidRequest(DateTime startDate, DateTime endDate, int resultCount)
        {
            StartDate = startDate;
            EndDate = endDate;
            ResultCount = resultCount;
        }
        
        public AsteroidRequest(string startDateString, string endDateString, string resultCountString)
        {
            StartDate = DateTime.Parse(startDateString);
            EndDate = DateTime.Parse(endDateString);
            ResultCount = Int32.Parse(resultCountString);
        }
        
        public DateTime StartDate { get; }
        
        public DateTime EndDate { get; }
        public int ResultCount { get; }
    }
}