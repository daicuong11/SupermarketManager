using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class QLDonNhapDAO
    {
        private static QLDonNhapDAO instance;
        public static QLDonNhapDAO Instance
        {
            get { if (instance == null) instance = new QLDonNhapDAO(); return QLDonNhapDAO.instance; }
            private set { QLDonNhapDAO.instance = value; }
        }

        public DataTable getDonNhapByDate(DateTime TuNgay, DateTime DenNgay)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_DSDN @TuNgay , @DenNgay", new object[] { TuNgay, DenNgay });
        }

        public int getCountDonNhap(DateTime TuNgay, DateTime DenNgay)
        {
            object result = DataProvider.Instance.ExecuteScalar("select count(*) from nhaphang where NgayNhap >= @TuNgay and NgayNhap <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if (result == null) return 0;
            return (int)result;
        }

        public decimal getVon(DateTime TuNgay, DateTime DenNgay)
        {
            if (!check(TuNgay, DenNgay)) return 0;

            object result = DataProvider.Instance.ExecuteScalar("select sum(ctnh.soluongnhap*hh.gianhap) from ChiTietNhapHang ctnh, hanghoa hh , nhaphang nh, lohang lh where ctnh.MaLoHang = lh.MaLoHang and ctnh.MaNhapHang = nh.MaNhapHang and lh.MaHangHoa = hh.MaHangHoa and nh.NgayNhap >= @TuNgay and nh.NgayNhap <= @DenNgay  ", new object[] { TuNgay, DenNgay });
            if (result != null)
            {
                return decimal.Parse(result.ToString());
            }
            return 0;
        }

        public decimal getGiaTriBan(DateTime TuNgay, DateTime DenNgay)
        {
            if (!check(TuNgay, DenNgay)) return 0;

            object result = DataProvider.Instance.ExecuteScalar("select sum(ctnh.soluongnhap*hh.GiaBan) from ChiTietNhapHang ctnh, hanghoa hh , nhaphang nh, lohang lh where ctnh.MaLoHang = lh.MaLoHang and ctnh.MaNhapHang = nh.MaNhapHang and lh.MaHangHoa = hh.MaHangHoa and nh.NgayNhap >= @TuNgay and nh.NgayNhap <= @DenNgay  ", new object[] { TuNgay, DenNgay });
            if (result != null)
            {
                return decimal.Parse(result.ToString());
            }
            return 0;
        }

        public int getSoLuongNhap()
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from Chitietnhaphang");
            if (count > 0)
            {
                object result = DataProvider.Instance.ExecuteScalar("select sum(soluongnhap) from ChiTietNhapHang");    
                return (int)result;
            }
            return 0;
        }

        public bool check(DateTime TuNgay, DateTime DenNgay)
        {
            int result = (int)DataProvider.Instance.ExecuteScalar("select count(*) from ChiTietNhapHang ctnh , nhaphang nh where ctnh.MaNhapHang = nh.MaNhapHang and nh.NgayNhap >= @TuNgay and nh.NgayNhap <= @DenNgay ", new object[] { TuNgay, DenNgay });
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
