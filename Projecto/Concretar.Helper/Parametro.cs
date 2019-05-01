using Concretar.Entities;
using System;

namespace Concretar.Helper
{
    public static class Parametro
    {
        public static string GetValue(string NameKey)
        {
            try
            {
                var uow = GetUOW();
                var valor = uow.ParametroRepository.Find(x => x.Clave == NameKey).Valor;
                return valor;
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No se encontró la key {0}", NameKey) + " en la tabla Parámetro.");
            }
        }

        static UnitOfWork GetUOW()
        {
            return new UnitOfWork();
        }
    }
}
