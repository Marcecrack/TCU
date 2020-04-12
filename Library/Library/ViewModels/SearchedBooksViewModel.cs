namespace Library.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models.GoogleModels;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Linq;

    public class SearchedBooksViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<Item> lstBooks;
        private string Book;
        private string Author;
        private string Category;
        private bool isRefreshing;

        #endregion



        #region Properties
        public ObservableCollection<Item> LstBooks
        {
            get { return this.lstBooks; }
            set { SetValue(ref lstBooks, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }
        
        #endregion

        #region Services
        public ServiceBooks apiService { get; set; }
        #endregion

        #region Constructors

        public SearchedBooksViewModel(string book, string author, string category)
        {
            LoadAttributes(book, author, category);
            apiService = new ServiceBooks();
            LoadBooks();
        }

        #endregion

        #region Methods
        public void LoadAttributes(string book, string author, string category)
        {
            Book = book;
            Author = author;
            Category = category;
        }
        public async void LoadBooks()
        {
            this.IsRefreshing = true;
            var connection = Connection.CheckConnection();

            if (!connection.Result.IsSuccess)
            {
                this.IsRefreshing = false;

                await App.Current.MainPage.DisplayAlert("Error", connection.Result.Message, "Cerrar");
                await App.Current.MainPage.Navigation.PopAsync();
            }

            var response = await this.apiService.GetList<JSONModel>(Book, Author, Category);



            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Cerrar");
                return;
            }

            var list = (List<Item>) response.Result;


            this.LstBooks = new ObservableCollection<Item>(list);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadBooks);
            }
        }

        #endregion
    }
}
