using _335as2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _335as2.Data {
    public interface IAPIRepo {
        IEnumerable<Staff> GetAllStaff();
        Staff GetStaffByID(int id);
        Staff AddStaff(Staff staff);
        User GetUserByPassword(string password);
        IEnumerable<Product> GetItems();
        IEnumerable<Product> GetItems(string name);
        public bool ValidLogin(string userName, string password);
        bool Register(User user);
        void DeleteStaff(int id);
        void SaveChanges();
    }
}
