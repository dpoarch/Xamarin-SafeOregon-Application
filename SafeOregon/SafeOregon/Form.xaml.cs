using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafeOregon.Model;

using System.Net;
using Xamarin.Forms;


namespace SafeOregon
{
    public partial class Form : ContentPage
    {
        public Form()
        {
            
            Title = "Form 1";
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Form 1");
            string[] incidentList = new string[] {
                "At the bus stop",
                "In the text message",
                "In an email",
                "In an instant message",
                "In class",
                "In front of school",
                "In hallway",
                "In the bathroom",
                "In the cafeteria",
                "In the gym",
                "In the locker room",
                "In the parking lot",
                "Off School Grounds",
                "On a voicemail",
                "On Facebook",
                "On the bus",
                "On the field",
                "On Youtube",
                "Twitter",
                "Other"
            };
            List<School> schoolList = new List<School>();
            for (int i = 0; i < 10; i++)
            {
                School data = new School();
                data.id = i.ToString();
                data.school = "School" + i.ToString();
                data.city = "City" + i.ToString();
                schoolList.Add(data);
            }
            List<School> schoolListNew = schoolList.OrderByDescending(i => i.id).ToList();

            picker.ItemsSource = incidentList;
         
            listview1.IsVisible = false;
        }
       
        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
           
            if (schoolName.Text == null)
            {
                await DisplayAlert("SafeOregon", "School name is invalid!", null, "OK");
            }
            else if (picker.SelectedItem == null)
            {
                await DisplayAlert("SafeOregon", "Please select where did you hear or see the incident!", null, "OK");
            }
            else
            {
                var page1 = new IncidentData
                {
                    SchoolName = schoolName.Text,
                    Incident = picker.ToString(),
                    DateHappened = dateHappened.ToString(),
                    TimeHappend = timeHappened.ToString(),
                    SchoolId = schoolId.Text
                };

                var form2 = new Form2(page1);
                await Navigation.PushAsync(form2);
            }

        }

        async void schoolNameChanged(object sender, EventArgs e)
        {
            string RestUrl = "https://tips.safeoregon.com/schoolLookup?stateid=OR&q=" + schoolName.Text + "&lan=en&token=qIAGAreFfSMviInoC55Y";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<School> Items = JsonConvert.DeserializeObject<List<School>>(content);
                List<string> data = new List<string>();
                foreach (School school in Items)
                {
                    data.Add(school.school);
                }

                listview1.IsVisible = true;
                listview1.ItemsSource = data;
            }
            else
            {
               new List<School>();
            }
        }

        async void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            string RestUrl = "https://tips.safeoregon.com/schoolLookup?stateid=OR&q=" + schoolName.Text + "&lan=en&token=qIAGAreFfSMviInoC55Y";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                School Items = JsonConvert.DeserializeObject<School>(content.TrimStart('[').TrimEnd(']'));
                schoolId.Text = Items.id.ToString();
            }
            
            schoolName.Text = listview1.SelectedItem.ToString();
            listview1.IsVisible = false;
        }

        public async Task<List<School>> RefreshDataAsync(object sender, TextChangedEventArgs e)
        {
            string RestUrl = "https://tips.safeoregon.com/schoolLookup?stateid=OR&q=all&getAll=all&lan=en&token=qIAGAreFfSMviInoC55Y";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<School> Items = JsonConvert.DeserializeObject<List<School>>(content);
                return Items;
            }else
            {
                return new List<School>();
            }
        }

        public class School
        {
            public string school { get; set; }
            public string city { get; set; }
            public string id { get; set; }

        }
        

    }
}
