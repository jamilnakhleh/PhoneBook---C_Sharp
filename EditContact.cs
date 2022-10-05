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

namespace Phonebook
{
    public partial class EditContact : Form
    {
        private int id;
        public EditContact(Contact mycontact)
        {
            InitializeComponent();
            textBox1.Text = mycontact.firstname;
            textBox2.Text = mycontact.lastname;
            textBox3.Text = mycontact.mobile;
            textBox4.Text = mycontact.notes;
            pictureBox1.Image = mycontact.photo;
            id = mycontact.id;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditContact_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.FileName = "";
            od.Filter = "Supported Images|*.jpg;*.jpeg;*.png";
            if (od.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(od.FileName);
            }
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-6FIGVEFQ\SQLEXPRESS;Integrated Security = SSPI; Database=Phonebook;");

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@firstname", textBox1.Text);
            command.Parameters.AddWithValue("@lastname", textBox2.Text);
            command.Parameters.AddWithValue("@mobile", textBox3.Text);
            command.Parameters.AddWithValue("@notes", textBox4.Text);
            command.Parameters.AddWithValue("@photo", new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[])));
            command.Parameters.AddWithValue("@id", id);

            command.CommandText = "update contacts set firstname=@firstname, lastname=@lastname, mobile=@mobile,notes=@notes, photo=@photo where id=@id";

            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Contact was updated!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to update contact!");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "delete from contacts where id=@id";

            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Contact was deleted!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to delete contact!");
                con.Close();
            }
        }
    }
}