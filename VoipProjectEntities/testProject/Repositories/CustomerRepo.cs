using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        string Baseurl = "https://localhost:44330/";

        #region "Get Customer Type Enum Value"
        public int GetEnumValue(string Type)
        {
            int enumInt = (int)Enum.Parse(typeof(CustomerType), Type);
            return enumInt;
        }
        #endregion

        #region "Get All Customers"
        public List<CustomerModel> GetAllCustomers()
        {
            try
            {
                List<CustomerModel> CustomerList = new List<CustomerModel>();

                HttpClient HC = new HttpClient();
                RootObject result = new RootObject();

                var insertedRecord = HC.GetAsync(Baseurl + "api/Customer");

                insertedRecord.Wait();

                var results = insertedRecord.Result;

                if (results.IsSuccessStatusCode)
                {
                    var UserResponse = results.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                    CustomerList = result.data.ToList();
                }

                HC.Dispose();
                return CustomerList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Validate Login"
        public Task<List<CustomerModel>> ValidateLogin(CustomerModel customer)
        {
            string api = "api/Customer/" + customer.CustomerName + "/" + customer.Password;
            Task<List<CustomerModel>> CustomerList = GetCustomerList(customer, api);

            return CustomerList;
        }
        #endregion

        #region "Forgot Password"
        public Task<List<CustomerModel>> ForgotPassword(CustomerModel customer)
        {
            string api = "api/Customer/ValidateEmail/" + customer.Email;
            Task<List<CustomerModel>> CustomerList = GetCustomerList(customer, api);       

            return CustomerList;
        }
        #endregion

        #region "Common Method - Get Customer List For Customer Model"
        public async Task<List<CustomerModel>> GetCustomerList(CustomerModel customer, string api)
        {
            try
            {
                List<CustomerModel> CustomerList = new List<CustomerModel>();
                RootObject result = new RootObject();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync(api);

                    if (Res.IsSuccessStatusCode)
                    {
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;

                        result = JsonConvert.DeserializeObject<RootObject>(UserResponse);
                        CustomerList = result.data.ToList();
                    }

                    return CustomerList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "Root Object"
        public class RootObject
        {
            public string status { get; set; }
            public CustomerModel[] data { get; set; }
        }
        #endregion
    }
}
