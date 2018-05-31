using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DontPad.Services
{
    public sealed class PlatformService
    {
        public async Task LaunchUri(Uri uri) => await Launcher.LaunchUriAsync(uri);

        public static Task Share(string content)
        {
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += (sender, args) =>
            {
                var request = args.Request;
                request.Data.SetText(content);
                request.Data.Properties.Title = "DontPad";
            };
            DataTransferManager.ShowShareUI();
            return Task.CompletedTask;
        }

        public static Task CopyTextToClipboard(string text)
        {
            var dataPackage = new DataPackage { RequestedOperation = DataPackageOperation.Copy };
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
            return Task.CompletedTask;
        }

        public async Task ResetApp()
        {
            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            foreach (var file in files) await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            Application.Current.Exit();
        }

        public static Task RegisterTheme(ElementTheme elementTheme)
        {
            var contentElement = (Frame)Window.Current.Content;
            contentElement.RequestedTheme = elementTheme;
            return Task.CompletedTask;
        }
    }
}