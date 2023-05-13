using QLST_N7.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemMoiNhaCungCap_Click(object sender, EventArgs e)
        {
            if (checkForm())
            {
                string tentaikhoan = txtTenTaiKhoan.Text.Trim();
                string matkhaucu = txtMatKhauCu.Text.Trim();
                string matkhaumoi = txtMatKhauMoi.Text.Trim();
                int manhanvien = AccountDAO.Instance.isExistUser(tentaikhoan, matkhaucu);
                if (manhanvien > 0)
                {
                    AccountDAO.Instance.updatePW(manhanvien, matkhaumoi);
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                    txtTenTaiKhoan.Clear();
                    txtMatKhauCu.Clear();
                    txtMatKhauMoi.Clear();
                    txtXacNhanLai.Clear();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản không tồn tại", "Thông báo");
                }
            }
        }

        private bool checkForm()
        {
            if (String.IsNullOrEmpty(txtTenTaiKhoan.Text.Trim()) || String.IsNullOrEmpty(txtMatKhauCu.Text.Trim()) || String.IsNullOrEmpty(txtMatKhauMoi.Text.Trim()) || String.IsNullOrEmpty(txtXacNhanLai.Text.Trim()) )
            {
                lblError.Text = "Vui lòng nhập đầy đủ thông tin";
                return false;
            }
            if (!(txtMatKhauMoi.Text.Trim().Equals(txtXacNhanLai.Text.Trim()))){
                lblError.Text = "Mật khẩu xác nhận không đúng";
                return false;
            }
            else
            {
                lblError.Text = "";
                return true;
            }
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
