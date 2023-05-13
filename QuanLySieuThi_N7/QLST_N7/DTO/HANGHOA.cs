using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST_N7.DTO
{
    public class HANGHOA
    {
        int maHangHoa;
        string tenHangHoa;
        float giaNhap;
        float giaBan;
        int soLuong;
        int maNhaCungCap;
        int maKho;

        public HANGHOA()
        {
        }
        public HANGHOA(int maHangHoa, int soLuong)
        {
            this.maHangHoa = maHangHoa;
            this.soLuong = soLuong;
        }

        public HANGHOA(string tenHangHoa, float giaBan, int soLuong)
        {
            this.tenHangHoa = tenHangHoa;
            this.giaBan = giaBan;
            this.soLuong = soLuong;
        }

        public HANGHOA(DataGridViewRow row)
        {
            this.maHangHoa = (int)row.Cells[0].Value;
            this.tenHangHoa = row.Cells[1].Value.ToString();
            this.soLuong = (int)row.Cells[2].Value;
            this.giaBan = float.Parse(row.Cells[3].Value.ToString());
        }

        public HANGHOA(int maHangHoa, string tenHangHoa, float giaNhap, float giaBan, int soLuong, int maNhaCungCap, int maKho)
        {
            this.maHangHoa = maHangHoa;
            this.tenHangHoa = tenHangHoa;
            this.giaNhap = giaNhap;
            this.giaBan = giaBan;
            this.soLuong = soLuong;
            this.maNhaCungCap = maNhaCungCap;
            this.maKho = maKho;
        }

        public HANGHOA(DataRow row)
        {
            this.maHangHoa = (int)row["MaHangHoa"];
            this.tenHangHoa = row["TenHangHoa"].ToString();
            this.giaNhap = float.Parse(row["GiaNhap"].ToString());
            this.giaBan = float.Parse(row["GiaBan"].ToString());
            this.soLuong = (int)row["SoLuong"];
            this.maNhaCungCap = (int)row["MaNhaCungCap"];
            this.maKho = (int)row["MaKho"];
        }

        public int MaHangHoa { get => maHangHoa; set => maHangHoa = value; }
        public string TenHangHoa { get => tenHangHoa; set => tenHangHoa = value; }
        public float GiaNhap { get => giaNhap; set => giaNhap = value; }
        public float GiaBan { get => giaBan; set => giaBan = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public int MaKho { get => maKho; set => maKho = value; }
    }
}
