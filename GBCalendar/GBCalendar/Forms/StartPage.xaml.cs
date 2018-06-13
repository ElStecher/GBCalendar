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
			InitializeComponent ();
            Label copyrightLabel = new Label()
            {
                Text = "Copyright © 2018 by Dario Berther, Samuel Maissen and Fabio Stecher. " + Environment.NewLine + "All rights reserved.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(copyrightLabel);
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