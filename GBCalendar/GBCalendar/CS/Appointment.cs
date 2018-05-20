using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Appointment
    {
        #region Felder und Eigenschaften der Klasse Appointment
        private int idAppointment;
        private string title;
        private Room room;
        private string startTime;
        private string endTime;
        private string allDayEvent;
        private Person creator;
        private string description;
        private SchoolClass schoolClass;


        public SchoolClass SchoolClass
        {
            get
            {
                return this.schoolClass;
            }

            set
            {
                this.schoolClass = this.SchoolClass;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = this.Description;
            }
        }



        public Person Creator
        {
            get
            {
                return this.creator;
            }
            set
            {
                this.creator = this.Creator;
            }
        }

        public string AllDayEvent
        {
            get
            {
                return this.allDayEvent;
            }
            set
            {
                this.allDayEvent = AllDayEvent;
            }
        }

        public string EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = EndTime;
            }
        }

        public string StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = StartTime;
            }
        }

        public Room Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = Room;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = Title;
            }
        }
        
        public int IdAppointment
        {
            get
            {
                return this.idAppointment;
            }
            private set
            {
                this.idAppointment = IdAppointment;
            }
        }
        #endregion

        #region Methoden der Klasse Appointment

        public Appointment(int idAppointment, string title, Room room, string startTime, string endTime, string allDayEvent, string description)
        {
            this.idAppointment = idAppointment;
            this.title = title;
            this.room = room;
            this.startTime = startTime;
            this.endTime = endTime;
            this.allDayEvent = allDayEvent;
            this.description = description;
        }

        #endregion
    }
}
