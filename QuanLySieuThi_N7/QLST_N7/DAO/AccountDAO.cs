using Bunifu.UI.WinForms.BunifuButton;
using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLST_N7.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private NHANVIEN nv = null;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }

        public NHANVIEN Nv { get => nv; set => nv = value; }

        private AccountDAO() { }

        public bool Login(string tentaikhoan, string matkhau)
        {
            String strQuery = "exec USP_Login @tentaikhoan , @matkhau";

            DataTable resulst = DataProvider.Instance.ExecuteQuery(strQuery, new object[]{ tentaikhoan, matkhau });
            if(resulst.Rows.Count > 0)
            {
                DataRow row = resulst.Rows[0];
                this.nv = new NHANVIEN(int.Parse(row[0].ToString()),row[1].ToString(), row[2].ToString(), row[3].ToString(), int.Parse(row[4].ToString()));
                return true;
            }
            return false;
        }

        public bool Register(string hoTen, string tenTaiKhoan, string matKhau, string email, int gioiTinh, int maQuyen = 2)
        {
            string strQuery = "exec USP_Register @tentaikhoan , @matkhau , @hoten , @gioitinh , @email , @maquyen ";
            int resulst = DataProvider.Instance.ExecuteNonQuery(strQuery, new object[] { tenTaiKhoan, matKhau, hoTen, gioiTinh, email, maQuyen });
            return resulst > 0;
        }

        public int isExistUser(string TenTaiKhoan, string MatKhau)
        {
            object result = DataProvider.Instance.ExecuteScalar("SELECT MaNhanVien FROM NHANVIEN nv, QUYEN q WHERE nv.MaQuyen = q.MaQuyen and TenTaiKhoan COLLATE Latin1_General_CS_AS = N'"+ TenTaiKhoan +"' COLLATE Latin1_General_CS_AS AND MatKhau COLLATE Latin1_General_CS_AS = N'"+ MatKhau+"' COLLATE Latin1_General_CS_AS; ");
            return (result != null) ? (int)result : -1;
        }
        public void enable(BunifuButton2 btnQuanLy, BunifuButton2 btnNhanVien, BunifuButton2 btnDangNhap, BunifuButton2 btnDangXuat)
        {
            if (AccountDAO.Instance.Nv != null)
            {
                btnDangNhap.Enabled = false;
                btnDangXuat.Enabled = true;
                if (AccountDAO.Instance.Nv.MaQuyen == 1)
                {
                    btnQuanLy.Enabled = true;
                    btnNhanVien.Enabled = true;
                }
                else if (AccountDAO.Instance.Nv.MaQuyen == 2)
                {
                    btnNhanVien.Enabled = true;
                }
            }
            else
            {
                btnDangXuat.Enabled = false;
                btnDangNhap.Enabled = true;
                btnQuanLy.Enabled = false;
                btnNhanVien.Enabled = false;
            }
        }

        public void updatePW(int MaNhanVien, string newPassWord)
        {
            string strQuery = "update NhanVien set MatKhau = N'" + newPassWord + "' where MaNhanVien = " + MaNhanVien;
            DataProvider.Instance.ExecuteNonQuery(strQuery);
        }

        public string loadUser()
        {
            string username = "Hãy đăng nhập!";
            if (AccountDAO.Instance.Nv != null)
            {
                string quyenhan = "";
                if (AccountDAO.Instance.Nv.MaQuyen == 1)
                {
                    quyenhan = "Quản lý)";
                }
                else if (AccountDAO.Instance.Nv.MaQuyen == 2) { quyenhan = "Nhân viên)"; }
                username = AccountDAO.Instance.Nv.Hoten + " (" + quyenhan;

                return username;
            }
            return username;
        }
    }
}
