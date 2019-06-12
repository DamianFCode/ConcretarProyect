using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concretar.Services
{
    public class ProyectoService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public ProyectoService(ILogger logger)
        {
            _logger = logger;
        }
        public GridProyectoModel GetAll(int rowPerPages, string nombre = null, int? page = null, int? rows = null)
        {
            var model = _uow.ProyectoRepository.All();
            var gridProyectoModel = new GridProyectoModel();
            if (!string.IsNullOrEmpty(nombre))
            {
                model = model.Where(x => x.Nombre.Contains(nombre));
            }
            
            var totalRows = model.Count();
            model = model.Skip((page - 1 ?? 0) * (rows ?? rowPerPages)).Take(rows ?? rowPerPages);
            gridProyectoModel.TotalRows = totalRows;
            var proyecto = model.Select(x => new ProyectoViewModel()
            {
                ProyectoId = x.ProyectoId,
                Nombre = x.Nombre,
                Ubicacion = x.Ubicacion,
                Dimension = x.Dimension,
                Precio = x.Precio,
            });
            gridProyectoModel.ListProyecto = proyecto.ToList();

            return gridProyectoModel;
        }
        public IEnumerable<SelectListItem> GetProyectoDropDown(string selectedId = null)
        {
            var proyecto = _uow.ProyectoRepository.All().OrderBy(x => x.Nombre).ToList();

            var listproyecto = proyecto.Select(x => new SelectListItem()
            {
                Selected = (x.ProyectoId.ToString() == selectedId),
                Text = x.Nombre,
                Value = x.ProyectoId.ToString()
            }).OrderBy(x => x.Text);

            return listproyecto;
        }

        public void Create(ProyectoViewModel model)
        {
            var proyecto = new Proyecto()
            {
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                Dimension = model.Dimension,
                Precio = model.Precio,
                Descripcion = model.Descripcion
            };
            _uow.ProyectoRepository.Create(proyecto);
            _uow.ProyectoRepository.Save();
        }
        public ProyectoViewModel GetById(int id)
        {
            var model = _uow.ProyectoRepository.Find(x => x.ProyectoId == id);
            var proyecto = new ProyectoViewModel()
            {
                ProyectoId = model.ProyectoId,
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                Dimension = model.Dimension,
                Precio = model.Precio,
                Descripcion = model.Descripcion
            };
            return proyecto;
        }
        public void Edit(ProyectoViewModel model)
        {
            var proyecto = _uow.ProyectoRepository.Find(x => x.ProyectoId == model.ProyectoId);
            proyecto.Nombre = model.Nombre;
            proyecto.Descripcion = model.Descripcion;
            proyecto.Dimension = model.Dimension;
            proyecto.Precio = model.Precio;
            proyecto.Ubicacion = model.Ubicacion;
            _uow.ProyectoRepository.Update(proyecto);
            _uow.ProyectoRepository.Save();
        }
        public void Delete(int proyectoId)
        {
            var proyecto = _uow.ProyectoRepository.Find(x => x.ProyectoId == proyectoId);
            _uow.ProyectoRepository.Delete(proyecto);
            _uow.ProyectoRepository.Save();
        }

    }
}
