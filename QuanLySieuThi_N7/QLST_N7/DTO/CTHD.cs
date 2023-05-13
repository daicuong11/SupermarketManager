using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class CTHD
    {
        int maHoaDon;
        int maHangHoa;
        int soLuongBan;

        public CTHD(int maHoaDon, int maHangHoa, int soLuongBan)
        {
            this.maHoaDon = maHoaDon;
            this.maHangHoa = maHangHoa;
            this.soLuongBan = soLuongBan;
        }

        public CTHD(int maHangHoa, int soLuongBan)
        {
            this.maHangHoa = maHangHoa;
            this.soLuongBan = soLuongBan;
        }

        public CTHD(DataRow row)
        {
            this.maHoaDon = (int)row["MaHoaDon"];
            this.maHangHoa = (int)row["MaHangHoa"];
            this.soLuongBan = (int)row["SoLuongBan"];
        }

        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public int MaHangHoa { get => maHangHoa; set => maHangHoa = value; }
        public int SoLuongBan { get => soLuongBan; set => soLuongBan = value; }
    }
}
