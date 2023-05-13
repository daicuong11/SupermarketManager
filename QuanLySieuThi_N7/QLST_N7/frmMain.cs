using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Schedule;
using QLST_N7.DAO;

namespace QLST_N7
{
    public partial class frmMain : Form
    {
        private frmDangKy frmdangky;
        private frmDangNhap frmdangnhap;
        private frmGioiThieu frmgioithieu;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmdangky = new frmDangKy();
            frmdangky.MdiParent = this;
            frmdangnhap = new frmDangNhap();
            frmdangnhap.MdiParent = this;
            frmdangnhap.Show();
            load_Name();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void enable()
        {
            AccountDAO.Instance.enable(this.btnQuanLy, this.btnNhanVien, this.btnDangNhap, this.btnDangXuat);
        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "frmDangNhap")
                {
                    return;
                }
            }
            frmdangnhap = new frmDangNhap();
            frmdangnhap.MdiParent = this;
            frmdangnhap.Show();
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = FormBorderStyle.None;
            frmBaoCaoBanHang frmbaocao = new frmBaoCaoBanHang();
            frmbaocao.Show();
            this.Hide();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = FormBorderStyle.None;

            frmBanHang frmbanhang = new frmBanHang();
            frmbanhang.Show();
            this.Hide();
        }

        private void btnGioiThieu_Click(object sender, EventArgs e)
        {
            frmgioithieu = new frmGioiThieu();
            frmgioithieu.MdiParent = this;
            frmgioithieu.Show();                                           
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {

                AccountDAO.Instance.Nv = null;
                this.enable();
                load_Name();
            }
        }
        public void load_Name()
        {
            lblTenNhanVien.Text = AccountDAO.Instance.loadUser();
        }

        private void lblTenNhanVien_Click(object sender, EventArgs e)
        {

        }
    }
}
