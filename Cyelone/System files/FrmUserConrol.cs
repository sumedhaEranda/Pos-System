using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Media;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class FrmUserConrol : Form
    {
        
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();

        public FrmUserConrol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textUserName1.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textUserName1.Focus();
                return;
            }
            if (textuserPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textuserPassword.Focus();
                return;
            }
            try
            {
                SqlConnection myConnection = default(SqlConnection);
                myConnection = new SqlConnection(cs.DBConn);

                SqlCommand myCommand = default(SqlCommand);

                myCommand = new SqlCommand("SELECT Username,password FROM User_info WHERE Username = @username AND password = @UserPassword", myConnection);
                SqlParameter uName = new SqlParameter("@username", SqlDbType.VarChar);
                SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                uName.Value = textUserName1.Text;
                uPassword.Value = textuserPassword.Text;
                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);

                myCommand.Connection.Open();

                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (myReader.Read() == true)
                {
                   
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select usertype from User_info where Username='" + textUserName1.Text + "' and Password='" + textuserPassword.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        
                    }

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                  
                    this.Hide();
                    invoicedelete();
                }
                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textUserName1.Clear();
                    textuserPassword.Clear();
                    textUserName1.Focus();
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FrmUserConrol_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel1.BackColor = Properties.Settings.Default.HeaderBackground;
            this.panel2.BackColor = Properties.Settings.Default.FooterBackground;
            this.label3.ForeColor = Properties.Settings.Default.FontHeaderColor;

            label4.Text=Text;
        }
        
        public void invoicedelete()
        {
            
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                 
                try
                {
                    deletesound();

                    int RowsAffected = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cq1 = "delete from productSold where InvoiceNo='" + label4.Text + "'";
                    cmd = new SqlCommand(cq1);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cq = "delete from Invoice_Info where InvoiceNo='" + label4.Text + "'";
                    cmd = new SqlCommand(cq);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    this.Hide();
                    frmSales frm = new frmSales();
                    frm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void deletesound()
        {
            System.Media.SoundPlayer soundpayer = new System.Media.SoundPlayer("delete.wav");
            soundpayer.Play();
        }
    }
}
