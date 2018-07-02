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
        #region Felder der Page StartPage
        public Int16 IdRole { get; private set; }
        #endregion

        #region Methoden der Page StartPage
        /// <summary>
        /// Initialisierung
        /// </summary>
        public StartPage ()
		{
            // Initialisierung
			InitializeComponent ();

            // Erstellen des Labels mit den Copyright informationen
            Label copyrightLabel = new Label()
            {
                Text = "Copyright © 2018 by Dario Berther, Samuel Maissen and Fabio Stecher. " + Environment.NewLine + "All rights reserved.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(copyrightLabel);
		}

        /// <summary>
        /// Wenn sich der Benutzer als Schüler anmelden möchte
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnStudentClicked(object sender, EventArgs args)
        {
            // Setzt die Rolle für einen Schüler
            IdRole = 2;
            await Navigation.PushAsync(new LoginPage(IdRole));
        }

        /// <summary>
        /// Wenn sich der Benutzer als Lehrer anmelden möchte
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnTeacherClicked(object sender, EventArgs args)
        {
            // Setzt die Rolle für einen Lehrer
            IdRole = 1;
            await Navigation.PushAsync(new LoginPage(IdRole));
        }
        #endregion
    }
}