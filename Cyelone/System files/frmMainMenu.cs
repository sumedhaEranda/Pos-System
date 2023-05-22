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
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmMainMenu : Form
    {
        public static int frmRestartCount = 0;
        ConnectionString cs = new ConnectionString();
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader rdr;

         
        public frmMainMenu()
        {
            InitializeComponent();
           

        }


        public void sound()
        {

        }
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmRestartCount++;
                frmCustomers frm = new frmCustomers();
                frm.Show();

            }
        }


        private void registrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRegistration frm = new frmRegistration();
            frm.Show();
        }

        private void profileEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmProduct frm = new frmProduct();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TaskMgr.exe");
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubCategory frm = new frmSubCategory();
            frm.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomersRecord frm = new frmCustomersRecord();
            frm.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCategory o1 = new frmCategory();
            o1.Hide();
            frmSubCategory o2 = new frmSubCategory();
            o2.Hide();
            frmProduct o3 = new frmProduct();
            o3.Hide();
            frmRegisteredUsersDetails o4 = new frmRegisteredUsersDetails();
            o4.Hide();
            frmRegistration o5 = new frmRegistration();
            o5.Hide();
            frmStockRecord o6 = new frmStockRecord();
            o6.Hide();
            frmCustomersRecord o7 = new frmCustomersRecord();
            o7.Hide();
            frmSuppliersRecord o8 = new frmSuppliersRecord();
            o8.Hide();
            frmProductsRecord2 o9 = new frmProductsRecord2();
            o9.Hide();
            frmSalesRecord2 o10 = new frmSalesRecord2();
            o10.Hide();

            this.Close();

            frmLogin frm = new frmLogin();
            frm.Show();
            frm.txtUserName.Text = "";
            frm.txtPassword.Text = "";
            frm.ProgressBar1.Visible = false;
            frm.txtUserName.Focus();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            getData();
            CheckLowOrder();
            loadChart();
            

            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();

            if (ConnectionString.userType == "C")
            {
                supplierToolStripMenuItem.Visible = false;
                toolStripMenuItemCategory.Visible = false;
                SubCategorytoolStripMenuItem4.Visible = false;
                toolStripMenuItem5.Visible = false;
                productToolStripMenuItem.Visible = false;
                loginDetailsToolStripMenuItem1.Visible = false;
                toolStripMenuIteSupliers.Visible = false;
                toolStripMenuItemBackup.Visible = false;

            }

            string monthname = "";
            int TotalSoldQuantiy;
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT MAX(DATENAME(MM,InvoiceDate)) AS  MONTHNAME, SUM(ProductSold.Quantity) AS TOTAL from Invoice_Info INNER JOIN ProductSold ON  Invoice_info.InvoiceNo=ProductSold.InvoiceNo INNER JOIN Product ON Product.ProductID=ProductSold.ProductID WHERE Product.ProductName='" + productnameText.Text + "'  GROUP BY MONTH(InvoiceDate) ;";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    monthname = rdr.GetString(0);
                    TotalSoldQuantiy = rdr.GetInt32(1);

                    productchart.Series["productsaleQty"].Points.AddXY(monthname, TotalSoldQuantiy);

                    if (monthname == "")
                    {
                        errorProvider1.SetError(productnameText, "Not Sold your Product ");
                    }
                }
                



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CheckLowOrder()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = " select Product.ProductID,Product.ProductName,Stock.Quantity from Product INNER join Stock on Product.ProductID=Stock.ProductID INNER join Supplier on Stock.SupplierID=Stock.SupplierId where Product.reoderlevel>Stock.Quantity  GROUP BY Product.ProductID,Stock.Quantity,Product.ProductName ";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read() == true)
                {
                    BIcon.Image = Properties.Resources.Active;
                }


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();
        }

        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.Show();
        }

        private void productsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmProductsRecord2 frm = new frmProductsRecord2();
            frm.Show();
        }


        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = lblUser.Text;
            frm.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmRestartCount++;
                frmStock frm = new frmStock();
                frm.label8.Text = lblUser.Text;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSales frm = new frmSales();
            frm.label6.Text = lblUser.Text;
            frm.Show();
        }

        private void salesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSalesRecord2 frm = new frmSalesRecord2();
            frm.Show();
        }

        private void loginDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginDetails frm = new frmLoginDetails();
            frm.Show();
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
            Environment.Exit(1);
        }

        private void profileEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSuppliers frm = new frmSuppliers();
            frm.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSuppliers frm = new frmSuppliers();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void suppliersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSuppliersRecord frm = new frmSuppliersRecord();
            frm.Hide();
        }

        private void stockToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmStockRecord frm = new frmStockRecord();
            frm.Show();
        }

        private void registrationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmRegistration frm = new frmRegistration();
            frm.Show();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSales frm = new frmSales();
                frmRestartCount++;
                frm.label6.Text = lblUser.Text;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms[" frmSubCategory"] as frmSubCategory) == null)
            {
                frmSubCategory frm = new frmSubCategory();
                frm.Show();
            }

        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSetting frm = new frmSetting();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmRegistration frm = new frmRegistration();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSalesRecord2 frm = new frmSalesRecord2();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void loginDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmLoginDetails frm = new frmLoginDetails();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void SubCategorytoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSubCategory frm = new frmSubCategory();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuIteSupliers_Click(object sender, EventArgs e)
        {

            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmSuppliersRecord frm = new frmSuppliersRecord();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItemCategory_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmCategory frm = new frmCategory();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItemStock_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmStockRecord frm = new frmStockRecord();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }

        }

        private void toolStripMenuItemProduct_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmProductsRecord2 frm = new frmProductsRecord2();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItemCustomer_Click(object sender, EventArgs e)
        {
            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmCustomersRecord frm = new frmCustomersRecord();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }
        private void toolStripMenuItemBackup_Click(object sender, EventArgs e)
        {

            if (frmRestartCount == 0)
            {
                MainPanel.Visible = true;
                frmBackup frm = new frmBackup();
                frmRestartCount++;
                frm.Show();
                frm.BringToFront();
                frm.TopMost = true;
            }
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void Nofitification_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = true;
            FrmLowStock frm = new FrmLowStock();
            frm.Show();
        }




        public void loadChart()
        {
            DbchartcategoryDataContext db = new DbchartcategoryDataContext();
            
            chartcategory.DataSource = db.viewcategs.ToList();
            chartcategory.Series["Cateagory"].XValueMember = "CategoryName";
            chartcategory.Series["Cateagory"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chartcategory.Series["Cateagory"].YValueMembers = "TotalQuantity";
            chartcategory.Series["Cateagory"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            MonthSalesChartDataContext mothdb = new MonthSalesChartDataContext();
            SalesChart.DataSource = mothdb.MonthSalesviews.ToList();
            SalesChart.Series["Total Monthy Sales"].XValueMember = "MonthName";
            SalesChart.Series["Total Monthy Sales"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

            SalesChart.Series["Total Monthy Sales"].YValueMembers = "TotalSale";
            SalesChart.Series["Total Monthy Sales"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            SalesChart.Series["Due Total Payment"].YValueMembers = "Total_Due_PymentSales";
            SalesChart.Series["Due Total Payment"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            EnattityChart();
        }

        public void EnattityChart()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "select count(User_info.UserName) as numOfuser from User_info; ";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int usercount = rdr.GetInt32(0);
                    noOfuser.Text = usercount.ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "select count(Customer.CustomerName) as numOfcustomer from Customer; ";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int customercount = rdr.GetInt32(0);
                    noOfCustomer.Text = customercount.ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "select count(Supplier.SupplierName) as numOfsuppliers from Supplier";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int supcount = rdr.GetInt32(0);
                    noOFsupplier.Text = supcount.ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "select count(Product.ProductName) as numOfproduct from Product; ";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int productcount = rdr.GetInt32(0);
                    noOfproduct.Text = productcount.ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT count(Invoice_Info.invoiceNo) as OrderNo from Invoice_Info,Customer where Invoice_Info.CustomerID=Customer.CustomerID and Invoice_Info.PaymentType='Credit'; ";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int DueOrder = rdr.GetInt32(0);
                    DuepymentOrder.Text = DueOrder.ToString();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void ProductName_MouseClick(object sender, MouseEventArgs e)
        {
            productchart.Series["productsale"].Points.Clear();

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

                    int n = LowStockGridView1.Rows.Add();
                    LowStockGridView1.Rows[n].Cells[0].Value = item["ProductID"].ToString();
                    LowStockGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                    LowStockGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productnameText_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(productnameText, null);
        }
        
        private void loadbtn_Click(object sender, EventArgs e)
        {
            this.productchart.Series["productsaleQty"].Points.Clear();

            string monthname="";
            int TotalSoldQuantiy;
            
            try
            {
                
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT MAX(DATENAME(MM,InvoiceDate)) AS  MONTHNAME, SUM(ProductSold.Quantity) AS TOTAL from Invoice_Info INNER JOIN ProductSold ON  Invoice_info.InvoiceNo=ProductSold.InvoiceNo INNER JOIN Product ON Product.ProductID=ProductSold.ProductID WHERE Product.ProductName='" + productnameText.Text + "'  GROUP BY MONTH(InvoiceDate) ;";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    
                     monthname = rdr.GetString(0);
                     TotalSoldQuantiy = rdr.GetInt32(1);
                   
                    productchart.Series["productsaleQty"].Points.AddXY(monthname,TotalSoldQuantiy);
                    loadbtn.Enabled = false;
                     
                   // productchart.Series["productsaleQty"].Points.AddXY(monthname, TotalSoldQuantiy);
                }
                if (monthname== "")
                {
                    errorProvider1.SetError(productnameText, "Not Sold your Product ");
                }
               


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        private void productnameText_Click(object sender, EventArgs e)
        {
            
            loadbtn.Enabled = true;
        }

        private void Dashbtn_Click_1(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            LowStockGridView1.Rows.Clear();
            getData();
        }

       
      

      
    }
}
