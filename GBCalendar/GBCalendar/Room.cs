﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    class Room
    {
        #region Felder und Eigenschaften der Klasse Room
        private int idRoom;
        private string roomName;

        public int IdRoom
        {
            get
            {
                return this.idRoom;
            }
            private set
            {
                this.idRoom = IdRoom;
            }
        }

        public string RoomName
        {
            get
            {
                return this.roomName;
            }

            private set
            {
                this.roomName = RoomName;
            }
        }

        #endregion

        #region Methoden der Klasse Room
        public Room(string roomName)
        {
            this.roomName = roomName;
        }
        #endregion
    }
}