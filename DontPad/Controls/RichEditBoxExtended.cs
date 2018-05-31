using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DontPad.Controls
{
    public class RichEditBoxExtended : RichEditBox
    {
        public static readonly DependencyProperty RtfTextProperty =
            DependencyProperty.Register(
            "RtfText", typeof(string), typeof(RichEditBoxExtended),
            new PropertyMetadata(default(string), RtfTextPropertyChanged));

        private bool _lockChangeExecution;

        public RichEditBoxExtended()
        {
            TextChanged += RichEditBoxExtended_TextChanged;
        }

        public string RtfText
        {
            get { return (string)GetValue(RtfTextProperty); }
            set { SetValue(RtfTextProperty, value); }
        }

        private void RichEditBoxExtended_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!_lockChangeExecution)
            {
                _lockChangeExecution = true;

                string text;
                Document.GetText(TextGetOptions.None, out text);
                if (string.IsNullOrWhiteSpace(text))
                {
                    RtfText = "";
                }
                else
                {
                    Document.GetText(TextGetOptions.None, out text);
                    RtfText = text;
                }
                _lockChangeExecution = false;
            }
        }

        private static void RtfTextPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var rtb = dependencyObject as RichEditBoxExtended;
            if (rtb == null) return;

            if (!rtb._lockChangeExecution)
            {
                if (string.IsNullOrWhiteSpace(rtb.RtfText))
                {
                    rtb.RtfText = "";
                }

                rtb._lockChangeExecution = true;
                rtb.Document.SetText(TextSetOptions.None, rtb.RtfText);
                rtb.Document.Selection.SetRange(rtb.RtfText.Length - 1, rtb.RtfText.Length - 1);
                rtb.Focus(FocusState.Keyboard);
                rtb._lockChangeExecution = false;
            }
        }
    }
}
