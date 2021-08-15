using QuanLiNhaHang.DAL;
using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.BLL
{
    class GiaBLL
    {
        public static bool ThemGia(GiaDTO g)
        {
            bool kq = GiaDAL.ThemGia(g);
            return kq;
        }

        public static bool XoaGiaTheoMaTDVaNgayAD(int maTD, DateTime ngayAD)
        {
            bool kq = GiaDAL.XoaGiaTheoMaTDVaNgayAD(maTD, ngayAD);
            return kq;
        }

        public static bool CapNhatGia(GiaDTO g)
        {
            bool kq = GiaDAL.CapNhatGia(g);
            return kq;
        }

        public static double LayGiaTheoMaThucDon(int maTD)
        {
            double gia = GiaDAL.LayGiaTheoMaThucDon(maTD);
            return gia;
        }
    }
}
