using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Media;
using Xamarin.Forms;
using System.Net.Http;

using SafeOregon;
using SafeOregon.Model;

namespace SafeOregon
{

    public partial class Form4 : ContentPage
    {
        public IncidentData newincidentData = new IncidentData();
        public Form4(IncidentData data)
        {
            Title = "Form 4";
            InitializeComponent();
            string[] string2 = new string[] {
                "Student",
                "Teacher",
                "Staff Member",
                "Administrator",
                "Board Member",
                "Volunteer",
                "Parent",
                "Other"
            };

            who_are_you.ItemsSource = string2;

            newincidentData = data;
        }


        async void OnUploadButtonClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Upload an Image", "Cancel", null, "Camera", "Choose Photo", "Take Video", "Choose Video");
            if (action == "Choose Photo")
            {

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Oops!", "Photo is not supported!", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }

                bool check = true;
                var stream = file.GetStream();
                file.Dispose();

                if (stream.Length >= 20971520)
                {
                    check = false;
                    await DisplayAlert("Oops!", "Photo must be less than 20mb", "OK");
                }

                if (check)
                {
                    ImageSource imgsrc = ImageSource.FromStream(() =>
                    {

                        var bytes = new byte[stream.Length];
                        stream.ReadAsync(bytes, 0, (int)stream.Length);
                        string base64 = System.Convert.ToBase64String(bytes);
                        return stream;

                    });
                }

            }
            else if (action == "Choose Video")
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickVideoSupported)
                {
                    await DisplayAlert("Oops!", "Video is not supported!", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickVideoAsync();

                if (file == null)
                {
                    return;
                }

                bool check = true;
                var stream = file.GetStream();
                file.Dispose();

                if (stream.Length >= 20971520)
                {
                    check = false;
                    await DisplayAlert("Oops!", "Video must be less than 20mb", "OK");
                }

                if (check)
                {
                    ImageSource imgsrc = ImageSource.FromStream(() =>
                    {

                        var bytes = new byte[stream.Length];
                        stream.ReadAsync(bytes, 0, (int)stream.Length);
                        string base64 = System.Convert.ToBase64String(bytes);
                        return stream;

                    });
                }

            }
            else if (action == "Camera")
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                bool check = true;
                var stream = file.GetStream();
                file.Dispose();

                if (stream.Length >= 20971520)
                {
                    check = false;
                    await DisplayAlert("Oops!", "Photo must be less than 20mb", "OK");
                }

                if (check)
                {
                    ImageSource imgsrc = ImageSource.FromStream(() =>
                    {

                        var bytes = new byte[stream.Length];
                        stream.ReadAsync(bytes, 0, (int)stream.Length);
                        string base64 = System.Convert.ToBase64String(bytes);
                        return stream;

                    });
                }
            }
            else if (action == "Take Video")
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Quality = Plugin.Media.Abstractions.VideoQuality.Medium,
                    Name = "video.mp4"

                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                bool check = true;
                var stream = file.GetStream();
                file.Dispose();

                if (stream.Length >= 20971520)
                {
                    check = false;
                    await DisplayAlert("Oops!", "Video must be less than 20mb", "OK");
                }

                if (check)
                {
                    ImageSource imgsrc = ImageSource.FromStream(() =>
                    {

                        var bytes = new byte[stream.Length];
                        stream.ReadAsync(bytes, 0, (int)stream.Length);
                        string base64 = System.Convert.ToBase64String(bytes);
                        return stream;

                    });
                }
            }
        }

        public async void OnSubmitButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Form4(newincidentData));
        }

        //Post Save
        //public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        //{
        //    // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
        //    var uri = new Uri(string.Format(Constants.RestUrl, item.ID));

        //    var json = JsonConvert.SerializeObject(item);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;
        //    if (isNewItem)
        //    {
        //        response = await client.PostAsync(uri, content);
        //    }

        //    if (response.IsSuccessStatusCode)
        //    {
        //        Debug.WriteLine(@"             TodoItem successfully saved.");

        //    }
        //}



    }
}
