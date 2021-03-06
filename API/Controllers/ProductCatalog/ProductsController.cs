using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Configuration;
using Application.Common.Interfaces;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.DeleteProduct;
using Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit;
using Application.ProductCatalog.ProductAggregate.Commands.ImportProducts;
using Application.ProductCatalog.ProductAggregate.Commands.SyncProductsWithAlgolia;
using Application.ProductCatalog.ProductAggregate.Commands.UpdateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit;
using Application.ProductCatalog.ProductAggregate.Queries.ListProducts;
using Application.ProductCatalog.ProductAggregate.Queries.ListUnitsByProductsIds;
using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductCatalog
{
    [EnableCors()]
    [Authorize]
    [Route("api/" + nameof(Contexts.ProductCatalog) + "/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;
        public ProductsController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListProductsQuery query)
        {
            if(User.IsInRole("Distributor"))
                query.DistributorId = User?.FindFirstValue("BusinessUserId");

            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("GetUnitsByProductsIds")]
        public async Task<IActionResult> GetUnitsByProductsIds([FromQuery] ListUnitsByProductsIdsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("SyncProductsWithAlgolia")]
        public async Task<IActionResult> SyncProductsWithAlgolia(SyncProductsWithAlgoliaCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById([FromRoute] ProductByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("ImportProducts")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> ImportProducts([FromBody] ImportProductsCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{productId}/AddUnit")]
        public async Task<IActionResult> AddUnit([FromBody] AddUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPut("{productId}/UpdateUnit")]
        public async Task<IActionResult> UpdateUnit([FromBody] UpdateUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{productId}/DeleteUnit")]
        public async Task<IActionResult> DeleteUnit([FromQuery] DeleteUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}