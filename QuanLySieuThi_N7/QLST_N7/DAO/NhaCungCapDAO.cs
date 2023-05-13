using QLST_N7.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST_N7.DAO
{
    public class NhaCungCapDAO
    {
        private static NhaCungCapDAO instance;
        private NHACUNGCAP ncc = null;
        public static NhaCungCapDAO Instance
        {
            get { if (instance == null) instance = new NhaCungCapDAO(); return NhaCungCapDAO.instance; }
            private set { NhaCungCapDAO.instance = value; }
        }


        public NHACUNGCAP insertNCC(string ten, string diachi, string sdt, string email)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_Insert_NhaCungCap @ten , @diachi , @sdt , @email ", new object[] { ten, diachi, sdt, email });
            int maNCC = (int)(DataProvider.Instance.ExecuteScalar("select  max(MaNhaCungCap) from nhacungcap"));
            return new NHACUNGCAP(maNCC, ten, diachi, sdt, email);
        }

        public List<NHACUNGCAP> loadNCC()
        {
            List<NHACUNGCAP> result = new List<NHACUNGCAP>();

            DataTable table = DataProvider.Instance.ExecuteQuery("select  * from nhacungcap");
            foreach (DataRow row in table.Rows)
            {
                result.Add(new NHACUNGCAP(row));
            }

            return result;
        }

        public NHACUNGCAP getNCCByID(int MaNhaCungCap)
        {
            return new NHACUNGCAP(((DataTable)DataProvider.Instance.ExecuteQuery("select * from nhacungcap where manhacungcap = " + MaNhaCungCap)).Rows[0]);
        }
    }
}
