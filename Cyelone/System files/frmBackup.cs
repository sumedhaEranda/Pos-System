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
using System.Security.Cryptography;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmBackup : Form
    {

        SqlConnection con = null;
        
        ConnectionString cs = new ConnectionString();
        public frmBackup()
        {
            InitializeComponent();
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel2.BackColor = Properties.Settings.Default.HeaderBackground;
            this.panel1.BackColor = Properties.Settings.Default.FooterBackground;
            this.label2.ForeColor = Properties.Settings.Default.FontHeaderColor;
        }

        private void btnBackupBrow_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dig = new FolderBrowserDialog();
            if (dig.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = dig.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                string database = con.Database.ToString();
                if (txtBackup.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter back file location");
                }
                else
                {
                    string srt = "BACKUP DATABASE [" + database + "] TO DISK= '" + txtBackup.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-mm-dd--hh-mm-ss") + ".bak'";
                    con.Open();
                    SqlCommand comm = new SqlCommand(srt, con);
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Database Backup done successfuly..");
                    txtBackup.Clear();
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Can't Backup. Please try again. Brows valid location");
            }
        }

        private void frmBackup_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu.frmRestartCount = 0;
        }

        private void txtBackup_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
