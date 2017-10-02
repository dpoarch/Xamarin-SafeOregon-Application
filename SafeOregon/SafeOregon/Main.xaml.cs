using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SafeOregon
{
    public partial class Main : ContentPage
    {
        public Main()
        {
            NavigationPage.SetTitleIcon(this, "@drawable/ic_safe_oregon.png");
            Title = "SafeOregon";
            InitializeComponent();

        }

 
        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Form());
        }

        async void OnResource(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Resources());
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "SafeOregon",
                    "Call the Help Line? if this is an emergency situation, call 911.",
                    "Call",
                    "Cancel"))
            {
                var dialer = DependencyService.Get<Phoneword.IDialer>();
                if (dialer != null)
                    dialer.Dial("844-472-3367");
            }
        }
    }

    namespace Phoneword
    {
        public interface IDialer
        {
            bool Dial(string number);
        }
    }
}
