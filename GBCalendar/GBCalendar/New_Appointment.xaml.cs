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
	public partial class New_Appointment : ContentPage
	{
        public New_Appointment ()
		{
            InitializeComponent();

            //Fill up Classes for Appointment
            Database_Reader readerclasses = new Database_Reader();

            //"8" ist id des Techeachers. Muss später noch durch Person.idPers ersetzt werden
            List<Class> classes = readerclasses.ReadClass(8);

            foreach(Class c in classes)
            {
                Classpicker.Items.Add(c.ClassName);
            }


            //Fill up Rooms for Appointment
            Database_Reader readerrooms = new Database_Reader();

            List<Room> rooms = readerrooms.ReadRoom();

            foreach (Room r in rooms)
            {
                Roompicker.Items.Add(r.RoomName);
            }

        }


    }
}