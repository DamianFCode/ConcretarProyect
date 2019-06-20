using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concretar.Services
{
    public class CuotaService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();
        public CuotaService(ILogger logger)
        {
            _logger = logger;
        }

        public List<CuotaViewModel> GetCuotaByVenta(int ventaId)
        {
            var model = _uow.CuotaRepository.FilterIncluding(x => x.VentaId == ventaId, c => c.Venta);
            var cuotas = model.Select(x => new CuotaViewModel()
            {
                Confirmado = x.Confirmado,
                CuotaId = x.CuotaId,
                MontoMora = x.MontoMora,
                Mora = x.Mora,
                NumeroCuota = x.NumeroCuota.ToString(),
                Precio = x.Precio,
                SubTotal = x.SubTotal,
                TotalPagado = x.TotalPagado,
                VentaId = x.VentaId,
                Venta = x.Venta,
                FechaVencimiento = x.FechaVencimiento
            }).ToList();
            return cuotas;
        }

        public void PagarCuota (int cuotaId, int ventaId)
        {
            var cuota = _uow.CuotaRepository.Find(x => x.CuotaId == cuotaId && x.VentaId == x.VentaId);

            var pago = new Pago()
            {
                CuotaId = cuota.CuotaId,
                Fecha = DateTime.Now,
            };
            var pagoInsert = _uow.PagoRepository.Create(pago);
            _uow.PagoRepository.Save();
            if (pagoInsert != null)
            {
                cuota.Confirmado = true;
                _uow.CuotaRepository.Update(cuota);
                _uow.CuotaRepository.Save();
            }
        }
    }
}
