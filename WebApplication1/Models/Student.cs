using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class Student
    {
        private string m_firstName;
        public string FirstName
        {
            get { return m_firstName; }
            set
            {
                m_firstName = value;

            }
        }

        private string m_lastName;
        public string LastName
        {
            get { return m_lastName; }
            set
            {
                m_lastName = value;

            }
        }

        private string m_id;
        public string ID
        {
            get { return m_id; }
            set
            {
                m_id = value;

            }
        }
    }
}