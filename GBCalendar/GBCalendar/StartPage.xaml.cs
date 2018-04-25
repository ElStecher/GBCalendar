using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GBCalendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
        private Int16 IdRole { get; set; }

        public StartPage ()
		{
			InitializeComponent ();
		}

        async void OnStudentClicked(object sender, EventArgs args)
        {
            IdRole = 2;
            await Navigation.PushAsync(new LoginPage(IdRole));
        }

        async void OnTeacherClicked(object sender, EventArgs args)
        {
            IdRole = 1;
            await Navigation.PushAsync(new LoginPage(IdRole));
        }

        async void OnTESTClicked(object sender, EventArgs args)
        {
            try
            {
                Navigation.InsertPageBefore(new New_Appointment(), this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Test", ex.Message.ToString(), "OK");
            }

           

        }

    }
}