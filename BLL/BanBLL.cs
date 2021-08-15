using QuanLiNhaHang.DAL;
using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.BLL
{
    class BanBLL
    {
        public static List<BanDTO> LayDSBan()
        {
            List<BanDTO> _ds;
            _ds = BanDAL.LayDSBan();
            return _ds;
        }
    }
}
