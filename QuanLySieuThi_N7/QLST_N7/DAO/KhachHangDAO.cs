using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;
        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return KhachHangDAO.instance; }
            private set { KhachHangDAO.instance = value; }
        }

        public int insertKhachHang(string HoTen, String SoDienThoai)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_Insert_KhachHang @a , @b ", new object[] { HoTen, SoDienThoai });
        }
    }
}
