using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeOregon.Model;

using Xamarin.Forms;

namespace SafeOregon
{
    public partial class Form2 : ContentPage
    {

        public IncidentData newincidentData = new IncidentData();

        public Form2(IncidentData data)
        {
            Title = "Form 2";
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Form 2");
            newincidentData = data;


        }

        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            newincidentData.WillBeHarmed = will_be_Harmed.Text;
            newincidentData.CausingHarm = causing_Harm.Text;
            newincidentData.WhatYouSaw = what_you_saw.Text;

            if (causing_Harm.Text == null)
            {
                await DisplayAlert("SafeOregon", "Please enter the name of the person who is causing harm!", null, "OK");
            }
            else if (will_be_Harmed.Text == null)
            {
                await DisplayAlert("SafeOregon", "Please enter the name of the person being harmed!", null, "OK");
            }
            else
            {
                await Navigation.PushAsync(new Form3(newincidentData));
            }
        }

    }
}
