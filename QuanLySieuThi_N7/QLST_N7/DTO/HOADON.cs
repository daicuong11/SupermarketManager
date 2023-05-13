using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class HOADON
    {
        int maHoaDon;
        int maKhachHang;
        int maNhanVien;
        DateTime ngayTao;
        float tongTien;

        public HOADON(int maHoaDon, int maKhachHang, int maNhanVien, DateTime ngayTao, float tongTien)
        {
            this.maHoaDon = maHoaDon;
            this.maKhachHang = maKhachHang;
            this.maNhanVien = maNhanVien;
            this.ngayTao = ngayTao;
            this.tongTien = tongTien;
        }

        public HOADON(DataRow row)
        {
            this.maHoaDon = (int)row["MaHoaDon"];
            this.maKhachHang = (int)row["MaKhachHang"];
            this.maNhanVien = (int)row["MaNhanVien"];
            this.ngayTao = (DateTime)row["NgayTao"];
            this.tongTien = float.Parse(row["TongTien"].ToString());
        }

        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public int MaKhachHang { get => maKhachHang; set => maKhachHang = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public float TongTien { get => tongTien; set => tongTien = value; }
    }
}
