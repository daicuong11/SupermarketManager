using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class CTHDDAO
    {
        private static CTHDDAO instance;
        public static CTHDDAO Instance
        {
            get { if (instance == null) instance = new CTHDDAO(); return CTHDDAO.instance; }
            private set { CTHDDAO.instance = value; }
        }

        public void creatNewCTHD(int maHoaDon, int maHangHoa, int soLuongBan)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_Insert_CTHD @MaHoaDon , @MaHangHoa , @SoLuongBan ", new object[] { maHoaDon, maHangHoa, soLuongBan });
        }

        public void updateACTHD(int maHoaDon, int maHangHoa, int soLuongBan)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_Update_CTHD @MaHoaDon , @MaHangHoa , @SoLuongBan ", new object[] { maHoaDon, maHangHoa, soLuongBan });
        }

        public DataTable loadCTHD(int maHoaDon)
        {
            string strQuery = "select hh.TenHangHoa, ct.SoLuongBan, hh.GiaBan, ct.SoLuongBan*hh.GiaBan as ThanhTien from chitiethoadon as ct, HangHoa as hh where ct.MaHangHoa = hh.MaHangHoa and MaHoaDon = " + maHoaDon;
            return DataProvider.Instance.ExecuteQuery(strQuery);
        }

        public void deleteCTHD(int maHoaDon)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_Delete_CTHD " + maHoaDon);
        }
        
        public void deleteARowCTHD(int MaHoaDon, int MaHangHoa)
        {
            DataProvider.Instance.ExecuteNonQuery("delete from chitiethoadon where MaHoaDon = @a and MaHangHoa = @b ", new object[] { MaHoaDon, MaHangHoa });
        }

        public decimal getTotal(int MaHoaDon)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from chitiethoadon as ct, HangHoa as hh where ct.MaHangHoa = hh.MaHangHoa and MaHoaDon = @a ", new object[] { MaHoaDon });
            if(count <= 0)
            {
                return 0;
            }
            string strQuery = "select sum(ct.SoLuongBan*hh.GiaBan) as ThanhTien from chitiethoadon as ct, HangHoa as hh where ct.MaHangHoa = hh.MaHangHoa and MaHoaDon = " + MaHoaDon;
            object result = DataProvider.Instance.ExecuteScalar(strQuery);
            return (decimal) result;
        }
    }
}
