namespace Library.Views
{
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ViewModels;
    using System.Collections.Generic;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedBooksPage : ContentPage
    {
        #region Singleton
        public SearchedBooksViewModel Books { get; set; }

        public SearchedBooksViewModel GetSearchedBooksViewModel(string book, string author, string category)
        {
            if (Books == null)
                Books = new SearchedBooksViewModel(book, author, category);
            return Books;
        }
        #endregion

        public SearchedBooksPage(string book, string author, string category)
        {
            InitializeComponent();

            Books = GetSearchedBooksViewModel(book, author, category);
            BindingContext = Books;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped." + e.Item, "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
