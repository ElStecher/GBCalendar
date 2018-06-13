using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Person
    {
        #region Felder und Eigenschaften der Klasse Person
        public int IdPerson { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        // Da kein Zugriff auf das Passwort möglich sein soll, wurde die Eigenschaft nicht implementiert
        public string Password { get; private set; }
        public int Role { get; private set; }

        #endregion

        #region Methoden der Klasse Person
        /// <summary>
        /// Konstruktor um eine angemeldete Person zu instanzieren
        /// </summary>
        /// <param name="email">Email den angemeldeten Benutzers</param>
        /// <param name="password">Passwort des angemeldeten Benutzers</param>
        /// <param name="role">Rolle des angemeldteten Benutzers</param>
        public Person(int id, string firstName, string lastName)
        {
            this.IdPerson = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        /// <summary>
        /// Konstruktor um einen Lehrer bzw. Creator zu instazieren
        /// </summary>
        /// <param name="id">ID der Person</param>
        /// <param name="firstName">Vorname der Person</param>
        /// <param name="lastName">Nachname der Person</param>
        /// <param name="email">E-Mail der Person</param>
        /// <param name="password">Passwort der Person</param>
        /// <param name="role">Rolle der Person</param>
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
