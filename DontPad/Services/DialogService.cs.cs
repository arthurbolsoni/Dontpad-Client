using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DontPad.Services
{

    public sealed class DialogService
    {
        public static async Task ShowDialog(string message, string title) => await new MessageDialog(message, title).ShowAsync();

        public static async Task<string> ShowDialogForResults(string message, string title)
        {
            var contentBox = new TextBox { PlaceholderText = message, Margin = new Thickness(0, 18, 0, 0) };
            var inputDialog = new ContentDialog
            {
                Title = title,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",
                Content = new StackPanel { Children = { contentBox } }
            };
            var result = await inputDialog.ShowAsync();
            return result == ContentDialogResult.Primary ? contentBox.Text : string.Empty;
        }
    }
}