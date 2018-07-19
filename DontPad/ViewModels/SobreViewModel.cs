using DontPad.Base;
using DontPad.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace DontPad.ViewModels
{
    public class SobreViewModel : ViewModelBase
    {
        public List<String> _ComboBox;
        public List<String> ComboBox { get => _ComboBox; set { _ComboBox = value; RaisePropertyChanged(); } }

        private String _SelectedTheme { get; set; }
        public String SelectedTheme { get => _SelectedTheme; set { _SelectedTheme = value; PlatformUpdateTheme(); PlatformUpdateTheme(); } }

        private void PlatformUpdateTheme()
        {
            ElementTheme elementTheme = (ElementTheme)Enum.Parse(typeof(ElementTheme), SelectedTheme);
            App.appSettings.Theme = elementTheme;
            PlatformService.RegisterTheme(elementTheme);
            App.appSettings.SaveSettings();
        }
        public SobreViewModel(INavigationService navigationService) : base(navigationService)
        {
            _ComboBox = Enum.GetNames(typeof(ElementTheme)).ToList<String>();
            SelectedTheme = App.appSettings.Theme.ToString();

            //_ComboBox = (int) App.appSettings.Theme == 2 ? false : true;
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        public override bool OnBackRequested()
        {
            base.NavigationService.GoBack();
            base.OnBackRequested();
            return true;
        }
    }
}
