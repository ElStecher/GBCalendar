using System;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GBCalendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private Int16 IdRole { get; set; }

        public LoginPage(Int16 idRole)
        {
            InitializeComponent();

            this.IdRole = idRole;
        }

        async void OnLoginClicked(object sender, EventArgs args)
        {
            //Instanzierung neuer Person und zuweisen der Eigenschaften

            var isValid = AreCredentialsCorrect(); //Person Objekt wird übergegeben

            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Fehler", "Keine Email oder Passwort eingegeben", "OK");
            }

            bool AreCredentialsCorrect()
            {
                Debug.WriteLine(entryMail.Text + " " + entryPassword.Text);
                return (!string.IsNullOrWhiteSpace(entryMail.Text) || !string.IsNullOrWhiteSpace(entryPassword.Text));
            }   
        }
    }
}