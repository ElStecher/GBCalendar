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
        #region Felder der Page NewAppointment
        private string alldayevent = "N";
        private DateTime startTime;
        private DateTime endTime;
        private List<Room> rooms;
        #endregion

        #region Methoden der Page NewAppointment
        /// <summary>
        /// Initialisierung
        /// </summary>
        public NewAppointment ()
		{
            try
            {
                // Initialiserung
                InitializeComponent();

                //Fill up Rooms for Appointment
                DatabaseReader readerrooms = new DatabaseReader();

                rooms = readerrooms.ReadRooms();

                foreach (Room r in rooms)
                {
                    Roompicker.Items.Add(r.RoomName);
                }

            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
        }

        /// <summary>
        /// Wird der Switch-Button angeklickt ändert sich der Zustand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnToggled(object sender, ToggledEventArgs args)
        {
            // Ist das Appointment ein ganztägiges Ereignis, werden die Informationen der Start- und Endzeit ausgeblendet
            if (args.Value == true)
            {
                LabelBegin.IsVisible = false;
                TimePickerStart_Time.IsVisible = false;
                LabelEnd.IsVisible = false;
                TimePickerEnd_Time.IsVisible = false;
                alldayevent = "Y";
            }

            // Ansonsten sind die Informationen zu den Zeiten wieder sichtbar
            if (args.Value == false)
            {
                LabelBegin.IsVisible = true;
                TimePickerStart_Time.IsVisible = true;
                LabelEnd.IsVisible = true;  
                TimePickerEnd_Time.IsVisible = true;
                alldayevent = "N";
            }
        }

        /// <summary>
        /// Wenn der Benutzer das erstellte Ereigniss speichern möchte
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        void OnCreateAppointmentClicked(object sender, EventArgs args)
        {
            try
            {
                DateTime date = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day);

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
                else if (alldayevent == "N" && TimePickerStart_Time.Time > TimePickerEnd_Time.Time)
                {
                    DisplayAlert("Zeitspanne ungültig", "Beginn darf nicht grösser als Ende sein.", "OK");
                    return;
                }

                else if (Roompicker.SelectedItem == null)
                {
                    DisplayAlert("Raum fehlt", "Bitte Raum auswählen", "OK");
                    return;
                };

                //Wert für Room setzen
                Room selectedroom = rooms.Find(room => room.RoomName == Roompicker.SelectedItem.ToString());

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

                //instanzierung Appointment
                Appointment appointment = new Appointment(AppointmentTitel.Text, selectedroom, MainPage.Selectedclass, startTime, endTime, alldayevent, AppointmentDescription.Text, App.UserLoggedIn);


                MainPage.Selectedclass.AddAppointment(appointment);

                // Zuerst muss die Klasse ausgewählt werden können bevor es zur MainPage weitergeht
                Navigation.InsertPageBefore(new MainPage(MainPage.Selectedclass), this); 
                Navigation.PopAsync();

                DisplayAlert("Ereignis erstellt!", "Das Ereignis wurde erfolgreich erstellt.", "OK");

            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
        }     
        #endregion
    }
}