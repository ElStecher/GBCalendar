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
        #region Felder der Page LoginPage
        public Int16 IdRole { get; private set; }
        private string Error { get; set; } = "Es ist ein Fehler aufgetreten, bitte versuchen Sie es erneut";
        #endregion

        #region Methoden der Page LoginPage
        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="idRole">Rolle der angemeldeteten Person</param>
        public LoginPage(Int16 idRole)
        {
            // Initialisierung
            InitializeComponent();
            this.IdRole = idRole;
        }

        /// <summary>
        /// Klickt der Benutzer auf den Button um sich anzumelden wird diese Methode aufgerufen
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnLoginClicked(object sender, EventArgs args)
        {
            try
            {
                bool isValid = AreCredentialsCorrect();

                // Wenn die Anmeldedaten korrekt sind, wird der Rest der App zugänglich
                if (isValid)
                {
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Fehler", Error, "OK");
                }

                // Überprüfung der Anmeldedaten
                bool AreCredentialsCorrect()
                {
                    // Wenn das Passwort oder die E-Mail leer ist oder Leerzeichen beinhaltet wird ein Fehler angezeigt
                    if (string.IsNullOrWhiteSpace(entryMail.Text) || string.IsNullOrWhiteSpace(entryPassword.Text))
                    {
                        Error = "Keine Email oder Passwort eingegeben";
                        return false;
                    }
                    else
                    {
                        // DatabaseReader um die Daten zu überprüfen
                        DatabaseReader checkUser = new DatabaseReader();
                        if (checkUser.AreUserCredentialsCorrect(entryMail.Text, entryPassword.Text, IdRole))
                        {
                            return true;
                        }
                        else
                        {
                            Error = "Passwort oder E-Mail nicht korrekt, bitte versuche es erneut und überprüfen Sie ob sie beim Start Schüler oder Lehrer ausgesucht haben.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
        }
        #endregion
    }
}