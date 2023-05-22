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
using System.Text.RegularExpressions;
namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmCustomers : Form
    {
      
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmCustomers()
        {
            InitializeComponent();
            

        }
        private void Reset()
        {
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtCustomerName.Text = "";
            txtContactNo1.Text = "000-0000000";
            txtNotes.Text = "";
            txtContactNo.Text = "000-0000000";
            txtCustomerID.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtCustomerName.Focus();

        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {
            auto();
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel1.BackColor = Properties.Settings.Default.HeaderBackground;
            this.panel2.BackColor = Properties.Settings.Default.FooterBackground;
            this.Label11.ForeColor = Properties.Settings.Default.FontHeaderColor;

            
        }
        private void auto()
        {
            txtCustomerID.Text = "C-" + GetUniqueKey(6);
        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
      
     
        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return;
            }
          
            if (txtContactNo.Text == "000-0000000")
            {
                MessageBox.Show("Please enter contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }
            if (txtContactNo1.Text == "000-0000000")
            {
                MessageBox.Show("Please enter Alt.contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter email Address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }

            try
            {
                auto();
             
                    con = new SqlConnection(cs.DBConn);
                    con.Open();

                    string cb = "insert into Customer(CustomerID,Customername,address,City,ContactNo,ContactNo1,Email,Notes) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";

                    cmd = new SqlCommand(cb);

                    cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtCustomerID.Text);
                cmd.Parameters.AddWithValue("@d2", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@d6", txtContactNo1.Text);
                cmd.Parameters.AddWithValue("@d7", txtEmail.Text);
                cmd.Parameters.AddWithValue("@d8", txtNotes.Text);
                  
                    cmd.ExecuteReader();
                    MessageBox.Show("Successfully saved", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Close();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {

            try
            {

              int RowsAffected = 0;
              con = new SqlConnection(cs.DBConn);
              con.Open();
              string cq = "delete from Customer where CustomerID='" + txtCustomerID.Text + "'";
              cmd = new SqlCommand(cq);
              cmd.Connection = con;
            
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                    con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                if (ConnectionString.userType == "C")
                {
                    MessageBox.Show("Unauthorized Access\nChcek your Administrator Type", "Cannot Delete ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            else {
                try
                {


                    if (MessageBox.Show("Do you really want to delete the record?", "Customer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        delete_records();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ConnectionString.userType == "C")
            {           
                    MessageBox.Show("Unauthorized Access\nChcek your Administrator Type", "Cannot Update ", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
            else
            {

                try
                {
                    if (txtCustomerName.Text == "")
                    {
                        MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCustomerName.Focus();
                        return;
                    }

                    if (txtAddress.Text == "")
                    {
                        MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAddress.Focus();
                        return;
                    }
                    if (txtCity.Text == "")
                    {
                        MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCity.Focus();
                        return;
                    }

                    if (txtContactNo.Text == "")
                    {
                        MessageBox.Show("Please enter contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContactNo.Focus();
                        return;
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "update Customer set Customername=@d2,address=@d3,City=@d4,ContactNo=@d5,ContactNo1=@d6,Email=@d7,Notes=@d8 where CustomerID=@d1";

                    cmd = new SqlCommand(cb);

                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtCustomerID.Text);
                    cmd.Parameters.AddWithValue("@d2", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                    cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@d6", txtContactNo1.Text);
                    cmd.Parameters.AddWithValue("@d7", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@d8", txtNotes.Text);
                    cmd.ExecuteReader();
                    MessageBox.Show("Successfully updated", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUpdate.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

     
      
      
        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }


            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }


        }

        private void txtContactNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }


            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord2 frm = new frmCustomersRecord2();
            frm.Show();
            frm.GetData();
        }

        private void frmCustomers_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu.frmRestartCount = 0;
            
            

        }

        private void txtContactNo_Enter(object sender, EventArgs e)
        {
            if (txtContactNo.Text == "000-0000000")
            {
                txtContactNo.Text = "";
                txtContactNo.ForeColor = Color.Black;
            }
        }

        private void txtContactNo_Leave(object sender, EventArgs e)
        {
            if (txtContactNo.Text == "")
            {
                txtContactNo.Text = "000-0000000";
                txtContactNo.ForeColor = Color.Silver;
            }
        }

        private void txtContactNo1_Click(object sender, EventArgs e)
        {
            if (txtContactNo.Text.Length == 11)
            {
                Regex phonenumber = new Regex("\\d{3}-\\d{7}");
                if (phonenumber.IsMatch(txtContactNo.Text))
                {
                   // MessageBox.Show("Valid phone number!", "Contact No", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Not Valid phone number!", "Contact No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            else
            {
                MessageBox.Show("Enter Only 10 Digits and Enter Correct format", "Contact No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            if (txtContactNo1.Text.Length == 11)
            {
                Regex phonenumber = new Regex("\\d{3}-\\d{7}");
                if (phonenumber.IsMatch(txtContactNo1.Text))
                {
                   // MessageBox.Show("Valid phone number!", "Alt.Contact No", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Not Valid phone number!", "Alt.Contact No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            else
            {
                MessageBox.Show("Enter Only 10 Digits and Enter Correct format", "Alt.Contact No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtContactNo1_Enter(object sender, EventArgs e)
        {
            if (txtContactNo1.Text == "000-0000000")
            {
                txtContactNo1.Text = "";
                txtContactNo1.ForeColor = Color.Black;
            }
        }

        private void txtContactNo1_Leave(object sender, EventArgs e)
        {
            if (txtContactNo1.Text == "")
            {
                txtContactNo1.Text = "000-0000000";
                txtContactNo1.ForeColor = Color.Silver;
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail.Text))
                {

                    MessageBox.Show("invalid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }
    }
}
