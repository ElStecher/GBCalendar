using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GBCalendar
{
    // Link zum Einfügen eines neuen Icons
    //https://stackoverflow.com/questions/37945767/how-to-change-application-icon-in-xamarin-forms
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
        public Int16 IdRole { get; private set; }

        public StartPage ()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
           
		}

        async void OnStudentClicked(object sender, EventArgs args)
        {
            try
            {
                IdRole = 2;
                await Navigation.PushAsync(new LoginPage(IdRole));

            }
            catch (Exception e)
            {
               await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }



        }

        async void OnTeacherClicked(object sender, EventArgs args)
        {
            try
            {
                IdRole = 1;
                await Navigation.PushAsync(new LoginPage(IdRole));
            }
            catch (Exception e)
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
        }
    }
}