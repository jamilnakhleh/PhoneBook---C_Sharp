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
    public partial class AddContact : Form
    {
        public AddContact()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-6FIGVEFQ\SQLEXPRESS;Integrated Security = SSPI; Database=Phonebook;");

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.FileName = "";
            od.Filter = "Supported Images|*.jpg;*.jpeg;*.png";
            if (od.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(od.FileName);   
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@photo", new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[])));
            command.Parameters.AddWithValue("@firstname",  textBox1.Text);
            command.Parameters.AddWithValue("@lastname", textBox2.Text);
            command.Parameters.AddWithValue("@mobile", textBox3.Text);
            command.Parameters.AddWithValue("@notes", textBox4.Text);

            command.CommandText = "insert into contacts (firstname,lastname,mobile,notes,photo) values (@firstname,@lastname,@mobile,@notes,@photo)";

            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Contact was added!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to add contact!");
                con.Close();
            }
        }

        private void AddContact_Load(object sender, EventArgs e)
        {

        }
    }
}