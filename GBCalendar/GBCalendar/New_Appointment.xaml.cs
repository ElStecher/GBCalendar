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
			InitializeComponent ();

            Roompicker.Items.Add("TEST");
            Roompicker.Items.Add("TEST2");
            Roompicker.Items.Add("TEST3");
            Roompicker.Items.Add("TEST");
            Roompicker.Items.Add("TEST2");
            Roompicker.Items.Add("TEST3");
            Roompicker.Items.Add("TEST");
            Roompicker.Items.Add("TEST2");
            Roompicker.Items.Add("TEST3");
            Roompicker.Items.Add("TEST");
            Roompicker.Items.Add("TEST2");
            Roompicker.Items.Add("TEST3");
            Roompicker.Items.Add("TEST");
            Roompicker.Items.Add("TEST2");
            Roompicker.Items.Add("TEST3");

        }



	}
}