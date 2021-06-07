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

namespace second
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string GetHtml(DataGridView grid)
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
            sqlConnection.ConnectionString = @"data source=SHAHZAIBHASSAN-;initial catalog=CRUDDB;integrated security=True;MultipleActiveResultSets=True;";                                                                                                                        //select fields to mail example student details  
            SqlCommand sqlCommand = new SqlCommand("select Name,DOB,Email,Mob from Student", sqlConnection); //select query command  
            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand; //add selected rows to sql data adapter  
            DataSet dataSetStud = new DataSet(); //create new data set  
            try
            {
                sqlDataAdapter.Fill(dataSetStud, "student"); //fill sql data adapter rows to data set  
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].HeaderText = "Student Name";
                dataGridView1.Columns[0].DataPropertyName = "Name";
                dataGridView1.Columns[1].HeaderText = "Date of birth";
                dataGridView1.Columns[1].DataPropertyName = "DOB";
                dataGridView1.Columns[2].HeaderText = "Mail Id";
                dataGridView1.Columns[2].DataPropertyName = "Email";
                dataGridView1.Columns[3].HeaderText = "Mobile No";
                dataGridView1.Columns[3].DataPropertyName = "Mob";
                dataGridView1.DataSource = dataSetStud;
                dataGridView1.DataMember = "student";
            }
            catch (Exception Ex)
            {
                System.Windows.Forms.MessageBox.Show(Ex.Message);
                sqlConnection.Close();
            }

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

        public static void Email(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ranashahzaibulhassan@gmail.com");
                message.To.Add(new MailAddress("shahzaibsafdarali786@gmail.com"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ranashahzaibulhassan@gmail.com", "safdar786");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                string htmlString = getHtml(dataGridView1); //here you will be getting an html string  
            Email(htmlString);
            MessageBox.Show("Your Email is Send");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            

            #region code
            //try
            //{
            //    MailMessage message = new MailMessage();
            //    SmtpClient smtp = new SmtpClient();
            //    message.From = new MailAddress("ranashahzaibulhassan@gmail.com");

            //    message.To.Add(new MailAddress("srpnctgbd@gmail.com"));
            //    message.Subject = "Test";
            //    message.IsBodyHtml = true; //to make message body as html  
            //    message.Body = "hello";
            //    smtp.Port = 587;
            //    smtp.Host = "smtp.gmail.com"; //for gmail host  
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential("ranashahzaibulhassan@gmail.com", "safdar786");
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.Send(message);
            //    MessageBox.Show("send the message");
            //}
            //catch (Exception) { }
            #endregion
        }

       
    }
   
}

