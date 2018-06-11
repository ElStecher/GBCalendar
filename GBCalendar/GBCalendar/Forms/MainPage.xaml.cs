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
            bool result = await DisplayAlert("Abmelden", "Sie werden abgemeldet", "OK", "Cancel");

            // Falls der Button "Cancel" bedrückt wurde, wird der Benutzer abgemeldet
            // https://forums.xamarin.com/discussion/71594/displayalert-detect-cancel-button-is-clicked-or-not
            if (result == true)
            {
                App.UserLoggedIn = null;
                Navigation.InsertPageBefore(new StartPage(), this);
                await Navigation.PopToRootAsync();
            }
            else
            {
                // Kehrt zur MainPage zurück
                return;
            }
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
            var scrollView = new ScrollView();
            
            var layout = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                Spacing = 0
            };
            scrollView.Content = layout;
            Content = scrollView;

            if (selectedclass.AppointmentList.Count == 0)
            {
                // https://forums.xamarin.com/discussion/69446/adding-label-to-page
                Label label = new Label { Text = "Keine Ereignisse gefunden", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center};
                Content = label;
            }
            else
            {
                foreach (Appointment appointment in selectedclass.AppointmentList)
                {
                    string appointmentDate = appointment.StartTime.Remove(11, 8);
                    string appointmentStart = "";
                    string appointmentEnd = "";
                    string showingText;

                    if (appointment.StartTime.Contains("00:00") && appointment.EndTime.Contains("23:59"))
                    {
                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + "Ganztägiges Ereignis";
                    }
                    else
                    {
                        appointmentDate = appointment.StartTime.Remove(11, 8);
                        appointmentStart = appointment.StartTime.Remove(0, 11).Remove(5, 3);
                        appointmentEnd = appointment.EndTime.Remove(0, 10).Remove(5, 3);

                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + appointmentStart + " -" + appointmentEnd;
                    }

                    var button = new Button
                    {
                        Text = showingText,
                        BackgroundColor = Color.LightGray,
                        BorderWidth = 0.5,
                        CornerRadius = 0,
                        BorderColor = Color.Black,
                        Margin = new Thickness(10,0,10,0)
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
                    

                }
            }
        }
    }
}