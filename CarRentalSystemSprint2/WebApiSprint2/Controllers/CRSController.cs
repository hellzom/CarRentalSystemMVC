using DALSprint2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
//using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Http.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;

namespace WebApiSprint2.Controllers
{
    //[EnableCors(origins: "https://localhost:7093","*","*")]
    public class CRSController : ApiController
    {
        //-----------Car Action Methods----------

        

        [HttpGet]
        [Route("api/GetCarData")]
        public IHttpActionResult Get()
        {
            //List<Car> cr = CarsTableModel.GetCarData().ToList();
            return Json(CarsTableModel.GetCarData());
        }

        [HttpGet]
        [Route("api/GetCarData")]
        public IHttpActionResult Get(int id)
        {
            //Car cr = CarsTableModel.GetCarData(id);
            return Json(CarsTableModel.GetCarData(id));
        }

        [HttpPost]
        [Route("api/CreateCar")]
        public string CreateCar([FromBody] Car car)
        {
            CarsTableModel.CreateCar(car);

            return $"{car.Brand}-{car.ModelName} Added Successfully";
        }

        [HttpPost]
        [Route("api/UserLogin")]
        public IHttpActionResult UserLogin([FromBody] Customer Cst)
        {
            int res = CustomersTableModel.UserLogin(Cst);
            if (res == 1)
            { //generate token
                var t = GetToken();
                return Json(t);
            }
            return Json(0); 
        }

        //[HttpPost]
        //[Route("api/CreateCar")]
        //public string Post([FromBody] Car cr)
        //{
        //    CarsTableModel.CreateCar(cr);

        //    return $"{cr.Brand}-{cr.ModelName} Added Successfully";
        //}

        [HttpPost]
        [Route("api/EditCar")]
        public string Editcar([FromBody]Car cr)
        {
            CarsTableModel.EditCar(cr);

            return "Record Updated Successfully";
        }

        [HttpPost]
        [Route("api/DeleteCar")]
        public string Delete(int id)
        {
            CarsTableModel.DeleteCar(id);
            return "Car Deleted";
        }


        //--------------Rental Action methods----------

        [HttpGet]
        [Route("api/GetRentalData")]
        public IHttpActionResult GetRentalData()
        {
           // List<Rental> r = RentalTableModel.GetRentalData().ToList();
            return Json(RentalTableModel.GetRentalData());
        }

        [HttpGet]
        [Route("api/GetRentalData")]
        public IHttpActionResult GetRentalData(int id)
        {
           // Rental r = RentalTableModel.GetRentalData(id);
            return Json(RentalTableModel.GetRentalData(id));
        }

        [HttpGet]
        [Route("api/GetRentalDataAdmin")]
        public IHttpActionResult GetRentalDataAdmin()
        {
            // Rental r = RentalTableModel.GetRentalData(id);
            return Json(RentalTableModel.GetRentalDataAdmin());
        }
        
        [HttpGet]
        [Route("api/GetUserRentalDataAdmin")]
        public IHttpActionResult GetUserRentalDataAdmin(int id)
        {
            // Rental r = RentalTableModel.GetRentalData(id);
            return Json(RentalTableModel.GetUserRentalDataAdmin(id));
        }

        [HttpPost]
        [Route("api/ReturnCar")]
        public string ReturnCar(int CarID,int RentalID)
        {
            RentalTableModel.ReturnCar(CarID, RentalID);
            return "Car Returned";
        }

        [HttpPost]
        [Route("api/BookCar")]
        public string BookCar(int CarID, [FromBody] Rental cr)
        {
            RentalTableModel.BookCar(CarID, cr);

            return "Booked Successfully";
        }

        //--------------- Customer Actions------------

        [HttpGet]
        [Route("api/GetCustomerData")]
        public IHttpActionResult GetCustomerData()
        {
            //List<Car> cr = CarsTableModel.GetCarData().ToList();
            return Json(CustomersTableModel.GetCustomerData());
        }

        [HttpGet]
        [Route("api/GetCustomerData")]
        public IHttpActionResult GetCustomerData(int id)
        {
           // Customer cr = CustomersTableModel.GetCustomerData(id);
              return Json(CustomersTableModel.GetCustomerData(id));
        }

        [HttpGet]
        [Route("api/GetCustomerDatabyEmail")]
        public IHttpActionResult GetCustomerDatabyEmail(string Email)
        
        {
            //var identity = User.Identity as ClaimsIdentity;
            // Customer cr = CustomersTableModel.GetCustomerData(id);
            if (User.Identity.IsAuthenticated)
            {
               
                return Json(CustomersTableModel.GetCustomerDatabyEmail(Email));
            }
            else
            {
                return Json("Invalid JWT");
            }
            
        }

        [HttpPost]
        [Route("api/CreateCustomer")]
        public string CreateCustomer([FromBody] Customer cr)
        {
            CustomersTableModel.CreateCustomer(cr);

            return ( $"{cr.UserName} Added Successfully");
        }

        [HttpPost]
        [Route("api/EditCustomer")]
        public string EditCustomer(int id, [FromBody]Customer cr)
        {
            CustomersTableModel.EditCustomer(id, cr);

            return "Record Updated Successfully";
        }

        [HttpDelete]
        [Route("api/DeleteCustomer")]
        public string DeleteCustomer(int id)
        {
            CustomersTableModel.DeleteCustomer(id);
            return "Record Deleted";
        }


        //-------------------------Admin Actions-----------------
        [HttpGet]
        [Route("api/GetAdminData")]
        public IHttpActionResult GetAdminData()
        {
            return Json(AdminTableModel.GetAdminData());
        }

        [HttpPost]
        [Route("api/EditAdminCredentials")]
        public string EditAdminCredentials(int id, [FromBody]AdminRecord cr)
        {
            AdminTableModel.EditAdminCredentials(id, cr);

            return "Record Updated Successfully";
        }

        [HttpGet]
        [Route("api/GetAdminDatabyEmail")]
        public IHttpActionResult GetAdminDatabyEmail(string Email)

        {
            //var identity = User.Identity as ClaimsIdentity;
            // Customer cr = CustomersTableModel.GetCustomerData(id);
         

                return Json(AdminTableModel.GetAdminDatabyEmail(Email));
            

        }

        [HttpPost]
        [Route("api/AdminLogin")]
        public int AdminLogin([FromBody] AdminRecord Ad)
        {
            int res = AdminTableModel.AdminLogin(Ad);
            if (res == 1)
            { 
                return 1;
            }
            else
            {
                return 0;
            }
            
        }



        //JWT IMPLEMENTATION
        [HttpGet]
        [Route("api/GetToken")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345";
            var issuer = "http://mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "rajdeep"));

            var token = new JwtSecurityToken(issuer,
           issuer,
           permClaims,
           expires: DateTime.Now.AddDays(1),
           signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }

        [HttpPost]
        [Route("api/GetName1")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/GetName2")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }

            return null;
        }


    }
}
