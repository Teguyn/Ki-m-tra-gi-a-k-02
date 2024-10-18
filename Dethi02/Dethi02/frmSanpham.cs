using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace Dethi02
{
    public partial class frmSanpham : Form
    {
        QuanLySPDB context = new QuanLySPDB();
        private readonly SanphamService sanphamService = new SanphamService();
        private readonly LoaiSPService loaiSPService = new LoaiSPService();
        public frmSanpham()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            try
            {
                setListViewStyle(lvSanpham);
                var listLoaiSP = loaiSPService.GetAll();
                var listSanpham = sanphamService.GetAll();
                FillFalcultyCombobox(listLoaiSP);
                BindListView(listSanpham);
                btnLuu.Visible = false;
                btnKLuu.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindListView(List<Sanpham> listSanpham)
        {
            lvSanpham.Items.Clear();
            foreach (var item in listSanpham)
            {
                ListViewItem lvItem = new ListViewItem(item.MaSP);  
                lvItem.SubItems.Add(item.TenSP);                 

             
                lvItem.SubItems.Add(item.Ngaynhap.HasValue ? item.Ngaynhap.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""); 

                lvItem.SubItems.Add(item.LoaiSP != null ? item.LoaiSP.TenLoai : ""); 

                lvSanpham.Items.Add(lvItem); 
            }
        }

        private void FillFalcultyCombobox(List<LoaiSP> listFacultys)
        {
            listFacultys.Insert(0, new LoaiSP());
            this.cboLoaiSp.DataSource = listFacultys;
            this.cboLoaiSp.DisplayMember = "TenLoai";
            this.cboLoaiSp.ValueMember = "MaLoai";
        }

        private void setListViewStyle(ListView lvSanpham)
        {

            lvSanpham.View = View.Details; 
            lvSanpham.FullRowSelect = true; 
            lvSanpham.GridLines = true; 
            lvSanpham.Columns.Clear();  

            // Thêm cột vào ListView
            lvSanpham.Columns.Add("Mã SP", 100, HorizontalAlignment.Left);    
            lvSanpham.Columns.Add("Tên SP", 200, HorizontalAlignment.Left);   
            lvSanpham.Columns.Add("Ngày Nhập", 150, HorizontalAlignment.Left);
        }

        private void lvSanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvSanpham.SelectedItems[0];
                txtMaSP.Text = selectedItem.SubItems[0].Text; 
                txtTenSP.Text = selectedItem.SubItems[1].Text; 
                dtNgaynhap.Value = Convert.ToDateTime(selectedItem.SubItems[2].Text); 
                cboLoaiSp.SelectedIndex = cboLoaiSp.FindStringExact(selectedItem.SubItems[3].Text); 
            }
        }

        private void cboLoaiSp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text == "" || txtTenSP.Text == "" || dtNgaynhap.Value == null || cboLoaiSp.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Sanpham sv = new Sanpham();
                sv.MaSP = txtMaSP.Text;
                sv.TenSP = txtTenSP.Text;
                sv.Ngaynhap = dtNgaynhap.Value;
                sv.MaLoai = cboLoaiSp.SelectedValue.ToString();

                if (sanphamService.GetAll().Any(s => s.MaSP == sv.MaSP))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại! Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaSP.Focus();
                }
                else
                {
                    sanphamService.Add(sv);
                    BindListView(sanphamService.GetAll());
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtNgaynhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var result = MessageBox.Show("Bạn có muốn xoá không?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string maSP = lvSanpham.SelectedItems[0].SubItems[0].Text;
                    sanphamService.Delete(maSP);
                    BindListView(sanphamService.GetAll());
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text == "" || txtTenSP.Text == "" || dtNgaynhap.Value == null || cboLoaiSp.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
     if (lvSanpham.SelectedItems.Count > 0)
            {
                string maSP = lvSanpham.SelectedItems[0].SubItems[0].Text;
                Sanpham sp = sanphamService.GetById(maSP);
                txtMaSP.Text = sp.MaSP;
                txtTenSP.Text = sp.TenSP;
                dtNgaynhap.Value = sp.Ngaynhap.Value;
                cboLoaiSp.SelectedValue = sp.MaLoai;

                btnLuu.Visible = true;
                btnKLuu.Visible = true;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnLuu.Visible = false;
                btnKLuu.Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            btnLuu.Visible = false;
            btnKLuu.Visible = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count > 0)
            {
                string maSP = lvSanpham.SelectedItems[0].SubItems[0].Text;
                Sanpham sp = sanphamService.GetById(maSP);
                sp.MaSP = txtMaSP.Text;
                sp.TenSP = txtTenSP.Text;
                sp.Ngaynhap = dtNgaynhap.Value;
                sp.MaLoai = cboLoaiSp.SelectedValue.ToString();

                sanphamService.Update(sp);
                BindListView(sanphamService.GetAll());
                MessageBox.Show("Lưu thành công!");
                btnLuu.Visible = false;
                btnKLuu.Visible = false;
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count > 0)
            {
                string maSP = lvSanpham.SelectedItems[0].SubItems[0].Text;
                Sanpham sp = sanphamService.GetById(maSP);
                txtMaSP.Text = sp.MaSP;
                txtTenSP.Text = sp.TenSP;
                dtNgaynhap.Value = sp.Ngaynhap.Value;
                cboLoaiSp.SelectedValue = sp.MaLoai;

                MessageBox.Show("Không Lưu thành công!");
                btnLuu.Visible = false;
                btnKLuu.Visible = false;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {

            string keyword = txtTim.Text;
            var listStudents = sanphamService.TimSinhVien(keyword);
            BindListView(listStudents);
        }
    }
}
