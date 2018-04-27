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
        private List<Class> classes;
        private Class selectedclass;


        //public Class Selectedclass
        //{
        //    get
        //    {
        //        return this.selectedclass;
        //    }

        //}


        public MainPage()
        {
            InitializeComponent();

            try
            {
                //Fill up Classes for Appointment
                DatabaseReader readerclasses = new DatabaseReader();

                //"8" ist id des Techeachers. Muss später noch durch Person.idPers ersetzt werden
                classes = readerclasses.ReadClass(8);

            }
            catch (Exception)
            {
                throw;

            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new StartPage(), this);
            await Navigation.PopToRootAsync();
        }


        async void OnKlasseAuswaehlenClicked(object sender, EventArgs args)
        {

            //www.stackoverflow.com/questions/32313996/rendering-a-displayactionsheet-with-observablecollection-data-in-xamarin-cross-p?rq=1
            string action = await DisplayActionSheet("Klasse wählen:", "Cancel", null, classes.Select(Class => Class.ClassName).ToArray());

            if (action != "Cancel")
            {
                ToolbarItemClass.Text = action;
                selectedclass = classes.Find(Class => Class.ClassName == action);
            }



        }

        

    }
}