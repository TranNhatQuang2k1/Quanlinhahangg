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
    class PhanCongBLL
    {
        public static DataTable LayDSPhanCong()
        {
            DataTable dt = PhanCongDAL.LayDSPhanCong();
            return dt;
        }
        public static bool ThemPhanCong(PhanCongDTO pc)
        {
            bool kq = PhanCongDAL.ThemPhanCong(pc);
            return kq;
        }
        public static bool XoaPhanCong(PhanCongDTO pc)
        {
            bool kq = PhanCongDAL.XoaPhanCong(pc);
            return kq;
        }

        public static int LayMaNVTheoMaBanVaCa(int maBan, int ca)
        {
            int maNV = PhanCongDAL.LayMaNVTheoMaBanVaCa(maBan, ca);
            return maNV;
        }

        public static int LayCaTheoGio(int gio)
        {
            int ca = PhanCongDAL.LayCaTheoGio(gio);
            return ca;
        }
    }
}
