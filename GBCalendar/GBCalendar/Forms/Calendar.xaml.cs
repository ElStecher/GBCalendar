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
	public partial class Calendar : ContentPage
	{
		public Calendar (/*Class selectedClass*/)
		{
			InitializeComponent ();
            
            // Funktion für Auslesen von Appointments und dynamisches Anzeigen in Xamarin Forms
		}
	}
}