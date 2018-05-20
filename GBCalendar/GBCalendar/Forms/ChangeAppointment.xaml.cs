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