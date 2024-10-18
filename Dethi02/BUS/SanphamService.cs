using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SanphamService
    {
        public void Add(Sanpham sp)
        {
            QuanLySPDB context = new QuanLySPDB();
            context.Sanpham.Add(sp);
            context.SaveChanges();
        }


        public void Delete(string maSP)
        {
            QuanLySPDB context = new QuanLySPDB()
;           Sanpham sp = context.Sanpham.Find(maSP);
            if (sp != null)
            {
                context.Sanpham.Remove(sp);
                context.SaveChanges();
            }    
        }

        public List<Sanpham> GetAll()
        {
            QuanLySPDB context = new QuanLySPDB();
            return context.Sanpham.ToList();
        }

        public Sanpham GetById(string maSP)
        {

            QuanLySPDB context = new QuanLySPDB();
            return context.Sanpham.Find(maSP);
        }

        public List<Sanpham> TimSinhVien(string keyword)
        {
            QuanLySPDB context =new QuanLySPDB();
            return context.Sanpham.Where(sv => sv.MaSP.Contains(keyword) || sv.TenSP.Contains(keyword)).ToList();
        }

        public void Update(Sanpham sp)
        {
            QuanLySPDB context = new QuanLySPDB();
            Sanpham oldSv = context.Sanpham.Find(sp.MaSP);
            if (oldSv != null)
            {
                oldSv.MaSP = sp.MaSP;
                oldSv.TenSP = sp.TenSP;
                oldSv.Ngaynhap = sp.Ngaynhap;
                oldSv.MaLoai = sp.MaLoai;
                context.SaveChanges();
            }
        }
    }

}


