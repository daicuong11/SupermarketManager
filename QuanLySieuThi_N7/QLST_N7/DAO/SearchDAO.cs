using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class SearchDAO
    {
        private static SearchDAO instance;
        public static SearchDAO Instance
        {
            get { if (instance == null) instance = new SearchDAO(); return SearchDAO.instance; }
            private set { SearchDAO.instance = value; }
        }

        public DataTable searchHangHoaByName(String name)
        {
            string strQuery = "select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán' from hanghoa h where  TenHangHoa like N'%" + name + "%'";
            return DataProvider.Instance.ExecuteQuery(strQuery);
        }


        public List<NHACUNGCAP> searchNCCByName(String name)
        {
            List<NHACUNGCAP> result = new List<NHACUNGCAP>();
            string strQuery = "select * from nhacungcap where TenNhaCungCap like N'%" + name + "%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(strQuery);
            if(dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new NHACUNGCAP(row));
                }
                return result;  
            }
            return null;
        }

        public DataTable searchNhanVienByName(string name)
        {
            return DataProvider.Instance.ExecuteQuery("select Manhanvien, HoTen, GioiTinh, TenTaiKhoan, DiaChi, SoDienThoai, Email, q.TenQuyen \r\nfrom nhanvien nv, Quyen q where nv.MaQuyen = q.MaQuyen\r\nand HoTen like N'%"  + name + "%'");
        }
    }
}
