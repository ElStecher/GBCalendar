﻿using System;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GBCalendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public Int16 IdRole { get; private set; }
        private string Error = "Es ist ein Fehler aufgetreten, bitte versuchen Sie es erneut"; 

        public LoginPage(Int16 idRole)
        {
            InitializeComponent();
            this.IdRole = idRole;
        }

        async void OnLoginClicked(object sender, EventArgs args)
        {
            try
            {
                
                var isValid = AreCredentialsCorrect();

                if (isValid)
                {
                    Navigation.InsertPageBefore(new MainPage(), this); // Zuerst muss die Klasse ausgewählt werden können bevor es zur MainPage weitergeht
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Fehler", Error, "OK");
                }

                bool AreCredentialsCorrect()
                {
                    if (string.IsNullOrWhiteSpace(entryMail.Text) || string.IsNullOrWhiteSpace(entryPassword.Text))
                    {
                        Error = "Keine Email oder Passwort eingegeben";
                        return false;
                    }
                    else
                    {
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
                await DisplayAlert("Fehler", e.ToString(), "OK");
            }
        }
     }
}