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
    class HoaDonBLL
    {
        public static DataTable LayDSHoaDon()
        {
            DataTable _ds = HoaDonDAL.LayDSHoaDon();
            return _ds;
        }
        public static int LaySoHDTuMaBan(int maBan)
        {
            int soHD = HoaDonDAL.LaySoHoaDonTuMaBan(maBan);
            return soHD;
        }
        public static int LaySoKhachTuSoHD(int soHD)
        {
            int soKhach = HoaDonDAL.LaySoKhachTuSoHD(soHD);
            return soKhach;
        }
        public static List<int> LayDSBanChuaThanhToan()
        {
            List<int> _ds = HoaDonDAL.LayDSBanChuaThanhToan();
            return _ds;
        }

        public static int LayGioLapHDChuaThanhToanTheoMaBan(int maBan)
        {
            int gio = HoaDonDAL.LayGioLapHDChuaThanhToanTheoMaBan(maBan);
            return gio;
        }

        public static bool LapHoaDon(HoaDonDTO hd)
        {
            bool kq = HoaDonDAL.LapHoaDon(hd);
            return kq;
        }
        public static bool CapNhatLapHoaDon(HoaDonDTO hd)
        {
            bool kq = HoaDonDAL.CapNhatLapHoaDon(hd);
            return kq;
        }

        public static bool CapNhatSoKhach(int SoKhach, int SoHD)
        {
            bool kq = HoaDonDAL.CapNhatSoKhach(SoKhach, SoHD);
            return kq;
        }

        public static int LayMaHoaDonCanLap()
        {
            int maHD = HoaDonDAL.MaTuTang();
            return maHD;
        }

        public static bool XoaHDTheoSoHD(int SoHD)
        {
            bool kq = HoaDonDAL.XoaHDTheoSoHD(SoHD);
            return kq;
        }

        public static DataTable ThongKeHDTheoNgay(DateTime ngay)
        {
            DataTable kq = HoaDonDAL.ThongKeHDTheoNgay(ngay);
            return kq;
        }

        public static DataTable ThongKeHDTheoThang(int thang, int nam)
        {
            DataTable dt = HoaDonDAL.ThongKeHDTheoThang(thang, nam);
            return dt;
        }

        public static DataTable ThongKeHDTheoKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = HoaDonDAL.ThongKeHDTheoKhoangNgay(tuNgay, denNgay);
            return dt;
        }
    }
}
