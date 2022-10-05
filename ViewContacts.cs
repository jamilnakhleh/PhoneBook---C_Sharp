using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Phonebook
{
    public partial class ViewContacts : Form
    {
        public ViewContacts()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-6FIGVEFQ\SQLEXPRESS;Integrated Security = SSPI; Database=Phonebook;");
        private List<Contact> Contacts = new List<Contact>();

        private void updateList(string search = "")
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@search", search + "%");
            command.CommandText = "Select * from Contacts where firstname LIKE @search OR lastname LIKE @search OR concat(firstname,' ',lastname) LIKE @search";

            SqlDataReader reader = command.ExecuteReader();
            Contacts.Clear();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string mobile = reader.GetString(3);
                string notes = reader.GetString(4);
                Image photo = null;
                try
                {
                    photo = Image.FromStream(new MemoryStream(reader.GetSqlBytes(5).Buffer));
                }
                catch (Exception ex)
                {
                    
                }
                
                Contacts.Add(new Contact(id, firstname, lastname, mobile, notes, photo));
                listBox1.Items.Add(firstname + " " + lastname);
            }

            con.Close();
        }

        private void ViewContacts_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox1.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = Contacts[listBox1.SelectedIndex].firstname;
            textBox3.Text = Contacts[listBox1.SelectedIndex].lastname;
            textBox4.Text = Contacts[listBox1.SelectedIndex].mobile;
            textBox5.Text = Contacts[listBox1.SelectedIndex].notes;
            pictureBox1.Image = Contacts[listBox1.SelectedIndex].photo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditContact editContact = new EditContact(Contacts[listBox1.SelectedIndex]);
            Hide();
            editContact.ShowDialog();
            updateList();
            Show();
        }
    }
}