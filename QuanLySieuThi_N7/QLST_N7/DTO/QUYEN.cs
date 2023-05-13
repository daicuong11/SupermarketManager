using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DTO
{
    public class QUYEN
    {
        int maQuyen;
        String tenQuyen;

        public QUYEN()
        {
        }

        public QUYEN(int maQuyen, string tenQuyen)
        {
            this.maQuyen = maQuyen;
            this.tenQuyen = tenQuyen;
        }

        public QUYEN(DataRow row)
        {
            this.maQuyen = (int)row["MaQuyen"];
            this.tenQuyen = row["TenQuyen"].ToString();
        }

        public int MaQuyen { get => maQuyen; set => maQuyen = value; }
        public string TenQuyen { get => tenQuyen; set => tenQuyen = value; }
    }
}
