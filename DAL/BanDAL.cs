using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.DAL
{
    class BanDAL
    {
        public static List<BanDTO> LayDSBan()
        {
            List<BanDTO> _ds = new List<BanDTO>();
            string sql = "select * from BanAn";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BanDTO b = new BanDTO();
                b.MaBan = int.Parse(dt.Rows[i]["MaSoBan"].ToString());
                b.SoGhe = int.Parse(dt.Rows[i]["SoGhe"].ToString());
                _ds.Add(b);
            }
            return _ds;
        }
    }
}
