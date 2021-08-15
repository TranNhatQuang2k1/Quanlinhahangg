using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaHang.DAL
{
    class NhanVienDAL
    {

        public static DataTable LayDSNhanVien()
        {
            string sql = string.Format("select MaNV as 'Mã NV', HoTen as 'Họ Tên', NgaySinh as 'Ngày Sinh', TenDN as 'Tên ĐN', Quyen as 'Quyền' from NhanVien");
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSNhanVienTiepTan()
        {
            string sql = "select MaNV as 'Mã NV', HoTen as 'Họ Tên', NgaySinh as 'Ngày Sinh', TenDN as 'Tên ĐN', Quyen as 'Quyền' from NhanVien where Quyen = N'Tiếp Tân'";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static DataTable LayDSNhanVienCoMK()
        {
            string sql = "select * from NhanVien";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static string LayMatKhauTuTenDN(string TenDN)
        {
            string sql = "select * from NhanVien where TenDN = N'" + TenDN + "'";
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            string MK = dt.Rows[0][4].ToString();
            return MK;
        }

        public static int LayMaNVTuTenNV(string tenNV)
        {
            int maNV;
            string sql = string.Format("select MaNV from NhanVien where HoTen = N'{0}'", tenNV);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                maNV = int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
            return maNV;
        }

        public static string LayTenNVTheoMaNV(int maNV)
        {
            string sql = string.Format("select HoTen from NhanVien where MaNV = {0}", maNV);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            string tenNV = dt.Rows[0]["HoTen"].ToString();
            return tenNV;
        }

        public static string LayQuyenNVTheoMaNV(int maNV)
        {
            string sql = "select Quyen from NhanVien where MaNV = " + maNV;
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            string quyen = dt.Rows[0]["Quyen"].ToString();
            return quyen;
        }

        public static bool KiemTraTenDNTonTai(string tenDN, int MaNV)
        {
            bool kq;
            string sql = "select * from NhanVien where TenDN = '" + tenDN + "'" + "and TenDN <> '' and MaNV <> " + MaNV;
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
                kq = true;
            else
                kq = false;
            return kq;
        }

        public static bool ThemNhanVien(NhanVienDTO nv)
        {
            nv.MaNV = MaTuTang();
            bool kq;
            string sql = string.Format("insert into NhanVien values ({0}, N'{1}', convert(varchar(10),'{2}',103), N'{3}', N'{4}', N'{5}')", nv.MaNV, nv.HoTen, nv.NgaySinh, nv.TenDN, nv.MatKhau, nv.Quyen);
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }

        public static bool CapNhatNhanVien(NhanVienDTO nv)
        {
            bool kq;
            string sql = string.Format("update NhanVien set HoTen = N'{0}', NgaySinh = convert(varchar(10),'{1}',103), TenDN = '{2}', MatKhau = '{3}', Quyen = N'{4}' where MaNV = {5}", nv.HoTen, nv.NgaySinh, nv.TenDN, nv.MatKhau, nv.Quyen, nv.MaNV);
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
            
        }

        public static bool XoaNhanVien(int maNV)
        {
            bool kq;
            string sql = string.Format("delete from NhanVien where MaNV = {0}", maNV);
            kq = DBHelper.Instance.ExecuteNonQuery(sql);
            return kq;
        }

        public static DataTable TraCuuNhanVienTheoTen(string tenNV)
        {
            string sql = string.Format("select MaNV as 'Mã NV', HoTen as 'Họ Tên', NgaySinh as 'Ngày Sinh', TenDN as 'Tên ĐN', Quyen as 'Quyền' from NhanVien where HoTen like N'%{0}%'", tenNV);
            DataTable dt = DBHelper.Instance.ExecuteQuery(sql);
            return dt;
        }

        public static int MaTuTang()
        {
            string sql = "select * from NhanVien";
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
