﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit;
using Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit;
using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Application.ProductCatalog.Products.Commands.DeleteProduct;
using Application.ProductCatalog.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductCatalog
{
    [EnableCors("AllowOrigin")]
    [Route("api/" + nameof(Contexts.ProductCatalog) + "/[controller]")]
    public class ProductsController : BaseController
    {

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById([FromQuery]ProductByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]DeleteProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{productId}/AddUnit")]
        public async Task<IActionResult> AddUnit([FromBody]AddUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{productId}/UpdateUnit")]
        public async Task<IActionResult> UpdateUnit([FromBody]UpdateUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{productId}/DeleteUnit")]
        public async Task<IActionResult> DeleteUnit([FromQuery]DeleteUnitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}