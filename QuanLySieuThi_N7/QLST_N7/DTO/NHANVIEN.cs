using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class NHANVIEN
    {
        int manhanvien;
        String tentaikhoan;
        String matkhau;
        String hoten;
        int gioiTinh;
        String diachi;
        String sodienthoai;
        string email;
        int maQuyen;

        public NHANVIEN()
        {
        }

        public NHANVIEN(int manhanvien, string tentaikhoan, string hoten, string email, int maQuyen)
        {
            this.manhanvien = manhanvien;
            this.tentaikhoan = tentaikhoan;
            this.hoten = hoten;
            this.email = email;
            this.maQuyen = maQuyen;
        }

        public NHANVIEN(string tentaikhoan, string hoten, string email, int maQuyen)
        {
            this.tentaikhoan = tentaikhoan;
            this.hoten = hoten;
            this.email = email;
            this.maQuyen = maQuyen;
        }

        public int Manhanvien { get => manhanvien; set => manhanvien = value; }
        public string Tentaikhoan { get => tentaikhoan; set => tentaikhoan = value; }
        public string Matkhau { get => matkhau; set => matkhau = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public int GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Sodienthoai { get => sodienthoai; set => sodienthoai = value; }
        public string Email { get => email; set => email = value; }
        public int MaQuyen { get => maQuyen; set => maQuyen = value; }
    }
}
