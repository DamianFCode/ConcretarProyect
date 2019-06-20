using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public GridVentaModel GetAll (int rowPerPages, string DNI = null, string Lote = null, string Proyecto = null, int? page = null, int? rows = null)
        {
            var model = _uow.VentaRepository.AllIncluding(c => c.Cliente, p => p.Proyecto, l => l.Lote);
            var gridVenta = new GridVentaModel();
            if (!string.IsNullOrEmpty(DNI))
            {
                model = model.Where(x => x.Cliente.DNI.Contains(DNI));
            }
            if (!string.IsNullOrEmpty(Lote))
            {
                model = model.Where(x => x.Lote.Nombre.Contains(Lote));
            }
            else
            {
                if (!string.IsNullOrEmpty(Proyecto))
                {
                    model = model.Where(x => x.Proyecto.Nombre.Contains(Proyecto));
                }
            }
            var totalRows = model.Count();
            model = model.Skip((page - 1 ?? 0) * (rows ?? rowPerPages)).Take(rows ?? rowPerPages);
            gridVenta.TotalRows = totalRows;

            gridVenta.ListVentas = model.Select(x => new VentaViewModel()
            {
                VentaId = x.VentaId,
                Cliente = x.Cliente,
                Lote = x.Lote,
                Proyecto = x.Proyecto,
                ClienteId = x.ClienteId,
                LoteId = x.LoteId,
                CantidadCuotas = x.CantidadCuotas,
                Fecha = x.Fecha,
                ProyectoId = x.ProyectoId,
                Interes = x.Interes
            }).ToList();
            return gridVenta;
        }
        public void Create(VentaViewModel model)
        {
            var venta = new Venta();
            if (model.ProyectoId.HasValue)
            {
                venta = new Venta()
                {
                    Interes = model.Interes,
                    CantidadCuotas = model.CantidadCuotas,
                    Anticipo = model.Anticipo,
                    ClienteId = model.ClienteId,
                    ProyectoId = model.ProyectoId,
                    Fecha = DateTime.Now
                };
            }
            else
            {
                venta = new Venta()
                {
                    Interes = model.Interes,
                    CantidadCuotas = model.CantidadCuotas,
                    Anticipo = model.Anticipo,
                    ClienteId = model.ClienteId,
                    LoteId = model.LoteId,
                    Fecha = DateTime.Now
                };
            }
            var ultimaventa = _uow.VentaRepository.Create(venta);
            _uow.VentaRepository.Save();

            for (int i = 1; i <= model.CantidadCuotas; i++)
            {
                var cuota = new Cuota()
                {
                    VentaId = ultimaventa.VentaId,
                    NumeroCuota = i,
                    Estado = "PENDIENTE",
                    Precio = model.Precio,
                    FechaVencimiento = Convert.ToDateTime(model.FechaVencimiento).AddMonths(i)
                };
                _uow.CuotaRepository.Create(cuota);
                _uow.CuotaRepository.Save();
            }
        }

        public VentaViewModel GetById (int ventaId)
        {
            var model = _uow.VentaRepository.Find(x => x.VentaId == ventaId);
            return new VentaViewModel()
            {
                VentaId = model.VentaId
            };
        }
    }
}
