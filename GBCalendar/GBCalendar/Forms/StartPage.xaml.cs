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

    }
}