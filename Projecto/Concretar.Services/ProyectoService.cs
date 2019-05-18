using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public List<ProyectoViewModel> GetAll()
        {
            var model = _uow.ProyectoRepository.All();
            var proyecto = new List<ProyectoViewModel>();
            foreach (var value in model)
            {
                proyecto.Add(new ProyectoViewModel { ProyectoId = value.ProyectoId, Nombre = value.Nombre, Ubicacion = value.Ubicacion, Dimencion = value.Dimencion, Precio = value.Precio, Descripcion = value.Descripcion });
            }
            return proyecto;
        }
        public void Create(ProyectoViewModel model)
        {
            var proyecto = new Proyecto()
            {
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                Dimencion = model.Dimencion,
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
                Dimencion = model.Dimencion,
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
            proyecto.Dimencion = model.Dimencion;
            proyecto.Precio = model.Precio;
            proyecto.Ubicacion = model.Ubicacion;
            _uow.ProyectoRepository.Update(proyecto);
            _uow.ProyectoRepository.Save();
        }
        public void Delete(int proyectoId)
        {
            var cliente = _uow.ProyectoRepository.Find(x => x.ProyectoId == proyectoId);
            _uow.ProyectoRepository.Delete(cliente);
            _uow.ProyectoRepository.Save();
        }

    }
}
