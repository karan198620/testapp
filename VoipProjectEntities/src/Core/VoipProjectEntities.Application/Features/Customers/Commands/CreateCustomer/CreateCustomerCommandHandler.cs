using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoipProjectEntities.Application.Contracts.Infrastructure;
using VoipProjectEntities.Application.Contracts.Persistence;
using VoipProjectEntities.Application.Models.Mail;
using VoipProjectEntities.Application.Responses;
using VoipProjectEntities.Domain.Entities;

namespace VoipProjectEntities.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<Guid>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository, IEmailService emailService, ILogger<CreateCustomerCommandHandler> logger)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<Response<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var validator = new CreateCustomerCommandValidator(_customerRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @customer = _mapper.Map<Customer>(request);

            @customer = await _customerRepository.AddAsync(@customer);

            //Sending email notification to admin address
            var email = new Email() { To = "gill@snowball.be", Body = $"A new event was created: {request}", Subject = "A new event was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                _logger.LogError($"Mailing about event {@customer.CustomerId} failed due to an error with the mail service: {ex.Message}");
            }

            var response = new Response<Guid>(@customer.CustomerId, "Inserted successfully ");

            _logger.LogInformation("Handle Completed");

            return response;
        }
    }
}
