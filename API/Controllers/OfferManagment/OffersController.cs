using System.Threading.Tasks;
using API.Configuration;
using Application.Common.Interfaces;
using Application.OffersManagment.Commands.AddProductToOffer;
using Application.OffersManagment.Commands.CreateOffer;
using Application.OffersManagment.Commands.DeleteOffer;
using Application.OffersManagment.Commands.RemoveProductFromOffer;
using Application.OffersManagment.Commands.ReOrderOffers;
using Application.OffersManagment.Commands.UpdateOffer;
using Application.OffersManagment.Queries.ListOffers;
using Application.OffersManagment.Queries.OfferById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OfferManagment
{
    [EnableCors()]
    [Authorize]
    [Route("api/" + nameof(Contexts.OfferManagment) + "/[controller]")]
    public class OffersController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;
        public OffersController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListOffersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{offerId}")]
        public async Task<IActionResult> GetById([FromRoute] OfferByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Post([FromBody] CreateOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery] DeleteOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("AddProductToOffer")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> AddProductToOffer([FromBody] AddProductToOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("RemoveProductFromOffer")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> RemoveProductFromOffer([FromBody] RemoveProductFromOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("ReOrder")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> ReOrder([FromBody] ReOrderOffersCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

    }
}
