using Domain.Common.Exceptions;
using Domain.CustomerManagment.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.DomainEventHandlers.CustomerDeletedDomainEventHandlers
{
    public class CustomerDeletedDomainEventHandler : INotificationHandler<CustomerDeleted>
    {
        private readonly IConfiguration _configuration;

        public CustomerDeletedDomainEventHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Handle(CustomerDeleted notification, CancellationToken cancellationToken)
        {
            HttpClient apiClient = new HttpClient();

            // TODO Set bearer toekn to http headers
            //apiClient.SetBearerToken(tokenResponse.AccessToken);


            var url = _configuration["IdentityServerAddress"] + "/api/Auth/DeleteUser?userId=" + notification.Customer.AccountId;

            var response = await apiClient.DeleteAsync(url);

            if (response.StatusCode != HttpStatusCode.OK) throw new BusinessException(HttpStatusCode.ServiceUnavailable, "Error while deleting customer account", "delete_customer_account_error");
        }
    }
}
