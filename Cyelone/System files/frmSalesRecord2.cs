﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmSalesRecord2 : Form
    {

        DataTable dTable;
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmSalesRecord2()
        {
            InitializeComponent();
        }

        private void frmSalesRecord_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOS_DBDataSet.Invoice_Info' table. You can move, or remove it, as needed.
           // this.invoice_InfoTableAdapter.Fill(this.pOS_DBDataSet.Invoice_Info);
            FillCombo();

            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel1.BackColor = Properties.Settings.Default.HeaderBackground;

            this.label21.ForeColor = Properties.Settings.Default.FontHeaderColor;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (DataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView1.RowCount - 1;
                colsTotal = DataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (DataGridView2.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView2.RowCount - 1;
                colsTotal = DataGridView2.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView2.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView2.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (DataGridView3.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView3.RowCount - 1;
                colsTotal = DataGridView3.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView3.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView3.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;
        }
        public void FillCombo()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct CustomerName FROM Invoice_Info,Customer where Invoice_Info.CustomerID=Customer.CustomerID", con);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dTable = ds.Tables[0];
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dTable.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox3.Visible = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Order No],RTRIM(InvoiceDate) as [Order Date],RTRIM(Invoice_Info.CustomerID) as [Customer ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(VATPer) as [Vat+ST %],RTRIM(VATAmount) as [VAT+ST Amount],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],RTRIM(PaymentType) as [Payment Type],RTRIM(Status) as [Status],Remarks from Invoice_Info,Customer where Invoice_Info.CustomerID=Customer.CustomerID and InvoiceDate between @d1 and @d2 order by InvoiceDate desc", con);
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateFrom.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView1.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[9].Value);
                    Int64 j = Convert.ToInt64(r.Cells[10].Value);
                    Int64 k = Convert.ToInt64(r.Cells[11].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;

                }
                TextBox1.Text = sum.ToString();
                TextBox2.Text = sum1.ToString();
                TextBox3.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GroupBox4.Visible = true;
                cmbCustomerName.Text = cmbCustomerName.Text.Trim();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Order No],RTRIM(InvoiceDate) as [Order Date],RTRIM(Invoice_Info.CustomerID) as [Customer ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(VATPer) as [Vat+ST %],RTRIM(VATAmount) as [VAT+ST Amount],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],RTRIM(PaymentType) as [Payment Type],RTRIM(Status) as [Status],Remarks from Invoice_Info,Customer where Invoice_Info.CustomerID=Customer.CustomerID and Customername='" + cmbCustomerName.Text.Trim() + "' order by CustomerName,InvoiceDate", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView3.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView3.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[9].Value);
                    Int64 j = Convert.ToInt64(r.Cells[10].Value);
                    Int64 k = Convert.ToInt64(r.Cells[11].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox6.Text = sum.ToString();
                TextBox5.Text = sum1.ToString();
                TextBox4.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox10.Visible = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Order No],RTRIM(InvoiceDate) as [Order Date],RTRIM(Invoice_Info.CustomerID) as [Customer ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(VATPer) as [Vat+ST %],RTRIM(VATAmount) as [VAT+ST Amount],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],RTRIM(PaymentType) as [Payment Type],RTRIM(Status) as [Status],Remarks from Invoice_Info,Customer where Invoice_Info.CustomerID=Customer.CustomerID and InvoiceDate between @d1 and @d2 and PaymentDue > 0 order by InvoiceDate desc", con);
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker2.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker1.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView2.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView2.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView2.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[9].Value);
                    Int64 j = Convert.ToInt64(r.Cells[10].Value);
                    Int64 k = Convert.ToInt64(r.Cells[11].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox12.Text = sum.ToString();
                TextBox11.Text = sum1.ToString();
                TextBox10.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }


        private void TabControl1_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCustomerName.Text == "")
                {
                    MessageBox.Show("Please select customer name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCustomerName.Focus();
                    return;
                }
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                POS_DBDataSet myDS = new POS_DBDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from Customer,Invoice_Info where Customer.CustomerID=Invoice_Info.CustomerID and CustomerName='" + cmbCustomerName.Text + "' order by InvoiceDate desc";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "Invoice_Info");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
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

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                POS_DBDataSet myDS = new POS_DBDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from Customer,Invoice_Info where Customer.CustomerID=Invoice_Info.CustomerID and InvoiceDate Between @d1 and @d2 order by InvoiceDate desc";
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateFrom.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateTo.Value.Date;
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "Invoice_Info");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptSales rpt = new rptSales();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                POS_DBDataSet myDS = new POS_DBDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from Customer,Invoice_Info where Customer.CustomerID=Invoice_Info.CustomerID and InvoiceDate Between @d1 and @d2 and PaymentDue > 0 order by InvoiceDate desc";
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker2.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker1.Value.Date;
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "Invoice_Info");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmSalesReport frm = new frmSalesReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomerName_Format(object sender, ListControlConvertEventArgs e)
        {
            if (object.ReferenceEquals(e.DesiredType, typeof(string)))
            {
                e.Value = e.Value.ToString().Trim();
            }
        }

        private void textserchrsale_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(InvoiceNo)as [InvoiceNo],RTRIM(UserName) as [UserName],RTRIM(InvoiceDate) as [InvoiceDate],RTRIM(CustomerID) as [CustomerID],RTRIM(SubTotal) as [SubTotal],RTRIM(VATPer) as [VATPer],RTRIM(VATAmount) as [VATAmount],RTRIM(DiscountPer) as [DiscountPer], RTRIM(DiscountAmount) as [DiscountAmount], RTRIM(GrandTotal) as [GrandTotal], RTRIM(TotalPayment) as [TotalPayment], RTRIM(PaymentDue) as [PaymentDue],RTRIM(PaymentType) as [PaymentType], RTRIM(Status) as [Status], RTRIM(Remarks) as [Remarks]from Invoice_Info where UserName like '" + textserchrsale.Text + "%' order by UserName ", con);

                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                dataGridView4.DataSource = myDataSet.Tables["Customer"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Exportbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView4.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = dataGridView4.RowCount - 1;
                colsTotal = dataGridView4.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView4.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView4.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void frmSalesRecord2_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu.frmRestartCount = 0;
        }

        private void GroupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void TabPage3_Click(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }
    }

}
