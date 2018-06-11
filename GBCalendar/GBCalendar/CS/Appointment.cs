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
        public string EndTime { get; set; }
        public string StartTime { get; set; }
        public Room Room { get; set; }
        public string Title { get; set; } 
        public int IdAppointment { get; set; }
     
        #endregion

        #region Methoden der Klasse Appointment

        public Appointment(int idAppointment, string title, Room room, string startTime, string endTime, string allDayEvent, string description)
        {
            this.IdAppointment = idAppointment;
            this.Title = title;
            this.Room = room;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.AllDayEvent = allDayEvent;
            this.Description = description;
        }

        public Appointment(string title, Room room, SchoolClass schoolClass, string startTime, string endTime, string allDayEvent, string description, Person creator)
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
