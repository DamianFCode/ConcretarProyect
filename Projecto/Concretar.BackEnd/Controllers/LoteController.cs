using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;

namespace Concretar.Backend.Controllers
{
    public class LoteController : CommonController
    {
        /*private readonly ILogger<LoteController> _logger;
        private readonly IConfiguration _configuration;
        public LoteController(ILogger<LoteController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }*/

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
