using QLST_N7.DAO;
using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLST_N7
{
    public partial class frmQLNhanVien : Form
    {
        BindingSource nhanvienList = new BindingSource();
        public frmQLNhanVien()
        {
            InitializeComponent();
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

        private void frmDSNhanVien_Load(object sender, EventArgs e)
        {
            disBtn();
            dtvDSNV.DataSource = nhanvienList;
            loadQuyen();
            loadNhanVien();
            addNhanVienBinding();
        }

        private void disBtn()
        {
            btnQLNV.Enabled = false;
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

        private void btnBaoCao_Click_1(object sender, EventArgs e)
        {
            frmBaoCaoBanHang frm = new frmBaoCaoBanHang();
            this.Close();
            frm.Show();
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


        private void btnThem_Click(object sender, EventArgs e)
        {
            string TenTaiKhoan = txtTenTaiKhoan.Text.Trim();
            if (QuanLyNhanVienDAO.Instance.TenTaiKhoanIsExist(TenTaiKhoan))
            {
                MessageBox.Show("Tài khoản đã tồn tại");
                return;
            }
            string HoTen = txtTenNhanVien.Text.Trim();
            int GioiTinh = int.Parse(txtGioiTinh.Text.Trim());
            string DiaChi = txtDiaChi.Text.Trim();
            string SoDienThoai = txtSoDienThoai.Text.Trim();
            string Email = txtEmail.Text.Trim();
            //MessageBox.Show(txtQuyen.SelectedValue.ToString());
            int MaQuyen = int.Parse(txtQuyen.SelectedValue.ToString());

            if (checkFormAdd(TenTaiKhoan, HoTen, DiaChi, SoDienThoai, Email))
            {
                Them(TenTaiKhoan, HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text) || dtvDSNV.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
                return;
            }
            int MaNhanVien = int.Parse(txtMaNhanVien.Text);
            if (MaNhanVien.Equals(AccountDAO.Instance.Nv.Manhanvien))
            {
                MessageBox.Show("Không thể xóa chính bạn");
                return;
            }
            else
            {
                bool flag = true;
                if (QuanLyNhanVienDAO.Instance.isInsertedHoaDon(MaNhanVien))
                {
                    if(MessageBox.Show("Nhân viên này đã lập hóa đơn, bạn có muốn tiếp tục xóa", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!QuanLyNhanVienDAO.Instance.updateHoaDonWhenDelete(MaNhanVien))
                        {
                            MessageBox.Show("Xóa thất bại");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã hủy xóa");
                        return;
                    }
                }

                if (QuanLyNhanVienDAO.Instance.isInsertedNhapHang(MaNhanVien))
                {
                    if (MessageBox.Show("Nhân viên này đã lập đơn hàng nhập, bạn vẫn muốn tiếp tục xóa", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!QuanLyNhanVienDAO.Instance.updateNhapHangWhenDelete(MaNhanVien))
                        {
                            MessageBox.Show("Xóa thất bại");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã hủy xóa");
                        return;
                    }

                }
                Xoa(MaNhanVien);
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text) || dtvDSNV.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa");
                return;
            }
            int MaNhanVien = int.Parse(txtMaNhanVien.Text.Trim());
            string HoTen = txtTenNhanVien.Text.Trim();
            int GioiTinh = int.Parse(txtGioiTinh.Text.Trim());
            string DiaChi = txtDiaChi.Text.Trim();
            string SoDienThoai = txtSoDienThoai.Text.Trim();
            string Email = txtEmail.Text.Trim();
            int MaQuyen = int.Parse(txtQuyen.SelectedValue.ToString());

            //MessageBox.Show(MaNhanVien.ToString() + " " + HoTen + " " + GioiTinh.ToString() + " " + DiaChi + " " + SoDienThoai + " " + Email + " " + MaQuyen.ToString());
            checkFormEdit(HoTen, DiaChi, SoDienThoai, Email);
            Sua(MaNhanVien, HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtTenTaiKhoan.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            int MaNhanVien = int.Parse(txtMaNhanVien.Text);
            ResetPassWord(MaNhanVien);
        }


        private void btnBaoCaoNhapHang_Click(object sender, EventArgs e)
        {
            frmBaoCaoNhapHang fm = new frmBaoCaoNhapHang();
            this.Close();
            fm.Show();
        }

        //method
        private bool checkFormAdd(string TenTaiKhoan, string HoTen,  string DiaChi, string SoDienThoai, string Email)
        {
            if (String.IsNullOrEmpty(TenTaiKhoan) || String.IsNullOrEmpty(HoTen) || String.IsNullOrEmpty(DiaChi) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(SoDienThoai))
            {
                return false;
            }
            return true;
        }
        private bool checkFormEdit( string HoTen, string DiaChi, string SoDienThoai, string Email)
        {
            if (String.IsNullOrEmpty(HoTen))
            {
                HoTen = " ";
            }
            if (String.IsNullOrEmpty(DiaChi))
            {
                DiaChi = " ";
            }
            if (String.IsNullOrEmpty(SoDienThoai))
            {
                SoDienThoai = " ";
            }
            if (String.IsNullOrEmpty(Email))
            {
                Email = " ";
            }
            return true;
        }

        private void addNhanVienBinding()
        {
            txtMaNhanVien.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "MaNhanVien", true, DataSourceUpdateMode.Never));
            txtTenNhanVien.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "HoTen", true, DataSourceUpdateMode.Never));
            txtGioiTinh.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
            txtTenTaiKhoan.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "TenTaiKhoan", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
            txtSoDienThoai.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "SoDienThoai", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "Email", true, DataSourceUpdateMode.Never));
            txtQuyen.DataBindings.Add(new Binding("Text", dtvDSNV.DataSource, "TenQuyen", true, DataSourceUpdateMode.Never));
        }

        private void loadNhanVien()
        {
            nhanvienList.DataSource = QuanLyNhanVienDAO.Instance.getListNhanVien();
        }

        private void loadQuyen()
        {
            txtQuyen.DataSource = QuanLyNhanVienDAO.Instance.loadQuyen();
            txtQuyen.DisplayMember = "TenQuyen";
            txtQuyen.ValueMember = "MaQuyen";
        }

        private void Them(string TenTaiKhoan, string HoTen, int GioiTinh, string DiaChi, string SoDienThoai, string Email, int MaQuyen)
        {
            if(QuanLyNhanVienDAO.Instance.AddNhanVien(TenTaiKhoan, HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen))
            {
                MessageBox.Show("Thêm nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại");
            }
            loadNhanVien();
        }

        private void Xoa(int MaNhanVien)
        {
            if (QuanLyNhanVienDAO.Instance.DeleteNhanVien(MaNhanVien))
            {
                MessageBox.Show("Xóa nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Xóa nhân viên thất bại");
            }
            loadNhanVien();
        }

        private void Sua(int MaNhanVien, string HoTen, int GioiTinh, string DiaChi, string SoDienThoai, string Email, int MaQuyen)
        {
            if (QuanLyNhanVienDAO.Instance.EditNhanVien(MaNhanVien, HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen))
            {
                MessageBox.Show("Sửa nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Sửa nhân viên thất bại");
            }
            loadNhanVien();
        }

        private void ResetPassWord(int MaNhanVien)
        {
            if (QuanLyNhanVienDAO.Instance.ResetPassword(MaNhanVien))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string name = txtTimKiemNhanVien.Text;
            if(string.IsNullOrEmpty(name) || name.Length == 0)
            {
                loadNhanVien();
                return;
            }
            DataTable dt = SearchDAO.Instance.searchNhanVienByName(name);
            this.nhanvienList.DataSource = dt;
        }

        private void txtTimKiemNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void txtTenNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
