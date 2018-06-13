using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class SchoolClass
    {
        #region Felder und Eigenschaften der Klasse Class
        public int IdClass { get; private set; }
        public string ClassName { get; private set; }
        public List<Appointment> AppointmentList { get; set; } = new List<Appointment>();
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

        public void AddAppointment(string title, SchoolClass schoolClass, Room room, DateTime startTime, DateTime endTime, string allDayEvent, string description, Person creator)
        {
            try
            { 
                // Id als Rückgabewert von Appointment welches in Datenbankgeschrieben wird
                int appointmentId;

                Appointment newAppointmentWithoutId = new Appointment(title, room, schoolClass, startTime, endTime, allDayEvent, description, creator);

                //Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();
                //als Rückgabewert Appointment ID
                appointmentId = Writer.WriteAppointment(newAppointmentWithoutId);

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
