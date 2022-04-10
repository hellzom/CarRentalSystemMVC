using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALSprint2.Models
{
   public class AdminTableModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string AdminEmail { get; set; }

        public static List<AdminRecord> GetAdminData()
        {
            CDBEntities db = new CDBEntities();
            {
                List<AdminRecord> cr = new List<AdminRecord>();
                var items = db.AdminRecords;

                foreach (var s in items)
                {
                    AdminRecord cr1 = new AdminRecord
                    {
                        ID = s.ID,
                        Username=s.Username,
                        Pass=s.Pass,
                        AdminEmail=s.AdminEmail

                    };

                    cr.Add(cr1);
                }
                return cr;
            }
        }

        public static int AdminLogin(AdminRecord adm)
        {
            int flag = 0;
            CDBEntities db = new CDBEntities();

            var ad = db.AdminRecords;
            foreach (var c in ad)
            {
                if (c.AdminEmail == adm.AdminEmail && c.Pass == adm.Pass)
                {
                    flag = 1;
                }

            }
            return flag;

        }

        public static AdminRecord GetAdminDatabyEmail(string Email)
        {

            CDBEntities db = new CDBEntities();

            AdminRecord obj = new AdminRecord();
            var data = db.AdminRecords.FirstOrDefault(f => f.AdminEmail == Email);
            if (data != null)
            {
                obj.ID = data.ID;
                obj.Username = data.Username;
                obj.AdminEmail = data.AdminEmail;
                obj.Pass = data.Pass;
         
            }

            return obj;

        }


        public static void EditAdminCredentials(int id, AdminRecord fb)
        {
            using (CDBEntities db = new CDBEntities())
            {
                var data = db.AdminRecords.FirstOrDefault(f => f.ID == id);
                data.Username = fb.Username;
                data.Pass = fb.Pass;
                db.SaveChanges();
            }
        }
    }
}
