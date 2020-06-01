using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Commands.AddItemToVan;
using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Commands.RemoveItemFromVan;
using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Queries.CurrentCustomerVan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ShoppingVan
{
    [Authorize]
    [EnableCors("AllowOrigin")]
    [Route("api/" + nameof(Contexts.ShoppingVan) + "/[controller]")]
    public class ShoppingVanController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;
        public ShoppingVanController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CurrentCustomerVanQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToVan([FromBody]AddItemToVanCommand command)
        {
            var uid = _currentUserService.UserId;

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItemFromVan([FromBody]RemoveItemFromVanCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}