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
        private string startTime;
        private string endTime;

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
            Room r = rooms.Find(room => room.RoomName == Roompicker.SelectedItem.ToString());

            if (alldayevent == "N")
            {
                //startTime = DateTime.Parse(DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerStart_Time.Time.ToString());
                //endTime = DateTime.Parse(DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerEnd_Time.Time.ToString());

                startTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerStart_Time.Time.ToString();
                endTime = DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerEnd_Time.Time.ToString();
            }
            else
            {
                startTime = "00:00:00";
                endTime = "23:59:59";

            }
            MainPage.SelectedClass.AddAppointment(AppointmentTitel.Text, MainPage.SelectedClass, r, startTime, endTime, alldayevent, AppointmentDescription.Text, App.UserLoggedIn);
        }
    }
}