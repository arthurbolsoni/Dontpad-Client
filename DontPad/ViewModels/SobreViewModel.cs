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
        private bool _ToggleSwitch;
        public bool ToggleSwitch { get => _ToggleSwitch; set { _ToggleSwitch = value; RaisePropertyChanged(); PlatformUpdateTheme(); } }

        private void PlatformUpdateTheme()
        {
            ElementTheme elementTheme = _ToggleSwitch ? ElementTheme.Light : ElementTheme.Dark;
            App.appSettings.Theme = elementTheme;
            PlatformService.RegisterTheme(elementTheme);
        }
        public SobreViewModel(INavigationService navigationService) : base(navigationService)
        {
            ToggleSwitch = (int) App.appSettings.Theme == 2 ? false : true;
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
