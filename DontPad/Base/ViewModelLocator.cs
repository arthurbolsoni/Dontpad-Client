using CommonServiceLocator;
using DontPad.ViewModels;
using DontPad.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace DontPad.Base
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();

            nav.Configure("MainView", typeof(MainView));
            nav.Configure("SobreView", typeof(SobreView));
            nav.Configure("ConteudoView", typeof(ConteudoView));

            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            else
            {

            }

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SobreViewModel>();
            SimpleIoc.Default.Register<ConteudoViewModel>();
        }
        
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public SobreViewModel SobreViewModel => ServiceLocator.Current.GetInstance<SobreViewModel>();
        public ConteudoViewModel ConteudoViewModel => ServiceLocator.Current.GetInstance<ConteudoViewModel>();
    }
}
