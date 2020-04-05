using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace MyAssignment
{
    
    public partial class Form2 : Form
    {
        bool result = false;
        public static string log;
        public static string filepath;
        static string activationcode;
        public static string unqiueName = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                 filepath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filepath);
            }

          
            if (pictureBox1.Image != null)
            {
                string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string pathToSaveImage = applicationBasePath + @"\images\";
                unqiueName = Guid.NewGuid().ToString() + ".jpg";
                string imgpath = pathToSaveImage + unqiueName;
                pictureBox1.Image.Save(imgpath);
            }



        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            System.IO.Directory.CreateDirectory(applicationBasePath + @"\images\");




        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validation();



            if (result== true)
            {
                if (validatemail()==true)
               {
                  if( validnic(maskedTextBox1.Text)==true)
                  {
                      if (sendcode() == true)
                      {
                          insertdata();
                          
                      }
                  }
                   
               }
            }

           

      }

     private   bool  validation()
        {


            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Name");
                textBox1.Focus();
                
                return result;
                
            }

            else if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Login");
                textBox2.Focus();
                return result;
            }

            else if (textBox3.Text == "")
            {
                MessageBox.Show("Enter Password");
                textBox3.Focus();
                return result;
                
            }

            else if (textBox4.Text == "")
            {
                MessageBox.Show("Enter Email");
                textBox4.Focus();
                return result;
                

            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("select Gender");
                comboBox1.Focus();
                return result;
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Enter Address");
                textBox5.Focus();
                return result;
            }

            else if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Enter Age");
                numericUpDown1.Focus();
                return result;
            }
            else if (maskedTextBox1.Text == "")
            {
                MessageBox.Show("Enter NIC");
                maskedTextBox1.Focus();
                return result;
            }


            else if (dateTimePicker1.Checked == false)
            {
                MessageBox.Show("Select Date");
                dateTimePicker1.Focus();
                return result;
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
            {
                MessageBox.Show("Enter Sports");

                return result;
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Choose picture");

                pictureBox1.Focus();
                return result;
            }

        else
            {
                result = true;
                return result;
            }
        }




     public bool sendcode()
     {

         var fromAddress = new MailAddress("EAD.SEMorning@gmail.com");
         var fromPassword = "SEMorning2017";
         var toAddress = new MailAddress(textBox4.Text);
         Random rand = new Random();

         activationcode = (rand.Next(99999)).ToString();
         string subject = "verification code";
         string body = "Dear " + textBox3.Text + " your varification code is " + activationcode;

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
                 

                 return true;
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Account with this email does not exist.");
                 return false;
             }
     }






     public bool validnic(string c)
     {
         Regex check = new Regex(@"^[0-9]{5}-[0-9]{7}-[0-9]{1}$");
         bool valid = false;
         valid = check.IsMatch(c);
         if (valid == true)
         {

             return true;
         }
         else
         {
             MessageBox.Show("Enter valid nic format (11111-1111111-1 )");
             return false;
         }
     }
         







     public bool validatemail()
     {
         
         
             string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-z])*@([0-9a-zA-z][-\\w]*[0-9a-zA-z]\\.)+[a-zA-Z]{2,9})$";
             if (Regex.IsMatch(textBox4.Text, pattern))
             {



                 return true;

             }
             else
             {
                 MessageBox.Show("Email is not valid");
                 return false;

                 
             }
         


     }

     
        private    void insertdata()
        {
                     string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                     SqlConnection ce=new SqlConnection(connstr);
                     SqlDataAdapter da = new SqlDataAdapter("select Name from dbo.Users where Login='"+textBox2.Text+"'", ce);
                     DataTable dt=new DataTable();
                     da.Fill(dt);
                     if(dt.Rows.Count>=1)
                     {
                         MessageBox.Show("User already exsist with this Login");
                     }
                     SqlDataAdapter db = new SqlDataAdapter("select Name from dbo.Users where Email='" + textBox4.Text +"'", ce);
                     DataTable dc = new DataTable();
                     da.Fill(dc);
                     if (dc.Rows.Count >= 1)
                     {
                         MessageBox.Show("User already exist with this email");
                     }
                     if (dc.Rows.Count == 0 && dt.Rows.Count == 0)
                     {
                         using (SqlConnection conn = new SqlConnection(connstr))
                         {
                             try
                             {


                                 log = textBox2.Text;

                                 string name = textBox1.Text;
                                 string login = textBox2.Text;
                                 string password = textBox3.Text;
                                 string email = textBox4.Text;
                                 char gender = Convert.ToChar(comboBox1.Text);
                                 string address = textBox5.Text;
                                 int age = Convert.ToInt32(numericUpDown1.Value);
                                 string nic = maskedTextBox1.Text;
                                 DateTime dob = Convert.ToDateTime(dateTimePicker1.Text);
                                 int  cricket =Convert.ToInt32( checkBox1.Checked == true ? 1 :0) ;
                                 int  hockey = Convert.ToInt32 (checkBox2.Checked == true ? 1 : 0 );
                                 int  chess = Convert.ToInt32(checkBox3.Checked ==true ? 1 : 0) ;




                                 conn.Open();
                                 //  string query = string.Format("INSERT INTO dbo.Users(Name,Login,Password,Email,Gender,Address,Age,NIC,DOB,IsCricket,Hockey,Chess,ImageName,CreatedOn) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",name,login,password,email,gender,address,age,nic,dob,cricket,hockey,chess,unqiueName,DateTime.Now);
                                 string query = string.Format("INSERT INTO dbo.Users(Name,Login,Password,Email,Gender,Address,Age,NIC,DOB,IsCricket,Hockey,Chess,ImageName,CreatedOn) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", name, login, password, email, gender, address, age, nic, dob, cricket, hockey, chess, unqiueName, DateTime.Now);
                                 SqlCommand command = new SqlCommand(query, conn);
                                 var rec = command.ExecuteNonQuery();
                                 MessageBox.Show("data inserted");
                                 conn.Close();
                                 this.Hide();
                                 Form3 obc = new Form3();
                                 obc.Show();

                             }

                             catch (Exception ex)
                             {
                                 MessageBox.Show(ex.Message);
                             }

                         }
                     }
                 }
          
                
            
           
        

        
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
