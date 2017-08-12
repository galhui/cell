using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellSimul
{
    public partial class Form : System.Windows.Forms.Form
    {
        Main main;

        public Form()
        {
            InitializeComponent();

            for(int i = 0; i <= 100; i+=10)
            {
                if (i == 0) continue;
                cmb_protoCell.Items.Add(i);
            }
            cmb_protoCell.SelectedIndex = 9;


            main = new Main(pictureBox, lab_datetime);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            main.protoCellMax = Convert.ToInt32(cmb_protoCell.Text);
            main.Start();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            main.Stop();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            main.Reset();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Reset();
            Application.ExitThread();
            System.Environment.Exit(0);
        }

        private void Form_Resize(object sender, EventArgs e)
        {   
            pictureBox.Width = this.Width - 140;
            pictureBox.Height = this.Height - 85;

            main.Resize(new Extent(pictureBox.Height, pictureBox.Width));
        }
    }
}
