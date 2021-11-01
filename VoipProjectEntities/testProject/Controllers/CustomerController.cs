﻿using Microsoft.AspNetCore.Mvc;
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
            List<CustomerModel> UserList = new List<CustomerModel>();
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
                    UserList = result.data.ToList();
                }

                return View(UserList);
            }
        }
        #endregion
        
        public class RootObject
        {
            public string status { get; set; }
            public CustomerModel[] data { get; set; }
        }
    }
}
