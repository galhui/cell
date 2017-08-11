using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellExe
{
    public partial class Form : System.Windows.Forms.Form
    {
        Cell.Main main;

        public Form()
        {
            InitializeComponent();

            main = new Cell.Main(pictureBox, lab_datetime);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
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
            Environment.Exit(0);
        }
    }
}
