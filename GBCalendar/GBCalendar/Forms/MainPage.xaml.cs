using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GBCalendar
{
    public partial class MainPage : ContentPage
    {
        private List<SchoolClass> classes;
        private static SchoolClass selectedclass;

        public static SchoolClass Selectedclass
        {
            get
            {
                return selectedclass;
            }
            private set
            {
                selectedclass = value;
            }
        }


        public MainPage()
        {
            InitializeComponent();

            if (App.UserLoggedIn.Role == 1)
            {
                ToolbarItem toolBarItemCreateNewAppointment = new ToolbarItem
                {
                    Text = "Ereignis erstellen",
                    Order = ToolbarItemOrder.Secondary,
                    Command = new Command(() => this.OnCallNewAppointmentPageClicked(null, null)),
                };

                this.ToolbarItems.Add(toolBarItemCreateNewAppointment);
            }
            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();

                //"8" ist id des Techeachers. Muss später noch durch Person.idPers ersetzt werden
                classes = readerclasses.ReadClasses(App.UserLoggedIn.IdPerson);

            }
            catch (Exception)
            {
                throw;

            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.UserLoggedIn = null;
            Navigation.InsertPageBefore(new StartPage(), this);
            await Navigation.PopToRootAsync();
        }


        async void OnClassSelectedClicked(object sender, EventArgs args)
        {

            //www.stackoverflow.com/questions/32313996/rendering-a-displayactionsheet-with-observablecollection-data-in-xamarin-cross-p?rq=1
            string action = await DisplayActionSheet("Klasse wählen:", "Cancel", null, classes.Select(SchoolClass => SchoolClass.ClassName).ToArray());

            if (action != "Cancel")
            {
                ToolbarItemClass.Text = action;
                selectedclass = classes.Find(SchoolClass => SchoolClass.ClassName == action);
                ShowAppointments();
            }
        }
        private async void OnCallNewAppointmentPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAppointment());
        }

        void ShowAppointments()
        { 
            StackLayout layout = new StackLayout();

            foreach (Appointment appointment in selectedclass.AppointmentList)
            {
               
                var button = new Button
                {
                Text = appointment.Title + "\n" + appointment.StartTime.Remove(11, 8) + "\n" +
                appointment.StartTime.Remove(0,11).Remove(5,3) + " -" + appointment.EndTime.Remove(0, 10).Remove(5, 3)
                };
                if (App.UserLoggedIn.Role == 1)
                {
                    button.Clicked += async delegate { await Navigation.PushAsync(new ChangeAppointment(appointment)); };
                }
                else
                {
                    button.Clicked += async delegate { await Navigation.PushAsync(new Forms.ShowAppointmentForStudent(appointment)); };
                }

                layout.Children.Add(button);
                Content = layout;

            }
        }
    }
}