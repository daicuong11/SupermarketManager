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
using QLST_N7.DAO;

namespace QLST_N7
{
    public partial class frmDangNhap : Form
    {
        frmMain frm = (frmMain)Application.OpenForms["frmMain"];
        public frmDangNhap()
        {
            InitializeComponent();

            this.MdiParent = frm;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "daicuong";
            txtMatKhau.Text = "1";
        }

        private bool checkLogIn()
        {
            String tenDangNhap = txtTenDangNhap.Text.Trim();
            String matKhau = txtMatKhau.Text.Trim();
            if (String.IsNullOrEmpty(tenDangNhap))
            {
                errorFormDangNhap.SetError(txtTenDangNhap, "Vui lòng nhập tên đăng nhập");
                txtTenDangNhap.Select();
                return false;
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                errorFormDangNhap.Clear();
                errorFormDangNhap.SetError(txtMatKhau, "Vui lòng nhập mật khẩu");
                txtMatKhau.Select();
                return false;

            }
            errorFormDangNhap.Clear();
            return true;
        }

        bool Login(string tentaikhoan, string matkhau)
        {
            return AccountDAO.Instance.Login(tentaikhoan, matkhau);
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangKy frmdangky = new frmDangKy();
            frmdangky.Show();
        }

        private void btnChuyenDangKy_MouseEnter(object sender, EventArgs e)
        {
            btnChuyenDangKy.ForeColor = Color.FromArgb(255, 148, 77);
        }

        private void btnChuyenDangKy_MouseLeave(object sender, EventArgs e)
        {
            btnChuyenDangKy.ForeColor = Color.FromArgb(254, 97, 50);
        }

        private void lblQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmDoiMatKhau frmquyenmatkhau = new frmDoiMatKhau();
            frmquyenmatkhau.ShowDialog();
            this.Show();
        }

        private void lblCanTroGiup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmCanTroGiup frmcantrogiup = new frmCanTroGiup();
            frmcantrogiup.ShowDialog();
            this.Show();
        }

        private void frmDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnDangNhap.PerformClick(); 
            }
        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            if (checkLogIn())
            {
                string tentaikhoan = txtTenDangNhap.Text.Trim();
                string matkhau = txtMatKhau.Text.Trim();

                if (Login(tentaikhoan, matkhau))
                {
                    this.frm.load_Name();
                    if (AccountDAO.Instance.Nv.MaQuyen == 1)
                    {
                        this.Hide();
                        frmBaoCaoBanHang frmbaocao = new frmBaoCaoBanHang();
                        frmbaocao.Show();
                        this.ParentForm.Hide();
                    }
                    else
                    {
                        this.Hide();
                        frmBanHang frmbanhang = new frmBanHang();
                        frmbanhang.Show();
                        this.ParentForm.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Text = "";
                    txtMatKhau.Text = "";
                    txtTenDangNhap.Select();
                }
            }
        }

        private void btnTroLai_Click_1(object sender, EventArgs e)
        {
            this.Hide();

        }
    }
}
