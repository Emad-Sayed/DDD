using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductCatalog
{
    [EnableCors("AllowOrigin")]
    [Route("api/" + nameof(Contexts.ProductCatalog) + "/[controller]")]
    public class ProductCategoriesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProductCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}