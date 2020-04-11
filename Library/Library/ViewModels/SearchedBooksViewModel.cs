namespace Library.ViewModels
{
    using Models.GoogleModels;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public class SearchedBooksViewModel
    {
        #region Attributes

        public ObservableCollection<Item> lstBooks;
        public string Book;
        public string Author;
        public string Category;

        #endregion

        #region Services

        public ServiceBooks apiService{ get; set; }
        #endregion

        #region Properties
        public ObservableCollection<Item> LstBooks
        {
            get { return this.lstBooks; }
            set { lstBooks = value; }
        }

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
            var response = await this.apiService.GetList<ModelBook>(Book, Author, Category);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Cerrar");
                return;
            }

            var list = (ModelBook)response.Result;
            LstBooks = new ObservableCollection<Item>(list.items);
        }
        #endregion
    }
}
