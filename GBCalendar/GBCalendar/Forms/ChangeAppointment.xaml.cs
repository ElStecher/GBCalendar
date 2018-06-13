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
        #region Felder der Page ChangeAppointment
        public Appointment SelectedAppointment { get; private set; }
        public string Alldayevent { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public List<Room> Rooms { get; private set; }
        #endregion

        #region Methoden der Page ChangeAppointment
        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="appointment">Das zu bearbeitende Appointment</param>
        public ChangeAppointment (Appointment appointment)
		{
            try
            {
                // Initialisierung
                InitializeComponent();

                // Ist der angemeldete Benutzer ein Schüler oder nicht der Ersteller des Appointments, kann das Appointment nicht bearbeitet werden 
                if (App.UserLoggedIn.Role == 0 || App.UserLoggedIn.IdPerson != appointment.Creator.IdPerson)
                {
                    // Alle Felder deaktivieren
                    AppointmentTitel.IsEnabled = false;
                    AppointmentDescription.IsEnabled = false;
                    Roompicker.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    AllDayEventSwitch.IsEnabled = false;
                    TimePickerStart_Time.IsEnabled = false;
                    TimePickerEnd_Time.IsEnabled = false;
                    ButtonDeleteAppointment.IsVisible = false;
                    ButtonSaveAppointment.IsVisible = false;
                    AppointmentCreator.IsEnabled = false;
                }
                else
                {
                    // Ist der angemeldete Benutzer auch der Ersteller des Appointments, kann dieser das Appointment auch löschen oder speichern
                    AppointmentLabelCreator.IsVisible = false;
                    AppointmentCreator.IsVisible = false;
                    
                }
                
                // Ausgewähltes appointment setzen
                SelectedAppointment = appointment;

                //Page Bennenen
                Title = SelectedAppointment.Title;

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
                DatePicker.Date = SelectedAppointment.StartTime.Date;
                TimePickerStart_Time.Time = TimeSpan.Parse(SelectedAppointment.StartTime.Hour + ":" + SelectedAppointment.StartTime.Minute);
                TimePickerEnd_Time.Time = TimeSpan.Parse(SelectedAppointment.EndTime.Hour + ":" + SelectedAppointment.EndTime.Minute);
                Alldayevent = SelectedAppointment.AllDayEvent;
                AppointmentCreator.Text = SelectedAppointment.Creator.FirstName + " " + SelectedAppointment.Creator.LastName;

                // Dauert das Appointment den ganzen Tag, kann keine Start- und Endzeit gesetzt werden
                if (Alldayevent == "Y")
                {
                    this.AllDayEventSwitch.IsToggled = true;

                    // Informationen zu Start- und Endzeit verbergen
                    LabelBegin.IsVisible = false;
                    TimePickerStart_Time.IsVisible = false;
                    LabelEnd.IsVisible = false;
                    TimePickerEnd_Time.IsVisible = false;

                }
                else
                {
                    this.AllDayEventSwitch.IsToggled = false;   
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Wenn der Benutzer den Switch-Button betätigt, wird der Zustand geändert
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        void OnToggled(object sender, ToggledEventArgs args)
        {
            // Wenn der Switch-Button getätigt wurde, dauert das Appointment den ganzen Tag
            if (args.Value == true)
            {
                LabelBegin.IsVisible = false;
                TimePickerStart_Time.IsVisible = false;
                LabelEnd.IsVisible = false;
                TimePickerEnd_Time.IsVisible = false;
                Alldayevent = "Y";
            }

            // Ansonsten dauert es über eine definierte Zeit
            if (args.Value == false)
            {
                LabelBegin.IsVisible = true;
                TimePickerStart_Time.IsVisible = true;
                LabelEnd.IsVisible = true;
                TimePickerEnd_Time.IsVisible = true;
                Alldayevent = "N";
            }

        }

        /// <summary>
        /// Wenn der Benutzer das Appointment fertig bearbeitet hat, kann er es speichern
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        void OnSaveAppointmentClicked(object sender, EventArgs args)
        {
            //Abfragen ob felder Korrekt/Ausgefüllt
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
            else if(TimePickerStart_Time.Time > TimePickerEnd_Time.Time)
            {
                DisplayAlert("Zeitspanne ungültig", "Begin darf nicht grösser als Ende sein.", "OK");
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
                StartTime = DatePicker.Date + TimePickerStart_Time.Time;
                EndTime = DatePicker.Date + TimePickerEnd_Time.Time;
            }
            else
            {
                StartTime = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day, 0, 0, 0);
                EndTime = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day, 23, 59, 59);
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

            //Naviegieren
            // Zuerst muss die Klasse ausgewählt werden können bevor es zur MainPage weitergeht
            Navigation.InsertPageBefore(new MainPage(MainPage.Selectedclass), this); 
            Navigation.PopAsync();
            DisplayAlert("Ereignis geändert!", "Das Ereignis wurde erfolgreich geändert.", "OK");
        }

        /// <summary>
        /// Wenn der Benutzer den "Löschen"-Button geklickt hat, wird das Appointment gelöscht
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnDeleteAppointmentClicked(object sender, EventArgs args)
        {
            MainPage.Selectedclass.DeleteAppointment(this.SelectedAppointment);
            await DisplayAlert("Ereignis gelöscht", "Das ausgewählte Ereignis wurde erfolgreich gelöscht!", "OK");

            //Naviegieren
            // Zuerst muss die Klasse ausgewählt werden können bevor es zur MainPage weitergeht
            Navigation.InsertPageBefore(new MainPage(MainPage.Selectedclass), this); 
            await Navigation.PopAsync();
        }

        #endregion

    }
}