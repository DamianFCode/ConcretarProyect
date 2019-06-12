using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concretar.Services
{
    public class LoteService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public LoteService(ILogger logger)
        {
            _logger = logger;
        }
        public GridLoteModel GetAll(int rowPerPages, string nombre = null, int? page = null, int? rows = null)
        {
            var model = _uow.LoteRepository.All();
            var gridLoteModel = new GridLoteModel();
            if (!string.IsNullOrEmpty(nombre))
            {
                model = model.Where(x => x.Nombre.Contains(nombre));
            }
            
            var totalRows = model.Count();
            model = model.Skip((page - 1 ?? 0) * (rows ?? rowPerPages)).Take(rows ?? rowPerPages);
            gridLoteModel.TotalRows = totalRows;
            var lote = model.Select(x => new LoteViewModel()
            {
                LoteId = x.LoteId,
                Nombre = x.Nombre,
                Ubicacion = x.Ubicacion,
                Dimension = x.Dimension,
                Precio = x.Precio,
            });
            gridLoteModel.ListLote = lote.ToList();

            return gridLoteModel;
        }
        public void Create(LoteViewModel model)
        {
            var lote = new Lote()
            {
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                Dimension = model.Dimension,
                Precio = model.Precio,
                Descripcion = model.Descripcion,
                ProyectId = model.ProyectId
            };
            _uow.LoteRepository.Create(lote);
            _uow.LoteRepository.Save();
        }
        public LoteViewModel GetById(int id)
        {
            var model = _uow.LoteRepository.Find(x => x.LoteId == id);
            var lote = new LoteViewModel()
            {
                LoteId = model.LoteId,
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                Dimension = model.Dimension,
                Precio = model.Precio,
                Descripcion = model.Descripcion,
                ProyectId = model.ProyectId
            };
            return lote;
        }
        public void Edit(LoteViewModel model)
        {
            var lote = _uow.LoteRepository.Find(x => x.LoteId == model.LoteId);
            lote.Nombre = model.Nombre;
            lote.Descripcion = model.Descripcion;
            lote.Dimension = model.Dimension;
            lote.Precio = model.Precio;
            lote.Ubicacion = model.Ubicacion;
            lote.ProyectId = model.ProyectId;
            _uow.LoteRepository.Update(lote);
            _uow.LoteRepository.Save();
        }
        public void Delete(int loteId)
        {
            var lote = _uow.LoteRepository.Find(x => x.LoteId == loteId);
            _uow.LoteRepository.Delete(lote);
            _uow.LoteRepository.Save();
        }


    }
}
