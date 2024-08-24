using DependencyInjection_Service.Models;
using DependencyInjection_Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DependencyInjection_Service.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private static readonly HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;
        
        private ISingletonService _singleton1;  //DI create singleton object 1 when application starts
        private ISingletonService _singleton2;  //--no object creation but reference--
        private IScopedService _scoped1;        //DI create scope object 1 per HTTP request
        private IScopedService _scoped2;        //--no object creation but reference--
        private ITransientService _transient1;  //DI create transient object 1 per reference
        private ITransientService _transient2;  //DI create transient object 2 per reference

        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger,

            ISingletonService singleton1,
            ISingletonService singleton2,
            IScopedService scoped1,
            IScopedService scoped2,
            ITransientService transient1,
            ITransientService transient2
        )
        {
            _logger = logger;
            _configuration = configuration;

            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Singleton()
        {
            this.ViewBag.singleton11 = _singleton1.GetGuid();
            this.ViewBag.singleton12 = _singleton1.GetGuid();
            this.ViewBag.singleton21 = _singleton2.GetGuid();
            this.ViewBag.singleton22 = _singleton2.GetGuid();
            return View("Singleton");
        }

        public IActionResult Scoped()
        {
            this.ViewBag.scoped11 = _scoped1.GetGuid();
            this.ViewBag.scoped12 = _scoped1.GetGuid();
            this.ViewBag.scoped21 = _scoped2.GetGuid();
            this.ViewBag.scoped22 = _scoped2.GetGuid();
            return View("Scoped");
        }

        [HttpPost]
        public async Task<IActionResult> GetScopedValues()
        {
            //"https://localhost:7292/api/ScopedValues"

            var AppSettingsLocalUrl = _configuration["AppSettings:LocalUrl"] ?? string.Empty;
            var environmentUrl = Environment.GetEnvironmentVariable("AppSettings:LocalUrl");
            var applicationUrl = environmentUrl ?? AppSettingsLocalUrl;
            applicationUrl += "/api/ScopedValues"; 

            var response1 = await client.GetAsync(applicationUrl);
            var content1 = await response1.Content.ReadAsStringAsync();

            var response2 = await client.GetAsync(applicationUrl);
            var content2 = await response2.Content.ReadAsStringAsync();

            return Json(new { scoped1 = content1, scoped2 = content2 });
        }

        public IActionResult Transient()
        {
            this.ViewBag.Transient11 = _transient1.GetGuid();   //this statement does not creates DI object
            this.ViewBag.Transient12 = _transient1.GetGuid();   //but only get object value
            this.ViewBag.Transient21 = _transient2.GetGuid();   //Transient21, Transient22 always same value
            this.ViewBag.Transient22 = _transient2.GetGuid();
            return View("Transient");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
