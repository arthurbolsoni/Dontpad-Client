using DontPad.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DontPad.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : ViewMode
    {
        String intro;

        public MainView()
        {
            this.InitializeComponent();
            intro = "Dont login, just use a URL\n" +
                "Dont save, text is auto - saved\n" +
                "Dont juggle attached files, edit online with your friends\n" +
                "Dont lose your content, download with YourURL.zip\n" +
                "Dont forget, you can use yourURL / yourFolder / yourSubfolder\n" +
                "Dontpad!";
        }
    }
}
