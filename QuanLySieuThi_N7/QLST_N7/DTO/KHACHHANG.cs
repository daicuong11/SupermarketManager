using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class KHACHHANG
    {
        int maKhachHang;
        string hoTen;
        string soDienThoai;

        public KHACHHANG(int maKhachHang, string hoTen, string diaChi, string soDienThoai, string email)
        {
            this.maKhachHang = maKhachHang;
            this.hoTen = hoTen;
            this.soDienThoai = soDienThoai;
        }

        public KHACHHANG(DataRow row)
        {
            this.maKhachHang = (int)row["MaKhachHang"];
            this.hoTen = row["HoTen"].ToString();
            this.soDienThoai = row["SoDienThoai"].ToString();
        }

        public int MaKhachHang { get => maKhachHang; set => maKhachHang = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
    }
}
