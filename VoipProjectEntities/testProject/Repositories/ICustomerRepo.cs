using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Repositories
{
    public interface ICustomerRepo
    {
        int GetEnumValue(string Type);
        Task<List<CustomerModel>> ValidateLogin(CustomerModel customer);
        List<CustomerModel> GetAllCustomers();
        Task<List<CustomerModel>> ForgotPassword(CustomerModel customer);
    }
}
