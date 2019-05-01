using Microsoft.Extensions.Logging;
using Concretar.Entities;
using Concretar.Helper.Exceptions;
using Concretar.Helper.Extensions;
using Concretar.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Concretar.Services
{
    public class ParametroService : IService<ParametroModel>
    {
        readonly UnitOfWork _uow = new UnitOfWork();
        private readonly ILogger _logger;

        public ParametroService(ILogger logger)
        {
            _logger = logger;
        }

        public void Create(ParametroModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ParametroModel model)
        {
            try
            {
                var parametro = _uow.ParametroRepository.Find(x => x.ParametroId == model.ParametroId);
                parametro.Valor = model.Valor;
                _uow.ParametroRepository.Update(parametro);
                _uow.ParametroRepository.Save();
                _logger.LogInformation("Parámetro con ID <{0}> editado exitosamente.", model.ParametroId);
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un problema al intentar editar el Parámetro con ID <{0}>.", model.ParametroId);
                throw new UpdateRecordException("Ocurrió un error al modificar el parámetro", ex);
            }
        }

        public List<ParametroModel> GetAll()
        {
            var parametros = _uow.ParametroRepository.All().ToList();

            var parametrosModel = parametros.Select(x => new ParametroModel()
            {
                ParametroId = x.ParametroId,
                Categoria = x.Categoria,
                Clave = x.Clave,
                Valor = x.Valor,
                Descripcion = x.Descripcion
            }).ToList();           
            return parametrosModel;

        }

        public ParametroModel GetById(int id)
        {
            try
            {
                var parametro = _uow.ParametroRepository.Find(x => x.ParametroId == id);

                var parametroModel = new ParametroModel
                {
                    Categoria = parametro.Categoria,
                    Clave = parametro.Clave,
                    Valor = parametro.Valor,
                    Descripcion = parametro.Descripcion,
                    ParametroId = parametro.ParametroId
                };
                _logger.LogInformation("Parámetro con ID <{0}> obtenido exitosamente.", id);
                return parametroModel;
            }
            catch(Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un problema al intentar obtener el Parámetro con ID <{0}>.", id, ex.Message);
                throw new GetRecordException("Ocurrió un error al obtener el parámetro", ex);
            }
        }

    }
}
