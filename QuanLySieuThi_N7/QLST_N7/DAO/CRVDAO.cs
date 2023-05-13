using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class CRVDAO
    {
        private static CRVDAO instance;
        public static CRVDAO Instance
        {
            get { if (instance == null) instance = new CRVDAO(); return CRVDAO.instance; }
            private set { CRVDAO.instance = value; }
        }

        public DataTable loadHoaDon(int maHoaDon)
        {
            string strQuery = "select ct.mahoadon, hh.TenHangHoa, ct.SoLuongBan, hh.GiaBan, ct.SoLuongBan*hh.GiaBan as ThanhTien from chitiethoadon as ct, HangHoa as hh where ct.MaHangHoa = hh.MaHangHoa and MaHoaDon = " + maHoaDon;
            return DataProvider.Instance.ExecuteQuery(strQuery);
        }
    }
}
