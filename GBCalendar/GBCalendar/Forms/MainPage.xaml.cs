﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GBCalendar
{
    public partial class MainPage : ContentPage
    {
        private List<Class> classes;
        private Class selectedclass;

        public MainPage()
        {
            InitializeComponent();

            if (App.UserLoggedIn.Role == 1)
            {
                ToolbarItem toolBarItemCreateNewAppointment = new ToolbarItem
                {
                    Text = "Ereignis erstellen",
                    Order = ToolbarItemOrder.Secondary,
                    Command = new Command(() => this.OnCallNewAppointmentPageClicked(null, null)),
                };

                this.ToolbarItems.Add(toolBarItemCreateNewAppointment);
            }
            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();

                //"8" ist id des Techeachers. Muss später noch durch Person.idPers ersetzt werden
                classes = readerclasses.ReadClass(App.UserLoggedIn.IdPerson);

            }
            catch (Exception)
            {
                throw;

            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.UserLoggedIn = null;
            Navigation.InsertPageBefore(new StartPage(), this);
            await Navigation.PopToRootAsync();
        }


        async void OnClassSelectedClicked(object sender, EventArgs args)
        {

            //www.stackoverflow.com/questions/32313996/rendering-a-displayactionsheet-with-observablecollection-data-in-xamarin-cross-p?rq=1
            string action = await DisplayActionSheet("Klasse wählen:", "Cancel", null, classes.Select(Class => Class.ClassName).ToArray());

            if (action != "Cancel")
            {
                ToolbarItemClass.Text = action;
                selectedclass = classes.Find(Class => Class.ClassName == action);
            }
        }
        private async void OnCallNewAppointmentPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAppointment());
        }

    }
}