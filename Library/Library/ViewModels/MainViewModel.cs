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

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("youtube.com");

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

        private async void SearchBooks()
        {

            var connection = CheckConnection();
            if (!connection.Result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Result.Message, "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(Book) &&
                string.IsNullOrWhiteSpace(Author) &&
                string.IsNullOrWhiteSpace(Category))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se se ingresó un valor para los campos", "Ok");
                return;
            }


            await Application.Current.MainPage.Navigation.PushAsync(new SearchedBooksPage(Book, Author, Category));
        }

        public MainViewModel()
        {

        }
    }
}
