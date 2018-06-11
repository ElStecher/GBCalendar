using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Room
    {
        #region Felder und Eigenschaften der Klasse Room
        public int IdRoom { get; set; }
        public string RoomName { get; set; }
        #endregion

        #region Methoden der Klasse Room
        public Room(int idRoom, string roomName)
        {
            this.IdRoom = idRoom;
            this.RoomName = roomName;
        }
        #endregion
    }
}
