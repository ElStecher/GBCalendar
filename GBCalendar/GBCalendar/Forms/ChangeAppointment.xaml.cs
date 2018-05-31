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

                foreach (Room r in rooms)
                {
                    Roompicker.Items.Add(r.RoomName);
                }

                if (appointment.AllDayEvent == "Y")
                {
                    this.AllDayEventSwitch.IsToggled = true;
                }
                else
                {
                    this.AllDayEventSwitch.IsToggled = false;
                }
                //string date = appointment.StartTime.Remove(11, 8);
                //string startTimeFull = appointment.StartTime.Remove(0, 11).Remove(5, 3);
                //string endTime = appointment.EndTime.Remove(0, 10).Remove(5, 3);

                //TimeSpan startTime = new TimeSpan(startTimeFull.Remove(2),)

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

        }

        void OnDeleteAppointmentClicked(object sender, EventArgs args)
        {

        }

    }
}