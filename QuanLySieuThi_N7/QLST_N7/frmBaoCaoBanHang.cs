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
    public partial class frmBaoCaoBanHang : Form
    {
        public frmBaoCaoBanHang()
        {
            InitializeComponent();
            LoadTimePicked();
            LoadDSHDByDate(dpTuNgay.Value, dpDenNgay.Value);
            LoadThongKeInFo(dpTuNgay.Value, dpDenNgay.Value);
        }

        private void disBtn()
        {
            btnBaoCao.Enabled = false;
        }
        private void frmHeThongQL_Load(object sender, EventArgs e)
        {
            disBtn();
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
            btnSideBar.BackColor = Color.FromArgb(204, 41, 0);

        }

        private void btnVeTrangChu_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)Application.OpenForms["frmMain"];
            if (frm != null)
            {
                frm.enable();
                frm.Show();
                this.Close();
            }
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

        private void pnDoanhThu_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnDoanhThu, e);

        }

        private void pnTienLai_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnTienLai, e);

        }

        private void pnMatHangTrongKho_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnMatHangTrongKho, e);

        }

        private void pnDonHang_Paint(object sender, PaintEventArgs e)
        {
            Paint_Color(pnDonHang, e);

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadDSHDByDate(dpTuNgay.Value, dpDenNgay.Value);
            LoadThongKeInFo(dpTuNgay.Value, dpDenNgay.Value);

        }
        private void btnQLNV_Click(object sender, EventArgs e)
        {
            frmQLNhanVien frm = new frmQLNhanVien();
            this.Close();
            frm.Show();
        }

        ////method
        private void LoadDSHDByDate(DateTime TuNgay, DateTime DenNgay)
        {
            dtvDSHD.DataSource = HoaDonDAO.Instance.getHoaDonByDate(TuNgay, DenNgay);
        }

        private void LoadTimePicked()
        {
            DateTime today = DateTime.Now;
            dpTuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dpDenNgay.Value = dpTuNgay.Value.AddMonths(1).AddDays(-1);
        }

        private void LoadThongKeInFo(DateTime TuNgay, DateTime DenNgay)
        {
            lblDonHang.Text = HoaDonDAO.Instance.getCountHoaDon(TuNgay, DenNgay).ToString();
            lblDoanhThu.Text = HoaDonDAO.Instance.getDoanhThu(TuNgay, DenNgay).ToString("N0");
            lblTienLai.Text = HoaDonDAO.Instance.getTienLai(TuNgay, DenNgay).ToString("N0");
            lblTonKho.Text = KhoDAO.Instance.getCount().ToString();
        }

        private void btnBaoCaoNhapHang_Click(object sender, EventArgs e)
        {
            frmBaoCaoNhapHang fm = new frmBaoCaoNhapHang();
            this.Close();
            fm.Show();
        }

        private void dpDenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThongKe.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
