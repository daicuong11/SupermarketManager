using QLST_N7.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmKho : Form
    {
        CultureInfo culture = new CultureInfo("vi-VN");
        public frmKho()
        {
            InitializeComponent();
        }

        private void loadAllKho()
        {
            dtvKho.DataSource = KhoDAO.Instance.loadAllKho();
            dtvKho.Refresh();
            txtTongSoLuong.Text = "00";
            txtTongGiaTri.Text = "00";
            txtTongSoLuong.Text = KhoDAO.Instance.getCount().ToString();
            txtTongGiaTri.Text = KhoDAO.Instance.getPrice().ToString("c", culture);
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            disBtn();
            loadAllKho();
        }

        private void disBtn()
        {
            btnKho.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSideBar_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;

        }

        private void btnSideBar_MouseEnter(object sender, EventArgs e)
        {
            btnSideBar.BackColor = Color.FromArgb(204, 204, 204);

        }

        private void btnSideBar_MouseLeave(object sender, EventArgs e)
        {
            btnSideBar.BackColor = Color.FromArgb(204, 204, 204);

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                frmMain frm = (frmMain)Application.OpenForms["frmMain"];
                if (frm != null)
                {
                    AccountDAO.Instance.Nv = null;
                    frm.enable();
                    frm.load_Name();

                    frm.Show();
                    this.Close();
                }
            }
        }

        private void btnVeTrangChu_Click_1(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)Application.OpenForms["frmMain"];
            if (frm != null)
            {
                frm.enable();
                frm.Show();
                this.Close();
            }
        }

        private void btnBanHang_Click_1(object sender, EventArgs e)
        {
            frmBanHang frm = new frmBanHang();
            this.Close();
            frm.Show();
        }

        private void btnNhapHang_Click_1(object sender, EventArgs e)
        {
            frmNhapHang frm = new frmNhapHang();
            this.Close();
            frm.Show();
        }

        private void btnTatCaPhienBan_Click(object sender, EventArgs e)
        {
            loadAllKho();
        }

        private void btnKho1_Click(object sender, EventArgs e)
        {
            dtvKho.DataSource = KhoDAO.Instance.loadKho1();
            dtvKho.Refresh();

            txtTongSoLuong.Text = KhoDAO.Instance.getCount(1).ToString();
            txtTongGiaTri.Text = KhoDAO.Instance.getPrice(1).ToString("c", culture);
        }

        private void btnKho2_Click(object sender, EventArgs e)
        {
            dtvKho.DataSource = KhoDAO.Instance.loadKho2();
            dtvKho.Refresh();
            txtTongSoLuong.Text = KhoDAO.Instance.getCount(2).ToString();
            txtTongGiaTri.Text = KhoDAO.Instance.getPrice(2).ToString("c", culture);
        }

        private void btnKho3_Click(object sender, EventArgs e)
        {
            dtvKho.DataSource = KhoDAO.Instance.loadKho3();
            dtvKho.Refresh();
            txtTongSoLuong.Text = KhoDAO.Instance.getCount(3).ToString();
            txtTongGiaTri.Text = KhoDAO.Instance.getPrice(3).ToString("c", culture);
        }

        private void btnTatCaKho_Click(object sender, EventArgs e)
        {
            loadAllKho();
        }

        private void btnTimKiemSanPham_Click(object sender, EventArgs e)
        {
            DataTable dt = SearchDAO.Instance.searchHangHoaByName(txtTimKiemSanPham.Text);
            dtvKho.DataSource = dt;
            dtvKho.Refresh();
            txtTongSoLuong.Text = KhoDAO.Instance.getCount(dt).ToString();
            txtTongGiaTri.Text = KhoDAO.Instance.getPrice(dt).ToString("c", culture);
        }

        private void txtTimKiemSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiemSanPham.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmKho_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
