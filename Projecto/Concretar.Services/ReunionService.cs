using Concretar.Entities;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concretar.Services
{
    public class ReunionService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public ReunionService(ILogger logger)
        {
            _logger = logger;
        }

        public GridReunionModel GetAll(int rowPerPages, string FechaCreacionDesde = null, string FechaCreacionHasta = null, int? page = null, int? rows = null)
        {
            var model = _uow.ReunionRepository.All();
            var gridReunionModel = new GridReunionModel();

            if (!String.IsNullOrEmpty(FechaCreacionDesde) || !String.IsNullOrEmpty(FechaCreacionHasta))
            {
                var dateCreateFrom = new DateTime();
                var dateCreateTo = new DateTime();
                if (FechaCreacionDesde == FechaCreacionHasta)
                {
                    var fechaDesde = Convert.ToDateTime(FechaCreacionDesde);
                    model = model.Where(x => x.Fecha == fechaDesde);
                }
                if (!String.IsNullOrEmpty(FechaCreacionDesde))
                {
                    dateCreateFrom = Convert.ToDateTime(FechaCreacionDesde);
                    model = model.Where(x => x.Fecha >= dateCreateFrom);
                }
                if (!String.IsNullOrEmpty(FechaCreacionHasta))
                {
                    dateCreateTo = Convert.ToDateTime(FechaCreacionHasta);
                    model = model.Where(x => x.Fecha <= dateCreateTo);
                }
            }

            var totalRows = model.Count();
            model = model.Skip((page - 1 ?? 0) * (rows ?? rowPerPages)).Take(rows ?? rowPerPages);
            gridReunionModel.TotalRows = totalRows;

            var reunion = model.Select(x => new ReunionViewModel()
            {
                Cliente = x.Cliente,
                ClienteId = x.ClienteId,
                Usuario = x.Usuario,
                UsuarioId = x.UsuarioId,
                Fecha = x.Fecha,
                Motivo = x.Motivo,
                Resultado = x.Resultado,
                ReunionId = x.ReunionId
            });
            gridReunionModel.ListReuniones = reunion.ToList();

            return gridReunionModel;
        }

        public void Create (ReunionViewModel model)
        {
            try
            {
                var cliente = _uow.ClienteRepository.Find(x => x.ClienteId == model.ClienteId);
                var usuario = _uow.UsuarioRepository.Find(x => x.UsuarioId == model.UsuarioId);
                var reunion = new Reunion()
                {
                    ClienteId = model.ClienteId,
                    Fecha = model.Fecha,
                    Motivo = model.Motivo,
                    Resultado = model.Resultado,
                    UsuarioId = model.UsuarioId,
                    Cliente = cliente,
                    Usuario = usuario
                };
                _uow.ReunionRepository.Create(reunion);
                _uow.ReunionRepository.Save();
                _logger.LogInformation("Reunion creada correctamente");
            }
            catch(Exception e)
            {
                _logger.LogError("Ocurrio un error al crear la reunion. Error {0}", e);
                throw;
            }
        }

        public ReunionViewModel GetById(int id)
        {
            try
            {
                var model = _uow.ReunionRepository.Find(x => x.ReunionId == id);
                
                var reunion = new ReunionViewModel()
                {
                    Cliente = model.Cliente,
                    ClienteId = model.ClienteId,
                    Fecha = model.Fecha,
                    Motivo = model.Motivo,
                    Resultado = model.Resultado,
                    ReunionId = model.ReunionId,
                    Usuario = model.Usuario,
                    UsuarioId = model.UsuarioId
                };
                _logger.LogInformation("Reunion obtenida correctamente");
                return reunion;
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener la reunion. Error {0}", e);
                throw;
            }
        }

        public void Edit (ReunionViewModel model)
        {
            try
            {
                var reunion = _uow.ReunionRepository.Find(x => x.ReunionId == model.ReunionId);
                reunion.Fecha = model.Fecha;
                reunion.ClienteId = model.ClienteId;
                reunion.UsuarioId = model.UsuarioId;
                reunion.Motivo = model.Motivo;
                reunion.Resultado = model.Resultado;
                _uow.ReunionRepository.Update(reunion);
                _uow.ReunionRepository.Save();
                _logger.LogInformation("Reunion editada correctamente");
            }
            catch(Exception e)
            {
                _logger.LogError("Ocurrio un error al editar la reunion. Error {0}", e);
                throw;
            }
        }

        public void Delete (int id)
        {
            try
            {
                var reunion = _uow.ReunionRepository.Find(x => x.ReunionId == id);
                _uow.ReunionRepository.Delete(reunion);
                _uow.ReunionRepository.Save();
                _logger.LogInformation("Reunion eliminada correctamente");
            }
            catch(Exception e)
            {
                _logger.LogError("Ocurrio un error al eliminar la reunion. Error {0}", e);
                throw;
            }
        }
        public IQueryable<ReunionViewModel> GetByMonth()
        {
            var model = _uow.ReunionRepository.All();
            model = model.Where(x => x.Fecha.Month == DateTime.Now.Month);
            var reunion = model.Select(x => new ReunionViewModel()
            {
                Cliente = x.Cliente,
                Fecha = x.Fecha,
                Motivo = x.Motivo
            });
            return reunion;
        }
    }
}
