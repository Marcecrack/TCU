﻿using Xamarin.Forms;

namespace Library.Views
{
    using ViewModels;
    public partial class MainPage : ContentPage
    {
        #region Singleton
        public MainViewModel Main { get; set; }

        public MainViewModel GetMainViewModel()
        {
            if (Main == null)
                Main = new MainViewModel();
            return Main;
        }

        #endregion
        public MainPage()
        {
            Main = GetMainViewModel();
            InitializeComponent();
            BindingContext = Main;

        }
    }
}
