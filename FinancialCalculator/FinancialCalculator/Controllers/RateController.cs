using FinancialCalculator.StaticFiles;
using Microsoft.AspNetCore.Mvc;

namespace FinancialCalculator.Controllers
{
    [ApiController]
    [Route(ControllerConstants.GetRateRoute)]
    public class RateController : Controller
    {
        [HttpGet]
        public decimal Get()
        {
            return RateConstants.RateBase;
        }
    }
}