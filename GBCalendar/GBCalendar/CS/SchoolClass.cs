using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class SchoolClass
    {
        #region Felder und Eigenschaften der Klasse Class
        private int idClass;
        private string className;
        public List<Appointment> AppointmentList { get; set; }

        public int IdClass
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

        //public List<Appointment> AppointmentList1
        //{
        //    get
        //    {
        //        return this.appointmentList;
        //    }
        //    set
        //    {
        //        this.appointmentList = AppointmentList1;
        //    }
        //}
        #endregion

        #region Methoden der Klasse Class
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="className">Name der Schulklasse</param>
        public SchoolClass(int idClass, string className)
        {
            this.idClass = idClass;
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
        public void AddAppointment(string title, SchoolClass schoolClass, Room room, string startTime, string endTime, string allDayEvent, string description, Person creator)
        {

            try
            { 
                //Id als Rückgabewert von Appointment welches in Datenbankgeschrieben wird
                int appointmentId;

                Appointment newAppointmentWithoutId = new Appointment(title, room, schoolClass, startTime, endTime, allDayEvent, description, creator);
                //Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();
                appointmentId = Writer.WriteAppointment(newAppointmentWithoutId);


                //Appointment zur Liste ergänzen
                Appointment newAppointment = new Appointment(appointmentId, title, room, startTime, endTime, allDayEvent, description);

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
                


                //Zuerst in Datenbank schreiben
                DatabaseWriter Writer = new DatabaseWriter();
                Writer.UpdateAppointment(appointment);


                //Appointment zur Liste ergänzen
                //Appointment newAppointment = new Appointment(appointmentId, title, room, startTime, endTime, allDayEvent, description);

                //AppointmentList.Add(newAppointment);

            }
            catch (Exception)
            {

                throw;
            }



        }
        #endregion
    }
}
