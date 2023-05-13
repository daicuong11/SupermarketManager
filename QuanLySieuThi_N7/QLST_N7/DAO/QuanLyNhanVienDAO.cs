using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLST_N7.DAO
{
    public class QuanLyNhanVienDAO
    {
        private static QuanLyNhanVienDAO instance;
        public static QuanLyNhanVienDAO Instance
        {
            get { if (instance == null) instance = new QuanLyNhanVienDAO(); return QuanLyNhanVienDAO.instance; }
            private set { QuanLyNhanVienDAO.instance = value; }
        }

        public DataTable getListNhanVien()
        {
            return DataProvider.Instance.ExecuteQuery("select Manhanvien, HoTen, GioiTinh, TenTaiKhoan, DiaChi, SoDienThoai, Email, q.TenQuyen from nhanvien nv, Quyen q where nv.MaQuyen = q.MaQuyen");
        }

        public List<QUYEN> loadQuyen()
        {
            List<QUYEN> lstQuyen = new List<QUYEN>();
            DataTable dtq = DataProvider.Instance.ExecuteQuery("select * from quyen");
            foreach (DataRow row in dtq.Rows)
            {
                QUYEN newQ = new QUYEN(row);
                lstQuyen.Add(newQ);
            }
            return lstQuyen;
        }

        public bool AddNhanVien(string TenTaiKhoan, string HoTen, int GioiTinh, string DiaChi, string SoDienThoai, string Email, int MaQuyen )
        {
            string strQuery = string.Format("INSERT INTO NHANVIEN (TenTaiKhoan, MatKhau, HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen) VALUES ( N'{0}', N'{1}', N'{2}', {3}, N'{4}', N'{5}', '{6}', {7} )", TenTaiKhoan, "1", HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen);
            int result = DataProvider.Instance.ExecuteNonQuery(strQuery);
            return result > 0;
        }

        public bool EditNhanVien(int MaNhanVien, string HoTen, int GioiTinh, string DiaChi, string SoDienThoai, string Email, int MaQuyen)
        {
            string strQuery = string.Format("Update NhanVien set HoTen = N'{0}' , GioiTinh = N'{1}' , DiaChi = N'{2}' , SoDienThoai = N'{3}' , Email = N'{4}' , MaQuyen = {5} where MaNhanVien = N'{6}'", HoTen, GioiTinh, DiaChi, SoDienThoai, Email, MaQuyen, MaNhanVien);
            int result = DataProvider.Instance.ExecuteNonQuery(strQuery);
            return result > 0;
        }

        public bool DeleteNhanVien(int MaNhanVien)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_Delete_NhanVien @a " , new object[] { MaNhanVien });
            return result > 0;
        }

        public bool ResetPassword(int MaNhanVien)
        {
            string strQuery = string.Format("Update nhanvien set MatKhau = '1' where MaNhanVien = " + MaNhanVien);
            int result = DataProvider.Instance.ExecuteNonQuery(strQuery);
            return result > 0;
        }

        public bool TenTaiKhoanIsExist(string tentaikhoan)
        {
            int result = (int)DataProvider.Instance.ExecuteScalar("select count(*) from nhanvien where tentaikhoan = @a " , new object[] { tentaikhoan});
            return result > 0;
        }

        public bool NhanVienDaNhapHang(int MaNhanVien)
        {
            return false;
        }

        public bool NhanVienDaLapHoaDon(int MaNhanVien)
        {
            return false;
        }

        //check nhân viên đã tạo hóa đơn hay đơn nhập hay chưa

        public bool isInsertedHoaDon(int MaNhanVien)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from hoadon where MaNhanVien = @a ", new object[] { MaNhanVien });
            return count > 0;
        }

        public bool isInsertedNhapHang(int MaNhanVien)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from nhaphang where MaNhanVien = @a ", new object[] { MaNhanVien });
            return count > 0;
        }

        //Update hoadons có mã nhân viên đã xóa thành nhân viên có mã nhân viên = 1 (Nhân viên đã xóa)

        public bool updateHoaDonWhenDelete(int MaNhanVien)
        {
            int count = DataProvider.Instance.ExecuteNonQuery("update hoadon set MaNhanVien = 1 where MaNhanVien = @a ", new object[] { MaNhanVien});
            return count > 0;
        }

        public bool updateNhapHangWhenDelete(int MaNhanVien)
        {
            int count = DataProvider.Instance.ExecuteNonQuery("update NhapHang set MaNhanVien = 1 where MaNhanVien = @a ", new object[] { MaNhanVien });
            return count > 0;
        }

        //public bool updateAllWhenDelete(int MaNhanVien)
        //{
        //    if (updateHoaDonWhenDelete(MaNhanVien))
        //    {
        //        return updateNhapHangWhenDelete(MaNhanVien);
        //    }
        //    return false;

        //}
    }
}
