using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class KHO
    {
        int maKho;
        string tenKho;
        string diaChi;

        public KHO()
        {
        }

        public KHO(int maKho, string tenKho, string diaChi)
        {
            this.maKho = maKho;
            this.tenKho = tenKho;
            this.diaChi = diaChi;
        }

        public int MaKho { get => maKho; set => maKho = value; }
        public string TenKho { get => tenKho; set => tenKho = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
