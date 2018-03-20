using DontPad.Base;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace DontPad.ViewModels
{
    public class SobreViewModel : ViewModelBase
    {
        public SobreViewModel(INavigationService navigationService) : base(navigationService)
        {

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
