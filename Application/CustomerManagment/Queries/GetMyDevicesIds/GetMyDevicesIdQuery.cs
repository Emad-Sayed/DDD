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
    public class GetMyDevicesIdQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetMyDevicesIdQuery, string>
        {
            private readonly ICustomerRepository _customersRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper, ICurrentUserService currentUserService)
            {
                _customersRepository = customerRepository;
                _mapper = mapper;
                _currentUserService = currentUserService;
            }

            public async Task<string> Handle(GetMyDevicesIdQuery request, CancellationToken cancellationToken)
            {
                var devicesIDs = await _customersRepository.GetCustomerDevicesIDsByAccountId(_currentUserService.UserId);

                return devicesIDs;
            }
        }
    }
}
