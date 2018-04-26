using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    class Appointment
    {
        #region Felder und Eigenschaften der Klasse Appointment
        private int idAppointment;
        private string title;
        private Room room;
        private DateTime startTime;
        private DateTime endTime;
        private bool allDayEvent;
        private Person creator;
        private string description;
        private string category;
        private int classId;


        public int ClassId
        {
            get
            {
                return this.classId;
            }

            set
            {
                this.classId = this.ClassId;
            }
        }



        public string Category
        {
            get
            {
                return this.category;
            }

            set
            {
                this.category = this.Category;
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

        public bool AllDayEvent
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

        public DateTime EndTime
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

        public DateTime StartTime
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

        public Appointment(string title, int classId, Room room, DateTime startTime, DateTime endTime, bool allDayEvent, Person creator)
        {
            this.classId = classId;
            this.title = title;
            this.room = room;
            this.startTime = startTime;
            this.endTime = endTime;
            this.allDayEvent = allDayEvent;
            this.creator = creator;
        }

        #endregion
    }
}
