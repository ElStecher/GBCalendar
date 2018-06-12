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
	public partial class NewAppointment : ContentPage
	{
        private string alldayevent = "N";
        private DateTime startTime;
        private DateTime endTime;
        private List<Room> rooms;

        public NewAppointment ()
		{
            InitializeComponent();
            

            try
            {
                //Fill up Rooms for Appointment
                DatabaseReader readerrooms = new DatabaseReader();

                rooms = readerrooms.ReadRooms();

                foreach (Room r in rooms)
                {
                    Roompicker.Items.Add(r.RoomName);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                LabelBegin.IsVisible = false;
                TimePickerStart_Time.IsVisible = false;
                LabelEnd.IsVisible = false;
                TimePickerEnd_Time.IsVisible = false;


                alldayevent = "Y";
            }

            if (e.Value == false)
            {
                LabelBegin.IsVisible = true;
                TimePickerStart_Time.IsVisible = true;
                LabelEnd.IsVisible = true;  
                TimePickerEnd_Time.IsVisible = true;
                alldayevent = "N";
            }
        }

        void OnCreateAppointmentClicked(object sender, EventArgs args)
        {
            DateTime date = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day);
            //Abfragen ob felder Ausgefüllt
            if (AppointmentTitel.Text == null)
            {
                DisplayAlert("Titel fehlt", "Bitte Titel für Ereignis eintragen", "OK");
                return;
            }
            else if (AppointmentDescription.Text == null)
            {
                DisplayAlert("Beschreibung fehlt", "Bitte Beschreibung für Ereignis eintragen", "OK");
                return;
            }
            else if (Roompicker.SelectedItem == null)
            {
                DisplayAlert("Raum fehlt", "Bitte Raum auswählen", "OK");
                return;
            };



            //Wert für Room setzen
            Room r = rooms.Find(room => room.RoomName == Roompicker.SelectedItem.ToString());

            //Werte setzen für Alldayevent
            if (alldayevent == "N")
            {
                startTime = date + TimePickerStart_Time.Time;
                endTime = date + TimePickerEnd_Time.Time;
            }
            else
            {
                startTime = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day, 0, 0, 0);
                endTime = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day, 23, 59, 59);
            }

            await DisplayAlert("Neeeee", DatePicker.Date.ToString(), "OK");


            //instanzierung Appointment
            MainPage.Selectedclass.AddAppointment(AppointmentTitel.Text, MainPage.Selectedclass, r, startTime, endTime, alldayevent, AppointmentDescription.Text, App.UserLoggedIn);
            Application.Current.MainPage.Navigation.PopAsync();

            // problem: refresh der Seite mit Appointments muss noch implementiert werden
            
            DisplayAlert("Ereignis erstellt!", "Das Ereignis wurde erfolgreich erstellt.", "OK");
           
        }
    }
}