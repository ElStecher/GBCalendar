using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;



namespace GBCalendar
{
    class DatabaseConnector
    {
        #region Felder der Klasse DatabaseConnector
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

        #region Methoden der Klasse DatabaseConnector
        /// <summary>
        /// Öffnet eine Verbindung mir der definierten Datenbank
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                this.connectionString = "SERVER=" + this.server + ";" + "PORT=" + this.port + ";" + "DATABASE=" +
                this.database + ";" + "UID=" + this.user + ";" + "PASSWORD=" + this.password + ";";
                this.Connection = new MySqlConnection(this.connectionString);
                this.Connection.Open();

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
