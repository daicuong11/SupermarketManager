
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
using System.Configuration;
using QLST_N7.DTO;
using DevComponents.DotNetBar.Controls;
using Bunifu.UI.WinForms;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using QLST_N7.DAO;
using System.Globalization;
using QLST_N7.CRV;

namespace QLST_N7
{
    public partial class frmBanHang : Form
    {
        CultureInfo culture = new CultureInfo("vi-VN");
        private int newMaHoaDon = HoaDonDAO.Instance.createNewHoaDon(1, AccountDAO.Instance.Nv.Manhanvien, 0);
        public frmBanHang()
        {
            InitializeComponent();
        }

        private bool search()
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
            {
                load_DSSP();
            }
            string strQuyery = "select mahanghoa as 'STT', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', giaban as N'Giá bán' from hanghoa where tenhanghoa like N'%" +txtTimKiem.Text.Trim()+ "%'";

            dtvDSSP.DataSource = DataProvider.Instance.ExecuteQuery(strQuyery);
            dtvDSSP.Refresh();
            return true;

        }

        private bool load_DSSP()
        {
            return ProductsDAO.Instance.load_DSSP(this.dtvDSSP);
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            disBtn();
            load_DSSP();
            load_Name();
        }

        private void disBtn()
        {
            btnBanHang.Enabled = false;
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

        private void btnBanHang_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            frmNhapHang frm = new frmNhapHang();
            this.Close();
            frm.Show();
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            frmKho frm = new frmKho();
            this.Close();
            frm.Show();
        }


        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSideBar_Click_1(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private bool addCue()
        {
            if (isZero())
            {
                return false;
            }
            else
            {
                double khachphaitra = double.Parse(txtTongTien.Text.Trim());
                int khachphaitra1 = int.Parse(txtTongTien.Text.Trim());

                decimal goiy2 = (decimal)(khachphaitra + 1000);
                int goiy3 = (((khachphaitra1 / 5000) + 1) * 5000);
                int goiy4 = (((khachphaitra1 / 10000) + 1) * 10000);
                int goiy5 = (((khachphaitra1 / 100000) + 1) * 100000);
                double goiy6 = (double)(Math.Ceiling(khachphaitra / 500000.0) * 500000);

                btnGoiY1.Text = khachphaitra.ToString();
                btnGoiY2.Text = goiy2.ToString();
                btnGoiY3.Text = goiy3.ToString();
                btnGoiY4.Text = goiy4.ToString();
                btnGoiY5.Text = goiy5.ToString();
                btnGoiY6.Text = goiy6.ToString();
            }
            if (!string.IsNullOrEmpty(txtTienKhacDua.Text.Trim()))
            {
                decimal tienthuatrakhach = decimal.Parse(txtTienKhacDua.Text.Trim()) - decimal.Parse(txtTongTien.Text.Trim());
                txtTienThuaTraKhach.Text = tienthuatrakhach.ToString();
            }
            return true;
        }
        
        private bool isZero()
        {
            if (string.IsNullOrEmpty(txtTongTien.Text.Trim()) || txtTongTien.Text == "0")
            {
                return true;
            }
            return false;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            decimal TongTien = CTHDDAO.Instance.getTotal(this.newMaHoaDon);
            if(TongTien <= 0)
            {
                MessageBox.Show("Vui lòng thêm sản phẩm");
                return;
            }
            if(string.IsNullOrEmpty(txtTienKhacDua.Text) || txtTienKhacDua.Text == "0")
            {
                MessageBox.Show("Vui lòng nhập tiền khách đưa");
                txtTienKhacDua.Select();
                return;
            }

            decimal TongTienHoaDon = decimal.Parse(TongTien.ToString());
            
            decimal TienKhachDua = decimal.Parse(txtTienKhacDua.Text);

            decimal TienThuaTraKhach = TienKhachDua - TongTienHoaDon;

            if(TienThuaTraKhach < 0)
            {
                MessageBox.Show("Tiền khách đưa không đủ");
                txtTienKhacDua.Select();
                return;
            }

            string HoTenKhachHang = txtTenKhacHang.Text.Trim();
            string SoDienThoai = txtSoDienThoaiKhachHang.Text.Trim();
            if (!String.IsNullOrEmpty(HoTenKhachHang))
            {
                KhachHangDAO.Instance.insertKhachHang(HoTenKhachHang, SoDienThoai);
                int MaKhachHang = (int)DataProvider.Instance.ExecuteScalar("select MaKhachHang from KhachHang where hoten = N'"+ HoTenKhachHang + "' and SoDienThoai = '" + SoDienThoai +"'");
                HoaDonDAO.Instance.updateKhachHang( this.newMaHoaDon, MaKhachHang);
            }
            else
            {
                HoTenKhachHang = "Khách lẻ";
                SoDienThoai = " ";
            }

            //update TongTien
            HoaDonDAO.Instance.updateTongTien(this.newMaHoaDon, TongTienHoaDon);
            //report
            DataTable hoadon = CRVDAO.Instance.loadHoaDon(this.newMaHoaDon);
            crHoaDon crhoadon = new crHoaDon();
            crhoadon.SetDataSource(hoadon);

            //setPar
            crhoadon.SetParameterValue("TenNhanVien", AccountDAO.Instance.Nv.Hoten);
            crhoadon.SetParameterValue("TenKhachHang", HoTenKhachHang);
            crhoadon.SetParameterValue("SoDienThoai", SoDienThoai);
            crhoadon.SetParameterValue("TongTien", TongTienHoaDon);
            crhoadon.SetParameterValue("TienKhachDua", TienKhachDua);
            crhoadon.SetParameterValue("TienTraKhach", TienThuaTraKhach);

            //show from in
            frmInHoaDon frminhoadon = new frmInHoaDon();
            frminhoadon.crvHoaDon.ReportSource = crhoadon;
            frminhoadon.ShowDialog();
            //lưu hóa đơn
            ProductsDAO.Instance.updateSoLuongTonKho(this.newMaHoaDon);
            load_DSSP();
            //tạo hóa đơn mới
            this.newMaHoaDon = HoaDonDAO.Instance.createNewHoaDon(1, AccountDAO.Instance.Nv.Manhanvien, 0);
            dtvHoaDon.DataSource = CTHDDAO.Instance.loadCTHD(this.newMaHoaDon);
            txtTongTien.Text = "0";
            txtTienKhacDua.Text = "0";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            search();
        }

        private bool kiem_tra_form_them_thu_cong()
        {
            if(string.IsNullOrEmpty(txtTenSanPham.Text.Trim())|| string.IsNullOrEmpty(txtSoluong.Text.Trim())|| string.IsNullOrEmpty(txtGia.Text.Trim())) { return false; }
            return true;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtTenSanPham.Text = "";
            txtSoluong.Text = "";
            txtGia.Text = "";
        }

        private void btnXoaToanBo_Click_1(object sender, EventArgs e)
        {
            CTHDDAO.Instance.deleteCTHD(newMaHoaDon);
            dtvHoaDon.DataSource = CTHDDAO.Instance.loadCTHD(newMaHoaDon);
            dtvHoaDon.Refresh();
            txtTongTien.Clear();
            txtTienKhacDua.Clear();
            txtTienThuaTraKhach.Clear();
            addCue();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dtvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtvHoaDon.SelectedRows[0];
                CTHDDAO.Instance.deleteARowCTHD(newMaHoaDon, (int)DataProvider.Instance.ExecuteScalar("select MaHanghoa from HangHoa where TenHangHoa like N'%" + selectedRow.Cells[0].Value.ToString() + "%'"));
                dtvHoaDon.DataSource = CTHDDAO.Instance.loadCTHD(newMaHoaDon);
                dtvHoaDon.Refresh();
                decimal TongTien = decimal.Parse(CTHDDAO.Instance.getTotal(this.newMaHoaDon).ToString());
                txtTongTien.Text = TongTien.ToString();
            }
            else
            {
                MessageBox.Show("Chọn dòng muốn bỏ");
            }
        }

