using Application.NotificationManagment.Commands.ReadNotification;
using Application.NotificationManagment.Queries.GetMyNotifications;
using Application.NotificationManagment.Queries.GetMyNotificationsCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.NotificationManagment
{
    [EnableCors()]
    [Route("api/[controller]")]
    public class NotificationsController : BaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMyNotificationsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("NotificationsCounter")]
        public async Task<IActionResult> Get([FromQuery] GetMyNotificationsCounterQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [Authorize]
        [HttpPost("ReadNotification")]
        public async Task<IActionResult> Post([FromBody] ReadNotificationCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

    }
}
