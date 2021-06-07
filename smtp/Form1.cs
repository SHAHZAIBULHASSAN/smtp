using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smtp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        public static string getHtml(DataGridView grid)
        {
            try
            {
                string messageBody = "<font>The following are the records: </font><br><br>";
                if (grid.RowCount == 0) return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Student Name" + htmlTdEnd;
                messageBody += htmlTdStart + "DOB" + htmlTdEnd;
                messageBody += htmlTdStart + "Email" + htmlTdEnd;
                messageBody += htmlTdStart + "Mobile" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= grid.RowCount - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[0].Value + htmlTdEnd; //adding student name  
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[1].Value + htmlTdEnd; //adding DOB  
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[2].Value + htmlTdEnd; //adding Email  
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[3].Value + htmlTdEnd; //adding Mobile  
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            // sqlConnection.ConnectionString = "server = SHAHZAIBULHASSAN; database = CRUDDB; "; //Connection Details  
            sqlConnection.ConnectionString = @"Data Source=SHAHZAIBHASSAN-;Initial Catalog=CRUDDB; User ID=UserName;Password=Password;";                                                                                                                        //select fields to mail example student details  
            SqlCommand sqlCommand = new SqlCommand("select Name,DOB,Email,Mob from Student", sqlConnection); //select query command  
            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand; //add selected rows to sql data adapter  
            DataSet dataSetStud = new DataSet(); //create new data set  
             
            
        }
            

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.UseDefaultCredentials = false;
                mail.From = new MailAddress("ranashahzaibulhassan@gmail.com");
                mail.To.Add("shahzaibsafdarali");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ranashahzaibulhassan@gmail.com", "safdar786");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Mail is  Send");
            }
            catch (Exception Ex) {
                System.Windows.Forms.MessageBox.Show(Ex.Message);

            }

        }
    }
    } 