        private void btnXoa2_Click_1(object sender, EventArgs e)
        {
            if (dtvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtvHoaDon.SelectedRows[0];
                CTHDDAO.Instance.deleteARowCTHD(newMaHoaDon, (int)DataProvider.Instance.ExecuteScalar("select MaHanghoa from HangHoa where TenHangHoa like N'%"+ selectedRow.Cells[0].Value.ToString() +"%'"));
                dtvHoaDon.DataSource = CTHDDAO.Instance.loadCTHD(newMaHoaDon);
                dtvHoaDon.Refresh();
                decimal TongTien = decimal.Parse(CTHDDAO.Instance.getTotal(this.newMaHoaDon).ToString());
                txtTongTien.Text = TongTien.ToString();
            }
            else
            {
                MessageBox.Show("Chọn dòng muốn bỏ");
            }
        }

        private void btnXoaToanBo2_Click(object sender, EventArgs e)
        {
            CTHDDAO.Instance.deleteCTHD(newMaHoaDon);
            dtvHoaDon.DataSource = CTHDDAO.Instance.loadCTHD(newMaHoaDon);
            dtvHoaDon.Refresh();
            txtTongTien.Clear();
            txtTienKhacDua.Clear();
            txtTienThuaTraKhach.Clear();
            addCue();
        }

