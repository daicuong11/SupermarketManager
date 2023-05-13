using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class NHACUNGCAP
    {
        int maNhaCungCap;
        string tenNhaCungCap;
        string diaChi;
        string soDienThoai;
        string email;

        public NHACUNGCAP(DataRow row)
        {
            this.maNhaCungCap = (int)row["maNhaCungCap"];
            this.tenNhaCungCap = row["TenNhaCungCap"].ToString();
            this.diaChi = row["DiaChi"].ToString();
            this.soDienThoai = row["SoDienThoai"].ToString();
            this.email = row["Email"].ToString();
        }

        

        public NHACUNGCAP(int maNhaCungCap, string tenNhaCungCap, string diaChi, string soDienThoai, string email)
        {
            this.maNhaCungCap = maNhaCungCap;
            this.tenNhaCungCap = tenNhaCungCap;
            this.diaChi = diaChi;
            this.soDienThoai = soDienThoai;
            this.email = email;
        }

        public int MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public string TenNhaCungCap { get => tenNhaCungCap; set => tenNhaCungCap = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string Email { get => email; set => email = value; }
    }
}
