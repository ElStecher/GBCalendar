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
        public List<SchoolClass> classes;
        public static SchoolClass Selectedclass { get; private set; }

        public MainPage()
        {
            NavigationPage.SetHasBackButton(this, false);

            InitializeComponent();  
            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();

                classes = readerclasses.ReadClasses(App.UserLoggedIn.IdPerson);

            }
            catch (Exception)
            {
                throw;

            }

        }


        /// <summary>
        /// Konstruktor für aufruf nach NewAppointment
        /// </summary>
        /// <param name="selectedclass"></param>

        public MainPage(SchoolClass selectedclass)
        {

            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();


            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();
                classes = readerclasses.ReadClasses(App.UserLoggedIn.IdPerson);

            }
            catch (Exception)
            {
                throw;

            }



            //Elemente für toolbar bereitstellen
            ToolbarItem toolBarItemCreateNewAppointment = new ToolbarItem
            {
                Text = "Ereignis erstellen",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => this.OnCallNewAppointmentPageClicked(null, null)),
            };

            ToolbarItem toolBarItemRefresh = new ToolbarItem
            {
                Icon = "refresh.png",
                Text = "Ereignisse aktualisieren",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => this.OnRefreshClicked(null, null)),
            };

            this.ToolbarItems.Add(toolBarItemRefresh);
            this.ToolbarItems.Add(toolBarItemCreateNewAppointment);

            // name wieder auf vorherige ausgewählte klasse setzen
            ToolbarItemClass.Text = selectedclass.ClassName;
            Selectedclass = selectedclass;
            ShowAppointments();

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

            if (App.UserLoggedIn.Role == 1 && ToolbarItemClass.Text == "Klasse Auswählen")
            {
                ToolbarItem toolBarItemCreateNewAppointment = new ToolbarItem
                {
                    Text = "Ereignis erstellen",
                    Order = ToolbarItemOrder.Secondary,
                    Command = new Command(() => this.OnCallNewAppointmentPageClicked(null, null)),
                };


                this.ToolbarItems.Add(toolBarItemCreateNewAppointment);
                
            }


            if (ToolbarItemClass.Text == "Klasse Auswählen")
            {
                ToolbarItem toolBarItemRefresh = new ToolbarItem
                {
                    Icon = "refresh.png",
                    Text = "Ereignisse aktualisieren",
                    Order = ToolbarItemOrder.Primary,
                    
                    Command = new Command(() => this.OnRefreshClicked(null, null)),
                };

                this.ToolbarItems.Add(toolBarItemRefresh);


            }


            if (action != "Cancel" && action != null)
            {
                ToolbarItemClass.Text = action;
                Selectedclass = classes.Find(SchoolClass => SchoolClass.ClassName == action);
                ShowAppointments();
            }
        }
        private async void OnCallNewAppointmentPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAppointment());
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {

            DatabaseReader dbr = new DatabaseReader();
            Selectedclass.AppointmentList = dbr.ReadAppointments(Selectedclass);

            ShowAppointments();
        }

         public void ShowAppointments()
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



            if (Selectedclass.AppointmentList.Count == 0)
            {
                // https://forums.xamarin.com/discussion/69446/adding-label-to-page
                Label label = new Label { Text = "Keine Ereignisse gefunden", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                Content = label;
            }
            else
            {
                foreach (Appointment appointment in Selectedclass.AppointmentList)
                {
                    string appointmentDate = appointment.StartTime.ToString("dd.MM.yyyy");
                    string appointmentStart = appointment.StartTime.ToString("HH:mm");
                    string appointmentEnd = appointment.EndTime.ToString("HH:mm");
                    string showingText;

                    if (appointmentStart.Contains("00:00") && appointmentEnd.Contains("23:59"))
                    {
                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + "Ganztägiges Ereignis";
                    }
                    else
                    {
                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + appointmentStart + " -" + appointmentEnd;
                    }

                    var button = new Button
                    {
                        Text = showingText,
                        BackgroundColor = Color.LightGray,
                        BorderWidth = 0.5,
                        CornerRadius = 0,
                        BorderColor = Color.Black,
                        Margin = new Thickness(10, 0, 10, 0)
                    };

                   
                    button.Clicked += async delegate { await Navigation.PushAsync(new ChangeAppointment(appointment)); };
                   
                    layout.Children.Add(button);
                }
            }
        }
    }
}