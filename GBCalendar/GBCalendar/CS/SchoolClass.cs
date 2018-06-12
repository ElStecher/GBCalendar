using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class SchoolClass
    {
        #region Felder und Eigenschaften der Klasse Class
        public int IdClass { get; set; }
        public string ClassName { get; set; }
        public List<Appointment> AppointmentList { get; set; }
        #endregion

        #region Methoden der Klasse Class
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="className">Name der Schulklasse</param>
        public SchoolClass(int idClass, string className)
        {
            this.IdClass = idClass;
            this.ClassName = className;
        }

        public void AddAppointment(string title, SchoolClass schoolClass, Room room, string startTime, string endTime, string allDayEvent, string description, Person creator)
        {
            try
            { 
                // Id als Rückgabewert von Appointment welches in Datenbankgeschrieben wird
                int appointmentId;

                Appointment newAppointmentWithoutId = new Appointment(title, room, schoolClass, startTime, endTime, allDayEvent, description, creator);
                //Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();
                appointmentId = Writer.WriteAppointment(newAppointmentWithoutId);


                
                // Formatierung Zeit/Datum (unterschiedliches Format für Programmund Datenbank)
                DateTime startTimeObj = new DateTime(int.Parse(startTime.Substring(0, 4)), int.Parse(startTime.Substring(5, 2)),int.Parse(startTime.Substring(8, 2)), int.Parse(startTime.Substring(11, 2)), int.Parse(startTime.Substring(14, 2)), int.Parse(startTime.Substring(17, 2)));
                startTime = startTimeObj.ToString("dd.MM.yyyy HH:mm:ss");

                DateTime endTimeObj = new DateTime(int.Parse(endTime.Substring(0, 4)), int.Parse(endTime.Substring(5, 2)), int.Parse(endTime.Substring(8, 2)), int.Parse(endTime.Substring(11, 2)), int.Parse(endTime.Substring(14, 2)), int.Parse(endTime.Substring(17, 2)));
                endTime = endTimeObj.ToString("dd.MM.yyyy HH:mm:ss");


                // Appointment zur Liste ergänzen
                Appointment newAppointment = new Appointment(appointmentId, title, room, startTime, endTime, allDayEvent, description, creator);
                AppointmentList.Add(newAppointment);

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Bearbeitet einen ausgewählten Termin
        /// </summary>
        public void EditAppointment(Appointment appointment)
        {
            try
            {
                // Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();
                Writer.UpdateAppointment(appointment);

                // Formatierung Zeit/Datum (unterschiedliches Format für Programmund Datenbank)
                DateTime startTimeObj = new DateTime(int.Parse(appointment.StartTime.Substring(0, 4)), int.Parse(appointment.StartTime.Substring(5, 2)), int.Parse(appointment.StartTime.Substring(8, 2)), int.Parse(appointment.StartTime.Substring(11, 2)), int.Parse(appointment.StartTime.Substring(14, 2)), int.Parse(appointment.StartTime.Substring(17, 2)));
                appointment.StartTime = startTimeObj.ToString("dd.MM.yyyy HH:mm:ss");

                DateTime endTimeObj = new DateTime(int.Parse(appointment.EndTime.Substring(0, 4)), int.Parse(appointment.EndTime.Substring(5, 2)), int.Parse(appointment.EndTime.Substring(8, 2)), int.Parse(appointment.EndTime.Substring(11, 2)), int.Parse(appointment.EndTime.Substring(14, 2)), int.Parse(appointment.EndTime.Substring(17, 2)));
                appointment.EndTime = endTimeObj.ToString("dd.MM.yyyy HH:mm:ss");

                // Appointment in Liste bearbeiten(löschen und neu Hinzufügen)
                MainPage.Selectedclass.AppointmentList.RemoveAt(MainPage.Selectedclass.AppointmentList.FindIndex(a => a.IdAppointment == appointment.IdAppointment));
                MainPage.Selectedclass.AppointmentList.Add(appointment);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
