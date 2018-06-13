
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GBCalendar
{
	public partial class App : Application
	{
        public static Person UserLoggedIn { get; set; }
        private Stopwatch stopWatch = new Stopwatch();
        private TimeSpan countDown = new TimeSpan(0, 3, 0);

        public App ()
		{
			InitializeComponent();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            
                stopWatch.Start();
               
        }

		protected override void OnResume ()
        {
            // Handle when your app resumes
            //Sets the right starter page
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            if (ts > countDown)
            {
                UserLoggedIn = null;
                MainPage = new NavigationPage(new StartPage());
            }
            
        }
	}
}
