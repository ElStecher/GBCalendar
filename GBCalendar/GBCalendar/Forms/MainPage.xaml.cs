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
        #region Felder der Page MainPage
        public List<SchoolClass> classes { get; private set; }
        public static SchoolClass Selectedclass { get; private set; }
        #endregion

        #region Methoden der Page MainPage
        /// <summary>
        /// Initialisierung
        /// </summary>
        public MainPage()
        {
            NavigationPage.SetHasBackButton(this, false);

            // Initialisierung
            InitializeComponent();  
            try
            {
                // Füllt die Klassen für das Appointment
                DatabaseReader readerclasses = new DatabaseReader();

                classes = readerclasses.ReadClasses(App.UserLoggedIn.IdPerson);

            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");

            }
        }

        /// <summary>
        /// Konstruktor für aufruf nach NewAppointment
        /// </summary>
        /// <param name="selectedclass">Ausgewählte Klasse</param>
        public MainPage(SchoolClass selectedclass)
        {

            try
            {

                NavigationPage.SetHasBackButton(this, false);
                InitializeComponent();


            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();
                classes = readerclasses.ReadClasses(App.UserLoggedIn.IdPerson);
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte wenden Sie sich an den Support: " + Environment.NewLine + e.Message, "OK");
            }
        }

        /// <summary>
        /// Button damit sich der Benutzer abmelden kann
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnLogoutButtonClicked(object sender, EventArgs args)
        {
            bool result = await DisplayAlert("Abmelden", "Sie werden abgemeldet", "OK", "Cancel");

            // Falls der Button "Cancel" bedrückt wurde, wird der Benutzer abgemeldet
            // https://forums.xamarin.com/discussion/71594/displayalert-detect-cancel-button-is-clicked-or-not
            // Hat der Benutzer auf OK geklickt, wird er abgemeldet
            if (result == true)
            {
                App.UserLoggedIn = null;
                Navigation.InsertPageBefore(new StartPage(), this);
                await Navigation.PopToRootAsync();
            }
            // Anonsten bleibt er auf der MainPage
            else
            {
                // Kehrt zur MainPage zurück
                return;
            }
        }

        /// <summary>
        /// Nachdem der Benutzer eine Klasse ausgewählt hat, kann er ein neues Ereignis erstellen und die Ereignisse für die gewählte Klasse werden aufgelistet
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        async void OnClassSelectedClicked(object sender, EventArgs args)
        {
            try
            {
                //www.stackoverflow.com/questions/32313996/rendering-a-displayactionsheet-with-observablecollection-data-in-xamarin-cross-p?rq=1
                string action = await DisplayActionSheet("Klasse wählen:", "Cancel", null, classes.Select(SchoolClass => SchoolClass.ClassName).ToArray());

            // Wenn der angemeldete Benutzer ein Lehrer ist, kann dieser ein neues Ereignis erstellen
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
            
            // Nachdem eine Klasse ausgewählt wurde, kann der Benutzer die Daten aktualisieren
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

            // Hat der Benutzer die Klasse ausgewählt, werden die Ereignisse der Klasse aufgelistet
            if (action != "Cancel" && action != null)
            {
                ToolbarItemClass.Text = action;
                Selectedclass = classes.Find(SchoolClass => SchoolClass.ClassName == action);
                ShowAppointments();
            }
        }

        /// <summary>
        /// Wenn der Benutzer ein neues Ereignis erstellen möchte wird er auf die entsprechende Seite navigiert
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        private async void OnCallNewAppointmentPageClicked(object sender, EventArgs args)
        {
            // Navigiert zur Seite
            await Navigation.PushAsync(new NewAppointment());
        }

        /// <summary>
        /// Will der Benutzer die Daten aktualisieren, kann er den Button klicken 
        /// </summary>
        /// <param name="sender">Autogeneriert</param>
        /// <param name="args">Autogeneriert</param>
        private void OnRefreshClicked(object sender, EventArgs args)
        {
            // Instanzierung
            DatabaseReader databaseReader = new DatabaseReader();

            databaseReader.ReadAppointments(Selectedclass);
            ShowAppointments();
        }

        /// <summary>
        /// Zeigt die Appointments der ausgewählten Klasse an
        /// </summary>
        public void ShowAppointments()
        {
            // Sortiert die Liste nach Datum
            SortAscending(Selectedclass.AppointmentList);

            ScrollView scrollView = new ScrollView();

            StackLayout layout = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                Spacing = 0
            };

                scrollView.Content = layout;
                Content = scrollView;

            // Wenn keine Ereignisse für die ausgewählte Klasse gefunden wurde, wirde eine Meldung ausgegeben
            if (Selectedclass.AppointmentList.Count == 0)
            {
                // https://forums.xamarin.com/discussion/69446/adding-label-to-page
                Label label = new Label
                {
                    Text = "Keine Ereignisse gefunden",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Content = label;
            }
            else
            {
                // Formatiert die Informationen des Appointments für die Anzeige
                // Erstellt ein Button für jedes Appointment
                foreach (Appointment appointment in Selectedclass.AppointmentList)
                {
                    // Fromatierung des Datums und der Zeiten
                    string appointmentDate = appointment.StartTime.ToString("dd.MM.yyyy");
                    string appointmentStart = appointment.StartTime.ToString("HH:mm");
                    string appointmentEnd = appointment.EndTime.ToString("HH:mm");
                    string showingText;

                    // Wenn es sich um ein ganztägiges Ereignis handelt, wird im Button dies angezeigt
                    if (appointmentStart.Contains("00:00") && appointmentEnd.Contains("23:59"))
                    {
                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + "Ganztägiges Ereignis";
                    }
                    else
                    {
                        showingText = appointment.Title + "\n" + appointmentDate + "\n" + appointmentStart + " - " + appointmentEnd;
                    }

                    // Erstellen des Buttons
                    Button button = new Button
                    {
                        Text = showingText,
                        BackgroundColor = Color.WhiteSmoke,
                        CornerRadius = 0,
                        Margin = new Thickness(10, 0, 10, 0)
                    };

                   
                    button.Clicked += async delegate { await Navigation.PushAsync(new ChangeAppointment(appointment)); };

                   // Button zum Layout hinzufügen
                    layout.Children.Add(button);
                }
            }

        }

        /// <summary>
        /// Sortiert eine Liste von Appointments nach dem Datum
        /// </summary>
        /// <param name="list">Liste mit den Appointments für die ausgewählte Klasse</param>
        /// <returns></returns>
        static List<Appointment> SortAscending(List<Appointment> list)
        {        
                list.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                return list;
        }
        #endregion
    }
}