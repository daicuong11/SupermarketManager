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

namespace QLST_N7
{
    public partial class frmThemNCC : Form
    {
        public frmThemNCC()
        {
            InitializeComponent();
        }

        private void btnThemMoiNhaCungCap_Click(object sender, EventArgs e)
        {
            if (checkForm())
            {
                string ten = txtTen.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string email = txtEmail.Text.Trim();

                NHACUNGCAP newNCC = NhaCungCapDAO.Instance.insertNCC(ten, diaChi, sdt, email);
                frmNhapHang frmnhaphang = (frmNhapHang)Application.OpenForms["frmNhapHang"];
                frmnhaphang.setNCC(newNCC);
                this.Close();
            }
        }

        private bool checkForm()
        {
            if(String.IsNullOrEmpty(txtTen.Text.Trim()) || String.IsNullOrEmpty(txtDiaChi.Text.Trim()) || String.IsNullOrEmpty(txtSDT.Text.Trim()) || String.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                lblError.Text = "Vui lòng nhập đầy đủ thông tin";
                return false;
            }
            else
            {
                lblError.Text = "";
                return true;
            }
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThemNCC_Load(object sender, EventArgs e)
        {

        }

        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemMoiNhaCungCap.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
