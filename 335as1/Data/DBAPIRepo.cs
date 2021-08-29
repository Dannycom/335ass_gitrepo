﻿using _335as1.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Data {
    public class DBAPIRepo : IAPIRepo {

        private readonly APIDbContext _dbContext;

        public DBAPIRepo(APIDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Staff AddStaff(Staff staff) {
            EntityEntry<Staff> e = _dbContext.Staffs.Add(staff);
            Staff s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public void DeleteStaff(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Staff> GetAllStaff() {
            IEnumerable<Staff> staffs = _dbContext.Staffs.ToList<Staff>();
            return staffs;
        }

        public IEnumerable<Product> GetItems() {
            IEnumerable<Product> products = _dbContext.Products.ToList<Product>();
            return products;
        }

        public IEnumerable<Product> GetItems(string name) {
            IEnumerable<Product> products = _dbContext.Products.ToList<Product>();
            List<Product> productsWithName = new List<Product>();
            foreach(Product product in products) {
                if(product.Name.ToLower().Contains(name.ToLower())) {
                    productsWithName.Add(product);
                }
            }
            return productsWithName;
        }

        public Staff GetStaffByID(int id) {
            Staff staff = _dbContext.Staffs.FirstOrDefault(e => e.Id == id);
            return staff;
        }

        public void SaveChanges() {
            _dbContext.SaveChanges();
        }
    }
}
