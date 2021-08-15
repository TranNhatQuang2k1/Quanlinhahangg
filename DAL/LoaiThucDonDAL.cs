using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.DAL
{
    class LoaiThucDonDAL
    {
        public static List<LoaiThucDonDTO> LayDSLoaiThucDon()
        {
            List<LoaiThucDonDTO> _ds = new List<LoaiThucDonDTO>();
            string sql = "select * from LoaiThucDon";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LoaiThucDonDTO loai = new LoaiThucDonDTO();
                loai.MaLoai = int.Parse(dt.Rows[i]["MaLoai"].ToString());
                loai.TenLoai = dt.Rows[i]["TenLoai"].ToString();
                _ds.Add(loai);
            }
            return _ds;
        }

        public static int LayMaLoaiTuTenLoai(string tenLoai)
        {
            int maLoai;
            string sql = string.Format("select MaLoai from LoaiThucDon where TenLoai = N'{0}'", tenLoai);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
                maLoai = int.Parse(dt.Rows[0]["MaLoai"].ToString());
            else
                return 0;
            return maLoai;
        }
    }
}
