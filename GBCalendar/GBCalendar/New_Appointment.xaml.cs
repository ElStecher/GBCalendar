using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class New_Appointment : ContentPage
	{

        private MySqlConnection connection;
        private string server = "maissen.internet-box.ch";
        private string database = "Prototype_GBCDrive";
        private string user = "App_User";
        private string port = "2100";
        private string password = "!KalenderApp_User100";
        private string connectionString;
        private string p_command = "SELECT Roomname FROM Room;";


        public New_Appointment ()
		{
            InitializeComponent();

            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();


            MySqlCommand command = connection.CreateCommand();
            command.CommandText = p_command;

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Roompicker.Items.Add(reader.GetValue(0).ToString());
            }

            reader.Close();

            //query liest nur bestimmte Klassen eines Lehrers Aus!
            p_command = "SELECT Classname FROM Class WHERE idclass IN (SELECT Class_idClass FROM Class_has_Person WHERE Person_idPerson LIKE 8);";
            command = connection.CreateCommand();
            command.CommandText = p_command;

            command.CommandText = p_command;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Classpicker.Items.Add(reader.GetValue(0).ToString());
            }

            reader.Close();
        }


    }
}