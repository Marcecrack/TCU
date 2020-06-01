namespace Library.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Library.Models;
    using Models.GoogleModels;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class SearchedBooksViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<ModelBook> lstBooks;
        private string Book;
        private string Author;
        private string Category;
        private bool isRefreshing;
        private bool lstHasGoogleBooks;
        private bool lblHasGoogleBooks;
        private string googleBooksFreeTotal;

        #endregion

        #region Properties
        public ObservableCollection<ModelBook> LstBooks
        {
            get { return this.lstBooks; }
            set { SetValue(ref lstBooks, value); }
        }

        public string GoogleBooksFreeTotal
        {
            get { return this.googleBooksFreeTotal; }
            set { SetValue(ref this.googleBooksFreeTotal, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }
        public bool LblHasGoogleBooks
        {
            get { return this.lblHasGoogleBooks; }
            set { SetValue(ref lblHasGoogleBooks, value); }
        }
        public bool LstHasGoogleBooks
        {
            get { return this.lstHasGoogleBooks; }
            set { SetValue(ref lstHasGoogleBooks, value); }
        }
        #endregion

        #region Services
        public ServiceBooks apiService { get; set; }
        #endregion

        #region Constructors

        public SearchedBooksViewModel(string book, string author, string category)
        {
            this.GoogleBooksFreeTotal = "0";
            LoadAttributes(book, author, category);
            apiService = new ServiceBooks();
            LoadBooks();
        }

        #endregion

        #region Methods

        private void BookTapped(object sender, ItemTappedEventArgs e)
        {
            var temp = e.Item;
        }

        public void LoadAttributes(string book, string author, string category)
        {
            Book = book;
            Author = author;
            Category = category;
        }

        public void HideGoogleList()
        {
            if (this.LstHasGoogleBooks) this.LstHasGoogleBooks = false;
            else this.LstHasGoogleBooks = true;
        }

        public async void LoadBooks()
        {
            this.IsRefreshing = true;
            var connection = await Connection.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;

                await App.Current.MainPage.DisplayAlert("Error", connection.Message, "Cerrar");
                await App.Current.MainPage.Navigation.PopAsync();
            }

            var response = await this.apiService.GetList<JSONModel>(Book, Author, Category);



            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Cerrar");
                return;
            }

            var list = (List<Item>)response.Result;

            var listBooks = (from books in list
                             select new ModelBook()
                             {
                                 BookTitle = books.volumeInfo.title,
                                 BookCategory = books.bookCategory,
                                 BookAuthor = books.bookAuthor,
                                 BookDescription = books.bookDescription,
                                 BookPageCount = books.volumeInfo.pageCount,
                                 BookAvailable = books.accessInfo.pdf.isAvailable,
                                 BookPdf = books.accessInfo.pdf
                             }).ToList();


            if (list == null)
            {
                this.LstHasGoogleBooks = false;
                this.LblHasGoogleBooks = true;
                this.LstBooks = new ObservableCollection<ModelBook>(new List<ModelBook>());
                this.GoogleBooksFreeTotal = "0";
            }
            else
            {
                this.LstHasGoogleBooks = true;
                this.LblHasGoogleBooks = false;
                this.LstBooks = new ObservableCollection<ModelBook>(listBooks);
                this.GoogleBooksFreeTotal = this.LstBooks.Count.ToString();
            }


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

        public void GoogleBookTapped(ModelBook book)
        {
            //App.Current.MainPage.Navigation.PushAsync()
        }

        public ICommand DisplayGoogleBooks
        {
            get
            {
                return new RelayCommand(HideGoogleList);
            }
        }

        #endregion
    }
}
