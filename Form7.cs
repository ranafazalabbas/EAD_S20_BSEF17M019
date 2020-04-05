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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MyAssignment
{
    public partial class Form7 : Form

    {
        public static string bb;
        public static bool result = false;
        public static string bol;
        static string activationcode;
        public static string uniname="";
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string applicationBasePath1 = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            System.IO.Directory.CreateDirectory(applicationBasePath1 + @"\images\");

            bol = Form3.num;

            string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            if (Form9.id == "")
            {
                string query = "select * from dbo.users where Login='" + bol + "'";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bb = (dr["Login"].ToString());
                    string path = Form2.filepath;
                    textBox1.Text = (dr["Name"].ToString());
                    textBox2.Text = (dr["Login"].ToString());
                    textBox3.Text = (dr["Password"].ToString());
                    textBox4.Text = (dr["Email"].ToString());
                    comboBox1.Text = (dr["Gender"]).ToString();
                    textBox5.Text = (dr["Address"].ToString());
                    numericUpDown1.Value = Convert.ToInt32(dr["Age"]);
                    maskedTextBox1.Text = (dr["NIC"].ToString());
                    dateTimePicker1.Text = (dr["DOB"].ToString());
                    checkBox1.Checked = Convert.ToBoolean(dr["IsCricket"]);
                    checkBox2.Checked = Convert.ToBoolean(dr["Hockey"]);
                    checkBox3.Checked = Convert.ToBoolean(dr["Chess"]);
                    string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

                    pictureBox1.Load(applicationBasePath + @"\images\" + (dr["ImageName"]).ToString());
                    if (uniname == "")
                    {
                        uniname = (dr["ImageName"]).ToString();
                    }
                    conn.Close();


                }
            }
            if (Form9.id != "")
            {
                string query1 = "select * from dbo.Users  where UserID='" + Form9.id + "'";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    bb = (dr1["Login"].ToString());
                    string path = Form2.filepath;
                    textBox1.Text = (dr1["Name"].ToString());
                    textBox2.Text = (dr1["Login"].ToString());
                    textBox3.Text = (dr1["Password"].ToString());
                    textBox4.Text = (dr1["Email"].ToString());
                    comboBox1.Text = (dr1["Gender"]).ToString();
                    textBox5.Text = (dr1["Address"].ToString());
                    numericUpDown1.Value = Convert.ToInt32(dr1["Age"]);
                    maskedTextBox1.Text = (dr1["NIC"].ToString());
                    dateTimePicker1.Text = (dr1["DOB"].ToString());
                    checkBox1.Checked = Convert.ToBoolean(dr1["IsCricket"]);
                    checkBox2.Checked = Convert.ToBoolean(dr1["Hockey"]);
                    checkBox3.Checked = Convert.ToBoolean(dr1["Chess"]);
                    string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

                    pictureBox1.Load(applicationBasePath + @"\images\" + (dr1["ImageName"]).ToString());
                    if (uniname == "")
                    {
                        uniname = (dr1["ImageName"]).ToString();
                    }
                    conn.Close();


                }

            }
        }


        private bool validation()
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
            string body = "Dear " + textBox4.Text + " your varification code is " + activationcode;

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


        private void insertdata()
        {
            string connstr = @"Data Source=DESKTOP-IMDE3PB\SQLEXPRESS2012;Initial Catalog=Assignment4;Integrated Security=False;User ID=sa;Password=Rana2017@;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
           /* SqlConnection conn = new SqlConnection(connstr);
            SqlDataAdapter da = new SqlDataAdapter("select Name from dbo.Users where Login='" + textBox2.Text + "'", ce);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("User already exsist with this Login");
            }
            SqlDataAdapter db = new SqlDataAdapter("select Name from dbo.Users where Email='" + textBox4.Text + "'", ce);
            DataTable dc = new DataTable();
            da.Fill(dc);
            if (dc.Rows.Count >= 1)
            {
                MessageBox.Show("User already exist with this email");
            }
            if (dc.Rows.Count == 0 && dt.Rows.Count == 0)
            {
             */   
            using (SqlConnection conn = new SqlConnection(connstr))
                {
                    try
                    {


                        

                        string name = textBox1.Text;
                        string login = textBox2.Text;
                        string password = textBox3.Text;
                        string email = textBox4.Text;
                        char gender = Convert.ToChar(comboBox1.Text);
                        string address = textBox5.Text;
                        int age = Convert.ToInt32(numericUpDown1.Value);
                        string nic = maskedTextBox1.Text;
                        DateTime dob = Convert.ToDateTime(dateTimePicker1.Text);
                        int cricket = Convert.ToInt32(checkBox1.Checked == true ? 1 : 0);
                        int hockey = Convert.ToInt32(checkBox2.Checked == true ? 1 : 0);
                        int chess = Convert.ToInt32(checkBox3.Checked == true ? 1 : 0);
                       

                        string loog=Form3.num;

                       
                        if (bb == textBox2.Text)
                        {
                            conn.Open();


                            string query = "update dbo.Users set    Name='" + name + "',Login='" + login + "',Password='" + password + "',Email='" + email + "',Gender='" + gender + "',Address='" + address + "',Age='" + age + "',NIC='" + nic + "',DOB='" + dob + "',IsCricket='" + cricket + "',Hockey='" + hockey + "',Chess='" + chess + "',ImageName='" + uniname + "',CreatedOn='" + DateTime.Now + "' where Login='" + loog + "' ";
                            SqlCommand command = new SqlCommand(query, conn);
                            var rec = command.ExecuteNonQuery();
                            MessageBox.Show("data updated");

                            conn.Close();
                            if (Form9.id != "")
                            {
                                this.Hide();
                            }
                            else
                            {
                                this.Hide();
                                Form3 obc = new Form3();
                                obc.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter the same login");
                        }

                       


                    }

                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }
        

        private void button2_Click(object sender, EventArgs e)
        {
    
            this.Hide();
            Form3 obj = new Form3();
           
            obj.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            validation();



            if (result == true)
            {
               if (validatemail() == true)
                {
                    if (validnic(maskedTextBox1.Text) == true)
                    {
                        if (sendcode() == true)
                        {
                            insertdata();

                        }
                    }

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
             string   filepath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filepath);
            }


            if (pictureBox1.Image != null)
            {
                string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string pathToSaveImage = applicationBasePath + @"\images\";
                uniname = Guid.NewGuid().ToString() + ".jpg";
                string imgpath = pathToSaveImage + uniname;
                pictureBox1.Image.Save(imgpath);
            }


        }
    }
}
