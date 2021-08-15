using QuanLiNhaHang.BLL;
using QuanLiNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang.GUI
{
    public partial class frmMain : Form
    {
        int flag;
        private NhanVienDTO _nv = new NhanVienDTO();
        internal NhanVienDTO Nv
        {
            get { return _nv; }
            set { _nv = value; }
        }
        public frmMain()
        {
            InitializeComponent();
            
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            DuaDSNhanVienLenDataGridView();
            DuaDanhSachPhanCongLenDataGridView();
            LoadNhanVienPhanCongLenCombobox();
            DuaLoaiThucDonLenCombobox();
            DuaDSNhanVienLenCombobox();
            DuaDSHoaDonLenDataGridView();
            DuaBanPhanCongLenCombobox();
            DuaBanLenCombobox();
            Dtp_NgayAD.MaxDate = DateTime.Today;
            dt_NgaySinh.MaxDate = DateTime.Today;
        }

        public void DuaLoaiThucDonLenCombobox()
        {
            List<LoaiThucDonDTO> _dsltd = LoaiThucDonBLL.LayDSLoaiThucDon();
            List<LoaiThucDonDTO> _dsltd1 = LoaiThucDonBLL.LayDSLoaiThucDon();
            cbb_CLoaiTD.DataSource = _dsltd;
            cbb_CLoaiTD.DisplayMember = "TenLoai";
            cbb_CLoaiTD.ValueMember = "MaLoai";
            Cbb_LTDCN.DataSource = _dsltd1;
            Cbb_LTDCN.DisplayMember = "TenLoai";
            Cbb_LTDCN.ValueMember = "MaLoai";
            Cbb_LTDCN.SelectedIndex = 0;
            
            cbb_LTD.DataSource = _dsltd;
            cbb_LTD.DisplayMember = "TenLoai";
            cbb_LTD.ValueMember = "MaLoai";
            Cbb_tracucloaitd.DataSource = _dsltd1;
            Cbb_tracucloaitd.DisplayMember= "TenLoai";
            Cbb_tracucloaitd.ValueMember = "MaLoai";

            


        }
       
        public void DuaDSNhanVienLenCombobox()
        {
            DataTable _dsNV = NhanVienBLL.LayDSNhanVienTiepTan();
            Cbb_DSNVHD.DataSource = _dsNV;
            Cbb_DSNVHD.DisplayMember = "Họ Tên";
            Cbb_DSNVHD.ValueMember = "Mã NV";
        }
        public void DuaDSNhanVienLenDataGridView()
        {
            cbb_Quyen.SelectedIndex = 0;
            DataTable _dsnd = NhanVienBLL.LayDSNhanVien();
            dgv_DSNV.DataSource = _dsnd;
        }
        public void DuaDSHoaDonLenDataGridView()
        {
            DataTable _dshd = HoaDonBLL.LayDSHoaDon();
            dtgDSHD.DataSource = _dshd;
        }
       
        private void dgv_DSNV_Click(object sender, EventArgs e)
        {
            int idx = dgv_DSNV.CurrentRow.Index;
            
            tb_NVHoten.Text = dgv_DSNV.Rows[idx].Cells[1].Value.ToString();
            //MessageBox.Show(dgv_DSNV.Rows[idx].Cells[2].Value.ToString());
            dt_NgaySinh.Text = dgv_DSNV.Rows[idx].Cells[2].Value.ToString();
            tb_NVDN.Text = dgv_DSNV.Rows[idx].Cells[3].Value.ToString();
            cbb_Quyen.Text = dgv_DSNV.Rows[idx].Cells[4].Value.ToString();
            if (cbb_Quyen.Text == "Admin")
            {
                tb_NVDN.ReadOnly = false;
                tb_MK.ReadOnly = false;
                tb_NLMK.ReadOnly = false;
                cbb_Quyen.Enabled = false;
            }
            else
                cbb_Quyen.Enabled = true;
            string MatKhau = NhanVienBLL.LayMatKhauTuTenDN(dgv_DSNV.Rows[idx].Cells[3].Value.ToString());
            tb_MK.Text = MatKhau;
            tb_NLMK.Text = tb_MK.Text;
        }

        private void Bt_TimNV_Click(object sender, EventArgs e)
        {
            if (tb_TimHTen.Text == "")
                MessageBox.Show("Chưa nhập tên nhân viên cần tra cứu!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tb_NVHoten.Text = "";
                tb_NVDN.Text = "";
                tb_MK.Text = "";
                tb_NLMK.Text = "";
                dt_NgaySinh.Text = "";
                cbb_Quyen.Text = "Tiếp Tân";
                DataTable dt = NhanVienBLL.TraCuuNhanVienTheoTen(tb_TimHTen.Text);
                dgv_DSNV.DataSource = dt;
              
            }
        }

        private void Bt_DSNV_Click(object sender, EventArgs e)
        {
            tb_NVHoten.Text = "";
            tb_NVDN.Text = "";
            tb_MK.Text = "";
            tb_NLMK.Text = "";
            dt_NgaySinh.Text = "";
            cbb_Quyen.Text = "Tiếp Tân";
            tb_TimHTen.Text = "";
            DuaDSNhanVienLenDataGridView();
        }
        public void ThemNhanVien()
        {
            NhanVienDTO nv = new NhanVienDTO();
            nv.HoTen = tb_NVHoten.Text;
            nv.NgaySinh = DateTime.Parse(dt_NgaySinh.Text);
            nv.TenDN = tb_NVDN.Text;
            nv.MatKhau = tb_NLMK.Text;
            nv.Quyen = cbb_Quyen.Text;
            if (!NhanVienBLL.KiemTraTenDNTonTai(nv.TenDN, nv.MaNV))
            {
                bool kq = NhanVienBLL.ThemNhanVien(nv);
                if (kq == true)
                {
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DuaDSNhanVienLenDataGridView();
                    tb_NVHoten.Text = "";
                    tb_NVDN.Text = "";
                    tb_MK.Text = "";
                    tb_NLMK.Text = "";
                    dt_NgaySinh.Text = "";
                    cbb_Quyen.Text = "Tiếp Tân";
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Tên đăng nhập này đã tồn tại!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Bt_themnv_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                if (cbb_Quyen.Text !="Tiếp Tân")
                {
                    if (tb_NVDN.Text.Length >= 6 && tb_NVDN.Text.Length <= 20 && tb_NVDN.Text !="")
                    {
                        if (tb_NVHoten.Text != "")
                        {
                            if (tb_MK.Text.Length >= 6 && tb_MK.Text.Length <= 20)
                            {
                                if (tb_MK.Text == tb_NLMK.Text)
                                {
                                    if (dt_NgaySinh.Text != "")
                                    {
                                        ThemNhanVien();
                                        LoadNhanVienPhanCongLenCombobox();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ngày sinh không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không trùng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tb_NLMK.Text = "";
                                    tb_NLMK.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu phải lớn hơn 5 và nhỏ hơn 21 ký tự!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tb_MK.Text = "";
                                tb_MK.Focus();
                            }
                         }   
                        else
                        {
                            MessageBox.Show("Họ tên không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_NVHoten.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên Đăng nhập phải lớn hơn 5 và nhỏ hơn 21 ký tự!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb_NVDN.Text = "";
                        tb_NVDN.Focus();
                    }
                }
                else
                {
                    if (tb_NVHoten.Text != "")
                    {
                        if (dt_NgaySinh.Text != "")
                        {
                            tb_MK.Text = "";
                            tb_NVDN.Text = "";
                            tb_NLMK.Text = "";
                            ThemNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Ngày sinh không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Họ tên nhân viên không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb_NVHoten.Focus();
                    }
                }

            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Bt_XoaNV_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                int idx = dgv_DSNV.CurrentRow.Index;
                int maNV = int.Parse(dgv_DSNV.Rows[idx].Cells[0].Value.ToString());

                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    string quyen = NhanVienBLL.LayQuyenNVTheoMaNV(maNV);
                    if (quyen != "Admin")
                    {
                        bool kq;
                        try
                        {
                            kq = NhanVienBLL.XoaNhanVien(maNV);
                            if (kq == true)
                            {
                                MessageBox.Show("Đã xóa nhân viên!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DuaDSNhanVienLenDataGridView();
                                tb_NVHoten.Text = "";
                                tb_NVDN.Text = "";
                                tb_MK.Text = "";
                                tb_NLMK.Text = "";
                            }
                            else
                                MessageBox.Show("Xóa nhân viên thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch
                        {
                            MessageBox.Show("Nhân viên đang được phân công không thể xóa!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        MessageBox.Show("Không thể xóa tài khoản Admin!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SuaNhanVien()
        {
            NhanVienDTO nv = new NhanVienDTO();
            int idx = dgv_DSNV.CurrentRow.Index;
            nv.MaNV = int.Parse(dgv_DSNV.Rows[idx].Cells[0].Value.ToString());
            nv.HoTen = tb_NVHoten.Text;
            nv.NgaySinh = DateTime.Parse(dt_NgaySinh.Text);
            nv.TenDN = tb_NVDN.Text;
            nv.MatKhau = tb_NLMK.Text;
            nv.Quyen = cbb_Quyen.Text;
            if (!NhanVienBLL.KiemTraTenDNTonTai(nv.TenDN, nv.MaNV))
            {
                bool kq = NhanVienBLL.CapNhatNhanVien(nv);
                if (kq == true)
                {
                    MessageBox.Show("Sửa người dùng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DuaDSNhanVienLenDataGridView();
                    tb_NVHoten.Text = "";
                    tb_NVDN.Text = "";
                    tb_MK.Text = "";
                    tb_NLMK.Text = "";
                    dt_NgaySinh.Text = "";
                    cbb_Quyen.Text = "Tiếp Tân";
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Tên đăng nhập này đã tồn tại!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Bt_Suanv_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                if (cbb_Quyen.Text != "Tiếp Tân")
                {
                    if (tb_NVDN.Text.Length >= 6 && tb_NVDN.Text.Length <= 20 && tb_NVDN.Text != "")
                    {
                        if (tb_NVHoten.Text != "")
                        {
                            if (tb_MK.Text.Length >= 6 && tb_MK.Text.Length <= 20)
                            {
                                if (tb_MK.Text == tb_NLMK.Text)
                                {
                                    if (dt_NgaySinh.Text != "")
                                    {
                                        SuaNhanVien();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ngày sinh không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không trùng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tb_NLMK.Text = "";
                                    tb_NLMK.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu phải lớn hơn 5 và nhỏ hơn 21 ký tự!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tb_MK.Text = "";
                                tb_MK.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Họ tên không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_NVHoten.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên Đăng nhập phải lớn hơn 5 và nhỏ hơn 21 ký tự!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb_NVDN.Text = "";
                        tb_NVDN.Focus();
                    }
                }
                else
                {
                    if (tb_NVHoten.Text != "")
                    {
                        if (dt_NgaySinh.Text != "")
                        {
                            tb_MK.Text = "";
                            tb_NVDN.Text = "";
                            tb_NLMK.Text = "";
                             SuaNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Ngày sinh không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Họ tên nhân viên không được rỗng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb_NVHoten.Focus();
                    }
                }

            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void DuaDanhSachPhanCongLenDataGridView()
        {
            DataTable dt = PhanCongBLL.LayDSPhanCong();
            dgv_Phancong.DataSource = dt;
        }
        public void LoadNhanVienPhanCongLenCombobox()
        {
            DataTable dt = NhanVienBLL.LayDSNhanVienTiepTan();
            cbb_PCNV.DataSource = dt;
            cbb_PCNV.DisplayMember = "Họ Tên";
            cbb_PCNV.ValueMember = "Mã NV";
        }
        public void DuaBanPhanCongLenCombobox()
        {
            List<BanDTO> dt = new List<BanDTO>();
            dt = BanBLL.LayDSBan();
            cbb_PCBAN.DataSource = dt;
            cbb_PCBAN.DisplayMember = "MaBan";
            cbb_PCBAN.ValueMember = "MaBan";
        }

        private void Bt_themPC_Click(object sender, EventArgs e)
        {
            try
            {
                if (_nv.Quyen == "Admin")
                {

                    if (cbb_PCNV.Text != "")
                    {
                        if (cbb_PCCA.Text != "")
                        {

                            if (cbb_PCBAN.Text != "")
                            {
                                PhanCongDTO pc = new PhanCongDTO();

                                pc.Ca = int.Parse(cbb_PCCA.Text);

                                pc.MsNV = int.Parse(cbb_PCNV.SelectedValue.ToString());

                                pc.MsBan = int.Parse(cbb_PCBAN.Text);



                                bool kq = PhanCongBLL.ThemPhanCong(pc);

                                if (kq == true)
                                {
                                    MessageBox.Show("Phân công nhân viên thành công!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DuaDanhSachPhanCongLenDataGridView();
                                }
                                else
                                {
                                    MessageBox.Show("Phân công thất bại!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                                MessageBox.Show("Chưa chọn bàn!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Chưa chọn ca!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Chưa chọn nhân viên!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch
            {
                MessageBox.Show("Nhân Viên Này đã được phân công!");
            }
        }

        private void Bt_XoaPC_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                PhanCongDTO pc = new PhanCongDTO();
                int idx = dgv_Phancong.CurrentRow.Index;
                pc.Ca = int.Parse(dgv_Phancong.Rows[idx].Cells[0].Value.ToString());
                pc.MsNV = NhanVienBLL.LayMaNVTuTenNV(dgv_Phancong.Rows[idx].Cells[1].Value.ToString());
                pc.MsBan = int.Parse(dgv_Phancong.Rows[idx].Cells[2].Value.ToString());

                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    bool kq = PhanCongBLL.XoaPhanCong(pc);
                    if (kq == true)
                    {
                        MessageBox.Show("Xóa phân công thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DuaDanhSachPhanCongLenDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Xóa phân công thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DuaBanLenCombobox()
        {
            List<BanDTO> _dsban = BanBLL.LayDSBan();
            List<int> _dsBanDaDat = HoaDonBLL.LayDSBanChuaThanhToan();
            List<int> _dsTam = new List<int>();
            for (int i = 0; i < _dsban.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < _dsBanDaDat.Count; j++)
                {
                    if (_dsban[i].MaBan == _dsBanDaDat[j])
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    _dsTam.Add(int.Parse(_dsban[i].MaBan.ToString()));
                }
            }
            cbb_GMBan.DataSource = _dsTam;
        }
        public void DuaDSBanDaGoiLenCombobox()
        {
            cmbDSBanCanLapHD.Items.Clear();
            cmbDSBanCapNhat.Items.Clear();
            cmbDSBanCanLapHD.Text = "";
            cmbDSBanCapNhat.Text = "";
            List<int> _dsMaBan = HoaDonBLL.LayDSBanChuaThanhToan();
            for (int i = 0; i < _dsMaBan.Count; i++)
            {
                cmbDSBanCanLapHD.Items.Add(_dsMaBan[i].ToString());
                cmbDSBanCapNhat.Items.Add(_dsMaBan[i].ToString());
            }
        }
        private void cbb_CLoaiTD_SelectedValueChanged(object sender, EventArgs e)
        {
            List<ThucDonDTO> _dstd = new List<ThucDonDTO>();
            string tenLoai = cbb_CLoaiTD.Text.ToString();
            int maLoaiTD = LoaiThucDonBLL.LayMaLoaiTuTenLoai(tenLoai);
            _dstd = ThucDonBLL.LayDSThucDonTheoMaLoai(maLoaiTD);
            lbDSTD.DataSource = _dstd;
            lbDSTD.DisplayMember = "TenTD";
            lbDSTD.ValueMember = "MaTD";
            tb_Dongia.Text = "";
            lbgiathamkhao.Text = "0";
            cbb_khuyenmai.SelectedIndex = 0;
        }

        private void Bt_ThemGM_Click(object sender, EventArgs e)
        {
                if (tb_Dongia.Text != "")
                {
                    int maTD = int.Parse(lbDSTD.SelectedValue.ToString());
                    string tenTD = ThucDonBLL.LayTenThucDonTuMaThucDon(maTD);
                    bool tonTai = false;
                    int dong = 0;
                    for (int i = 0; i < lvChiTietGoiMon.Items.Count; i++)
                    {
                        if (int.Parse(lvChiTietGoiMon.Items[i].SubItems[0].Text) == maTD)
                        {
                            tonTai = true;
                            dong = i;
                        }
                    }
                    string soLuong = "1";
                    if (tb_soluong.Text != "")
                    {
                        soLuong = tb_soluong.Text;
                    }
                    if (tonTai == false)
                    {
                        string donGia = tb_Dongia.Text;
                        ListViewItem item = new ListViewItem();
                        item.Text = maTD.ToString();
                        item.SubItems.Add(tenTD);
                        item.SubItems.Add(donGia);
                        item.SubItems.Add(soLuong);
                        this.lvChiTietGoiMon.Items.Add(item);
                        tb_soluong.Text = "1";
                    }
                    else
                    {
                        int sl = int.Parse(lvChiTietGoiMon.Items[dong].SubItems[3].Text) + int.Parse(soLuong);
                        lvChiTietGoiMon.Items[dong].SubItems[3].Text = sl.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn nhập giá không chính xác!");
                }
         
        }

        private void lbDSTD_Click(object sender, EventArgs e)
        {
            int maTD = int.Parse(lbDSTD.SelectedValue.ToString());
            double gia = GiaBLL.LayGiaTheoMaThucDon(maTD);
            lbgiathamkhao.Text = gia.ToString();
            tb_Dongia.Text = Convert.ToString(double.Parse(lbgiathamkhao.Text) - (double.Parse(cbb_khuyenmai.Text) / 100) * double.Parse(lbgiathamkhao.Text));
        }

        private void cbb_khuyenmai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tb_Dongia.Text != "")
            {
                tb_Dongia.Text = Convert.ToString(double.Parse(lbgiathamkhao.Text) - (double.Parse(cbb_khuyenmai.Text) / 100) * double.Parse(lbgiathamkhao.Text));
            }
        }

        private void Bt_Luugoimon_Click(object sender, EventArgs e)
        {
            if (lvChiTietGoiMon.Items.Count > 0)
            {
                if (tb_sokhach.Text != "")
                {
                    HoaDonDTO hd = new HoaDonDTO();
                    CT_HoaDonDTO cthd = new CT_HoaDonDTO();
                    hd.MsBan = int.Parse(cbb_GMBan.Text);
                    int maHD = HoaDonBLL.LayMaHoaDonCanLap();
                    hd.TongTien = 0;
                    hd.MsNVLap = _nv.MaNV;

                    hd.MsNVTT = _nv.MaNV;
                    try
                    {
                        int soKhach = int.Parse(tb_sokhach.Text);
                        if (soKhach > 0)
                        {
                            hd.SoKhach = soKhach;

                            bool kq = HoaDonBLL.LapHoaDon(hd);
                            if (kq == true)
                            {
                                for (int i = 0; i < lvChiTietGoiMon.Items.Count; i++)
                                {
                                    cthd.SoHD = hd.SoHD;
                                    cthd.MaTD = int.Parse(lvChiTietGoiMon.Items[i].SubItems[0].Text);
                                    cthd.DonGia = double.Parse(lvChiTietGoiMon.Items[i].SubItems[2].Text);
                                    cthd.SoLuong = int.Parse(lvChiTietGoiMon.Items[i].SubItems[3].Text);
                                    CT_HoaDonBLL.ThemChiTietHoaDon(cthd);
                                }
                                MessageBox.Show("Lưu gọi món thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DuaDSBanDaGoiLenCombobox();
                                DuaBanLenCombobox();
                                lvChiTietGoiMon.Items.Clear();
                                tb_Dongia.Text = "";
                                tb_sokhach.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số khách phải lớn hơn 0!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_sokhach.Text = "";
                            tb_sokhach.Focus();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kiểu dữ liệu số khách không đúng!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb_sokhach.Text = "";
                        tb_sokhach.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập số lượng khách!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_sokhach.Text = "";
                    tb_sokhach.Focus();
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn thực đơn!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_GMxoathucdondcchon_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa thực đơn này không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    lvChiTietGoiMon.FocusedItem.Remove();
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ĐT cần xóa!");
            }
        }

        private void bt_GMxoadanhsachthucdon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa hết danh sách không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
            if (result == DialogResult.Yes)
            {
                lvChiTietGoiMon.Items.Clear();
            }
        }

      

        private void lbDSTDCN_Click(object sender, EventArgs e)
        {
            int maTD = int.Parse(lbDSTDCN.SelectedValue.ToString());
            cbb_KMCN.SelectedIndex = 0;
            double gia = GiaBLL.LayGiaTheoMaThucDon(maTD);
            lb_giaTKCN.Text = gia.ToString();
           tb_DongiaCN.Text = Convert.ToString(double.Parse(lb_giaTKCN.Text) - (double.Parse(cbb_KMCN.Text) / 100) * double.Parse(lb_giaTKCN.Text));
        }

        private void cmbDSBanCapNhat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SoHD = HoaDonBLL.LaySoHDTuMaBan(int.Parse(cmbDSBanCapNhat.SelectedItem.ToString()));
            DataTable _ds = new DataTable();
            _ds = CT_HoaDonBLL.LayDSCTHDTuMaHD(SoHD);
            lvChiTietGoiMonCN.Items.Clear();
            int soKhach = HoaDonBLL.LaySoKhachTuSoHD(SoHD);
            tb_sokhachCN.Text = soKhach.ToString();
            for (int i = 0; i < +_ds.Rows.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.Text = _ds.Rows[i]["Tên TĐ"].ToString();
                li.SubItems.Add(_ds.Rows[i]["Đơn Giá"].ToString());
                li.SubItems.Add(_ds.Rows[i]["Số Lượng"].ToString());
                lvChiTietGoiMonCN.Items.Add(li);
            }
        }

        private void cbb_KMCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_DongiaCN.Text = Convert.ToString(double.Parse(lb_giaTKCN.Text) - (double.Parse(cbb_KMCN.Text) / 100) * double.Parse(lb_giaTKCN.Text));
        }

        private void Bt_ThemCNGM_Click(object sender, EventArgs e)
        {
            if (cmbDSBanCapNhat.Text != "")
            {
                if (tb_DongiaCN.Text != "")
                {
                    int maTD = int.Parse(lbDSTDCN.SelectedValue.ToString());
                    string tenTD = ThucDonBLL.LayTenThucDonTuMaThucDon(maTD);
                    bool tonTai = false;
                    int dong = 0;
                    for (int i = 0; i < lvChiTietGoiMonCN.Items.Count; i++)
                    {
                        if (lvChiTietGoiMonCN.Items[i].SubItems[0].Text == tenTD)
                        {
                            tonTai = true;
                            dong = i;
                        }
                    }
                    string soLuong = "1";
                    if (tb_soluongCN.Text != "")
                    {
                        soLuong = tb_soluongCN.Text;
                    }
                    if (tonTai == false)
                    {
                        string donGia = tb_DongiaCN.Text;
                        ListViewItem item = new ListViewItem();
                        item.Text = tenTD;
                        item.SubItems.Add(donGia);
                        item.SubItems.Add(soLuong);
                        this.lvChiTietGoiMonCN.Items.Add(item);
                        tb_soluongCN.Text = "1";
                    }
                    else
                    {
                        int sl = int.Parse(lvChiTietGoiMonCN.Items[dong].SubItems[2].Text) + int.Parse(soLuong);
                        lvChiTietGoiMonCN.Items[dong].SubItems[2].Text = sl.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn nhập giá không chính xác!");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bàn cần cập nhật!");
            }
        }

        private void Bt_CNGM_Click(object sender, EventArgs e)
        {
            if (lvChiTietGoiMonCN.Items.Count > 0)
            {
                if (tb_sokhachCN.Text != "")
                {
                    HoaDonDTO hd = new HoaDonDTO();
                    CT_HoaDonDTO cthd = new CT_HoaDonDTO();
                    hd.MsBan = int.Parse(cmbDSBanCapNhat.Text);
                    hd.SoKhach = int.Parse(tb_sokhachCN.Text);
                    hd.SoHD = HoaDonBLL.LaySoHDTuMaBan(int.Parse(cmbDSBanCapNhat.Text));
                    HoaDonBLL.CapNhatSoKhach(hd.SoKhach, hd.SoHD);
                    bool kq = CT_HoaDonBLL.XoaCTHDTheoSoHD(hd.SoHD);

                    for (int i = 0; i < lvChiTietGoiMonCN.Items.Count; i++)
                    {
                        cthd.SoHD = hd.SoHD;
                        cthd.MaTD = ThucDonBLL.LayMaThucDonTuTenThucDon(lvChiTietGoiMonCN.Items[i].SubItems[0].Text);
                        cthd.DonGia = double.Parse(lvChiTietGoiMonCN.Items[i].SubItems[1].Text);
                        cthd.SoLuong = int.Parse(lvChiTietGoiMonCN.Items[i].SubItems[2].Text);
                        CT_HoaDonBLL.ThemChiTietHoaDon(cthd);
                    }
                    if (kq == true)
                    {
                        MessageBox.Show("Cập nhật gọi món thành công!");
                        DuaDSBanDaGoiLenCombobox();
                        DuaBanLenCombobox();
                        lvChiTietGoiMonCN.Items.Clear();
                        tb_DongiaCN.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Nhập số lượng khách!");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn thực đơn!");
            }
        }
        private void Bt_XoaTDCN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa thực đơn này không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    lvChiTietGoiMonCN.FocusedItem.Remove();
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ĐT cần xóa!");
            }
        }
        private void Bt_XoaDSTDCN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa hết danh sách không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
            if (result == DialogResult.Yes)
            {
                lvChiTietGoiMonCN.Items.Clear();
            }
        }
        private void cmbDSBanCanLapHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cbb_DSNVHD.Text = "";
            lvDSCTGMCanLapHD.Items.Clear();
            DataTable _ds = new DataTable();
            int maBan = int.Parse(cmbDSBanCanLapHD.Text);
            int maHD = HoaDonBLL.LaySoHDTuMaBan(maBan);
            _ds = CT_HoaDonBLL.LayDSCTHDTuMaHD(maHD);
            for (int i = 0; i < _ds.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = _ds.Rows[i]["Tên TĐ"].ToString();
                item.SubItems.Add(_ds.Rows[i]["Đơn Giá"].ToString());
                item.SubItems.Add(_ds.Rows[i]["Số Lượng"].ToString());
                lvDSCTGMCanLapHD.Items.Add(item);
            }

            int gioLap = HoaDonBLL.LayGioLapHDChuaThanhToanTheoMaBan(maBan);
            int ca = PhanCongBLL.LayCaTheoGio(gioLap);
            int maNV = PhanCongBLL.LayMaNVTheoMaBanVaCa(maBan, ca);

            string tenNV = NhanVienBLL.LayTenNVTheoMaNV(maNV);
            Cbb_DSNVHD.Text = tenNV;
        }

        private void bt_Tinhtien_Click(object sender, EventArgs e)
        {
            if (cmbDSBanCanLapHD.Text == "")
                MessageBox.Show("Chưa chọn bàn cần tính!");
            else
            {
                double tongTien = 0;
                for (int i = 0; i < lvDSCTGMCanLapHD.Items.Count; i++)
                {
                    double donGia = double.Parse(lvDSCTGMCanLapHD.Items[i].SubItems[1].Text);
                    int soLuong = int.Parse(lvDSCTGMCanLapHD.Items[i].SubItems[2].Text);
                    tongTien += donGia * soLuong;
                }
                lb_tien.Text = tongTien.ToString();
            }
        }

        private void Bt_LHD_Click(object sender, EventArgs e)
        {
            if (lvDSCTGMCanLapHD.Items.Count > 0)
            {
                if (Cbb_DSNVHD.Text == "")
                    MessageBox.Show("Chưa chọn nhân viên tiếp tân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (lb_tien.Text == "0")
                        MessageBox.Show("Chưa tính tổng tiền!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        HoaDonDTO hd = new HoaDonDTO();
                        hd.MsNVTT = int.Parse(Cbb_DSNVHD.SelectedValue.ToString());
                        hd.MsBan = int.Parse(cmbDSBanCanLapHD.Text);
                        hd.SoHD = HoaDonBLL.LaySoHDTuMaBan(hd.MsBan);
                        hd.TongTien = double.Parse(lb_tien.Text);
                        bool kq = HoaDonBLL.CapNhatLapHoaDon(hd);
                        if (kq == true)
                        {
                            MessageBox.Show("Lập hóa đơn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DuaDSHoaDonLenDataGridView();
                            DuaBanLenCombobox();
                            lvDSCTGMCanLapHD.Items.Clear();
                            DuaDSBanDaGoiLenCombobox();
                            lb_tien.Text = "0";                        
                        }
                        else
                        {
                            MessageBox.Show("Lập hóa đơn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bàn!");
            }
        }

        private void Bt_timHDngay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dtp_theongay.Value;
                DataTable kq = HoaDonBLL.ThongKeHDTheoNgay(ngay);
                dtgDSHD.DataSource = kq;
            }
            catch
            {
                MessageBox.Show("Mời chọn ngày cần tra cứu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void XoaDSHD(DataTable dt)
        {
            if (_nv.Quyen == "Admin")
            {
                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int soHD = int.Parse(dt.Rows[i][0].ToString());
                        CT_HoaDonBLL.XoaCTHDTheoSoHD(soHD);
                        HoaDonBLL.XoaHDTheoSoHD(soHD);
                    }
                    MessageBox.Show("Xóa hóa đơn thành công!");
                    DuaDSHoaDonLenDataGridView();

                    dtgDSCTHD.DataSource = null;
                    try
                    {
                        int idx2 = dtgDSHD.CurrentRow.Index;
                        int maHD = int.Parse(dtgDSHD.Rows[idx2].Cells[0].Value.ToString());
                        DataTable _ds = CT_HoaDonBLL.LayDSCTHDTuMaHD(maHD);
                        dtgDSCTHD.DataSource = _ds;
                    }
                    catch { }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Bt_xoaHDngay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dtp_theongay.Value;
                DataTable dt = HoaDonBLL.ThongKeHDTheoNgay(ngay);
                XoaDSHD(dt);
            }
            catch
            {
                MessageBox.Show("Xóa hóa đơn không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDSHD_Click(object sender, EventArgs e)
        {
            if (dtgDSHD.CurrentRow != null)
            {
                try
                {
                    int idx = dtgDSHD.CurrentRow.Index;
                    int maHD = int.Parse(dtgDSHD.Rows[idx].Cells[0].Value.ToString());
                    DataTable _ds = CT_HoaDonBLL.LayDSCTHDTuMaHD(maHD);
                    dtgDSCTHD.DataSource = _ds;
                }
                catch
                {
                    MessageBox.Show("Lỗi!");
                }
            }
        }

        private void Bt_timThang_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(Cbb_HDthang.Text);
                int nam = int.Parse(tb_HDnam.Text);
                DataTable dt = HoaDonBLL.ThongKeHDTheoThang(thang, nam);
                dtgDSHD.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Mời chọn tháng cần tra cứu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_XoaThang_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(Cbb_HDthang.Text);
                int nam = int.Parse(tb_HDnam.Text);
                DataTable dt = HoaDonBLL.ThongKeHDTheoThang(thang, nam);
                dtgDSHD.DataSource = dt;
                XoaDSHD(dt);
            }
            catch
            {
                MessageBox.Show("Xóa hóa đơn không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Bt_TimKhoangngay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = Dtp_Tungay.Value;
                DateTime denNgay = Dtp_Denngay.Value;

                DataTable dt = HoaDonBLL.ThongKeHDTheoKhoangNgay(tuNgay, denNgay);
                dtgDSHD.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Chưa chọn mốc ngày tra cứu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_XoaKhoangngay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = Dtp_Tungay.Value;
                DateTime denNgay = Dtp_Denngay.Value;

                DataTable dt = HoaDonBLL.ThongKeHDTheoKhoangNgay(tuNgay, denNgay);
                dtgDSHD.DataSource = dt;
                XoaDSHD(dt);
            }
            catch
            {
                MessageBox.Show("Xóa hóa đơn không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Bt_xoaHDduocchon_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int idx = dtgDSHD.CurrentRow.Index;
                        int SoHD = int.Parse(dtgDSHD.Rows[idx].Cells[0].Value.ToString());
                        CT_HoaDonBLL.XoaCTHDTheoSoHD(SoHD);
                        HoaDonBLL.XoaHDTheoSoHD(SoHD);
                        MessageBox.Show("Xóa Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DuaDSHoaDonLenDataGridView();
                        dtgDSCTHD.DataSource = null;
                        try
                        {
                            if (dtgDSHD.CurrentRow.Index != null)
                            {
                                int idx2 = dtgDSHD.CurrentRow.Index;
                                int maHD = int.Parse(dtgDSHD.Rows[idx2].Cells[0].Value.ToString());
                                DataTable _ds = CT_HoaDonBLL.LayDSCTHDTuMaHD(maHD);
                                dtgDSCTHD.DataSource = _ds;
                            }
                        }
                        catch { 

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không có hóa đơn thanh toán nào trong hệ thống!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_Hienthi_Click(object sender, EventArgs e)
        {
            DuaDSHoaDonLenDataGridView();
        }

        private void Cbb_tracucloaitd_SelectedValueChanged(object sender, EventArgs e)
        {
            int maLoai = LoaiThucDonBLL.LayMaLoaiTuTenLoai(Cbb_tracucloaitd.Text);
            DataTable dsTD = ThucDonBLL.LayDanhSachTDTheoMaLoai(maLoai);
            dgvDSThucDon.DataSource = dsTD;
            Tb_TracucTenTd.Text = "";
        }

        private void dgvDSThucDon_Click(object sender, EventArgs e)
        {
            int idx = dgvDSThucDon.CurrentRow.Index;
            Tb_TenTD.Text = dgvDSThucDon.Rows[idx].Cells[1].Value.ToString();
            Tb_DGia.Text = dgvDSThucDon.Rows[idx].Cells[2].Value.ToString();
            Dtp_NgayAD.Text = dgvDSThucDon.Rows[idx].Cells[3].Value.ToString();
            Tb_Tendonvi.Text = dgvDSThucDon.Rows[idx].Cells[4].Value.ToString();
            MessageBox.Show(dgvDSThucDon.Rows[idx].Cells[5].Value.ToString());
            cbb_LTD.Text = dgvDSThucDon.Rows[idx].Cells[5].Value.ToString();
        }

        private void Bt_TìmTD_Click(object sender, EventArgs e)
        {
            Tb_TenTD.Text = "";
            cbb_LTD.Text = "";
            Dtp_NgayAD.Text = "";
            Tb_DGia.Text = "";
            cbb_LTD.Text = "";
            if (Tb_TracucTenTd.Text == "")
                MessageBox.Show("Chưa nhập tên thực đơn cần tra cứu!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DataTable kq = ThucDonBLL.TraCuuThucDonTheoTen(Tb_TracucTenTd.Text);
                dgvDSThucDon.DataSource = kq;
            }
        }
        public void ThemThucDon()
        {
            ThucDonDTO td = new ThucDonDTO();
            GiaDTO g = new GiaDTO();
            td.MaTD = ThucDonBLL.MaTuTang();
            td.MaLoai = LoaiThucDonBLL.LayMaLoaiTuTenLoai(cbb_LTD.Text);
            td.TenTD = Tb_TenTD.Text;
            td.DonViTinh = Tb_Tendonvi.Text;

            g.MaTD = td.MaTD;
            g.NgayADGia = Dtp_NgayAD.Value;

            
            {
                g.Gia = double.Parse(Tb_DGia.Text);
                bool kt = ThucDonBLL.KiemTraTrungTenThucDon(td.TenTD);
                if (kt == true)
                {
                    bool kq1 = ThucDonBLL.ThemThucDon(td);
                    bool kq2 = GiaBLL.ThemGia(g);
                    if (kq1 == true && kq2==true)
                        MessageBox.Show("Thêm thực đơn thành công!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Thực đơn này đã có!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
           
        }
        private void Bt_ThemTD_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                if (Tb_TenTD.Text != "")
                {
                    if (Tb_DGia.Text != "")
                    {
                        if (Dtp_NgayAD.Text != "")
                        {
                            if (Tb_Tendonvi.Text != "")
                            {
                                ThemThucDon();
                            }
                            else
                                MessageBox.Show("Chưa nhập đơn vị tính!");
                        }
                        else
                            MessageBox.Show("Chưa nhập ngày áp dụng đơn giá!");
                    }
                    else
                        MessageBox.Show("Chưa nhập đơn giá!");
                }
                else
                    MessageBox.Show("Chưa nhập tên thực đơn!");
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbLoaiTDCN_SelectedValueChanged(object sender, EventArgs e)
        {
            List<ThucDonDTO> _dstd = new List<ThucDonDTO>();
            string tenLoai = Cbb_LTDCN.Text.ToString();
            int maLoaiTD = LoaiThucDonBLL.LayMaLoaiTuTenLoai(tenLoai);
            _dstd = ThucDonBLL.LayDSThucDonTheoMaLoai(maLoaiTD);
            lbDSTDCN.DataSource = _dstd;
            lbDSTDCN.DisplayMember = "TenTD";
            lbDSTDCN.ValueMember = "MaTD";
            tb_DongiaCN.Text = "";
            lb_giaTKCN.Text = "0";
            Cbb_LTDCN.SelectedIndex = 0;
            cbb_KMCN.SelectedIndex = 0;
        }

        private void Cbb_LTDCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ThucDonDTO> _dstd = new List<ThucDonDTO>();
            string tenLoai = Cbb_LTDCN.Text.ToString();
            int maLoaiTD = LoaiThucDonBLL.LayMaLoaiTuTenLoai(tenLoai);
            _dstd = ThucDonBLL.LayDSThucDonTheoMaLoai(maLoaiTD);
            lbDSTDCN.DataSource = _dstd;
            lbDSTDCN.DisplayMember = "TenTD";
            lbDSTDCN.ValueMember = "MaTD";
            tb_DongiaCN.Text = "";
            lb_giaTKCN.Text = "0";
        }

        private void Bt_SuaTD_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                ThucDonDTO td = new ThucDonDTO();
                GiaDTO g = new GiaDTO();

                int idx = dgvDSThucDon.CurrentRow.Index;
                if (Tb_TenTD.Text != "")
                {

                    if (Tb_Tendonvi.Text != "")
                    {
                        td.MaTD = int.Parse(dgvDSThucDon.Rows[idx].Cells[0].Value.ToString());
                        td.MaLoai = LoaiThucDonBLL.LayMaLoaiTuTenLoai(cbb_LTD.Text);
                        td.TenTD = Tb_TenTD.Text;
                        td.DonViTinh = Tb_Tendonvi.Text;
                        bool kt;
                        kt = ThucDonBLL.KiemTraTenTDCapNhat(Tb_TenTD.Text, td.MaTD);
                        if (kt == true)
                        {
                            g.MaTD = td.MaTD;
                            if (Dtp_NgayAD.Text != "")
                            {
                                g.NgayADGia = DateTime.Parse(Dtp_NgayAD.Text.ToString());

                                try
                                {
                                    double gia = double.Parse(Tb_DGia.Text);
                                    if (gia > 0)
                                    {
                                        g.Gia = gia;
                                        bool kq1 = ThucDonBLL.CapNhatThucDon(td);
                                        bool kq2 = GiaBLL.CapNhatGia(g);
                                        if (kq1 == true && kq2 == true)
                                        {
                                            MessageBox.Show("Cập nhật thực đơn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Tb_TenTD.Text = "";
                                            Tb_DGia.Text = "";
                                            Dtp_NgayAD.Text = "";
                                            Tb_Tendonvi.Text = "";
                                            Cbb_tracucloaitd_SelectedValueChanged(sender, e);
                                        }
                                        else
                                            MessageBox.Show("Cập nhật thực đơn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đơn giá phải lớn hơn 0!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Tb_DGia.Text = "";
                                        Tb_DGia.Focus();
                                    }
                                }

                                catch
                                {
                                    MessageBox.Show("Chưa nhập đơn giá hoặc kiểu dữ liệu đơn giá không đúng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Tb_DGia.Text = "";
                                    Tb_DGia.Focus();
                                }
                            }
                            else
                                MessageBox.Show("Chưa nhập ngày áp dụng giá!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Tên thực đơn bị trùng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Tb_TenTD.Text = "";
                            Tb_TenTD.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập đơn vị tính!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tb_Tendonvi.Text = "";
                        Tb_Tendonvi.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên thực đơn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tb_Tendonvi.Text = "";
                    Tb_Tendonvi.Focus();
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_XoaTD_Click(object sender, EventArgs e)
        {
            if (_nv.Quyen == "Admin")
            {
                try
                {
                    int idx = dgvDSThucDon.CurrentRow.Index;
                    int maTD = int.Parse(dgvDSThucDon.Rows[idx].Cells[0].Value.ToString());
                    DateTime ngayAD = DateTime.Parse(Dtp_NgayAD.Text);
                    DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                    if (result == DialogResult.Yes)
                    {
                        bool kq1, kq2;
                        try
                        {
                            kq1 = GiaBLL.XoaGiaTheoMaTDVaNgayAD(maTD, ngayAD);
                            kq2 = ThucDonBLL.XoaThucDonTheoMaTD(maTD);

                            if (kq1 == true && kq2 == true)
                            {
                                MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Tb_TenTD.Text = "";
                                tb_Dongia.Text = "";
                                Dtp_NgayAD.Text = "";
                                Tb_Tendonvi.Text = "";

                                if (Tb_TracucTenTd.Text != "")
                                    Bt_TìmTD_Click(sender, e);
                                if (Cbb_tracucloaitd.Text != "")
                                    Cbb_tracucloaitd_SelectedValueChanged(sender, e);
                            }
                            else
                                MessageBox.Show("Xóa thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show("Thực đơn đã được gọi món hoặc có trong hóa đơn. Không thể xóa!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Chưa chọn thực đơn cần xóa!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Admin mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void ThongKe(DataTable kq)
        {

            if (kq.Rows.Count > 0)
            {
                double tongDoanhThu = 0;
                int tongKhachDen = 0;
                for (int i = 0; i < dgvThongKe.Rows.Count - 1; i++)
                {
                    tongDoanhThu += double.Parse(dgvThongKe.Rows[i].Cells[6].Value.ToString());
                    tongKhachDen += int.Parse(dgvThongKe.Rows[i].Cells[3].Value.ToString());
                }
                lbTongDoanhThu.Text = tongDoanhThu.ToString() + " VNÐ";
                lbSoLuongKhachDen.Text = tongKhachDen.ToString() + " Khách";

                DataTable _ds = new DataTable();
                _ds.Columns.Add("SoHD", typeof(int));
                _ds.Columns.Add("MaThucDon", typeof(int));
                _ds.Columns.Add("SoLuong", typeof(int));
                _ds.Columns.Add("DonGia", typeof(double));
                _ds.PrimaryKey = new DataColumn[] { _ds.Columns["SoHD"], _ds.Columns["MaThucDon"] };
                for (int i = 0; i < kq.Rows.Count; i++)
                {
                    int SoHD = int.Parse(kq.Rows[i][0].ToString());
                    if (_ds.Rows.Count == 0)
                    {
                        DataTable dtct = CT_HoaDonBLL.LayDSCTHD(SoHD);
                        for (int j = 0; j < dtct.Rows.Count; j++)
                        {
                            DataRow ct = _ds.NewRow();
                            ct[0] = int.Parse(dtct.Rows[j][0].ToString());
                            ct[1] = int.Parse(dtct.Rows[j][1].ToString());
                            ct[2] = int.Parse(dtct.Rows[j][2].ToString());
                            ct[3] = double.Parse(dtct.Rows[j][3].ToString());
                            _ds.Rows.Add(ct);
                        }
                    }
                    else
                    {
                        DataTable dtct = CT_HoaDonBLL.LayDSCTHD(SoHD);
                        for (int j = 0; j < dtct.Rows.Count; j++)
                        {
                            bool kt = false;
                            int dong = 0;
                            for (int k = 0; k < _ds.Rows.Count; k++)
                            {
                                if (dtct.Rows[j][1].ToString() == _ds.Rows[k][1].ToString())
                                {
                                    dong = k;
                                    kt = true;
                                }
                            }
                            if (kt == true)
                            {
                                _ds.Rows[dong][2] = int.Parse(_ds.Rows[dong][2].ToString()) + int.Parse(dtct.Rows[j][2].ToString());
                            }
                            else
                            {
                                DataRow ct = _ds.NewRow();
                                ct[0] = int.Parse(dtct.Rows[j][0].ToString());
                                ct[1] = int.Parse(dtct.Rows[j][1].ToString());
                                ct[2] = int.Parse(dtct.Rows[j][2].ToString());
                                ct[3] = double.Parse(dtct.Rows[j][3].ToString());
                                _ds.Rows.Add(ct);
                            }
                        }
                    }
                }

                DataTable _dstd = new DataTable();
                _dstd.Columns.Add("SoHD", typeof(int));
                _dstd.Columns.Add("MaThucDon", typeof(int));
                _dstd.Columns.Add("SoLuong", typeof(int));
                _dstd.Columns.Add("DonGia", typeof(double));
                _dstd.PrimaryKey = new DataColumn[] { _dstd.Columns["SoHD"], _dstd.Columns["MaThucDon"] };

                DataTable _dstu = new DataTable();
                _dstu.Columns.Add("SoHD", typeof(int));
                _dstu.Columns.Add("MaThucDon", typeof(int));
                _dstu.Columns.Add("SoLuong", typeof(int));
                _dstu.Columns.Add("DonGia", typeof(double));
                _dstu.PrimaryKey = new DataColumn[] { _dstu.Columns["SoHD"], _dstu.Columns["MaThucDon"] };

                for (int i = 0; i < _ds.Rows.Count; i++)
                {
                    if (ThucDonBLL.KiemTraThucAnNuocUong(int.Parse(_ds.Rows[i][1].ToString())))
                    {
                        DataRow ct = _dstd.NewRow();
                        ct[0] = int.Parse(_ds.Rows[i][0].ToString());
                        ct[1] = int.Parse(_ds.Rows[i][1].ToString());
                        ct[2] = int.Parse(_ds.Rows[i][2].ToString());
                        ct[3] = double.Parse(_ds.Rows[i][3].ToString());
                        _dstd.Rows.Add(ct);
                    }
                    else
                    {
                        DataRow ct = _dstu.NewRow();
                        ct[0] = int.Parse(_ds.Rows[i][0].ToString());
                        ct[1] = int.Parse(_ds.Rows[i][1].ToString());
                        ct[2] = int.Parse(_ds.Rows[i][2].ToString());
                        ct[3] = double.Parse(_ds.Rows[i][3].ToString());
                        _dstu.Rows.Add(ct);
                    }
                }

                if (_dstd.Rows.Count > 0)
                {
                    int MaxTD = int.Parse(_dstd.Rows[0][2].ToString());
                    for (int i = 0; i < _dstd.Rows.Count; i++)
                    {
                        int sl = int.Parse(_dstd.Rows[i][2].ToString());
                        if (MaxTD < sl)
                            MaxTD = int.Parse(_dstd.Rows[i][2].ToString());
                    }
                    int y = 0;
                    for (int i = 0; i < _dstd.Rows.Count; i++)
                    {
                        if (MaxTD == int.Parse(_dstd.Rows[i][2].ToString()))
                            y = i;
                    }

                    int MaTD = int.Parse(_dstd.Rows[y][1].ToString());
                    lbSLTDBanNhieuNhat.Text = MaxTD.ToString();
                    lbTDBanNhieuNhat.Text = ThucDonBLL.LayTenThucDonTuMaThucDon(MaTD);
                    lbDVTDBanNhieuNhat.Text = ThucDonBLL.LayDonViTinhTuMaTD(MaTD);
                }

                if (_dstu.Rows.Count > 0)
                {
                    int MaxTU = 0;
                    for (int i = 0; i < _dstu.Rows.Count; i++)
                    {
                        if (MaxTU < int.Parse(_dstu.Rows[i][2].ToString()))
                            MaxTU = int.Parse(_dstu.Rows[i][2].ToString());
                    }
                    int z = 0;
                    for (int i = 0; i < _dstu.Rows.Count; i++)
                    {
                        if (MaxTU == int.Parse(_dstu.Rows[i][2].ToString()))
                            z = i;
                    }

                    int MaTU = int.Parse(_dstu.Rows[z][1].ToString());
                    lbSLTUBanNhieuNhat.Text = MaxTU.ToString();
                    lbTUBanNhieuNhat.Text = ThucDonBLL.LayTenThucDonTuMaThucDon(MaTU);
                    lbDVTUBanNhieuNhat.Text = ThucDonBLL.LayDonViTinhTuMaTD(MaTU);
                }

            }
            else
            {
                lbTDBanNhieuNhat.Text = "Null";
                lbTUBanNhieuNhat.Text = "Null";
                lbDVTDBanNhieuNhat.Text = "Null";
                lbDVTUBanNhieuNhat.Text = "Null";
                lbSLTDBanNhieuNhat.Text = "0";
                lbSLTUBanNhieuNhat.Text = "0";
                lbTongDoanhThu.Text = "0";
                lbSoLuongKhachDen.Text = "0";
            }

        }
        private void Bt_TKeNgay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = Dtp_NgayTK.Value;
                DataTable kq = HoaDonBLL.ThongKeHDTheoNgay(ngay);
                if (kq != null)
                {
                    dgvThongKe.DataSource = kq;
                }
                else
                {
                    lbTDBanNhieuNhat.Text = "Null";
                    lbTUBanNhieuNhat.Text = "Null";
                    lbDVTDBanNhieuNhat.Text = "Null";
                    lbDVTUBanNhieuNhat.Text = "Null";
                    lbSLTDBanNhieuNhat.Text = "0";
                    lbSLTUBanNhieuNhat.Text = "0";
                    lbTongDoanhThu.Text = "0";
                    lbSoLuongKhachDen.Text = "0";
                }    
                ThongKe(kq);
                if (kq.Rows.Count > 0)
                    flag = 1;
                else
                    flag = 0;
            }
            catch
            {
                MessageBox.Show("Mời chọn ngày cần thống kê!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_Thongketheothang_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(Cbb_Tkthang.Text);
                int nam = int.Parse(Tb_Tknam.Text);
                DataTable dt = HoaDonBLL.ThongKeHDTheoThang(thang, nam);
                dgvThongKe.DataSource = dt;
                if (dt != null)
                {
                    dgvThongKe.DataSource = dt;
                }
                else
                {
                    lbTDBanNhieuNhat.Text = "Null";
                    lbTUBanNhieuNhat.Text = "Null";
                    lbDVTDBanNhieuNhat.Text = "Null";
                    lbDVTUBanNhieuNhat.Text = "Null";
                    lbSLTDBanNhieuNhat.Text = "0";
                    lbSLTUBanNhieuNhat.Text = "0";
                    lbTongDoanhThu.Text = "0";
                    lbSoLuongKhachDen.Text = "0";
                }
                ThongKe(dt);
                if (dt.Rows.Count > 0)
                    flag = 2;
                else
                    flag = 0;
            }
            catch
            {
                MessageBox.Show("Mời chọn tháng cần thống kê!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bt_Thongkekhoangngay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = Dtp_TkTungay.Value;
                DateTime denNgay = Dtp_Tkdenngay.Value;

                DataTable dt = HoaDonBLL.ThongKeHDTheoKhoangNgay(tuNgay, denNgay);
                if (dt != null)
                {
                    dgvThongKe.DataSource = dt;
                }
                else
                {
                    lbTDBanNhieuNhat.Text = "Null";
                    lbTUBanNhieuNhat.Text = "Null";
                    lbDVTDBanNhieuNhat.Text = "Null";
                    lbDVTUBanNhieuNhat.Text = "Null";
                    lbSLTDBanNhieuNhat.Text = "0";
                    lbSLTUBanNhieuNhat.Text = "0";
                    lbTongDoanhThu.Text = "0";
                    lbSoLuongKhachDen.Text = "0";


                }
                dgvThongKe.DataSource = dt;
                ThongKe(dt);
                if (dt.Rows.Count > 0)
                    flag = 3;
                else
                    flag = 0;
            }
            catch
            {
                MessageBox.Show("Chưa chọn mốc ngày thống kê!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
    }
    }

