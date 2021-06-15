using System.Text.Json;

namespace D02.Model
{
    public class Movie : ISearchable
    {
        public string Title { get; set; }
        private string _summaryShort;
        private bool _isCriticsPick;
        private string _rating;
        private string _url;


        public Movie(string title, string summaryShort, bool isCriticsPick, string rating, string url)
        {
            Title = title; 
            _summaryShort = summaryShort;
            _isCriticsPick = isCriticsPick;
            _rating = rating;
            _url = url;
        }

        public Movie(JsonElement info)
        {

            Title = info.GetProperty("title").ToString();
            _summaryShort = info.GetProperty("summary_short").ToString();
            _isCriticsPick = info.GetProperty("critics_pick").ToString() != "0";
            _rating = info.GetProperty("mpaa_rating").ToString();
            _url = info.GetProperty("link").GetProperty("url").ToString();
        }
        
        public override string ToString()
        {
            string result = $"{Title} {(_isCriticsPick ? "[NYT critic’s pick]" : "")}\n" +
                            $"{_summaryShort}\n" +
                            $"{_url}";
            
            return result;
        }
    }
}