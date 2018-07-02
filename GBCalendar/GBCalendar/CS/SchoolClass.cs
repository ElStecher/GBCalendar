using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class SchoolClass
    {
        #region Felder und Eigenschaften der Klasse SchoolClass
        public int IdClass { get; private set; }
        public string ClassName { get; private set; }
        public List<Appointment> AppointmentList { get; set; } = new List<Appointment>();
        #endregion

        #region Methoden der Klasse SchoolClass
        
        /// <summary>
        /// Standart Konstruktor
        /// </summary>
        /// <param name="idClass">ID der Klasse</param>
        /// <param name="className">Klassenname bzw. -bezeichnung</param>
        public SchoolClass(int idClass, string className)
        {
            this.IdClass = idClass;
            this.ClassName = className;
        }

       /// <summary>
       /// Fügt ein ausgewähltes Appointment zur Liste der Appointments einer Klasse und zur Datenbank hinzu
       /// </summary>
       /// <param name="appointment">Appointment das zur Liste hinzugefügt werden soll</param>
        public void AddAppointment(Appointment appointment)
        {
            try
            { 
                //Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();

                //als Rückgabewert Appointment ID
                appointment.IdAppointment = Writer.WriteAppointment(appointment);

                // Appointment zur Liste ergänzen
                AppointmentList.Add(appointment);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Speichert ein bearbeitetes Appointment auch in der Datenbank
        /// </summary>
        /// <param name="appointment">Das zu bearbeitende Appointment</param>
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

        /// <summary>
        /// Löscht ein ausgewähltes Appointment aus der Liste und der Datenbank
        /// </summary>
        /// <param name="appointment">Das zu löschende Appointment</param>
        public void DeleteAppointment(Appointment appointment)
        {
            try
            {
                // Zuerst in Datenbank löschen
                DatabaseWriter Writer = new DatabaseWriter();
                Writer.DelteAppointment(appointment);

                // Appointment in Liste löschen 
                MainPage.Selectedclass.AppointmentList.RemoveAt(MainPage.Selectedclass.AppointmentList.FindIndex(a => a.IdAppointment == appointment.IdAppointment));

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
