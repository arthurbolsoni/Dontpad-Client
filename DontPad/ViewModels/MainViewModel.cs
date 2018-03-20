using CommonServiceLocator;
using DontPad.Base;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DontPad.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand GoSobreView { get; set; }

        public RelayCommand<string> GoMainView { get; set; }

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoSobreView = new RelayCommand(() => base.NavigationService.NavigateTo("SobreView"));
            GoMainView = new RelayCommand<string>((s) => base.NavigationService.NavigateTo("ConteudoView", s));
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}