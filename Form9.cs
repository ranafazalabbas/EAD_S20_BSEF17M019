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
    public partial class Form9 : Form
    {
        public static string id;
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'assignment4DataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.assignment4DataSet.Users);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                MessageBox.Show("are yor shure to edit them");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Please enter the UserID against which you want to edit the record.");
            }
            if(textBox1.Text!="")
            {
                string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                SqlConnection conn = new SqlConnection(connstr);
                SqlDataAdapter da = new SqlDataAdapter("select Name from dbo.Users where UserID='" + textBox1.Text + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count< 1)
                {
                    MessageBox.Show("User Does not exist against this UserID");
                }

                else
                {
                    id=textBox1.Text;
                    Form7 obj = new Form7();
                    obj.Show();



                }

            }
        }
    }
}
