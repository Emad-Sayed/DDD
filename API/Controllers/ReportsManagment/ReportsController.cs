using Application.ReportsManagment.Queries.BrandPerformanceReport;
using Application.ReportsManagment.Queries.DashbordReport;
using Application.ReportsManagment.Queries.DistributorPerformanceReport;
using Application.ReportsManagment.Queries.ProductPerformanceReport;
using Application.ReportsManagment.Queries.RetailerPerformanceReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.ReportsManagment
{
    [Authorize]
    [EnableCors()]
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        public ReportsController()
        {
        }

        [HttpGet("DashbordReport")]
        public async Task<IActionResult> DashbordReport(DashbordReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("CustomerPerformanceReport")]
        public async Task<IActionResult> CustomerPerformanceReport(CustomerPerformanceReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("ProductPerformanceReport")]
        public async Task<IActionResult> ProductPerformanceReport(ProductPerformanceReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TopSellingProductsPerArea")]
        public async Task<IActionResult> TopSellingProductsPerArea(TopSellingProductsPerAreaQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TopSellingProductsPerCity")]
        public async Task<IActionResult> TopSellingProductsPerCity(TopSellingProductsPerCityQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TopSellingProductsPerQuantity")]
        public async Task<IActionResult> TopSellingProductsPerQuantity(TopSellingProductsPerQuantityQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TopSellingProductsPerDistributor")]
        public async Task<IActionResult> TopSellingProductsPerDistributor(TopSellingProductsPerDistributorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TopSellingBrandsPerDistributor")]
        public async Task<IActionResult> TopSellingBrandsPerDistributor(TopSellingBrandsPerDistributorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("BrandPerformanceReport")]
        public async Task<IActionResult> BrandPerformanceReport(BrandPerformanceReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("DistributorPerformanceReport")]
        public async Task<IActionResult> DistributorPerformanceReport(DistributorPerformanceReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
