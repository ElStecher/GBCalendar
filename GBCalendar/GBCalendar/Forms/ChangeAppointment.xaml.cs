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
        private Appointment selectedAppointment { get; set; }

        public ChangeAppointment (Appointment appointment)
		{
            try
            {
                InitializeComponent();
                this.selectedAppointment = appointment;
                this.AppointmentTitel.Text = this.selectedAppointment.Title;
                this.AppointmentDescription.Text = this.selectedAppointment.Description;

                DatabaseReader readerrooms = new DatabaseReader();
                List<Room> rooms = readerrooms.ReadRooms();
         
                foreach (Room room in rooms)
                {
                    Roompicker.Items.Add(room.RoomName);
                }

                if (appointment.AllDayEvent == "Y")
                {
                    this.AllDayEventSwitch.IsToggled = true;
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
            
        }

        void OnSaveAppointmentClicked(object sender, EventArgs args)
        {
            string startTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerStart_Time.Time.ToString();
            string endTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerEnd_Time.Time.ToString();

            string allDayEvent;
            if (this.AllDayEventSwitch.IsToggled == true)
            {
                allDayEvent = "Y";
            }
            else
            {
                allDayEvent = "N";
            }
            
        }

        async void OnDeleteAppointmentClicked(object sender, EventArgs args)
        {
            DatabaseWriter databaseWriter = new DatabaseWriter();
            databaseWriter.DelteAppointment(this.selectedAppointment);
            await DisplayAlert("Ereignis gelöscht", "Das ausgewählte Ereignis wurde erfolgreich gelöscht!", "OK");
            await Navigation.PushAsync(new MainPage());
        }

    }
}