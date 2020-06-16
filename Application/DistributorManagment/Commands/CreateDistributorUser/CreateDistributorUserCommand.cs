using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.CreateDistributorUser
{
    public class CreateDistributorUserCommand : IRequest
    {
        public string DistributorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<CreateDistributorUserCommand>
        {
            private readonly IDistributorRepository _distributorRepository;
            private readonly IConfiguration _configuration;


            public Handler(IDistributorRepository distributorRepository, IConfiguration configuration)
            {
                _distributorRepository = distributorRepository;
                _configuration = configuration;
            }

            public async Task<Unit> Handle(CreateDistributorUserCommand request, CancellationToken cancellationToken)
            {
                var distributor = await _distributorRepository.FindByIdAsync(request.DistributorId);
                if (distributor == null) if (distributor == null) throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Distributor = $"Distributor with id {request.DistributorId} not found ", code = "distributor_notfound" });


                var accountId = await CreateUserAccountAsync(request);


                distributor.CreateUser(accountId, request.FullName, request.Email);

                _distributorRepository.Update(distributor);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }

            private async Task<string> CreateUserAccountAsync(CreateDistributorUserCommand request)
            {
                HttpClient apiClient = new HttpClient();
                var body = new
                {
                    request.FullName,
                    request.Email
                };

                var json = JsonConvert.SerializeObject(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = _configuration["IdentityServerAddress"] + "/api/Auth/RegisterDistributor";

                var response = await apiClient.PostAsync(url, data);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Distributor = $"An error occurs while creating distributor account /" + responseString, code = "distributor_account_error" });
                return responseString;
            }
        }
    }
}
