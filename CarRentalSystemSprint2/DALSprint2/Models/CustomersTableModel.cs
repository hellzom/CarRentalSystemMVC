using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALSprint2.Models
{
    public class CustomersTableModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string LicenseNumber { get; set; }
        public string UserPassword { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public static List<Customer> GetCustomerData()
        {
            CDBEntities db = new CDBEntities();
            {
                List<Customer> cust = new List<Customer>();
                var items = db.Customers;

                foreach (var s in items)
                {
                    Customer cr1 = new Customer
                    {
                        ID = s.ID,
                        UserName=s.UserName,
                        LicenseNumber=s.LicenseNumber,
                       // UserPassword=s.UserPassword,
                        PhoneNumber=s.PhoneNumber,
                        Email=s.Email

                    };

                    cust.Add(cr1);
                }
                return cust;
            }
        }


        public static Customer GetCustomerData(int id)
        {

            CDBEntities db = new CDBEntities();

            Customer obj = new Customer();
            var data = db.Customers.FirstOrDefault(f => f.ID == id);
            if (data != null)
            {
                obj.ID = data.ID;
                obj.UserName = data.UserName;
                obj.LicenseNumber = data.LicenseNumber;
                obj.PhoneNumber = data.PhoneNumber;
                obj.Email = data.Email;
                obj.UserPassword = data.UserPassword;
            }

            return obj;

}
        public static Customer GetCustomerDatabyEmail(string Email)
        {

            CDBEntities db = new CDBEntities();

            Customer obj = new Customer();
            var data = db.Customers.FirstOrDefault(f => f.Email == Email);
            if (data != null)
            {
                obj.ID = data.ID;
                obj.UserName = data.UserName;
                obj.LicenseNumber = data.LicenseNumber;
                obj.PhoneNumber = data.PhoneNumber;
                obj.Email = data.Email;
            }

            return obj;

        }
        public static int UserLogin(Customer fb)
        {
            int flag = 0;
            CDBEntities db = new CDBEntities();

            // var items = db.Customers;
            //Customer cr = new Customer();
            //cr = db.Customers.Find(id);


            //return cr;
            var cust = db.Customers;
            foreach(var c in cust)
            {
                if(c.Email == fb.Email && c.UserPassword == fb.UserPassword)
                {
                    flag = 1;
                } 
                
            }
            //Customer obj = new Customer();
            //var data = db.Customers.Where(f => f.Email == fb.Email && f.UserPassword == fb.UserPassword).Count();
            
            return flag;

        }



        public static void CreateCustomer(Customer fb)
        {
            using (CDBEntities db = new CDBEntities())
            {
                db.Customers.Add(fb);
                db.SaveChanges();
            }
        }

        //public static Customer CreateCustomer(Customer fb)
        //{
        //    using (CDBEntities db = new CDBEntities())
        //    {
        //        db.Customers.Add(fb);
        //        db.SaveChanges();
        //        return fb;
        //    }
        //}



        public static void EditCustomer(int id, Customer fb)
        {
            using (CDBEntities db = new CDBEntities())
            {
                var data = db.Customers.FirstOrDefault(f => f.ID == id);
              //  data.ID = fb.ID;
                data.UserName = fb.UserName;
                data.LicenseNumber = fb.LicenseNumber;
                data.UserPassword = fb.UserPassword;
                data.PhoneNumber = fb.PhoneNumber;
                data.Email = fb.Email;
                db.SaveChanges();
            }
        }
        public static void DeleteCustomer(int id)
        {
            using (CDBEntities db = new CDBEntities())
            {
                db.Customers.Remove(db.Customers.FirstOrDefault(f => f.ID == id));
                db.SaveChanges();
            }
        }


    }
}
