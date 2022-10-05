using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Phonebook
{
    public class Contact
    {
        public string firstname, lastname, mobile, notes;
        public int id;
        public Image photo;
        public Contact(int id, string firstname, string lastname, string mobile, string notes, Image photo)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.mobile = mobile;
            this.notes = notes;
            this.photo = photo;
        }
    }
}