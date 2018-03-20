using GalaSoft.MvvmLight.Command;
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

namespace DontPad.Base
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        Frame rootFrame = Window.Current.Content as Frame;

        protected readonly INavigationService NavigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;

            if (rootFrame == null)
                return;

            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        public void BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.OnBackRequested())
            {
                SystemNavigationManager.GetForCurrentView().BackRequested -= BackRequested;
                NavigationService.GoBack();
            }
            e.Handled = true;
        }

        public virtual bool OnBackRequested()
        {
            return false;
        }
    }
}
