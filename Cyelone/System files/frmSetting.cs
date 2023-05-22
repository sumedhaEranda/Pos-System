using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private void btnFormBack_Click(object sender, EventArgs e)
        {
            ColorDialog colorDig = new ColorDialog();
            if (colorDig.ShowDialog()==DialogResult.OK)
            {
                Properties.Settings.Default.FormBackground = colorDig.Color;
                Properties.Settings.Default.Save();
                this.BackColor = colorDig.Color;
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackground;
            this.panel1.BackColor = Properties.Settings.Default.HeaderBackground;
            this.panel2.BackColor = Properties.Settings.Default.FooterBackground;
            this.label1.ForeColor = Properties.Settings.Default.FontHeaderColor;
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            ColorDialog colorDig = new ColorDialog();
            if (colorDig.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.HeaderBackground = colorDig.Color;
                Properties.Settings.Default.Save();
                panel1.BackColor = colorDig.Color;
            }
        }

        private void btnFooter_Click(object sender, EventArgs e)
        {
            ColorDialog colorDig = new ColorDialog();
            if (colorDig.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.FooterBackground = colorDig.Color;
                Properties.Settings.Default.Save();
                panel2.BackColor = colorDig.Color;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            ColorDialog colorDig = new ColorDialog();
            if (colorDig.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.FontHeaderColor = colorDig.Color;
                Properties.Settings.Default.Save();
                label1.ForeColor = colorDig.Color;
            }
        }

        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
             frmMainMenu.frmRestartCount = 0;
        }
    }
}
