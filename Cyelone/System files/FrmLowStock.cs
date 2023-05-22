using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Net;
using System.Net.Mail;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class FrmLowStock : Form
    {
      
        SqlConnection con;
        
        ConnectionString cs = new ConnectionString();
        public FrmLowStock()
        {
            InitializeComponent();
        }

        private void FrmLowStock_Load(object sender, EventArgs e)
        {
            getData();
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel2.BackColor = Properties.Settings.Default.HeaderBackground;
            this.label7.ForeColor = Properties.Settings.Default.FontHeaderColor;
        }
        public void getData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = " select Product.ProductID,Product.ProductName,Stock.Quantity from Product INNER join Stock on Product.ProductID=Stock.ProductID INNER join Supplier on Stock.SupplierID=Stock.SupplierId where Product.reoderlevel>Stock.Quantity  GROUP BY Product.ProductID,Stock.Quantity,Product.ProductName  ";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, con);
                DataTable dtb = new System.Data.DataTable();
                adpt.Fill(dtb);

                foreach (DataRow item in dtb.Rows)
                {

                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["ProductID"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendEmailbtn_Click(object sender, EventArgs e)
        {
            String TO = textBoxEmail.Text;
            String htmlString = GETHTML(dataGridView1);
            Email(htmlString,TO);
        }

        public static String GETHTML(DataGridView gridView)
        {
            try
            {
                String messageBody ="<font> The following are Low stock Product items ";
                if (gridView.RowCount == 0)
                    return messageBody;

                String htmlTableStart= "<table style=\"border-collapse:collapse; text-align:center;\">";
                String htmlTableEnd = "</table>";

                String htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                String htmlHeaderRowEnd = "</tr>";

                String htmlTrStart = "<tr style=\" color:#555555;\">";
                String htmlTrEnd= "</tr>";

                String htmlTdStart = "<td style=\"border-color:#5c87b2;border-style:solid; border-width:thin; padding:5px;\">";
                String htmlTdEnd = "</td>";



                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Product ID" + htmlTdEnd;
                messageBody += htmlTdStart + "ProductName" + htmlTdEnd;
                messageBody += htmlTdStart + " Current Quantity" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;


                for(int i=0;i<=gridView.RowCount-1;i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + gridView.Rows[i].Cells[0].Value + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + gridView.Rows[i].Cells[1].Value + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + gridView.Rows[i].Cells[2].Value + htmlTdEnd;
                    messageBody = messageBody + htmlTdEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                return messageBody;
            }
            catch (Exception ){
                return null;
            }

            
        }
        
        public static void Email( String htmlString,String TO)
        {
            
            bool aa = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("sumeadaeranda19961125@gmail.com ");
                message.To.Add(new MailAddress(TO ));
                message.Subject = "Ceylon Motors";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sumeadaeranda19961125@gmail.com", "sumeada1996eranda");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                smtp.Send(message);
                aa = true;
                if (aa)
                {
                    MessageBox.Show("Email send Succesfully", "Message Send", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Erroor, not Sending Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(e.Message);
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if(!row.IsNewRow) 
                dataGridView1.Rows.Remove(row);
            }

            
        }
    }
}
