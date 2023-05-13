using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmMoDau : Form
    {
        int i = 0;
        List<int> lstTime = new List<int>();
        public frmMoDau()
        {
            InitializeComponent();

        }

        private void loadListTime()
        {
            lstTime.Add(10);
            lstTime.Add(40);
            lstTime.Add(20);
            lstTime.Add(20);
            lstTime.Add(10);
        }

        private void frmMoDau_Load(object sender, EventArgs e)
        {
            loadListTime();
            this.timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int val = lstTime[i];

            pgbMoDau.Value += val;
            i++;
            if (pgbMoDau.Value >= pgbMoDau.Maximum)
            {
                timer.Stop();
                frmMain frmmain = new frmMain();
                this.Hide();
                frmmain.Show();
            }

        }
    }
}
