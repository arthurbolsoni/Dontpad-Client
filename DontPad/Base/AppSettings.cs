using DontPad.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DontPad.Base
{
    public class AppSettings
    {
        private ElementTheme _theme;
        public ElementTheme Theme { get => _theme; set { _theme = value; } }

        public int FontSize { get; set; }

        public AppSettings()
        {
            dynamic loadObject = LoadSettings();

            Theme = (ElementTheme)loadObject.theme;
        }

        public void SaveSettings()
        {
            // Save a composite setting that will be roamed between devices
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();

            composite["theme"] = (int)Theme;
            composite["fontSize"] = FontSize;

            localSettings.Values["ThemeSetting"] = composite;
        }

        public dynamic LoadSettings()
        {
            // load a composite setting that roams between devices
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values["ThemeSetting"];
            
            dynamic expando = new ExpandoObject();
            var loadObject = expando as IDictionary<String, object>;


            if (composite != null)
            {
                loadObject["theme"] = composite["theme"];
                loadObject["fontsize"] = composite["fontSize"];
            }
            return loadObject;
        }

        public static void ClearSettings()
        {
            
        }
    }
}
