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
    class NhanVienBLL
    {
        public static DataTable LayDSNhanVien()
        {
            DataTable _ds = NhanVienDAL.LayDSNhanVien();
            return _ds;
        }
        public static string LayMatKhauTuTenDN(string TenDN)
        {
            string MK = NhanVienDAL.LayMatKhauTuTenDN(TenDN);
            return MK;
        }
        public static DataTable TraCuuNhanVienTheoTen(string tenNV)
        {
            DataTable dt = NhanVienDAL.TraCuuNhanVienTheoTen(tenNV);
            return dt;
        }
        public static bool KiemTraTenDNTonTai(string tenDN, int MaNV)
        {
            bool kq = NhanVienDAL.KiemTraTenDNTonTai(tenDN, MaNV);
            return kq;
        }
        public static bool ThemNhanVien(NhanVienDTO nd)
        {
            bool kq = NhanVienDAL.ThemNhanVien(nd);
            return kq;
        }
        public static DataTable LayDSNhanVienTiepTan()
        {
            DataTable dt = NhanVienDAL.LayDSNhanVienTiepTan();
            return dt;
        }
        public static DataTable LayDSNhanVienCoMK()
        {
            DataTable dt = NhanVienDAL.LayDSNhanVienCoMK();
            return dt;
        }
        public static string LayQuyenNVTheoMaNV(int maNV)
        {
            string quyen = NhanVienDAL.LayQuyenNVTheoMaNV(maNV);
            return quyen;
        }
        public static bool XoaNhanVien(int maNV)
        {
            bool kq = NhanVienDAL.XoaNhanVien(maNV);
            return kq;
        }
        public static bool CapNhatNhanVien(NhanVienDTO nv)
        {
            bool kq = NhanVienDAL.CapNhatNhanVien(nv);
            return kq;
        }
        public static int LayMaNVTuTenNV(string tenNV)
        {
            int maNV = NhanVienDAL.LayMaNVTuTenNV(tenNV);
            return maNV;
        }
        public static string LayTenNVTheoMaNV(int maNV)
        {
            string tenNV = NhanVienDAL.LayTenNVTheoMaNV(maNV);
            return tenNV;
        }

    }
}
