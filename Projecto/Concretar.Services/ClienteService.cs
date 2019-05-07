using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services
{
    public class ClienteService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public ClienteService(ILogger logger)
        {
            _logger = logger;
        }
        public List<ClienteViewModel> GetAll()
        {
            var model = _uow.ClienteRepository.All();
            var marca = new List<ClienteViewModel>();
            foreach (var value in model)
            {
                marca.Add(new ClienteViewModel { ClienteId = value.ClienteId, Nombre = value.Nombre, Apellido = value.Apellido, Telefono = value.Telefono, Correo = value.Correo });
            }
            return marca;
        }
        public void Create(ClienteViewModel model)
        {
            var cliente = new Cliente()
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Edad = model.Edad,
                FechaNacimiento = model.FechaNacimiento,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Domicilio = model.Domicilio,
                NumeroDomicilio = model.NumeroDomicilio,
                Observacion = model.Observacion
            };
            _uow.ClienteRepository.Create(cliente);
            _uow.ClienteRepository.Save();
        }

        public ClienteViewModel GetById(int id)
        {
            var model = _uow.ClienteRepository.Find(x => x.ClienteId == id);
            var marca = new ClienteViewModel()
            {
                ClienteId = model.ClienteId,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Edad = model.Edad,
                FechaNacimiento = model.FechaNacimiento,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Domicilio = model.Domicilio,
                NumeroDomicilio = model.NumeroDomicilio,
                Observacion = model.Observacion
            };
            return marca;
        }
        public void Edit(ClienteViewModel model)
        {
            var cliente = _uow.ClienteRepository.Find(x => x.ClienteId == model.ClienteId);
            cliente.Nombre = model.Nombre;
            cliente.Apellido = model.Apellido;
            cliente.Edad = model.Edad;
            cliente.FechaNacimiento = model.FechaNacimiento;
            cliente.Correo = model.Correo;
            cliente.Telefono = model.Telefono;
            cliente.Domicilio = model.Domicilio;
            cliente.NumeroDomicilio = model.NumeroDomicilio;
            cliente.Observacion = model.Observacion;

            _uow.ClienteRepository.Update(cliente);
            _uow.ClienteRepository.Save();
        }

    }
}
