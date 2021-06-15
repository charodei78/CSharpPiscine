using System.Text.Json;

namespace D02.Model
{
    public class Book : ISearchable
    {
        public string Title { get; set; }
        private string _author;
        private string _summaryShort;
        private string _listName;
        private string _url;
        private int _rank;

        public Book(string title, string author, string summaryShort, int rank, string listName, string url)
        {
            Title = title; 
            _summaryShort = summaryShort;
            _author = author;
            _listName = listName;
            _rank = rank;
            _url = url;
        }

        public Book(JsonElement data)
        {
            var details = data.GetProperty("book_details")[0];
            Title = details.GetProperty("title").ToString();
            _summaryShort = details.GetProperty("description").ToString();
            _author = details.GetProperty("author").ToString();
            _listName = data.GetProperty("list_name").ToString();
            _rank = data.GetProperty("rank").GetInt32();
            _url = data.GetProperty("amazon_product_url").ToString();
        }
        
        public override string ToString()
        {
            string result = 
                $"{Title} by {_author} [{_rank} on NYT’s {_listName}]\n"+
                $"{_summaryShort}\n"+
                $"{_url}";

            return result;
        }
    }
}