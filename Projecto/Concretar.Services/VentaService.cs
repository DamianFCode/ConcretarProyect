using Concretar.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services
{
    public class VentaService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();
        public VentaService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
