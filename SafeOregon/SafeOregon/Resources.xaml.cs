using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SafeOregon
{
    public partial class Resources : ContentPage
    {
        public Resources()
        {
            Title = "WebActivity";
            InitializeComponent();
            webview.Source = new UrlWebViewSource { Url = "https://tips.safeoregon.com/" };
            webview.VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
