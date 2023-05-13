using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7.DAO
{
    public class ProductsDAO
    {
        private static ProductsDAO instance;
        
        public static ProductsDAO Instance
        {
            get { if (instance == null) instance = new ProductsDAO(); return ProductsDAO.instance; }
            private set { ProductsDAO.instance = value; }
        }


        private ProductsDAO() { }

        public int getSoluongByID(int MaHangHoa)
        {
            string strQuery = "select SoLuong from HangHoa where MaHangHoa = " + MaHangHoa;
            return (int)DataProvider.Instance.ExecuteScalar(strQuery);
        }

        public bool load_DSSP(DataGridView dtvDSSP)
        {
            string strQuery = "select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', giaban as N'Giá bán' from hanghoa";

            dtvDSSP.DataSource = DataProvider.Instance.ExecuteQuery(strQuery);
            dtvDSSP.Refresh();
            return true;
        }

        public bool load_DSSP_to_NhapHang(DataGridView dtvDSSP)
        {
            string strQuery = "select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán' from hanghoa";

            dtvDSSP.DataSource = DataProvider.Instance.ExecuteQuery(strQuery);
            dtvDSSP.Refresh();
            return true;
        }

        public void updateSoLuongTonKho(int MaHoaDon)
        {
            List<HANGHOA> lstHangDaBan = new List<HANGHOA>();

            DataTable HangTonKHo = DataProvider.Instance.ExecuteQuery("select cthd.MaHangHoa, (hh.SoLuong - cthd.SoLuongBan) as TonKho from chitiethoadon as cthd, hanghoa as hh where cthd.MaHangHoa = hh.MaHangHoa and cthd.mahoadon = " + MaHoaDon);

            if(HangTonKHo.Rows.Count > 0)
            {
                foreach (DataRow item in HangTonKHo.Rows)
                {
                    HANGHOA hh = new HANGHOA((int)item[0], (int)item[1]);
                    DataProvider.Instance.ExecuteNonQuery("USP_Update_SoHangTonKho @a , @b " , new object[] { hh.MaHangHoa, hh.SoLuong});
                }
            }
        }

        public int insertHangHoa(string TenHangHoa, decimal GiaNhap, decimal GiaBan, int SoLuong, int MaNhaCungCap, int MaKho = 1)
        {
            string strQuery = "EXEC dbo.USP_insertHangHoa @TenHangHoa , @GiaNhap , @GiaBan , @SoLuong , @MaNhaCungCap , @MaKho ";
            return (int)DataProvider.Instance.ExecuteScalar(strQuery, new object[] { TenHangHoa, GiaNhap, GiaBan, SoLuong, MaNhaCungCap, MaKho });
        }
    }
}
