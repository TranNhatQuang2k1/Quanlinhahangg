using QuanLiNhaHang.DAL;
using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.BLL
{
    class CT_HoaDonBLL
    {
        public static bool ThemChiTietHoaDon(CT_HoaDonDTO cthd)
        {
            bool kq = CT_HoaDonDAL.ThemChiTietHoaDon(cthd);
            return kq;
        }
        public static bool XoaCTHDTheoSoHD(int soHD)
        {
            bool kq = CT_HoaDonDAL.XoaCTHDTheoSoHD(soHD);
            return kq;
        }
        public static bool XoaCTHDTheoSoHDVaMaTD(int soHD, int MaTD)
        {
            bool kq = CT_HoaDonDAL.XoaCTHDTheoSoHDVaMaTD(soHD, MaTD);
            return kq;
        }
        public static DataTable LayDSCTHDTuMaHD(int maHD)
        {
            DataTable _ds = CT_HoaDonDAL.LayDSCTHDTuMaHD(maHD);
            return _ds;
        }
        public static DataTable LayDSCTHD(int SoHD)
        {
            DataTable dt = CT_HoaDonDAL.LayDSCTHD(SoHD);
            return dt;
        }
    }
}
