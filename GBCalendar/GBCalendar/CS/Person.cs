using System;
using System.Collections.Generic;
using System.Text;

namespace GBCalendar
{
    public class Person
    {
        #region Felder und Eigenschaften der Klasse Person
        private int idPerson;
        private string firstName;
        private string lastName;
        private string email;
        // Da kein Zugriff auf das Passwort möglich sein soll, wurde die Eigenschaft nicht implementiert
        private string password;
        private int role;

        public int IdPerson
        {
            get
            {
                return this.idPerson;
            }
            private set
            {
                this.idPerson = IdPerson;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            private set
            {
                this.firstName = FirstName;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            private set
            {
                this.lastName = LastName;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            private set
            {
                this.email = Email;
            }
        }

        public int Role
        {
            get
            {
                return this.role;
            }
            private set
            {
                this.role = Role;
            }
        }

        #endregion

        #region Methoden der Klasse Person
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="email">Email den angemeldeten Benutzers</param>
        /// <param name="password">Passwort des angemeldeten Benutzers</param>
        /// <param name="role">Rolle des angemeldteten Benutzers</param>
        public Person(int id, string firstName, string lastName)
        {
            this.idPerson = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }


        public Person(int id, string firstName, string lastName, string email, string password, int role)
        {
            this.idPerson = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.role = role;
        }

        /// <summary>
        /// Authentifiziert den Benutzer
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public void LogIn(string email, string password)
        {
            
        }

        
        #endregion
    }
}
