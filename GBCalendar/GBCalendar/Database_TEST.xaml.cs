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
	public partial class Database_TEST : ContentPage
	{
		public Database_TEST ()
		{
			InitializeComponent ();
		}

        void OnDatabaseTestClicked(object sender, EventArgs args)
        {
            

            //try
            //{
            //    Database database_1 = new Database();
            //    database_1.Write("TEST");
            //}
            //catch (Exception e)
            //{
            //    DisplayAlert("Alert", e.Message.ToString(), "OK");
            //}  
        }


    }
}