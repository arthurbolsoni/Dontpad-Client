using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DontPad.Base
{
    public class ViewMode : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string fullName = $"DontPad.ViewModels.{e.SourcePageType.Name.Replace("View", "ViewModel")}";
            Type viewModelType = Type.GetType(fullName);

            ViewModelBase viewModel = (ViewModelBase)ServiceLocator.Current.GetInstance(viewModelType);
            viewModel.OnNavigatedTo(e);

            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
        }
    }
}
