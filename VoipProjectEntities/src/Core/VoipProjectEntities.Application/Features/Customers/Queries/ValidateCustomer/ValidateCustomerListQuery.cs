using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VoipProjectEntities.Application.Features.Customers.Queries.GetCustomerList;
using VoipProjectEntities.Application.Responses;

namespace VoipProjectEntities.Application.Features.Customers.Queries.ValidateCustomer
{
    public class ValidateCustomerListQuery : IRequest<Response<IEnumerable<CustomerListVm>>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
