using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeOregon.Model;
using Xamarin.Forms;

namespace SafeOregon
{
    public partial class Form3 : ContentPage
    {
        public IncidentData newincidentData = new IncidentData();
        
        public Form3(IncidentData data)
        {
            Title = "Form 3";
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Form 3");
            string[] string1 = new string[] {
                "This is the first time",
                "One other time",
                "Once a month",
                "Once a week",
                "Every day"
            };

            string[] string2 = new string[] {
                "No",
                "Yes"
            };

            picker1.ItemsSource = string1;
            picker2.ItemsSource = string2;
            lbladultName.IsVisible = false;
            adultName.IsVisible = false;

            newincidentData = data;
        }
        
        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            newincidentData.TimesHappened = picker1.SelectedItem.ToString();
            newincidentData.ReportedAdult = picker2.SelectedItem.ToString();
            newincidentData.Who = adultName.Text;

            if (picker1.SelectedItem == null)
            {
                await DisplayAlert("SafeOregon", "Please select how many times the situation happened!", null, "OK");
            }
            else if (picker2.SelectedItem.ToString() == "Yes" && adultName == null)
            {
                await DisplayAlert("SafeOregon", "Please enter the adults name!", null, "OK");
            }
            else
            {
                await Navigation.PushAsync(new Form4(newincidentData));
            }
        }

        public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if(picker2.SelectedItem.ToString() == "Yes")
            {
                adultName.IsVisible = true;
                lbladultName.IsVisible = true;
            }
            else
            {
                adultName.IsVisible = false;
                lbladultName.IsVisible = false;
            }
        }
    }
}
