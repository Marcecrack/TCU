namespace Library.ViewModels
{
    #region usings
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using System.Windows.Input;
    using Views;
    using Services;
    #endregion

    public class MainViewModel : BaseViewModel
    {
        #region Properties
        public string Book { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public Image ImageBackFrame { get; set; }
        public ICommand SearchBooksCommand { get { return new RelayCommand(SearchBooks); } }
        #endregion



        private async void SearchBooks()
        {

            var connection = Connection.CheckConnection();
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
