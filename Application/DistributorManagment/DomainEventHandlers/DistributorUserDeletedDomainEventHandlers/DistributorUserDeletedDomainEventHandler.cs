using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DistributorUserDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorUserDeletedDomainEventHandler(IConfiguration configuration, ILogger<DistributorUserDeletedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _configuration = configuration;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DistributorUserDeleted notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorUserDeleted), _currentUserService.UserId, _currentUserService.Name, notification);
            HttpClient apiClient = new HttpClient();

            // TODO Set bearer toekn to http headers
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            var url = _configuration["IdentityServerAddress"] + "/api/Auth/DeleteUser?userId=" + notification.DistributorUser.AccountId;

            var response = await apiClient.DeleteAsync(url);

            if (response.StatusCode != HttpStatusCode.OK) throw new BusinessException(HttpStatusCode.ServiceUnavailable, "Error while deleting customer account", "delete_customer_account_error");
        }
    }
}
