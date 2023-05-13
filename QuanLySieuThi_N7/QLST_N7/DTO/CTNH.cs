using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class CTNH
    {
        int maNhapHang;
        int maLoHang;
        int soLuongNhap;

        public CTNH(int maNhapHang, int maLoHang, int soLuongNhap)
        {
            this.maNhapHang = maNhapHang;
            this.maLoHang = maLoHang;
            this.soLuongNhap = soLuongNhap;
        }
        public CTNH(DataRow row)
        {
            this.maNhapHang = (int)row["MaNhapHang"];
            this.maLoHang = (int)row["MaLoHang"];
            this.soLuongNhap = (int)row["SoLuongNhap"];
        }

        public int MaNhapHang { get => maNhapHang; set => maNhapHang = value; }
        public int MaLoHang { get => maLoHang; set => maLoHang = value; }
        public int SoLuongNhap { get => soLuongNhap; set => soLuongNhap = value; }
    }
}
