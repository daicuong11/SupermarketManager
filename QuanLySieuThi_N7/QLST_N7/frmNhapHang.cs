using QLST_N7.CRV;
using QLST_N7.DAO;
using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmNhapHang : Form
    {
        private frmThemNCC frmncc;

        CultureInfo culture = new CultureInfo("vi-VN");
        private int newMaNhapHang = QuanLyNhapHangDAO.Instance.insertNhapHang(AccountDAO.Instance.Nv.Manhanvien, DateTime.Now);
        public frmNhapHang()
        {
            InitializeComponent();

            frmncc = new frmThemNCC();
            loadNhanVien();
            loadNCC();
        }

        private void loadNhanVien()
        {
            txtTenNhanVien.Text = AccountDAO.Instance.Nv.Hoten;
        }

        private bool lay_DSSP()
        {
            return ProductsDAO.Instance.load_DSSP_to_NhapHang(this.dtvDSSP);
        }

        private void disBtn()
        {
            btnNhapHang.Enabled = false;
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            disBtn();
            lay_DSSP();
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

        private void btnVeTrangChu_Click_2(object sender, EventArgs e)
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

        private void btnKho_Click_1(object sender, EventArgs e)
        {
            frmKho frm = new frmKho();
            this.Close();
            frm.Show();
        }

        private void btnDangXuat1_Click(object sender, EventArgs e)
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

        private void btnThemMoiNhaCungCap_Click(object sender, EventArgs e)
        {
            frmncc.ShowDialog();
        }

        private void upNCC(NHACUNGCAP ncc)
        {
            if(ncc != null)
            {
                lblTenNhaCungCap.Text = editLabel(ncc.TenNhaCungCap);
                lblSDTNhaCungCap.Text = editLabel(ncc.SoDienThoai);
                lblDiaChiNhaCungCap.Text = editLabel(ncc.DiaChi);
            }
            else
            {
                lblTenNhaCungCap.Text = "Tên nhà cung cấp";
                lblSDTNhaCungCap.Text = "Số điện thoại";
                lblDiaChiNhaCungCap.Text = "Địa chỉ";
            }
        }

        private List<NHACUNGCAP> loadNCC()
        {
            List<NHACUNGCAP> lstNCC = NhaCungCapDAO.Instance.loadNCC();
            cbbNCC.DataSource = lstNCC;
            cbbNCC.DisplayMember = "TenNhaCungCap";
            cbbNCC.ValueMember = "MaNhaCungCap";
            upNCC(lstNCC[0]);
            return lstNCC;
        }

        private void cbbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            upNCC((NHACUNGCAP)cbbNCC.SelectedItem);
        }

        private string editLabel(String text, int maxLength = 20)
        {
            if (text.Length > maxLength)
            {;
                text = text.Substring(0, Math.Min(text.Length, 20)) + "...";
            }
            return text;
        }

        public void setNCC(NHACUNGCAP ncc)
        {
            upNCC(ncc);
            List<NHACUNGCAP> a = loadNCC();
            cbbNCC.SelectedIndex = a.Count - 1;
        }

        private bool checkForm()
        {
            if (String.IsNullOrEmpty(txtTenSanPham.Text.Trim()) || String.IsNullOrEmpty(txtSoluong.Text.Trim()) || String.IsNullOrEmpty(txtGiaNhap.Text.Trim()) || String.IsNullOrEmpty(txtGiaBan.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return false;
            }
            return true;
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (checkForm())
            {
                string tensanpham = txtTenSanPham.Text.Trim();
                decimal gianhap = decimal.Parse(txtGiaNhap.Text.Trim());
                decimal giaban = decimal.Parse(txtGiaBan.Text.Trim());
                int soluong = int.Parse(txtSoluong.Text);
                int maNCC = (int)cbbNCC.SelectedValue;
                int maKho = (int)(nudMaKho.Value); //mặc định kho 1

                DateTime ngayNhap = DateTime.Now;

                int maHanghoa = ProductsDAO.Instance.insertHangHoa(tensanpham, gianhap, giaban, soluong, maNCC, maKho);
                int maLoHang = QuanLyNhapHangDAO.Instance.insertLoHang(ngayNhap, maHanghoa);
                LOHANG newLH = new LOHANG(maLoHang, ngayNhap, maHanghoa);

                QuanLyNhapHangDAO.Instance.insertChiTietNhapHang(this.newMaNhapHang, maLoHang, soluong);

                loadDonNhap();
                int tongsoluongnhap = QuanLyNhapHangDAO.Instance.count(this.newMaNhapHang);
                decimal tongtien = QuanLyNhapHangDAO.Instance.sumTotal(this.newMaNhapHang);
                txtTongSoLuong.Text = tongsoluongnhap.ToString();

                txtTongTien.Text = tongtien.ToString();
                txtTienCanTra.Text = tongtien.ToString();
            }
        }

        

        private void loadDonNhap()
        {
            dtvDonNhap.DataSource = QuanLyNhapHangDAO.Instance.loadDonNhap(this.newMaNhapHang);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSanPham.Clear();
            txtSoluong.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
        }

        private void dtvDSSP_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dtvDSSP.Rows[e.RowIndex];
            txtTenSanPham.Text = row.Cells[1].Value.ToString();
            txtSoluong.Text = row.Cells[2].Value.ToString();
            txtGiaNhap.Text = row.Cells[3].Value.ToString();
            txtGiaBan.Text = row.Cells[4].Value.ToString();
        }

        private void btnTaoDonNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbbNCC.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp");
                return;
            }
            //get dữ liêu
            int TongSoLuong = QuanLyNhapHangDAO.Instance.count(this.newMaNhapHang);
            decimal tongtien = QuanLyNhapHangDAO.Instance.sumTotal(this.newMaNhapHang);
            int idNCC = (int)cbbNCC.SelectedValue;
            NHACUNGCAP ncc = NhaCungCapDAO.Instance.getNCCByID(idNCC);
            //check
            if(TongSoLuong <= 0)
            {
                MessageBox.Show("Vui lòng thêm sản phẩm vào đơn nhập");
                return;
            }

            if (!checkDaTraDuTien(tongtien))
            {
                MessageBox.Show("Bạn chưa trả đủ tiền");
                return;
            }

            //update time
            DateTime ngayNhap = dpHenNgayGiao.Value;
            QuanLyNhapHangDAO.Instance.updateHenNgayGiao(this.newMaNhapHang, ngayNhap);
            //update tổng tiền
            //report
            DataTable donhap = QuanLyNhapHangDAO.Instance.loadDonNhap(this.newMaNhapHang);
            crDonNhap crdonnhap = new crDonNhap();
            crdonnhap.SetDataSource(donhap);

            //setPar
            crdonnhap.SetParameterValue("TenNhanVien", AccountDAO.Instance.Nv.Hoten);
            crdonnhap.SetParameterValue("TenNhaCungCap", ncc.TenNhaCungCap);
            crdonnhap.SetParameterValue("DiaChiNhaCungCap", ncc.DiaChi);
            crdonnhap.SetParameterValue("TongSoLuong", TongSoLuong.ToString());
            crdonnhap.SetParameterValue("GiaTriDon", tongtien);
            crdonnhap.SetParameterValue("MaDonNhap", this.newMaNhapHang.ToString());
            crdonnhap.SetParameterValue("NgayNhan", ngayNhap);

            //show from in
            frmInDonNhap frmindonnhap = new frmInDonNhap();
            frmindonnhap.crvHienThiDonNhap.ReportSource = crdonnhap;
            frmindonnhap.ShowDialog();
            //update tong tien
            QuanLyNhapHangDAO.Instance.updateTongTienNhap(this.newMaNhapHang, tongtien);
            lay_DSSP();

            //tao đơn nhập mới
            newMaNhapHang = QuanLyNhapHangDAO.Instance.insertNhapHang(AccountDAO.Instance.Nv.Manhanvien, ngayNhap);
            loadDonNhap();
        }

        private bool checkDaTraDuTien(decimal TongTien)
        {
            string tiendatra = txtTienDaTra.Text.Trim();
            if (string.IsNullOrEmpty(tiendatra))
            {
                return false;
            }
            else
            {
                decimal TienDaTra = decimal.Parse(tiendatra);
                decimal TienConPhaiTra = TongTien - TienDaTra;
                if(TienConPhaiTra <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void frmNhapHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuanLyNhapHangDAO.Instance.resetListHangHoa(this.newMaNhapHang);

            QuanLyNhapHangDAO.Instance.deleteCTNH(this.newMaNhapHang);

            QuanLyNhapHangDAO.Instance.deldeteNH(this.newMaNhapHang);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtvDonNhap.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtvDonNhap.SelectedRows[0];
                //MessageBox.Show(selectedRow.Cells[0].Value.ToString());
                int MaLoHang = int.Parse(selectedRow.Cells[0].Value.ToString());
                QuanLyNhapHangDAO.Instance.resetHangHoa(this.newMaNhapHang, MaLoHang);
                loadDonNhap();
                return;
            }
            else
            {
                MessageBox.Show("Chọn dòng muốn bỏ");
            }
        }

        private void txtTienDaTra_TextChange(object sender, EventArgs e)
        {
            decimal tongien = QuanLyNhapHangDAO.Instance.sumTotal(this.newMaNhapHang);
            decimal tiendatra = 0;
            decimal conphaitra = 0;
            if(!string.IsNullOrEmpty(txtTienDaTra.Text.Trim()))
            {
                tiendatra = decimal.Parse(txtTienDaTra.Text.Trim());
                conphaitra = tongien - tiendatra;
                txtConPhaiTra.Text = conphaitra.ToString();
            }
        }

        private void txtSoluong_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtTienDaTra_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<NHACUNGCAP> lstNCC = SearchDAO.Instance.searchNCCByName(txtTimKiem.Text.Trim());
            cbbNCC.DataSource = lstNCC;
            cbbNCC.DisplayMember = "TenNhaCungCap";
            cbbNCC.ValueMember = "MaNhaCungCap";
            if(lstNCC == null)
            {
                cbbNCC.Items.Clear();
                cbbNCC.Refresh();
                return;
            }
            upNCC(lstNCC[0]);
        }

        private void btnTimKiemSanPham_Click(object sender, EventArgs e)
        {
            string name = txtTimKiemSanPham.Text.Trim();
            this.dtvDSSP.DataSource = SearchDAO.Instance.searchHangHoaByName(name);
            dtvDSSP.Refresh();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void txtTimKiemSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiemSanPham.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void txtSoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemSanPham.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void txtTenSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemSanPham.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void txtTongTien_TextChange(object sender, EventArgs e)
        {
            txtTienDaTra.Text = txtTongTien.Text;

        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTienDaTra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTaoDonNhap.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
