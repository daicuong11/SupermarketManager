using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7.DAO
{
    public class KhoDAO
    {
        private static KhoDAO instance;
        public static KhoDAO Instance
        {
            get { if (instance == null) instance = new KhoDAO(); return KhoDAO.instance; }
            private set { KhoDAO.instance = value; }
        }

        public void insertKho(string TenKho, string DiaChi)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_insertKho @a , @b " , new object[] { TenKho, DiaChi});
        }

        public DataTable loadAllKho()
        {
            DataTable a = DataProvider.Instance.ExecuteQuery("select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán', ncc.TenNhaCungCap as N'Nhà cung cấp' from hanghoa hh, NhaCungCap ncc, kho k where hh.MaNhaCungCap = ncc.MaNhaCungCap and hh.MaKho = k.MaKho");
            if (a != null)
            {
                return a;
            }
            return null;
        }

        public DataTable loadKho1()
        {
            DataTable a = DataProvider.Instance.ExecuteQuery("select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán', ncc.TenNhaCungCap as N'Nhà cung cấp' from hanghoa hh, NhaCungCap ncc, kho k where hh.MaNhaCungCap = ncc.MaNhaCungCap and hh.MaKho = k.MaKho and k.MaKho = 1");
            if(a != null)
            {
                return a;
            }
            return null;
        }

        public DataTable loadKho2()
        {
            DataTable a = DataProvider.Instance.ExecuteQuery("select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán', ncc.TenNhaCungCap as N'Nhà cung cấp' from hanghoa hh, NhaCungCap ncc, kho k where hh.MaNhaCungCap = ncc.MaNhaCungCap and hh.MaKho = k.MaKho and k.MaKho = 2");
            if (a != null)
            {
                return a;
            }
            return null;
        }

        public DataTable loadKho3()
        {
            DataTable a = DataProvider.Instance.ExecuteQuery("select mahanghoa as N'Mã sản phẩm', tenhanghoa as N'Tên sản phẩm', soluong as N'Số lượng', gianhap as N'Giá nhập', giaban as N'Giá bán', ncc.TenNhaCungCap as N'Nhà cung cấp' from hanghoa hh, NhaCungCap ncc, kho k where hh.MaNhaCungCap = ncc.MaNhaCungCap and hh.MaKho = k.MaKho and k.MaKho = 3");
            if (a != null)
            {
                return a;
            }
            return null;
        }

        public int getCount(int makho)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from hanghoa where makho = " + makho);
            if (count <= 0) return 0;
            return (int)DataProvider.Instance.ExecuteScalar("select sum(soluong) from hanghoa where makho = " + makho);
        }

        public decimal getPrice(int makho)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from hanghoa where makho = " + makho);
            if (count <= 0) return 0;
            return (decimal)DataProvider.Instance.ExecuteScalar("select sum(soluong*GiaBan) from hanghoa where makho = " + makho);
        }

        public int getCount()
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from hanghoa");
            if (count > 0)
            {
                object a = DataProvider.Instance.ExecuteScalar("select sum(soluong) from hanghoa");
                return (int)a;
            }
            return 0;
        }

        public decimal getPrice()
        {
            int count = (int)DataProvider.Instance.ExecuteScalar("select count(*) from hanghoa");
            if (count <= 0) return 0;
            return (decimal)DataProvider.Instance.ExecuteScalar("select sum(soluong*GiaBan) from hanghoa");
        }

        public int getCount(DataTable a)
        {
            int sum = 0;
            if(a.Rows.Count > 0)
            {
                foreach (DataRow row in a.Rows)
                {
                    sum += (int)row[2];
                }
            }
            return sum;
        }

        public decimal getPrice(DataTable a)
        {
            decimal sum = 0;
            if (a.Rows.Count > 0)
            {
                foreach (DataRow row in a.Rows)
                {
                    int soluong = (int)row[2];
                    decimal giaban = decimal.Parse(row[3].ToString());
                    sum += (soluong*giaban);
                }
            }
            return sum;
        }
    }
}
