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

namespace MyAssignment
{
    public partial class Form6 : Form
    {
        public static string mail;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mail = Form4.email;
            string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            string query = string.Format("UPDATE dbo.Users set Password='"+textBox1.Text+"'  where Email='"+mail+"' ");
            SqlCommand command = new SqlCommand(query, conn);
            var rec = command.ExecuteNonQuery();
            MessageBox.Show("Password is updated");
            conn.Close();
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
        
        }
    }
}
