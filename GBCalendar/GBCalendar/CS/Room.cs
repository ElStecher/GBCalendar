using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Room
    {
        #region Felder und Eigenschaften der Klasse Room
        public int IdRoom { get; private set; }
        public string RoomName { get; private set; }
        #endregion

        #region Methoden der Klasse Room
        /// <summary>
        /// Standart Konstruktor
        /// </summary>
        /// <param name="idRoom">ID des Rooms in der Datenbank</param>
        /// <param name="roomName">Name bzw. Beschreibung des Raums</param>
        public Room(int idRoom, string roomName)
        {
            this.IdRoom = idRoom;
            this.RoomName = roomName;
        }
        #endregion
    }
}
