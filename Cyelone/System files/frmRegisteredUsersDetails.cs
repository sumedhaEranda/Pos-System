﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmRegisteredUsersDetails : Form
    {
        ConnectionString cs = new ConnectionString();
        public frmRegisteredUsersDetails()
        {
            InitializeComponent();
        }
      
        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(cs.DBConn);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        public DataView GetData()
        {
            dynamic SelectQry = "SELECT RTRIM(Username) as [User Name],RTRIM(Password) as [Password],RTRIM(Name) as [Name],RTRIM(ContactNo) as [Contact No.],RTRIM(Email) as [Email ID],RTRIM(joiningdate) as [Date Of Joining] FROM User_info";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }
        private void frmRegisteredUsersDetails_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();

            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel1.BackColor = Properties.Settings.Default.HeaderBackground;
           
            this.label2.ForeColor = Properties.Settings.Default.FontHeaderColor;
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

        private void frmRegisteredUsersDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            
                frmRegistration frm = new frmRegistration();

                frm.Show();
           
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

         }
    }
}
