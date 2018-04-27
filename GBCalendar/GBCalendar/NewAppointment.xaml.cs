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

                rooms = readerrooms.ReadRoom();

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

        void OnEreignisErstellenClicked(object sender, EventArgs args)
        {

            //Wird nur zum testen gebraucht bis Objekte zur verfügung stehen
            Class c = new Class("TBM76B");
            Person logged = new Person("samuel.maissen@hotmail.com", "TEST123", 1);
   
            Room r = rooms.Find(room => room.RoomName == Roompicker.SelectedItem.ToString());
            //Console.WriteLine(r.RoomName);

            if (alldayevent == "J")
            {
                startTime = DateTime.Parse(DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerStart_Time.Time.ToString());
                endTime = DateTime.Parse(DatePicker.Date.ToString("yyyy-MM-dd") + " " + TimePickerEnd_Time.Time.ToString());

            }


            c.AddAppointment(AppointmentTitel.Text, c, r, startTime, endTime, alldayevent, AppointmentDescription.Text, logged);
        }
    }
}