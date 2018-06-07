﻿using System;
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

        public void WriteAppointment(Appointment appointment)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "INSERT VALUES(" + appointment.Title + ", " + appointment.SchoolClass.IdClass + ", " + appointment.Room.IdRoom + ", " + appointment.StartTime + ", " + appointment.EndTime + ", " + appointment.Description + ", " + appointment.AllDayEvent + ") INTO Appointment(Title, Class_idClass, Room_idRoom, Start_Time, End_Time, Description,  Alldayevent) ";
                command.ExecuteNonQuery();
                Connect.CloseConnection();

            }
            catch (Exception)
            {
                throw;
            }
            
        }
        // @Sam ToDo: Methode erstellen um Daten in der Datenbank upzudaten

        public void UpdateAppointment(Appointment appointment)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "UPDATE Appointment SET Title =" + appointment.Title + ", Class_idClass=" + appointment.SchoolClass.IdClass + ", Room_idRoom=" + appointment.Room.IdRoom + ", Start_Time=" + appointment.StartTime + ", End_Time=" + appointment.EndTime + ", " + appointment.Description + ", Alldayevent=" + appointment.AllDayEvent + ", Category=" + string.Empty + ") WHERE idAppointment=" + appointment.IdAppointment + ";";
                command.ExecuteNonQuery();
                Connect.CloseConnection();

            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion
    }
}
