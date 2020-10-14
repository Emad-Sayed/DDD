using Application.ReportsManagment.Queries.DashbordReport;
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

        [HttpGet]
        public async Task<IActionResult> Get(DashbordReportQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

    }
}
