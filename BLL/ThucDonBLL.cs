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
    class ThucDonBLL
    {
        public static bool ThemThucDon(ThucDonDTO td)
        {
            bool kq = ThucDonDAL.ThemThucDon(td);
            return kq;
        }

        public static bool XoaThucDonTheoMaTD(int maTD)
        {
            bool kq = ThucDonDAL.XoaThucDonTheoMaTD(maTD);
            return kq;
        }

        public static bool CapNhatThucDon(ThucDonDTO td)
        {
            bool kq = ThucDonDAL.CapNhatThucDon(td);
            return kq;
        }

        //Rút trích dữ liệu: select 
        public static List<ThucDonDTO> LayDSThucDon()
        {
            List<ThucDonDTO> _ds;
            _ds = ThucDonDAL.LayDSThucDon();
            return _ds;
        }

        public static List<ThucDonDTO> LayDSThucDonTheoMaLoai(int maLoai)
        {
            List<ThucDonDTO> _ds;
            _ds = ThucDonDAL.LayDSThucDonTheoMaLoai(maLoai);
            return _ds;
        }

        public static List<ThucDonDTO> LayDSMaTDVaTenTDTheoMaLoai(int maLoai)
        {
            List<ThucDonDTO> dsTD = ThucDonDAL.LayDSMaTDVaTenTDTheoMaLoai(maLoai);
            return dsTD;
        }

        public static string LayTenThucDonTuMaThucDon(int maTD)
        {
            string tenTD;
            tenTD = ThucDonDAL.LayTenThucDonTuMaThucDon(maTD);
            return tenTD;
        }
        public static int LayMaThucDonTuTenThucDon(string tenTD)
        {
            int maTD;
            maTD = ThucDonDAL.LayMaThucDonTuTenTD(tenTD);
            return maTD;
        }

        public static DataTable LayDanhSachTDTheoMaLoai(int maLoai)
        {
            DataTable dt = ThucDonDAL.LayDanhSachTDTheoMaLoai(maLoai);
            return dt;
        }

        public static string LayDonViTinhTuMaTD(int maTD)
        {
            string dvt = ThucDonDAL.LayDonViTinhTheoMaTD(maTD);
            return dvt;
        }

        public static bool KiemTraThucAnNuocUong(int maTD)
        {
            bool kq = ThucDonDAL.KiemTraThucDonNuocUong(maTD);
            return kq;
        }

        public static bool KiemTraTrungTenThucDon(string tenTD)
        {
            bool kq = ThucDonDAL.KiemTraTrungTenThucDon(tenTD);
            return kq;
        }

        public static bool KiemTraTenTDCapNhat(string tenTD, int maTD)
        {
            bool kq = ThucDonDAL.KiemTraTenTDCapNhat(tenTD, maTD);
            return kq;
        }

        public static DataTable TraCuuThucDonTheoTen(string tenTD)
        {
            DataTable kq = ThucDonDAL.TraCuuThucDonTheoTen(tenTD);
            return kq;
        }

        public static int MaTuTang()
        {
            int ma = ThucDonDAL.MaTuTang();
            return ma;
        }
    }
}
