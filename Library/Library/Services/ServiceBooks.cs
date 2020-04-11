namespace Library.Services
{
    using Models.GoogleModels;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ServiceBooks
    {
        #region Attributes

        private string Url = "https://www.googleapis.com/books/v1/volumes?q=";
        private readonly string FilterBook = "intitle:";
        private readonly string FilterAuthor = "inauthor:";
        private readonly string FreeBook = "&filter=free-ebooks";
        private readonly string FilterCategory = "subject:";
        private readonly string MaxResult = "&maxResults=40";
        private readonly string Plus = "+";
        private HttpClient _client;
        #endregion

        #region Constructor
        public ServiceBooks()
        {
        }
        #endregion

        #region Methods
        public async Task<Response> GetList<T>(string book, string author, string category)
        {
            GetClient();
            try
            {
                var response = await _client.GetAsync(BuildUriString(book, author, category));
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };


                var list = JsonConvert.DeserializeObject<ModelBook>(result);


                return new Response
                {
                    IsSuccess = true,
                    Message = "OK",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public void GetClient()
        {
            if (_client == null)
                _client = new HttpClient();
        }


        public string BuildUriString(string book, string author, string category)
        {
            var changed = false;
            if (!string.IsNullOrWhiteSpace(book))
            {
                Url = string.Concat(Url, FilterBook, book);
                changed = true;
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                if(changed) Url = string.Concat(Url, Plus, FilterAuthor, author);
                else Url = string.Concat(Url, FilterAuthor, author);

            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                if(changed) Url = string.Concat(Url, Plus, FilterCategory, category);
                else Url = string.Concat(Url, FilterCategory, category);
            }

            return string.Concat(Url, FreeBook, MaxResult);
        }
        #endregion
    }
}
