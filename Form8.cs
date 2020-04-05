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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            SqlConnection ce = new SqlConnection(connstr);
            SqlDataAdapter da = new SqlDataAdapter("select AdminName from dbo.Admin where Login='" + textBox1.Text + "'  and Password='" + textBox2.Text + "'", ce);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                
                this.Hide();
                Form9 al = new Form9();
                al.Show();
            }
            else
            {
                MessageBox.Show("Enter valid login and Password");
            }

        }
    }
}
