using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace MyAssignment
{
    public partial class Form4 : Form
    {
      public static  string activationcode;
      public static string email;
      public static string lo;
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 dd = new Form1();
            dd.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            SqlConnection ce = new SqlConnection(connstr);
            SqlDataAdapter da = new SqlDataAdapter("select Name from dbo.Users where Login='" + textBox1.Text + "'  and Password='"+textBox2.Text+"'" , ce);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                lo = textBox1.Text;
                this.Hide();
                Form3 al = new Form3();
                al.Show();
            }
            else
            {
                MessageBox.Show("Enter valid login and Password");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            email = textBox3.Text;

            if (sendcode() == true)
            {
                this.Hide();
                Form5 obj = new Form5();
                obj.Show();
            }

        }




   public bool sendcode()
  {

      var fromAddress = new MailAddress("EAD.SEMorning@gmail.com");
      var fromPassword = "SEMorning2017";
      var toAddress = new MailAddress(textBox3.Text);
      Random rand = new Random();
      
      activationcode = (rand.Next(99999)).ToString();
      string subject = "verification code";
      string body = "Dear "+textBox3.Text+" your varification code is "+activationcode;

      System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
      {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
          UseDefaultCredentials = false,
          Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
      };

      using (var message = new MailMessage(fromAddress, toAddress)
      {
          Subject = subject,
          Body = body
      })
          try
          {
              smtp.Send(message);
              MessageBox.Show("code sent");
                 
              return true;
          }
      catch(Exception ex)
          {
              MessageBox.Show("Account with this email does not exist.");
              return false;
          }
  }

   private void Form4_Load(object sender, EventArgs e)
   {

   }


     








    }
}
