namespace Library.ViewModels
{
    #region usings
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using System.Windows.Input;
    using Views;
    using Library.Models.GoogleModels;
    using System.Threading.Tasks;
    using Plugin.Connectivity;
    #endregion

    public class MainViewModel
    {
        #region Properties
        public string Book { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public Image ImageBackFrame { get; set; }
        public ICommand SearchBooksCommand { get { return new RelayCommand(SearchBooks); } }
        #endregion

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor active su internet!"
                };
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor revice su conexión con internet!"
                };
            return new Response
            {
                IsSuccess = true,
                Message = "OK"
            };

        }
        #region Service
        private string Url = "https://www.googleapis.com/books/v1/volumes?q=";
        private readonly string FilterBook = "intitle:";
        private readonly string FilterAuthor = "inauthor:";
        private readonly string FreeBook = "&filter=free-ebooks";
        private readonly string FilterCategory = "subject:";
        public string BuildUriString(string book, string author, string category)
        {
            if (!string.IsNullOrWhiteSpace(book)) Url = string.Concat(Url, FilterBook, book);
            if (!string.IsNullOrWhiteSpace(author)) Url = string.Concat(Url, FilterAuthor, author);
            if (!string.IsNullOrWhiteSpace(category)) Url = string.Concat(Url, FilterCategory, category);

            return string.Concat(Url, FreeBook);
        }
        #endregion
        private async void SearchBooks()
        {

            //var connection = CheckConnection();
            //if (!connection.Result.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", connection.Result.Message, "Ok");
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(Book) &&
            //    string.IsNullOrWhiteSpace(Author) &&
            //    string.IsNullOrWhiteSpace(Category))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "No se se ingresó un valor para los campos", "Ok");
            //    return;
            //}


            await Application.Current.MainPage.Navigation.PushAsync(new SearchedBooksPage(Book, Author, Category));
        }

        public MainViewModel()
        {

        }
    }
}
