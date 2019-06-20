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
    }
}
