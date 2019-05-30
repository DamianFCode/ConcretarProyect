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
    public class ClienteService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public ClienteService(ILogger logger)
        {
            _logger = logger;
        }
        public GridClienteModel GetAll(int rowPerPages, string nombre = null, string apellido = null, string dni = null, int? page = null, int? rows = null)
        {
            var model = _uow.ClienteRepository.All();
            var gridClienteModel = new GridClienteModel();
            if (!string.IsNullOrEmpty(nombre))
            {
                model = model.Where(x => x.Nombre == nombre);
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                model = model.Where(x => x.Apellido == apellido);
            }
            if (!string.IsNullOrEmpty(dni))
            {
                model = model.Where(x => x.DNI == dni);
            }

            var totalRows = model.Count();
            model = model.Skip((page - 1 ?? 0) * (rows ?? rowPerPages)).Take(rows ?? rowPerPages);
            gridClienteModel.TotalRows = totalRows;

            var cliente = model.Select(x => new ClienteViewModel()
            {
                Apellido = x.Apellido,
                ClienteId = x.ClienteId,
                Correo = x.Correo,
                DNI = x.DNI,
                Domicilio = x.Domicilio,
                Edad = x.Edad,
                FechaNacimiento = Convert.ToDateTime(x.FechaNacimiento.ToString("dd/MM/yyyy")),
                Nombre = x.Nombre,
                NumeroDomicilio = x.NumeroDomicilio,
                Observacion = x.Observacion,
                Telefono = x.Telefono
            });
            gridClienteModel.ListClientes = cliente.ToList();

            return gridClienteModel;
        }
        public void Create(ClienteViewModel model)
        {
            var cliente = new Cliente()
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Edad = !string.IsNullOrEmpty(model.Edad) ? model.Edad : GetEdad(model.FechaNacimiento).ToString(),
                DNI = model.DNI,
                FechaNacimiento = model.FechaNacimiento.Date,
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
                DNI = model.DNI,
                FechaNacimiento = Convert.ToDateTime(model.FechaNacimiento.Date.ToString("dd/MM/yyyy")),
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
            cliente.Edad = !string.IsNullOrEmpty(model.Edad) ? model.Edad : GetEdad(model.FechaNacimiento).ToString();
            cliente.FechaNacimiento = model.FechaNacimiento;
            cliente.Correo = model.Correo;
            cliente.Telefono = model.Telefono;
            cliente.Domicilio = model.Domicilio;
            cliente.NumeroDomicilio = model.NumeroDomicilio;
            cliente.Observacion = model.Observacion;
            cliente.DNI = model.DNI;
            _uow.ClienteRepository.Update(cliente);
            _uow.ClienteRepository.Save();
        }
        public void Delete(int clienteId)
        {
            var cliente = _uow.ClienteRepository.Find(x => x.ClienteId == clienteId);
            _uow.ClienteRepository.Delete(cliente);
            _uow.ClienteRepository.Save();
        }

        public int GetEdad(DateTime date)
        {
            var today = DateTime.Today;
            var age = today.Year - date.Year;
            if (today.DayOfWeek < date.Date.DayOfWeek) {
                age = age - 1;
            }
            age = today.Year - date.Year;

            return age;
        }

        public IEnumerable<SelectListItem> GetClientesDropDown(string selectedId = null)
        {
            var clientes = _uow.ClienteRepository.All().OrderBy(x => x.Nombre).ToList();
            var listClientes = clientes.Select(x => new SelectListItem()
            {
                Selected = (x.ClienteId.ToString() == selectedId),
                Text = x.Nombre + " " + x.Apellido,
                Value = x.ClienteId.ToString()
            }).OrderBy(x => x.Text);

            return listClientes;
        }
    }
}
