using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Controllers
{
    public class CustomerController : Controller
    {
        string Baseurl = "https://localhost:44330/";

        #region "Get All Customers / Get All Existing Users"
        public async Task<IActionResult> Index()
        {
            List<CustomerViewModel> CustomerList = new List<CustomerViewModel>();
            RootObject result = new RootObject();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Customer");

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                    CustomerList = result.data.ToList();
                }

                return View(CustomerList);
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
            customer.CustomerTypeID = 2;

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
        public async Task<IActionResult> Login(CustomerViewModel customer)
        {
            int custTypeid = GetEnumValue(Convert.ToString(customer.CustomerTypeID));

            List<CustomerViewModel> CustomerList = new List<CustomerViewModel>();
            RootObject result = new RootObject();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string api = "api/Customer/" + customer.CustomerName + "/" + customer.Password;
                HttpResponseMessage Res = await client.GetAsync(api);

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                    CustomerList = result.data.ToList();

                    if(CustomerList.Count > 0)
                    {
                        return RedirectToAction("Home", "Index");
                    }
                    else
                    {
                        ViewBag.ShowAlert = true;
                    }
                }

                return View();
            }
        }
        #endregion

        #region "Forgot Password"
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            ViewBag.ShowAlert = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(CustomerModel customer)
        {
            //int custTypeid = GetEnumValue(Convert.ToString(forgetPassword.Email));

            List<CustomerViewModel> CustomerList = new List<CustomerViewModel>();
            RootObject result = new RootObject();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string api = "api/Customer/ValidateEmail/" + customer.Email;
                HttpResponseMessage Res = await client.GetAsync(api);

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                    CustomerList = result.data.ToList();

                    if (CustomerList.Count > 0)
                    {
                        return RedirectToAction("Home", "Index");
                    }
                    else
                    {
                        ViewBag.ShowAlert = true;
                    }
                }

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

        #region "Root Object"
        public class RootObject
        {
            public string status { get; set; }
            public CustomerViewModel[] data { get; set; }
        }
        #endregion

        #region "Get Customer Type Enum Value"
        public int GetEnumValue(string Type)
        {
           int enumInt = (int)Enum.Parse(typeof(CustomerType), Type);
           return enumInt;
        }
        #endregion
    }
}
