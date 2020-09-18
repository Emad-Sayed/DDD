using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.UpdateArea
{
    public class UpdateAreaCommnad : IRequest<string>
    {
        public string CityId { get; set; }
        public string AreaId { get; set; }
        public string Name { get; set; }


        public class Handler : IRequestHandler<UpdateAreaCommnad, string>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<string> Handle(UpdateAreaCommnad request, CancellationToken cancellationToken)
            {
                var city = await _distributorRepository.FindCityByIdAsync(request.CityId);
                if (city == null) throw new CityNotFoundException(request.CityId);

                city.UpdateArea(request.AreaId, request.Name);

                _distributorRepository.UpdateCity(city);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return city.Id.ToString();
            }
        }
    }
}
