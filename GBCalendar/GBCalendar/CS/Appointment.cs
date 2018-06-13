using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Appointment
    {
        #region Felder der Klasse Appointment
        
        public SchoolClass SchoolClass { get; set; }
        public string Description { get; set; }
        public Person Creator { get; set; }
        public string AllDayEvent { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public Room Room { get; set; }
        public string Title { get; set; } 
        public int IdAppointment { get; set; }
     
        #endregion

        #region Methoden der Klasse Appointment

        /// <summary>
        /// Konstruktor zum auslesen der Appointments aus der Datenbank
        /// </summary>
        /// <param name="idAppointment">ID des Appointments</param>
        /// <param name="title">Title des Appointments</param>
        /// <param name="room">Raum in dem das Appointment stattfindet</param>
        /// <param name="startTime">Startzeit des Appointments</param>
        /// <param name="endTime">Endzeit des Appointments</param>
        /// <param name="allDayEvent">Booelan falls das Appointment den ganzen Tag dauert</param>
        /// <param name="description">Beschreibung des Appointments</param>
        /// <param name="creator">Lehrer der das Appointment erstellt hat</param>
        public Appointment(int idAppointment, string title, Room room, DateTime startTime, DateTime endTime, string allDayEvent, string description, Person creator)
        {
            this.IdAppointment = idAppointment;
            this.Title = title;
            this.Room = room;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.AllDayEvent = allDayEvent;
            this.Description = description;
            this.Creator = creator;
        }

        /// <summary>
        /// Konstruktor um ein Appointment in die Datenbank zuschreiben
        /// </summary>
        /// <param name="title">Title des Appointments</param>
        /// <param name="room">Raum des Appointments</param>
        /// <param name="schoolClass">Schulklasse für die das Appointment gilt</param>
        /// <param name="startTime">Startzeit des Appointments</param>
        /// <param name="endTime">Endzeit des Appointments</param>
        /// <param name="allDayEvent">Booelan falls das Appointment den ganzen Tag dauert</param>
        /// <param name="description">Beschreibung zum Appointment</param>
        /// <param name="creator">Lehrer der das Appointment erstellt hat</param>
        public Appointment(string title, Room room, SchoolClass schoolClass, DateTime startTime, DateTime endTime, string allDayEvent, string description, Person creator)
        {
            this.Title = title;
            this.Room = room;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.AllDayEvent = allDayEvent;
            this.Description = description;
            this.SchoolClass = schoolClass;
            this.Creator = creator;
        }

        #endregion
    }
}
