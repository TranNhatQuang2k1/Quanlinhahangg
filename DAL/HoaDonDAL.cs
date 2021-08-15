using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.DAL
{
    class HoaDonDAL
    {
        public static DataTable LayDSHoaDon()
        {
            string sql = "select hd.SoHD as 'Số HĐ', hd.ThoiGianLap as 'TG Lập', hd.MaSoBan as 'MS Bàn', hd.SoKhach as 'Số Khách', nv1.HoTen as 'Người Lập', nv2.HoTen as 'Tiếp Tân', hd.TongTien as 'Tổng Tiền' from HoaDon hd, NhanVien nv1, NhanVien nv2 where nv1.MaNV = hd.MaNVLap and nv2.MaNV = hd.MaNVTT";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static int LaySoHoaDonTuMaBan(int maBan)
        {
            int soHD = 0;
            string sql = "select * from HoaDon where MaSoBan = " + maBan + " and TongTien = 0";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                soHD = int.Parse(dt.Rows[0]["SoHD"].ToString());
            }
            return soHD;
        }
        public static int LaySoKhachTuSoHD(int soHD)
        {
            int soKhach = 0;
            string sql = "select * from HoaDon where SoHD = " + soHD;
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                soKhach = int.Parse(dt.Rows[0]["SoKhach"].ToString());
            }
            return soKhach;
        }

        public static List<int> LayDSBanChuaThanhToan()
        {
            List<int> _ds = new List<int>();
            string sql = "select * from HoaDon where TongTien = 0";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int maBan = int.Parse(dt.Rows[i]["MaSoBan"].ToString());
                _ds.Add(maBan);
            }
            return _ds;
        }

        public static int LayGioLapHDChuaThanhToanTheoMaBan(int maBan)
        {
            string sql = string.Format("select convert(varchar(2), ThoiGianLap, 108)as 'GioLap' from HoaDon where MaSoban = {0} and TongTien = 0", maBan);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            int gio = int.Parse(dt.Rows[0]["GioLap"].ToString());
            return gio;
        }

        public static bool LapHoaDon(HoaDonDTO hd)
        {
            hd.SoHD = MaTuTang();
            string sql = string.Format("insert into HoaDon values ({0}, '{1}', {2}, {3}, {4}, {5}, {6})", hd.SoHD, DateTime.Now, hd.MsBan, hd.SoKhach, hd.MsNVLap, hd.MsNVTT, hd.TongTien);
            bool kq;
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatLapHoaDon(HoaDonDTO hd)
        {
            string sql = string.Format("update HoaDon set ThoiGianLap = '{0}', MaNVTT = {1}, TongTien = {2} where SoHD = {3}", DateTime.Now, hd.MsNVTT, hd.TongTien, hd.SoHD);
            bool kq;
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }

        public static bool CapNhatSoKhach(int SoKhach, int SoHD)
        {
            string sql = string.Format("update HoaDon set SoKhach = {0} where SoHD = {1}", SoKhach, SoHD);
            bool kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }

        public static DataTable ThongKeHDTheoNgay(DateTime ngay)
        {
            string sql = "select SoHD as 'Số HĐ', ThoiGianLap as 'Thời Gian Lập', MaSoBan as 'Mã Bàn', SoKhach as 'Số Khách', nv1.HoTen as 'Người Lập', nv2.HoTen as 'Tiếp Tân', TongTien as 'Tổng Tiền' from HoaDon, NhanVien nv1, NhanVien nv2 where HoaDon.MaNVLap = nv1.MaNV and HoaDon.MaNVTT = nv2.MaNV and convert(varchar(10), ThoiGianLap,103) = convert(varchar(10), convert(datetime, '" + ngay + "'), 103)";
            DataTable kq = DBHelper.Instance.ExecuteQuery(sql);
            return kq;
        }

        public static DataTable ThongKeHDTheoThang(int thang, int nam)
        {
            string sql = string.Format("select hd.SoHD as 'Số HĐ', hd.ThoiGianLap as 'Thời Gian Lập', hd.MaSoBan as 'Mã Bàn', hd.SoKhach as 'Số Khách', nv1.HoTen as 'Người Lập', nv2.HoTen as 'Tiếp Tân', hd.TongTien as 'Tổng Tiền' from HoaDon hd, NhanVien nv1, NhanVien nv2 where hd.MaNVLap = nv1.MaNV and hd.MaNVTT = nv2.MaNV and convert(nvarchar(10), ThoiGianLap, 103) like '%{0}/{1}'", thang, nam);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static DataTable ThongKeHDTheoKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = string.Format("select SoHD as 'Số HĐ', ThoiGianLap as 'Thời Gian Lập', MaSoBan as 'Mã Bàn', SoKhach as 'Số Khách', nv1.HoTen as 'Người Lập', nv2.HoTen as 'Tiếp Tân', TongTien as 'Tổng Tiền' from HoaDon, NhanVien nv1, NhanVien nv2 where HoaDon.MaNVLap = nv1.MaNV and HoaDon.MaNVTT = nv2.MaNV and convert(varchar(10), ThoiGianLap,103) >= convert(varchar(10),convert(datetime,'{0}'), 103) and convert(varchar(10), ThoiGianLap,103) <= convert(varchar(10),convert(datetime,'{1}'), 103)", tuNgay, denNgay);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static bool XoaHDTheoSoHD(int SoHD)
        {
            bool kq;
            string sql = string.Format("delete HoaDon where SoHD = {0}", SoHD);
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }

        public static int MaTuTang()
        {
            string sql = "select * from HoaDon";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            int maTuTang = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse(dt.Rows[i][0].ToString()) != maTuTang)
                {
                    return maTuTang;
                }
                maTuTang++;
            }
            return maTuTang;
        }
    }
}
