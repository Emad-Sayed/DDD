using Application.Common.Interfaces;
using Application.OrderManagment.Commands.CancelOrder;
using Application.OrderManagment.Commands.ConfirmOrder;
using Application.OrderManagment.Commands.DeliverOrder;
using Application.OrderManagment.Commands.PlaceOrder;
using Application.OrderManagment.Commands.ShippOrder;
using Application.OrderManagment.Commands.UpdateOrder;
using Application.OrderManagment.Queries.CustomerOrders;
using Application.OrderManagment.Queries.ListOrders;
using Application.OrderManagment.Queries.OrderById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Order
{
    [EnableCors()]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;
        public OrdersController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListOrdersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("CustomerOrders/{customerId}")]
        public async Task<IActionResult> GetCustomerOrders([FromQuery] CustomerOrdersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("MyOrders")]
        public async Task<IActionResult> GetMyOrders([FromQuery] CustomerOrdersQuery query)
        {
            query.CustomerId = _currentUserService.UserId;
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("OrderDetails/{OrderId}")]
        public async Task<IActionResult> GetById(OrderByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("ShippOrder")]
        public async Task<IActionResult> ShippOrder([FromBody] ShippOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("DeliverOrder")]
        public async Task<IActionResult> DeliverOrder([FromBody] DeliverOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }


    }
}
