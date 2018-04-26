using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class DatabaseWriter
    {
        #region Felder und Eigenschaften der Klasse DatabaseWriter

        #endregion
        // @Fabio ToDo: Schauen wie Singleton-Pattern genau umgesetzt wird und implementieren
        #region Methoden der Klasse DatabaseWriter

        // @Sam ToDo: Methode Write anpassen und Parameter erweitern um SQL-String korrekt zusammenzustellen
        public void WriteAppointment(Appointment A)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "INSERT VALUES(" + A.Title + ", " + A.ClassId + ", " + A.StartTime + ", " + A.EndTime + ", " + A.Description + ", " + A.AllDayEvent + ", " + A.Category + ") INTO Appointment(Title, Class_idClass, Room_idRoom, Start_Time, End_Time, Description,  Alldayevent, Category) ";
                command.ExecuteNonQuery();
                Connect.CloseConnection();

            }
            catch (Exception)
            {
                throw;
            }
            
        }
        // @Sam ToDo: Methode erstellen um Daten in der Datenbank upzudaten
        #endregion
    }
}
