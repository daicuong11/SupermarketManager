using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class NHAPHANG
    {
        int maNhapHang;
        int maNhanVien;
        DateTime ngayNhap;

        public NHAPHANG(DataRow row)
        {
            this.maNhapHang = (int)row["MaNhapHang"];
            this.maNhanVien = (int)row["MaNhanVien"];
            this.ngayNhap = (DateTime)row["NgayNhap"];
        }
        public NHAPHANG(int maNhapHang, int maNhanVien, DateTime ngayNhap)
        {
            this.maNhapHang = maNhapHang;
            this.maNhanVien = maNhanVien;
            this.ngayNhap = ngayNhap;
        }

        public int MaNhapHang { get => maNhapHang; set => maNhapHang = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
    }
}
