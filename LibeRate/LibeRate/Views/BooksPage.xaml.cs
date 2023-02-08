﻿using LibeRate.Services;
using LibeRate.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibeRate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {

        public BooksPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            if(BindingContext is BooksViewModel viewModel && App.LanguageChanged==true)
            {
                viewModel.Refresh();
            }
        }
    }
}