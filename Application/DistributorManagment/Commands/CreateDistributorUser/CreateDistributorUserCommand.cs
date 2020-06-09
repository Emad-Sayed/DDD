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


            public Handler(IDistributorRepository distributorRepository,IConfiguration configuration)
            {
                _distributorRepository = distributorRepository;
                _configuration = configuration;
            }

            public async Task<Unit> Handle(CreateDistributorUserCommand request, CancellationToken cancellationToken)
            {
                var distributor = await _distributorRepository.FindByIdAsync(request.DistributorId);
                if (distributor == null) throw new Exception("can't find Distributor");

                var accountId = await CreateUserAccountAsync(request);
                if (accountId == null) throw new Exception("Error while createing user account");

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
                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("error while creating the account");
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
    }
}
