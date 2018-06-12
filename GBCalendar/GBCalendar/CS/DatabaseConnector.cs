using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;



namespace GBCalendar
{
    class DatabaseConnector
    {
        #region Felder und Eigenschaften der Klasse DatabaseConnector
        public MySqlConnection Connection { get; private set; }

        #region Eigenschaften um Verbindung mit Datenbank herzustellen
        private string server = "maissen.internet-box.ch";
        private string database = "Prototype_GBCDrive";
        private string user = "App_User";
        private string port = "2100";
        private string password = "!KalenderApp_User100";
        private string connectionString;
        #endregion
        #endregion
        // @Fabio ToDo: Schauen wie Singleton-Pattern genau umgesetzt wird und implementieren
        #region Methoden der Klasse DatabaseConnector

        /// <summary>
        /// Öffnet eine Verbindung mir der definierten Datenbank
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
                Connection = new MySqlConnection(connectionString);
                Connection.Open();

            }
            catch (Exception e)
            {
                throw new Exception("Fehler beim Erstellen einer Verbindung mit der Datenbank: " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Schliesst die offene Verbindung
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                this.Connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Fehler beim Schliessender Verbindung mit der Datenbank: " +  e.Message.ToString());
            }
            
        }
        #endregion
    }
}
