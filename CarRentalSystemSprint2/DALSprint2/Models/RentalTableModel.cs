using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALSprint2.Models
{
    public class ViewModel
    {
        public Car car { get; set; }
        public Rental rental { get; set; }
        public Customer customer { get; set; }
    }
    public class RentalTableModel
    {

        public int ID { get; set; }
        public int CarID { get; set; }
        public int UserID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Price { get; set; }

        public string BookingStatus { get; set; }





        public static List<Rental> GetRentalData()
        {
            CDBEntities db = new CDBEntities();
            {
                List<Rental> rt = new List<Rental>();
                var items = db.Rentals;

                foreach (var s in items)
                {
                    Rental cr1 = new Rental
                    {
                        ID = s.ID,
                        StartDateTime = s.StartDateTime,
                        EndDateTime = s.EndDateTime,
                       
                        Price = s.Price

                    };

                rt.Add(cr1);
                }
                return rt;
            }
        }


        //public static List<Rental> GetRentalData(int UserID)
        //{
        //    CDBEntities db = new CDBEntities();
        //    List<Rental> rt = new List<Rental>();
        //    var items = db.Rentals.Where(f=> f.UserID == UserID);

        //    foreach (var s in items)
        //    {
        //        Rental cr1 = new Rental
        //        {
        //            ID = s.ID,
        //            CarID = s.CarID,
        //            UserID = s.UserID,
        //            StartDateTime = s.StartDateTime,
        //            EndDateTime = s.EndDateTime,
        //            Price = s.Price,
        //            BookingStatus = s.BookingStatus

        //        };

        //        rt.Add(cr1);
        //    }

           
        //    return rt;

        //}


        public static List<ViewModel> GetRentalData(int UserID)
        {
            CDBEntities db = new CDBEntities();
            //List<Rental> rt = new List<Rental>();
            //var items = db.Rentals.Where(f => f.UserID == UserID);

            //List<Car> cars = db.Cars.ToList();
            //List<Rental> rentals = db.Rentals.ToList();
            var data = from c in db.Cars
                       join r in db.Rentals on c.ID equals r.CarID where r.UserID == UserID
                       orderby r.ID descending
                       select new ViewModel
                       {
                           car = c,
                           rental = r
                       };
            var  viewM = data.ToList();

            
            return viewM;  

        }

        public static List<ViewModel> GetRentalDataAdmin()
        {
            CDBEntities db = new CDBEntities();
            //List<Rental> rt = new List<Rental>();
            //var items = db.Rentals.Where(f => f.UserID == UserID);

            //List<Car> cars = db.Cars.ToList();
            //List<Rental> rentals = db.Rentals.ToList();
            var data = from c in db.Cars
                       join r in db.Rentals on c.ID equals r.CarID into table1
                       from r in table1.ToList()
                       join i in db.Customers on r.UserID equals i.ID orderby r.ID descending



                       select new ViewModel
                       {
                           car = c,
                           rental = r,
                           customer = i
                       };
            var viewM = data.ToList();


            return viewM;

        } 
        
        public static List<ViewModel> GetUserRentalDataAdmin(int id)
        {
            CDBEntities db = new CDBEntities();
            //List<Rental> rt = new List<Rental>();
            //var items = db.Rentals.Where(f => f.UserID == UserID);

            //List<Car> cars = db.Cars.ToList();
            //List<Rental> rentals = db.Rentals.ToList();
            var data = from c in db.Cars
                       join r in db.Rentals on c.ID equals r.CarID into table1
                       from r in table1.ToList()
                       join i in db.Customers on r.UserID equals i.ID
                       where r.UserID == id
                       orderby r.ID descending

                       select new ViewModel
                       {
                           car = c,
                           rental = r,
                           customer = i
                       };
            var viewM = data.ToList();


            return viewM;

        }

        public static void BookCar(int CarID, Rental r)
        {
            using (CDBEntities db = new CDBEntities())
            {
                var cardata = db.Cars.FirstOrDefault(f => f.ID == CarID);
                cardata.CarStatus = "Rented";
                db.Rentals.Add(r);
                db.SaveChanges();
            }
        }

        public static void ReturnCar(int CarID,int RentalID)
        {
            using (CDBEntities db = new CDBEntities())
            {
                var cardata = db.Cars.FirstOrDefault(f => f.ID == CarID);
                var rentaldata = db.Rentals.FirstOrDefault(f => f.ID == RentalID);
                rentaldata.BookingStatus = "Over";
                cardata.CarStatus = "Available";
                db.SaveChanges();
            }
        }


        //public static void Edit(int id, Rental fb)
        //{
        //    using (CDBEntities db = new CDBEntities())
        //    {
        //        var data = db.Rentals.FirstOrDefault(f => f.ID == id);
        //        data.StartDateTime= fb.StartDateTime;
        //        data.EndDateTime = fb.EndDateTime;
        //        data.Price = fb.Price;
        //        db.SaveChanges();
        //    }
        //}
        //public static void Delete(int id)
        //{
        //    using (CDBEntities db = new CDBEntities())
        //    {
        //        db.Rentals.Remove(db.Rentals.FirstOrDefault(f => f.ID == id));
        //        db.SaveChanges();
        //    }
        //}

    }
}
