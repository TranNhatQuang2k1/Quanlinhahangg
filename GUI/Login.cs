using QuanLiNhaHang.BLL;
using QuanLiNhaHang.DTO;
using QuanLiNhaHang.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void DangNhap()
        {
            if (tbTenDN.Text == "")
            {
                MessageBox.Show("Tên đang nhập không được rỗng!");
            }
            else if (tbMatKhau.Text == "")
            {
                MessageBox.Show("Mật khẩu không được rỗng!");
            }
            else
            {
                DataTable _ds = NhanVienBLL.LayDSNhanVienCoMK();
                 bool flag = false;
                 for (int i = 0; i < _ds.Rows.Count; i++)
                 {
                     if (tbTenDN.Text == _ds.Rows[i]["TenDN"].ToString() && tbMatKhau.Text == _ds.Rows[i]["MatKhau"].ToString())
                     {
                         frmMain frmM = new frmMain();
                         frmM.Nv = new NhanVienDTO(int.Parse(_ds.Rows[i]["MaNV"].ToString()), _ds.Rows[i]["HoTen"].ToString(), DateTime.Parse(_ds.Rows[i]["NgaySinh"].ToString()), _ds.Rows[i]["TenDN"].ToString(), _ds.Rows[i]["MatKhau"].ToString(), _ds.Rows[i]["Quyen"].ToString());
                         
                        frmM.Show();
                         this.Hide();
                         flag = true;
                     }
                 }
                 if (flag == false)
                 {
                     MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
                     tbMatKhau.Text = "";
                 }
                
             }
         }        
       private void Bt_DN_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void tbTenDN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbMatKhau.Focus();
            }
        }

        private void tbMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DangNhap();
            }
        }
    }
}

