﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSPService
    {
        public List<LoaiSP> GetAll()
        {
            QuanLySPDB context = new QuanLySPDB();
            return context.LoaiSP.ToList();
        }
    }
}
