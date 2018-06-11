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
	public partial class ChangeAppointment : ContentPage
	{
        public Appointment SelectedAppointment { get; set; }
        public string Alldayevent { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<Room> Rooms { get; set; }

        public ChangeAppointment (Appointment appointment)
		{
            try
            {
                InitializeComponent();
                // Ausgewähltes appointment setzen
                this.SelectedAppointment = appointment;

                // Räume auslesen und abfüllen
                DatabaseReader readerrooms = new DatabaseReader();
                Rooms = readerrooms.ReadRooms();
                foreach (Room room in Rooms)
                {
                    Roompicker.Items.Add(room.RoomName);
                }
          
                // Werte Abfüllen
                this.AppointmentTitel.Text = this.SelectedAppointment.Title;
                this.AppointmentDescription.Text = this.SelectedAppointment.Description;
                Roompicker.SelectedIndex = Roompicker.Items.IndexOf(this.SelectedAppointment.Room.RoomName);
                DatePicker.Date = new DateTime(int.Parse(SelectedAppointment.StartTime.Substring(6, 4)), 
                    int.Parse(SelectedAppointment.StartTime.Substring(3, 2)), int.Parse(SelectedAppointment.StartTime.Substring(0, 2)));
                TimePickerStart_Time.Time = TimeSpan.Parse(SelectedAppointment.StartTime.Substring(11, 5));
                TimePickerEnd_Time.Time = TimeSpan.Parse(SelectedAppointment.EndTime.Substring(11, 5));

                if (appointment.AllDayEvent == "Y")
                {
                    this.AllDayEventSwitch.IsToggled = true;
                    LabelBegin.IsVisible = false;
                    TimePickerStart_Time.IsVisible = false;
                    LabelEnd.IsVisible = false;
                    TimePickerEnd_Time.IsVisible = false;

                }
                else
                {
                    this.AllDayEventSwitch.IsToggled = false;
                   
                }



                // Siehe in OnSaveAppointmentClicked()
            }
            catch (Exception e)
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


                Alldayevent = "Y";
            }

            if (e.Value == false)
            {
                LabelBegin.IsVisible = true;
                TimePickerStart_Time.IsVisible = true;
                LabelEnd.IsVisible = true;
                TimePickerEnd_Time.IsVisible = true;
                Alldayevent = "N";
            }

        }

        void OnSaveAppointmentClicked(object sender, EventArgs args)
        {
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
            Room selectedRoom = Rooms.Find(room => room.RoomName == Roompicker.SelectedItem.ToString());

            //Werte setzen für Alldayevent
            if (Alldayevent == "N")
            {
                StartTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerStart_Time.Time.ToString();
                EndTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerEnd_Time.Time.ToString();

            }
            else
            {
                StartTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + "00:00:00";
                EndTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + "23:59:59";
            }

            //Werte für Appointment anpassen
            SelectedAppointment.Title = AppointmentTitel.Text;
            SelectedAppointment.Description = AppointmentDescription.Text;
            SelectedAppointment.Room = selectedRoom;
            SelectedAppointment.AllDayEvent = Alldayevent;
            SelectedAppointment.StartTime = StartTime;
            SelectedAppointment.EndTime = EndTime;

            //Geändertes Appointment übergeben
            MainPage.Selectedclass.EditAppointment(SelectedAppointment);
            Application.Current.MainPage.Navigation.PopAsync();

            // problem: refresh der Seite mit Appointments muss noch implementiert werden

            DisplayAlert("Ereignis geändert!", "Das Ereignis wurde erfolgreich geändert.", "OK");

        }

        async void OnDeleteAppointmentClicked(object sender, EventArgs args)
        {
            DatabaseWriter databaseWriter = new DatabaseWriter();
            databaseWriter.DelteAppointment(this.SelectedAppointment);
            await DisplayAlert("Ereignis gelöscht", "Das ausgewählte Ereignis wurde erfolgreich gelöscht!", "OK");
            await Navigation.PushAsync(new MainPage());
        }

    }
}