using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testProject.Models;
using testProject.Repositories;

namespace testProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo repo;
        public CustomerController(ICustomerRepo _repo)
        {
            repo = _repo;
        }

        #region "Get All Customers / Get All Existing Users"
        public IActionResult Index()
        {
            List<CustomerModel> CustomerList = repo.GetAllCustomers();
            ViewBag.ShowAlert = false;

            if (CustomerList.Count > 0)
            {
                return View(CustomerList);
            }
            else
            {
               return  ViewBag.ShowAlert = true;
            }
        }
        #endregion

        #region "Sign Up"
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(CustomerModel customer)
        {
            //customer.CustomerTypeID = 2;

            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44330/api/Customer");

            var insertedRecord = HC.PostAsJsonAsync<CustomerModel>("Customer", customer);
            insertedRecord.Wait();

            var recordDisplay = insertedRecord.Result;

            if (recordDisplay.IsSuccessStatusCode)
            {
                return RedirectToAction("Home", "Index");
            }

            return View();
        }
        #endregion

        #region "Login"
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ShowAlert = false;
            return View();
        }

        [HttpPost]
        public IActionResult Login(CustomerModel customer)
        {
            int custTypeid = repo.GetEnumValue(Convert.ToString(customer.CustomerTypeID));

            Task<List<CustomerModel>> CustomerList = repo.ValidateLogin(customer);

            if (CustomerList.Result.Count > 0)
            {
                return RedirectToAction("Home", "Index");
            }
            else
            {
                ViewBag.ShowAlert = true;
                return View();
            }
        }
        #endregion

        #region "Forgot Password"
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.ShowAlert = false;
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(CustomerModel customer)
        {
            Task<List<CustomerModel>> CustomerList = repo.ForgotPassword(customer);

            if (CustomerList.Result.Count > 0)
            {
                return RedirectToAction("Home", "Index");
            }
            else
            {
                ViewBag.ShowAlert = true;
                return View();
            }
        }
        #endregion

        #region "Create Menu Access"
        [HttpGet]
        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMenu(MenuAccessModel menuaccess)
        {
            //menuaccess.MenuAccessId = Guid.Parse("{BA0EB0EF-B69B-46FD-B8E2-41B4178AE725}");
            //menuaccess.MenuLink = 2;
            //menuaccess.IsAccess = false;
            //menuaccess.CreatedAt = DateTime.Today;
            //menuaccess.UpdatedAt = DateTime.Now.AddDays(1);
            //menuaccess.CustomerID = Guid.Parse("{fe98f549-e790-4e9f-aa16-18c2292a2ee9}");

            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44330/");

            var insertedRecord = HC.PostAsJsonAsync("api/Menu", menuaccess);
            insertedRecord.Wait();

            var recordDisplay = insertedRecord.Result;

            if (recordDisplay.IsSuccessStatusCode)
            {
                return RedirectToAction("Home", "Index");
            }

            return View();
        }
        #endregion       
    }
}
