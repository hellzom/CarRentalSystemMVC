using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALSprint2.Models
{
   
    public class CarsTableModel
    {

        public int ID { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string CategoryName { get; set; }
        public int SeatingCapacity { get; set; }
        public string CarStatus { get; set; }
        public string FuelType { get; set; }
        public double CarPricePerHour { get; set; }

        


        public static List<Car> GetCarData()
        {
            CDBEntities db = new CDBEntities();
            {
                List<Car> cr = new List<Car>();
                        var items = db.Cars;

                        foreach (var s in items)
                        {
                    Car cr1 = new Car
                    {
                        ID=s.ID,
                        Brand = s.Brand,
                        ModelName=s.ModelName,
                        CategoryName=s.CategoryName,
                        SeatingCapacity=s.SeatingCapacity,
                        CarStatus=s.CarStatus,
                        FuelType=s.FuelType,
                        CarPricePerHour=s.CarPricePerHour
                        
                            };
                            
                            cr.Add(cr1);
                      }
                       return cr;
            }
        }


        public static Car GetCarData(int id)
        {
            CDBEntities db = new CDBEntities();
            Car obj = new Car();
            var data=db.Cars.FirstOrDefault(f => f.ID == id);

            if (data != null) {
                obj.ID = data.ID;
                obj.Brand = data.Brand;
                obj.ModelName = data.ModelName;
                obj.CategoryName = data.CategoryName;
                obj.SeatingCapacity = data.SeatingCapacity;
                obj.CarStatus = data.CarStatus;
                obj.FuelType = data.FuelType;
                obj.CarPricePerHour = data.CarPricePerHour;
            }
        return obj;
            }

       

        public static void CreateCar(Car fb)
        {
            using (CDBEntities db = new CDBEntities())
            {
                db.Cars.Add(fb);
                db.SaveChanges();
            }
        }

        public static void EditCar(Car fb)
        {
            using (CDBEntities db = new CDBEntities())
            {
                var data = db.Cars.FirstOrDefault(f => f.ID == fb.ID);
                data.Brand = fb.Brand;
                data.ModelName = fb.ModelName;
                data.CategoryName = fb.CategoryName;
                data.SeatingCapacity = fb.SeatingCapacity;
                data.CarStatus = fb.CarStatus;
                data.FuelType = fb.FuelType;
                data.CarPricePerHour = fb.CarPricePerHour;
                db.SaveChanges();
            }
        }
        public static void DeleteCar(int id)
        {
            using (CDBEntities db = new CDBEntities())
            {
                db.Cars.Remove(db.Cars.FirstOrDefault(f => f.ID == id));
                if(db.Rentals.Any(f=>f.CarID == id))
                {
                    db.Rentals.Remove(db.Rentals.FirstOrDefault(f => f.CarID == id));
                }
               
                db.SaveChanges();
            }
        }



    }
}
