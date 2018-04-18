using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GBCalendar
{
	public partial class App : Application
	{
        public static bool IsUserLoggedIn { get; set; }

		public App ()
		{
			InitializeComponent();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            MainPage = new NavigationPage(new StartPage());
        }

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
            System.Threading.Thread.Sleep(300000);
            IsUserLoggedIn = false;
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes
            //Sets the right starter page
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new StartPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }
	}
}
