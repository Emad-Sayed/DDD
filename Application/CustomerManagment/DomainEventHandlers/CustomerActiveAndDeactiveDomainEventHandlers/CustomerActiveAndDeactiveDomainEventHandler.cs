using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.CustomerManagment.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class CustomerActiveAndDeactiveDomainEventHandler : INotificationHandler<CustomerActivedOrDeactived>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerActiveAndDeactiveDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CustomerActiveAndDeactiveDomainEventHandler(IConfiguration configuration, ILogger<CustomerActiveAndDeactiveDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _configuration = configuration;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(CustomerActivedOrDeactived notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CustomerActivedOrDeactived), _currentUserService.UserId, _currentUserService.Name, notification);
            HttpClient apiClient = new HttpClient();

            // TODO Set bearer toekn to http headers
            //apiClient.SetBearerToken(tokenResponse.AccessToken);


            var url = _configuration["IdentityServerAddress"] + "/api/Auth/ActiveAndDeactiveCustomer/" + notification.Customer.AccountId;
            
            var body = new { };
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await apiClient.PostAsync(url, data);

            if (response.StatusCode != HttpStatusCode.OK) throw new BusinessException(HttpStatusCode.ServiceUnavailable, "Error while deleting customer account", "delete_customer_account_error");
        }
    }
}
