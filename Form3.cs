using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAssignment
{
    public partial class Form3 : Form
    {
        public static string num;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 a3 = new Form1();
            a3.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            Form7 obj = new Form7();
            obj.Show();
            
            
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Form3.num = null;
            GC.Collect();
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Form4.lo == null)
            {
                num = Form2.log;
            }

            else
            {
                num = Form4.lo;
            }
            label1.Text = "Welcome "+num;
            
          

        }
    }
}
