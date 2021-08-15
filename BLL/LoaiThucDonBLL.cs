using QuanLiNhaHang.DAL;
using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.BLL
{
    class LoaiThucDonBLL
    {
        public static List<LoaiThucDonDTO> LayDSLoaiThucDon()
        {
            List<LoaiThucDonDTO> _ds;
            _ds = LoaiThucDonDAL.LayDSLoaiThucDon();
            return _ds;
        }

        public static int LayMaLoaiTuTenLoai(string tenLoai)
        {
            int maLoai = LoaiThucDonDAL.LayMaLoaiTuTenLoai(tenLoai);
            return maLoai;
        }
    }
}
