﻿using System;
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
namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmSales : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();

        public frmSales()
        {
            InitializeComponent();
        }
        private void auto()
        {
            txtInvoiceNo.Text = "IN-" + GetUniqueKey(7);
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

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerID.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer Details", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Button1.Focus();
                    return;
                }

                if (txtTaxPer.Text == "")
                {
                    MessageBox.Show("Please enter tax percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }
                if (txtDiscountPer.Text == "")
                {
                    MessageBox.Show("Please enter discount percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountPer.Focus();
                    return;
                }
                if (txtTotalPayment.Text == "")
                {
                    MessageBox.Show("Please enter total payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPayment.Focus();
                    return;
                }
                if (cmbPaymentType.Text == "")
                {
                    MessageBox.Show("Please select payment type", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbPaymentType.Focus();
                    return;
                }
                if (cmbStatus.Text == "")
                {
                    MessageBox.Show("Please select status", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbStatus.Focus();
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no product added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                auto();

                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert Into Invoice_Info(InvoiceNo,InvoiceDate,CustomerID,SubTotal,VATPer,VATAmount,DiscountPer,DiscountAmount,GrandTotal,TotalPayment,PaymentDue,PaymentType,Status,Remarks) VALUES ('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Text + "','" + txtCustomerID.Text + "'," + txtSubTotal.Text + "," + txtTaxPer.Text + "," + txtTaxAmt.Text + "," + txtDiscountPer.Text + "," + txtDiscountAmount.Text + "," + txtTotal.Text + "," + txtTotalPayment.Text + "," + txtPaymentDue.Text + ",'" + cmbPaymentType.Text + "','" + cmbStatus.Text + "','" + txtRemarks.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into ProductSold(InvoiceNo,ProductID,ProductName,Quantity,Price,TotalAmount) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtInvoiceNo.Text);
                    cmd.Parameters.AddWithValue("d2", ListView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", ListView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", ListView1.Items[i].SubItems[4].Text);
                    cmd.Parameters.AddWithValue("d5", ListView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d6", ListView1.Items[i].SubItems[5].Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update temp_stock set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where ProductID= '" + ListView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                GetData();
                MessageBox.Show("Successfully Placed", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            auto();
            Save.Enabled = false;
            button2.Enabled = false;
            GetData();
            toolStripStatusLabel4.Text= label6.Text;
            btnPrint.Enabled = false;
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel3.BackColor = Properties.Settings.Default.HeaderBackground;
            this.label21.ForeColor = Properties.Settings.Default.FontHeaderColor;
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord1 frm = new frmCustomersRecord1();
            frm.label1.Text = label6.Text;
            frm.Visible = true;
            
        }

        public void Calculate()
        {
            if (txtTaxPer.Text != "")
            {
                txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();

            }
            if (txtDiscountPer.Text != "")
            {
                txtDiscountAmount.Text = Convert.ToInt32(((Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)) * Convert.ToDouble(txtDiscountPer.Text) / 100)).ToString();
            }
            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            int val4 = 0;
            int val5 = 0;
            int.TryParse(txtTaxAmt.Text, out val1);
            int.TryParse(txtSubTotal.Text, out val2);
            int.TryParse(txtDiscountAmount.Text, out val3);
            int.TryParse(txtTotal.Text, out val4);
            int.TryParse(txtTotalPayment.Text, out val5);
            val4 = val1 + val2 - val3;
            txtTotal.Text = val4.ToString();
            int I = (val4 - val5);
            txtPaymentDue.Text = I.ToString();
        }
        private void txtSaleQty_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtPrice.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            int I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
        }

        public double subtot()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;

            try
            {

                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(ListView1.Items[i].SubItems[5].Text);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {

                    if (txtProductName.Text == "")
                    {
                       Warningsound();
                    MessageBox.Show("Please retrieve product name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (txtSaleQty.Text == "")
                    {
                        Warningsound();
                        MessageBox.Show("Please enter no. of sale quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSaleQty.Focus();
                        return;
                    }
                    int SaleQty = Convert.ToInt32(txtSaleQty.Text);
                    if (SaleQty == 0)
                    {
                         Warningsound();
                        MessageBox.Show("no. of sale quantity can not be zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSaleQty.Focus();
                        return;
                    }

                    if (ListView1.Items.Count == 0)
                    {

                        ListViewItem lst = new ListViewItem();
                        lst.SubItems.Add(txtProductID.Text);
                        lst.SubItems.Add(txtProductName.Text);
                        lst.SubItems.Add(txtPrice.Text);
                        lst.SubItems.Add(txtSaleQty.Text);
                        lst.SubItems.Add(txtTotalAmount.Text);
                        ListView1.Items.Add(lst);
                        txtSubTotal.Text = subtot().ToString();

                        Calculate();
                        txtProductName.Text = "";
                        txtProductID.Text = "";
                        txtPrice.Text = "";
                        txtAvailableQty.Text = "";
                        txtSaleQty.Text = "";
                        txtTotalAmount.Text = "";
                        txtProduct.Text = "";
                        return;
                    }

                    for (int j = 0; j <= ListView1.Items.Count - 1; j++)
                    {
                        if (ListView1.Items[j].SubItems[1].Text == txtProductID.Text)
                        {
                            ListView1.Items[j].SubItems[1].Text = txtProductID.Text;
                            ListView1.Items[j].SubItems[2].Text = txtProductName.Text;
                            ListView1.Items[j].SubItems[3].Text = txtPrice.Text;
                            ListView1.Items[j].SubItems[4].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[4].Text) + Convert.ToInt32(txtSaleQty.Text)).ToString();
                            ListView1.Items[j].SubItems[5].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[5].Text) + Convert.ToInt32(txtTotalAmount.Text)).ToString();
                            txtSubTotal.Text = subtot().ToString();
                            Calculate();
                            txtProductName.Text = "";
                            txtProductID.Text = "";
                            txtPrice.Text = "";
                            txtAvailableQty.Text = "";
                            txtSaleQty.Text = "";
                            txtTotalAmount.Text = "";
                            return;

                        }
                    }

                    ListViewItem lst1 = new ListViewItem();

                    lst1.SubItems.Add(txtProductID.Text);
                    lst1.SubItems.Add(txtProductName.Text);
                    lst1.SubItems.Add(txtPrice.Text);
                    lst1.SubItems.Add(txtSaleQty.Text);
                    lst1.SubItems.Add(txtTotalAmount.Text);
                    ListView1.Items.Add(lst1);
                    txtSubTotal.Text = subtot().ToString();
                    Calculate();
                    txtProductName.Text = "";
                    txtProductID.Text = "";
                    txtPrice.Text = "";
                    txtAvailableQty.Text = "";
                    txtSaleQty.Text = "";
                    txtTotalAmount.Text = "";
                    return;
                    


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        { 
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    Warningsound();
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        
                       // Dim lst1 As New ListViewItem(i);
                       // ListView1.Items(i).SubItems(0).Text = t;
                        t = t + 1;

                    }
                    txtSubTotal.Text = subtot().ToString();
                    Calculate();
                }

                btnRemove.Enabled = false;
                if (ListView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaxPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT Product.ProductID,ProductName,Features,Price,sum(Quantity) from Temp_Stock,Product where Temp_Stock.ProductID=Product.ProductID and ProductName like '" + txtProduct.Text + "%' group by product.ProductID,productname,Price,Features,Quantity having(quantity>0) order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtProductID.Text = dr.Cells[0].Value.ToString();
                txtProductName.Text = dr.Cells[1].Value.ToString();
                txtPrice.Text = dr.Cells[3].Value.ToString();
                txtAvailableQty.Text = dr.Cells[4].Value.ToString();
                txtSaleQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT Product.ProductID,ProductName,Features,Price,sum(Quantity) from Temp_Stock,Product where Temp_Stock.ProductID=Product.ProductID group by Product.productID,productname,Price,Features,Quantity having(Quantity>0)  order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            txtInvoiceNo.Text = "";
            cmbStatus.Text = "";
            cmbPaymentType.Text = "";
            dtpInvoiceDate.Text = DateTime.Today.ToString();
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtProductName.Text = "";
            txtProductID.Text = "";
            txtPrice.Text = "";
            txtAvailableQty.Text = "";
            txtSaleQty.Text = "";
            txtTotalAmount.Text = "";
            ListView1.Items.Clear();
            txtDiscountAmount.Text = "";
            txtDiscountPer.Text = "";

            txtSubTotal.Text = "";
            txtTaxPer.Text = "";
            txtTaxAmt.Text = "";
            txtTotal.Text = "";
            txtTotalPayment.Text = "";
            txtPaymentDue.Text = "";
            txtProduct.Text = "";
            txtRemarks.Text = "";
            Save.Enabled = true;
            Delete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnPrint.Enabled = false;
            ListView1.Enabled = true;
            Button7.Enabled = true;

        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
            Reset();
        }
        public void deletesound()
        {
            System.Media.SoundPlayer soundpayer = new System.Media.SoundPlayer("delete.wav");
            soundpayer.Play();
        }
        public void Warningsound()
        {
            System.Media.SoundPlayer soundpayer = new System.Media.SoundPlayer("warning.wav");
            soundpayer.Play();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (ConnectionString.userType == "C")
            {
                FrmUserConrol frm = new FrmUserConrol();
                frm.Text = txtInvoiceNo.Text;
                frm.Show();

            }

           else if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    deletesound();
                    delete_records();
                } 
          
        }
        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq1 = "delete from productSold where InvoiceNo='" + txtInvoiceNo.Text + "'";
                cmd = new SqlCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Invoice_Info where InvoiceNo='" + txtInvoiceNo.Text + "'";
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
                    Warningsound();
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            int I = (val1 - val2);
            txtPaymentDue.Text = I.ToString();
        }

        private void txtTotalPayment_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
             if (val2 >val1)
              {
                  MessageBox.Show("Total Payment can't be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  txtTotalPayment.Text = "";
                  txtPaymentDue.Text = "";
                  txtTotalPayment.Focus();
                  return;
              }
        }

        private void txtSaleQty_Validating(object sender, CancelEventArgs e)
        {

            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtAvailableQty.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Selling quantities are more than available quantities", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSaleQty.Text = "";
                txtTotalAmount.Text = "";
                txtSaleQty.Focus();
                return;
            }
        }

        private void CustomerIdIncrement()
        {

            try
            {
                string Sql = "select MAX(CustomerId) from Customer ";
                con = new SqlConnection(cs.DBConn);
                cmd = new SqlCommand(Sql, con);
                con.Open();
                var maxid = cmd.ExecuteScalar() as string;

                if (maxid == null)
               {
                   txtCustomerID.Text = "C-100000000000000000";
               }
              else
              {
                    int intval = int.Parse(maxid.Substring(2,5));
                    intval++;
                    txtCustomerID.Text = String.Format("C-{0:00000}", intval);
                    txtCustomerName.Text = "noname";
                    savecustomer_id_and_name();
             }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void savecustomer_id_and_name()
        {
            String aa = txtCustomerID.Text;
            String bb = txtCustomerName.Text;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string cb = "insert into Customer(CustomerId ,CustomerName ,Address ,City ,ContactNo ,ContactNo1 ,Email ,Notes) VALUES ('" +this.txtCustomerID.Text + "','" +this.txtCustomerName.Text + "','adress','city','contctno','contctno1','email','notes')";
            cmd = new SqlCommand(cb, con);
            cmd.ExecuteReader();
            con.Close();  
        }


        public void btncontrol()
        {
            if (txtTaxPer.Text == "" && txtDiscountPer.Text == "" && txtTotalPayment.Text == "" && cmbPaymentType.Text == "" && cmbStatus.Text == "" && ListView1.Items.Count == 0)
            {
                btnPrint.Enabled = false;
            }
            else {
                btnPrint.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if ((txtCustomerID.Text == "") && (txtCustomerName.Text == ""))
            {  
                //create incremet customer id
                CustomerIdIncrement();          
            }
            savedata();
            // print the invoice in crystal report

                
                try
                {

                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rptInvoice rpt = new rptInvoice();
                    //The report you created.
                    cmd = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    POS_DBDataSet myDS = new POS_DBDataSet();
                    //The DataSet you created.
                    con = new SqlConnection(cs.DBConn);
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * from product,invoice_info,productsold,customer where invoice_info.invoiceno=productsold.invoiceno and invoice_info.customerID=Customer.CustomerID and ProductSold.ProductID=Product.ProductID and Invoice_info.invoiceNo='" + txtInvoiceNo.Text + "' ";
                    cmd.CommandType = CommandType.Text;
                    myDA.SelectCommand = cmd;
                    myDA.Fill(myDS, "product");
                    myDA.Fill(myDS, "Invoice_Info");
                    myDA.Fill(myDS, "ProductSold");
                    myDA.Fill(myDS, "Customer");
                    rpt.SetDataSource(myDS);
                    frmInvoiceReport frm = new frmInvoiceReport();
                    frm.crystalReportViewer1.ReportSource = rpt;
                    frm.Visible = true;
                    frm.BringToFront();
                    frm.TopMost = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
                                               
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   
           
            
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String cb = "update Invoice_info set CustomerID='" + txtCustomerID.Text + "', VATPer=" + txtTaxPer.Text + ",VATAmount=" + txtTaxAmt.Text + ",DiscountPer=" + txtDiscountPer.Text + ",DiscountAmount=" + txtDiscountAmount.Text + ",GrandTotal= " + txtTotal.Text + ",TotalPayment= " + txtTotalPayment.Text + ",PaymentDue= " + txtPaymentDue.Text + ",Remarks='" + txtRemarks.Text + "',Status='" + cmbStatus.Text + "',PaymentType='" + cmbPaymentType.Text + "' where Invoiceno= '" + txtInvoiceNo.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                GetData();
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSalesRecord1 frm = new frmSalesRecord1();
            frm.label9.Text= toolStripStatusLabel4.Text;
            frm.DataGridView1.DataSource = null;
            frm.dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            frm.dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            frm.GroupBox3.Visible = false;
            frm.DataGridView3.DataSource = null;
            frm.cmbCustomerName.Text = "";
            frm.GroupBox4.Visible = false;
            frm.DateTimePicker1.Text = DateTime.Today.ToString();
            frm.DateTimePicker2.Text = DateTime.Today.ToString();
            frm.DataGridView2.DataSource = null;
            frm.GroupBox10.Visible = false;
            frm.FillCombo();
            frm.label9.Text = label6.Text;
            
            frm.Show();
            frm.BringToFront();
            frm.TopMost = true;
        }

        private void txtSaleQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTotalPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }


        //save data in invoice database
        public void savedata() {


            try
            {

                if (txtTaxPer.Text == "") 
                {
                    Warningsound();
                    MessageBox.Show("Please enter tax percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }
                if (txtDiscountPer.Text == "")
                {
                    Warningsound();
                    MessageBox.Show("Please enter discount percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountPer.Focus();
                    return;
                }
                if (txtTotalPayment.Text == "")
                {
                    Warningsound();
                    MessageBox.Show("Please enter total payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPayment.Focus();
                    return;
                }
                if (cmbPaymentType.Text == "")
                {
                    Warningsound();
                    MessageBox.Show("Please select payment type", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbPaymentType.Focus();
                    return;
                }
                if (cmbStatus.Text == "")
                {
                    Warningsound();
                    MessageBox.Show("Please select status", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbStatus.Focus();
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    Warningsound();
                    MessageBox.Show("sorry no product added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    return;
                }
                
                auto();
            
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert Into Invoice_Info(InvoiceNo,UserName,InvoiceDate,CustomerID,SubTotal,VATPer,VATAmount,DiscountPer,DiscountAmount,GrandTotal,TotalPayment,PaymentDue,PaymentType,Status,Remarks) VALUES ('" + txtInvoiceNo.Text + "','" + toolStripStatusLabel4.Text + "','" + dtpInvoiceDate.Text + "','" + txtCustomerID.Text + "'," + txtSubTotal.Text + "," + txtTaxPer.Text + "," + txtTaxAmt.Text + "," + txtDiscountPer.Text + "," + txtDiscountAmount.Text + "," + txtTotal.Text + "," + txtTotalPayment.Text + "," + txtPaymentDue.Text + ",'" + cmbPaymentType.Text + "','" + cmbStatus.Text + "','" + txtRemarks.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();

               

                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into ProductSold(InvoiceNo,ProductID,ProductName,Quantity,Price,TotalAmount) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtInvoiceNo.Text);
                    cmd.Parameters.AddWithValue("d2", ListView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", ListView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", ListView1.Items[i].SubItems[4].Text);
                    cmd.Parameters.AddWithValue("d5", ListView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d6", ListView1.Items[i].SubItems[5].Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               
                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update temp_stock set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where ProductID= '" + ListView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               

                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update Stock set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where ProductID= '" + ListView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                Save.Enabled = false;
                btnPrint.Enabled = true;
                GetData();
                MessageBox.Show("Successfully Placed", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu.frmRestartCount = 0;
        }

        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentType.SelectedIndex.ToString() != "")
            {
                btncontrol();
             } 
        }
        public void returndata()
        {


            try
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String cb = "update Invoice_info set Status='" + cmbStatus.Text + "' where Invoiceno= '" + txtInvoiceNo.Text + "'";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();

                    GetData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update temp_stock set Quantity = Quantity + " + ListView1.Items[i].SubItems[4].Text + " where ProductID= '" + ListView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update Stock set Quantity = Quantity + " + ListView1.Items[i].SubItems[4].Text + " where ProductID= '" + ListView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                Save.Enabled = false;
                btnPrint.Enabled = true;
                GetData();
                MessageBox.Show("Successfully Placed", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {  


            returndata();
           
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "Returned")
            {
                button2.Enabled = true;
            }
        }
    }
}