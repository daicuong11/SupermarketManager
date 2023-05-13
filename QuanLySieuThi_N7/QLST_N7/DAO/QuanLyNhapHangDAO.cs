using QLST_N7.CRV;
using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLST_N7.DAO
{
    public class QuanLyNhapHangDAO
    {
        private static QuanLyNhapHangDAO instance;
        private List<HANGHOA> lstDN = new List<HANGHOA>();
        private DataTable dtDN = new DataTable();
        public static QuanLyNhapHangDAO Instance
        {
            get { if (instance == null) instance = new QuanLyNhapHangDAO(); return QuanLyNhapHangDAO.instance; }
            private set { QuanLyNhapHangDAO.instance = value; }
        }

        public int insertNhapHang(int MaNhanVien, DateTime NgayNhap)
        {
            return (int)DataProvider.Instance.ExecuteScalar("USP_Insert_NhapHang @a , @b ", new object[] { MaNhanVien, NgayNhap});
        }

        public int insertLoHang(DateTime NgayNhap, int MaHangHoa)
        {
            return (int)DataProvider.Instance.ExecuteScalar("USP_Insert_LoHang @a , @b ", new object[] { NgayNhap, MaHangHoa });
        }

        public void insertChiTietNhapHang(int MaNhapHang, int MaLoHang, int SoLuongNhap)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertOrUpdateChiTietNhapHang @a , @b , @c ", new object[] { MaNhapHang, MaLoHang, SoLuongNhap});
        }

        public DataTable loadDonNhap( int MaNhapHang)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_getCTNH @MaNhapHang ", new object[] { MaNhapHang });
        }

        public decimal sumTotal(int MaNhapHang)
        {
            decimal sum = 0;
            DataTable donnhap = loadDonNhap(MaNhapHang);
            foreach (DataRow row in donnhap.Rows)
            {
                sum += decimal.Parse(row["ThanhTien"].ToString());
            }
            return sum;
        }
        public int count(int MaNhapHang)
        {
            int sum = 0;
            DataTable donnhap = loadDonNhap(MaNhapHang);
            foreach (DataRow row in donnhap.Rows)
            {
                sum += int.Parse(row["SoLuongNhap"].ToString());
            }
            return sum;
        }

        public void deleteCTNH(int maNhapHang)
        {
            DataProvider.Instance.ExecuteNonQuery("delete from ChiTietNhapHang where MaNhapHang = @a ", new object[] { maNhapHang } );
        }

        //public void deleteARowCTNH(int MaNhapHang, int MaLoHang)
        //{
        //    DataProvider.Instance.ExecuteNonQuery("delete from ChiTietNhapHang where MaNhapHang = @a and MaLoHang = @b ", new object[] { MaNhapHang, MaLoHang });
        //    DataProvider.Instance.ExecuteNonQuery("delete from lohang where MaLoHang = @b ", new object[] { MaLoHang });
        //}

        public void deldeteNH(int MaNhapHang)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_Delete_NhapHang @a ", new object[] { MaNhapHang });
        }

        public void resetSoLuongHangHoaAll(int MaNhapHang, int soluongnhap, int MaLoHang)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_ResetHangHoa @c , @a , @b ", new object[] { MaNhapHang, soluongnhap, MaLoHang });
        }

        public void resetSoLuongHangHoaARow(int MaNhapHang, int soluongnhap, int MaLoHang)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_ResetARowHangHoa @c , @a , @b ", new object[] { MaNhapHang, soluongnhap, MaLoHang });
        }
        public int getSoLuongNhap(int MaNhapHang, int MaLohang)
        {
            object a = DataProvider.Instance.ExecuteScalar("select soluongnhap from chitietnhaphang where manhaphang = @a and malohang = @b ", new object[] { MaNhapHang, MaLohang });
            if (a == null) return 0;
            return (int)a;
        }

        public void resetListHangHoa(int MaNhapHang)
        {
            DataTable listCTNH = DataProvider.Instance.ExecuteQuery("select * from chitietnhaphang where MaNhapHang = @a order by (MaLoHang) desc ", new object[] {MaNhapHang});
            foreach (DataRow row in listCTNH.Rows)
            {
                CTNH newCTNH = new CTNH(row);
                int soluongnhap = getSoLuongNhap(MaNhapHang, newCTNH.MaLoHang);
                resetSoLuongHangHoaAll(MaNhapHang, soluongnhap, newCTNH.MaLoHang);
            }
        }

        public int getMaHangHoaByLoHang(int MaLoHang)
        {
            object a = DataProvider.Instance.ExecuteScalar("select MaHangHoa from LoHang where MaLohang = @a ", new object[] { MaLoHang });
            if (a == null) return -1;
            return (int)a;
        }
        public void resetHangHoa(int MaNhapHang, int MaLoHang)
        {
            int soluongnhap = getSoLuongNhap(MaNhapHang, MaLoHang);
            resetSoLuongHangHoaARow( MaNhapHang ,soluongnhap, MaLoHang);
        }

        public bool updateTongTienNhap(int MaNhapHang, decimal TongTien)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("Update NhapHang set TongTien = @TongTien where MaNhapHang = @ ", new object[] { TongTien, MaNhapHang });
            return result > 0;
        }

        public void updateHenNgayGiao(int MaNhapHang, DateTime time)
        {
            DataProvider.Instance.ExecuteNonQuery("Update nhaphang set ngaynhap = @a where MaNhapHang = @b ", new object[] { time, MaNhapHang });
        }
    }
}
