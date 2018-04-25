using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    class Class
    {
        #region Felder und Eigenschaften der Klasse Class
        private string idClass;
        private string className;
        private List<Appointment> appointmentList;

        public string IdClass
        {
            get
            {
                return this.idClass;
            }
            private set
            {
                this.idClass = IdClass;
            }
        }

        public string ClassName
        {
            get
            {
                return this.className;
            }
            private set
            {
                this.className = ClassName;
            }
        }

        public List<Appointment> AppointmentList
        {
            get
            {
                return this.appointmentList;
            }
            private set 
            {
                this.appointmentList = AppointmentList;
            }
        }
        #endregion

        #region Methoden der Klasse Class
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="className">Name der Schulklasse</param>
        public Class(string className)
        {
            this.className = className;
        }

        /// <summary>
        /// Listet alle Appointments der ausgewählten Schulklasse auf
        /// </summary>
        private void ListAppointments()
        {

        }

        /// <summary>
        /// Löscht einen Termin aus der Liste 
        /// und aktualisiert die Datensätze in der Datenbank
        /// </summary>
        public void RemoveAppointment()
        {

        }

        /// <summary>
        /// Fügt einen neuen Termin zur Liste
        /// und zur Datenbank hinzu
        /// </summary>
        public void AddAppointment(string title, Room room, DateTime startTime, DateTime endTime, bool allDayEvent, Person Creator)
        {
            Appointment newAppointment = new Appointment(title, room, startTime, endTime, allDayEvent, Creator);
            AppointmentList.Add(newAppointment);

            
        }

        /// <summary>
        /// Bearbeitet einen ausgewählten Termin
        /// </summary>
        public void EditAppointment()
        {
        
        }
        #endregion
    }
}