        private void dtvDSSP_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            HANGHOA hanghoa = new HANGHOA(dtvDSSP.Rows[e.RowIndex]);

            int MaHangHoa = hanghoa.MaHangHoa;
            int SoLuongBan = (int)numUpDown.Value;

            if(SoLuongBan > hanghoa.SoLuong)
            {
                MessageBox.Show("Sản phẩm " + hanghoa.TenHangHoa + " chỉ có số lượng là " + hanghoa.SoLuong, "Sản phẩm không đủ số lượng để bán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CTHDDAO.Instance.creatNewCTHD(newMaHoaDon, MaHangHoa, SoLuongBan);

            DataTable newCTHD = CTHDDAO.Instance.loadCTHD(newMaHoaDon);

            dtvHoaDon.DataSource = newCTHD;
            dtvHoaDon.Refresh();

            txtTongTien.Text = CTHDDAO.Instance.getTotal(this.newMaHoaDon).ToString();
            addCue();
        }

        private void txtGia_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoluong_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSideBar_MouseEnter_1(object sender, EventArgs e)
        {
            btnSideBar.BackColor = Color.FromArgb(204, 204, 204);

        }

        private void btnSideBar_MouseLeave_1(object sender, EventArgs e)
        {
            btnSideBar.BackColor = Color.FromArgb(204, 41, 0);

        }

        private void dtvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dtvHoaDon.Rows[e.RowIndex];
            txtTenSanPham.Text = row.Cells[0].Value.ToString();
            txtSoluong.Text = row.Cells[1].Value.ToString();
            txtGia.Text = row.Cells[2].Value.ToString();
        }

        void load_Name()
        {
            lblTenNhanVien.Text = AccountDAO.Instance.loadUser();
        }

        private void frmBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            CTHDDAO.Instance.deleteCTHD(this.newMaHoaDon);
            HoaDonDAO.Instance.deleteHoaDon(this.newMaHoaDon);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!kiem_tra_form_them_thu_cong())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            string tensanpham = txtTenSanPham.Text.Trim();
            int soluong = int.Parse(txtSoluong.Text.Trim());
            decimal giaban = decimal.Parse(txtGia.Text.Trim());
            int maHangHoa = (int)DataProvider.Instance.ExecuteScalar("select MaHanghoa from HangHoa where TenHangHoa like N'%" + tensanpham + "%'");
            int tonKho = ProductsDAO.Instance.getSoluongByID(maHangHoa);

            if(tonKho < soluong)
            {
                MessageBox.Show("Sản phẩm " + tensanpham + " chỉ có số lượng là " + tonKho ,"Sản phẩm không đủ số lượng để bán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CTHDDAO.Instance.updateACTHD(newMaHoaDon, maHangHoa, soluong);

            DataTable newCTHD = CTHDDAO.Instance.loadCTHD(newMaHoaDon);

            dtvHoaDon.DataSource = newCTHD;
            dtvHoaDon.Refresh();

            txtTongTien.Text = CTHDDAO.Instance.getTotal(this.newMaHoaDon).ToString();
            addCue();
        }

        private void pnsubThongTinThanhToan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGoiY1_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY1.Text.Trim();
        }

        private void btnGoiY2_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY2.Text.Trim();
        }

        private void btnGoiY3_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY3.Text.Trim();
        }

        private void btnGoiY4_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY4.Text.Trim();
        }

        private void btnGoiY5_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY5.Text.Trim();
        }

        private void btnGoiY6_Click(object sender, EventArgs e)
        {
            txtTienKhacDua.Text = "00";
            txtTienKhacDua.Text = btnGoiY6.Text.Trim();
        }

        private void txtTongTien_TextChange(object sender, EventArgs e)
        {

        }

        private void txtTienKhacDua_Click_1(object sender, EventArgs e)
        {
            txtTienKhacDua.SelectionStart = 0;
            txtTienKhacDua.SelectionLength = txtTienKhacDua.Text.Length;
        }

        private void txtTienKhacDua_TextChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTienKhacDua.Text.Trim()))
            {
                decimal tienthuatrakhach = decimal.Parse(txtTienKhacDua.Text.Trim()) - decimal.Parse(txtTongTien.Text.Trim());
                txtTienThuaTraKhach.Text = tienthuatrakhach.ToString();
            }
        }

        private void txtTienKhacDua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
