using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7.DAO
{
    public class HoaDonDAO
    {
        private static HoaDonDAO instance;
        public static HoaDonDAO Instance
        {
            get { if (instance == null) instance = new HoaDonDAO(); return HoaDonDAO.instance; }
            private set { HoaDonDAO.instance = value; }
        }

        public int createNewHoaDon(int maKhachHang, int maNhanVien, decimal TongTien)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_Insert_HoaDon @MaKhachHang , @MaNhanVien , @NgayTao , @TongTien ", new object[] { maKhachHang, maNhanVien, DateTime.Now, TongTien});
            int maHoaDon = (int)DataProvider.Instance.ExecuteScalar("select Max(MaHoaDon) from hoadon");
            return maHoaDon;
        }

        public void updateKhachHang(int MaHoaDon, int MaKhachHang)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_Update_KhachHang_In_HoaDon @a , @b ", new object[] { MaHoaDon, MaKhachHang });
        }

        public void deleteHoaDon(int mahoadon)
        {
            DataProvider.Instance.ExecuteNonQuery(" exec USP_Delete_HoaDon @mahoadon " , new object[] { mahoadon });
        }

        public void updateTongTien(int MaHoaDon, decimal TongTien)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_UpdateTongTienHoaDon @MaHoaDon , @TongTien ", new object[] { MaHoaDon, TongTien});
        }

        public DataTable getHoaDonByDate(DateTime TuNgay, DateTime DenNgay)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_DSHD @TuNgay , @DenNgay", new object[] {TuNgay, DenNgay});
        }

        public decimal getDoanhThu(DateTime TuNgay, DateTime DenNgay)
        {
            if (!check(TuNgay, DenNgay)) return 0;
            object result = DataProvider.Instance.ExecuteScalar("select sum(tongtien) from hoadon where NgayTao >= @TuNgay and NgayTao <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if (result != null)
            {
                return decimal.Parse(result.ToString()); 
            }
            return 0;
        }

        public decimal getVon(DateTime TuNgay, DateTime DenNgay)
        {
            if (!check(TuNgay, DenNgay)) return 0;

            object result = DataProvider.Instance.ExecuteScalar("select sum(cthd.soluongban*hh.gianhap) from ChiTietHoaDon cthd, hanghoa hh , hoadon hd where cthd.MaHangHoa = hh.MaHangHoa and cthd.MaHoaDon = hd.MaHoaDon and hd.NgayTao >= @TuNgay and hd.NgayTao <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if (result != null)
            {
                return decimal.Parse(result.ToString());
            }
            return 0;
        }

        public decimal getTienLai(DateTime TuNgay, DateTime DenNgay)
        {
            return getDoanhThu(TuNgay, DenNgay) - getVon(TuNgay, DenNgay);
        }

        public int getCountHoaDon(DateTime TuNgay, DateTime DenNgay)
        {
            object result = DataProvider.Instance.ExecuteScalar("select count(*) from hoadon where NgayTao >= @TuNgay and NgayTao <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if (result == null) return 0;
            return (int)result;
        }

        public bool check(DateTime TuNgay, DateTime DenNgay)
        {
            int result = (int)DataProvider.Instance.ExecuteScalar("select count(*) from ChiTietHoaDon cthd , hoadon hd where cthd.MaHoaDon = hd.MaHoaDon and hd.NgayTao >= @TuNgay and hd.NgayTao <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if(result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
