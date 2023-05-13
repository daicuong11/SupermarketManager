using QLST_N7.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmBaoCaoNhapHang : Form
    {
        public frmBaoCaoNhapHang()
        {
            InitializeComponent();
            LoadTimePicked();
            LoadDSNHByDate(dpTuNgay.Value, dpDenNgay.Value);
            LoadThongKeInFo(dpTuNgay.Value, dpDenNgay.Value);
        }

        private void frmBaoCaoNhapHang_Load(object sender, EventArgs e)
        {
            disBtn();
        }

        private void disBtn()
        {
            btnBaoCaoNhapHang.Enabled = false;
        }


        public void Paint_Color(Panel panel, PaintEventArgs e)
        {
            Color startColor = Color.FromArgb(254, 97, 50);

            Color endColor = Color.Violet;

            LinearGradientBrush brush = new LinearGradientBrush(
                panel.ClientRectangle,
                startColor,
                endColor,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(brush, panel.ClientRectangle);
        }

        ////method
        private void LoadDSNHByDate(DateTime TuNgay, DateTime DenNgay)
        {
            dtvDSDN.DataSource = QLDonNhapDAO.Instance.getDonNhapByDate(TuNgay, DenNgay);
        }

        private void LoadTimePicked()
        {
            DateTime today = DateTime.Now;
            dpTuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dpDenNgay.Value = dpTuNgay.Value.AddMonths(1).AddDays(-1);
        }

        private void LoadThongKeInFo(DateTime TuNgay, DateTime DenNgay)
        {
            lblDonNhap.Text = QLDonNhapDAO.Instance.getCountDonNhap(TuNgay, DenNgay).ToString();
            lblVonBoRa.Text = QLDonNhapDAO.Instance.getVon(TuNgay, DenNgay).ToString("N0");
            lblGiaTriBan.Text = QLDonNhapDAO.Instance.getGiaTriBan(TuNgay, DenNgay).ToString("N0");
            lblSoLuongNhap.Text = QLDonNhapDAO.Instance.getSoLuongNhap().ToString();
        }

        //main even
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmBaoCaoBanHang frm = new frmBaoCaoBanHang();
            this.Close();
            frm.Show();
        }

        private void btnQLNV_Click_1(object sender, EventArgs e)
        {
            frmQLNhanVien frm = new frmQLNhanVien();
            this.Close();
            frm.Show();
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
            btnSideBar.BackColor = Color.FromArgb(204, 41, 0);

        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            LoadDSNHByDate(dpTuNgay.Value, dpDenNgay.Value);
            LoadThongKeInFo(dpTuNgay.Value, dpDenNgay.Value);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
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

        private void pnDonNhap_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnDonNhap, e);
        }

        private void pnVonBoRa_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnVonBoRa, e);
        }

        private void pnGiaTriBan_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnGiaTriBan, e);
        }

        private void pnSoLuongNhap_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnSoLuongNhap, e);
        }
    }
}
