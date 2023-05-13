using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class LOHANG
    {
        int maLoHang;
        DateTime ngayNhap;
        int maHangHoa;

        public LOHANG(int maLoHang, DateTime ngayNhap, int maHangHoa)
        {
            this.maLoHang = maLoHang;
            this.ngayNhap = ngayNhap;
            this.maHangHoa = maHangHoa;
        }
        public LOHANG(DataRow row)
        {
            this.maLoHang = (int)row["MaLoHang"];
            this.ngayNhap = (DateTime)row["NgayNhap"];
            this.maHangHoa = (int)row["MaHangHoa"];
        }

        public int MaLoHang { get => maLoHang; set => maLoHang = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
        public int MaHangHoa { get => maHangHoa; set => maHangHoa = value; }
    }
}
