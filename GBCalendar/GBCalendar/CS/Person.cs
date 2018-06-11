using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Person
    {
        #region Felder und Eigenschaften der Klasse Person
        public int IdPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        // Da kein Zugriff auf das Passwort möglich sein soll, wurde die Eigenschaft nicht implementiert
        public string Password { get; }
        public int Role { get; set; }

        #endregion

        #region Methoden der Klasse Person
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="email">Email den angemeldeten Benutzers</param>
        /// <param name="password">Passwort des angemeldeten Benutzers</param>
        /// <param name="role">Rolle des angemeldteten Benutzers</param>
        public Person(int id, string firstName, string lastName, string email, string password, int role)
        {
            this.IdPerson = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
        
        #endregion
    }
}
