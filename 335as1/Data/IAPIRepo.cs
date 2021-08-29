﻿using _335as1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Data {
    public interface IAPIRepo {

        IEnumerable<Staff> GetAllStaff();
        Staff GetStaffByID(int id);
        Staff AddStaff(Staff staff);
        IEnumerable<Product> GetItems();
        IEnumerable<Product> GetItems(string name);
        void DeleteStaff(int id);
        void SaveChanges();


    }
}
