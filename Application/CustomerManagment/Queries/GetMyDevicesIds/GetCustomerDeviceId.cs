using Application.Common.Interfaces;
using AutoMapper;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Queries.GetMyDevicesIds
{
    public class GetCustomerDeviceId : IRequest<string>
    {
        public string CustomerId { get; set; }
        public class Handler : IRequestHandler<GetCustomerDeviceId, string>
        {
            private readonly ICustomerRepository _customersRepository;

            public Handler(ICustomerRepository customerRepository)
            {
                _customersRepository = customerRepository;
            }

            public async Task<string> Handle(GetCustomerDeviceId request, CancellationToken cancellationToken)
            {
                var devicesIDs = await _customersRepository.GetCustomerDevicesID(request.CustomerId);

                return devicesIDs;
            }
        }
    }
}
