using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LibeRate.Models;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using LibeRate.Services;
using LibeRate.Views;
using Xamarin.CommunityToolkit.Helpers;
using LibeRate.Resx;

namespace LibeRate.ViewModels
{
    class SelectTargetLanguageViewModel : BaseViewModel
    {
        public ObservableCollection<LanguageMenuItem> Languages { get; set; }
        private LanguageMenuItem _selectedLanguage;
        public Command ConfirmSelectionCommand { get;}

        public SelectTargetLanguageViewModel()
        {
            LocalizedString english = new LocalizedString(() => string.Format(AppResources.English));
            LocalizedString japanese = new LocalizedString(() => string.Format(AppResources.Japanese));
            Languages = new ObservableCollection<LanguageMenuItem>
            {
                new LanguageMenuItem("english", english.Localized, "united_kingdom.png"),
                new LanguageMenuItem("japanese", japanese.Localized, "japan.png")
            };

            ConfirmSelectionCommand = new Command(ConfirmSelection);
        }

        public LanguageMenuItem SelectedLanguage 
        { 
            get => _selectedLanguage;
            set 
            { 
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public async void ConfirmSelection()
        {
            IUserService userService = DependencyService.Get<IUserService>();

            await userService.SetTargetLanguage(App.CurrentUser.Id, SelectedLanguage.LanguageID);

            App.CurrentUser = await userService.GetUser(App.CurrentUser.Id);
            App.LanguageChanged = true;
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync($"//{nameof(SearchPage)}");
        }

    }
}
