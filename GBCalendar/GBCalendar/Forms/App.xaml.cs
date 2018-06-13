
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
        #region Felder der Page App
        public static Person UserLoggedIn { get; set; }
        private Stopwatch stopWatch = new Stopwatch();
        private TimeSpan countDown = new TimeSpan(0, 3, 0);
        #endregion

        #region Methoden der Page App

        /// <summary>
        /// Initialisierung
        /// </summary>
        public App ()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Methode wenn die App neugestartet wird
        /// </summary>
		protected override void OnStart ()
		{
            // Handle when your app starts
            MainPage = new NavigationPage(new StartPage());
        }

        /// <summary>
        /// Methode wenn man die App verlässt, aber noch im Hintergrund offen hat
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps 
            stopWatch.Start();
        }

        /// <summary>
        /// Methode wenn man nach dem Verlassen wieder in die App kommt
        /// </summary>
		protected override void OnResume ()
        {
            // Handle when your app resumes
            // Sets the right starter page
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            if (ts > countDown)
            {
                UserLoggedIn = null;
                MainPage = new NavigationPage(new StartPage());
            }
            
        }
        #endregion
    }
}
