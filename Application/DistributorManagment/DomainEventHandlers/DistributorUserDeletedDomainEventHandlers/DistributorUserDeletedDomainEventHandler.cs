using Domain.Common.Exceptions;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorUserDeletedDomainEventHandlers
{
    public class DistributorUserDeletedDomainEventHandler : INotificationHandler<DistributorUserDeleted>
    {
        private readonly IConfiguration _configuration;

        public DistributorUserDeletedDomainEventHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Handle(DistributorUserDeleted notification, CancellationToken cancellationToken)
        {
            HttpClient apiClient = new HttpClient();

            // TODO Set bearer toekn to http headers
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            var url = _configuration["IdentityServerAddress"] + "/api/Auth/DeleteUser?userId=" + notification.DistributorUser.AccountId;

            var response = await apiClient.DeleteAsync(url);

            if (response.StatusCode != HttpStatusCode.OK) throw new RestException(HttpStatusCode.ServiceUnavailable, new { Customer = "Error while deleting customer account", code = "delete_customer_account_error" });
        }
    }
}
