using QLST_N7.DAO;
using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Match match = Regex.Match(email, emailPattern);
            return match.Success;
        }

        private bool checkDangKy(string hoTen, string tenTaiKhoan, string matKhau, string email, int gioiTinh)
        {
            if (String.IsNullOrEmpty(hoTen))
            {
                errorEmpty.SetError(txtHoTen, "Vui lòng nhập họ tên");
                txtHoTen.Select();
                return false;
            }
            else if (String.IsNullOrEmpty(tenTaiKhoan))
            {
                errorEmpty.Clear();
                errorEmpty.SetError(txtTenTaiKhoan, "Vui lòng nhập tên tài khoản");
                txtTenTaiKhoan.Select();
                return false;
            }
            else if ((String.IsNullOrEmpty(matKhau) || String.IsNullOrEmpty(txtNhapLai.Text.Trim())))
            {
                errorEmpty.Clear();
                errorEmpty.SetError(txtMatKhauDk, "Vui lòng nhập mật khẩu");
                txtMatKhauDk.Clear();
                txtMatKhauDk.Select();
                return false;
            }
            else if (matKhau != txtNhapLai.Text.Trim())
            {
                errorEmpty.Clear();
                MessageBox.Show("Hai mật khẩu không khớp", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauDk.Clear();
                txtNhapLai.Clear();
                txtMatKhauDk.Select();
                return false;
            }
            else if (rbtnNam.Checked == false && rbtnNu.Checked == false)
            {
                errorEmpty.Clear();
                MessageBox.Show("Vui lòng chọn giới tính", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (String.IsNullOrEmpty(email) || !this.IsValidEmail(email))
            {
                errorEmpty.Clear();
                errorEmpty.SetError(txtEmail, "Email để trống hoặc không tồn tại");
                txtEmail.Select();
                return false;
            }

            errorEmpty.Clear();
            return true;
        }
        private bool Register(string hoTen, string tenTaiKhoan, string matKhau, string email, int gioiTinh, int maQuyen)
        {
            return AccountDAO.Instance.Register(hoTen, tenTaiKhoan, matKhau, email, gioiTinh, maQuyen);
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnChuyenDangNhap_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain frmmain = (frmMain)Application.OpenForms["frmMain"];
            frmDangNhap frmdangnhap = new frmDangNhap();
            frmdangnhap.MdiParent = frmmain;
            frmdangnhap.Show();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {

        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
        }

        private void ckcChapNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcChapNhan.Checked)
            {
                btnDangKy.Enabled = true;
                //btnDangKy.BackColor = Color.FromArgb(254, 97, 50);
                //btnDangKy.ForeColor = Color.White;
            }
            else
            {
                btnDangKy.Enabled = false;
                //btnDangKy.BackColor = Color.FromArgb(255, 228, 179);
                //btnDangKy.ForeColor = Color.White;
            }
        }


        private void btnChuyenDangNhap_MouseEnter(object sender, EventArgs e)
        {
            btnChuyenDangNhap.ForeColor = Color.FromArgb(255, 148, 77);

        }

        private void btnChuyenDangNhap_MouseLeave(object sender, EventArgs e)
        {
            btnChuyenDangNhap.ForeColor = Color.FromArgb(254, 97, 50);

        }

        private void frmDangKy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnDangKy.PerformClick(); 
            }
        }

        private void btnDangKy_Click_1(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string tenTaiKhoan = txtTenTaiKhoan.Text.Trim();
            string matKhau = txtMatKhauDk.Text.Trim();
            string nhapLaiMK = txtNhapLai.Text.Trim();
            string email = txtEmail.Text.Trim();
            int gioiTinh = 0; //nam = 0; nu = 1;
            int maQuyen = 2; // chỉ đăng ký cho nhân viên

            if (rbtnNu.Checked == true)
            {
                gioiTinh = 1;
            }

            if (checkDangKy(hoTen, tenTaiKhoan, matKhau, email, gioiTinh))
            {
                if (Register(hoTen, tenTaiKhoan, matKhau, email, gioiTinh, maQuyen))
                {
                    MessageBox.Show("Đăng ký thành công!!!");
                    this.Close();
                    frmMain frmmain = (frmMain)Application.OpenForms["frmMain"];
                    frmDangNhap frmdangnhap = new frmDangNhap();
                    frmdangnhap.MdiParent = frmmain;
                    frmdangnhap.Show();
                }
                else
                {
                    MessageBox.Show("Đăng ký thât bại...");
                }
            }
        }

        private void btnTroLai_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
