using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Helper
{
    public class FunctionsHelper
    {
        public static decimal ConvertToDecimal(string monto)
        {
            try
            {
                var montoToInt = int.Parse(monto);
                var total = string.Format("{0:N2}", (decimal)montoToInt / 100);
                var montoTotal = decimal.Parse(total);
                return montoTotal;
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error al convertir a decimal el monto <" + monto + ">. Error {0}", e);
            }
        }
    }
}
