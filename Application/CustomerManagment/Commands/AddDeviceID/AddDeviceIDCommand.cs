﻿using Application.Common.Interfaces;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.AddDeviceID
{
    public class AddDeviceIDCommand : IRequest<string>
    {
        public string DeviceId { get; set; }

        public class Handler : IRequestHandler<AddDeviceIDCommand, string>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly ICustomerRepository _customerRepository;

            public Handler(ICustomerRepository customerRepository, ICurrentUserService currentUserService)
            {
                _customerRepository = customerRepository;
                _currentUserService = currentUserService;
            }

            public async Task<string> Handle(AddDeviceIDCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetCustomerByAccountId(_currentUserService.UserId);
                if (customer == null) throw new CustomerNotFoundException(_currentUserService.UserId);

                customer.DevicesId = request.DeviceId;

                _customerRepository.Update(customer);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return customer.Id.ToString();
            }
        }
    }
}
