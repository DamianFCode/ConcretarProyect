using Concretar.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Concretar.Services
{
    public class PagoService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();
        public PagoService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
