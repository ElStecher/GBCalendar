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
        private SchoolClass selectedclass;

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
            foreach (var item in selectedclass.AppointmentList)
            {
                var button = new Button();
                button.Text = item.Title;
                button.Clicked += async delegate { await Navigation.PushAsync(new ChangeAppointment(item)); };

                layout.Children.Add(button);
            }
        }
    }
}