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
    public partial class Form5 : Form
    {
      public static  string code;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            code = Form4.activationcode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code1 = textBox1.Text;
            if(code==code1)
            {
                this.Hide();
                Form6 obj = new Form6();
                obj.Show();
            }
            if(code1!=code)
            {
                MessageBox.Show("Enter Valid Code Sent To Email");
            }
        }
    }
}
