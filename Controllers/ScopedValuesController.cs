using DependencyInjection_Service.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace DependencyInjection_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopedValuesController : ControllerBase
    {
        private readonly IScopedService _scoped1;
        private readonly IScopedService _scoped2;

        public ScopedValuesController(IScopedService scoped1, IScopedService scoped2)
        {
            _scoped1 = scoped1;
            _scoped2 = scoped2;
        }

        [HttpGet]
        public IActionResult GetScopedValues()
        {
            var scoped1 = _scoped1.GetGuid();
            var scoped2 = _scoped2.GetGuid();

            return Ok(new { scoped1, scoped2 });
            //return Ok(new { scoped1 = _scoped1.GetGuid(), scoped2 = _scoped2.GetGuid() });
        }


    }
}
