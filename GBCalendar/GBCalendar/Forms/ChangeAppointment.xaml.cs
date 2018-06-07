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
        public ChangeAppointment (Appointment appointment)
		{
            try
            {
                InitializeComponent();
                this.AppointmentTitel.Text = appointment.Title;
                this.AppointmentDescription.Text = appointment.Description;
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

            //new DatabaseWriter().UpdateAppointment(new Appointment(1, this.AppointmentTitel.Text, new DatabaseReader().ReadRooms().Find(room => room.RoomName == Roompicker.SelectedItem.ToString()), MainPage.SelectedClass, startTime, endTime, allDayEvent, this.AppointmentDescription.Text));
            
        }

        // @stecher TODO: Methode implementieren welche ein Appointment löschen kann
        void OnDeleteAppointmentClicked(object sender, EventArgs args)
        {
        }

    }
}