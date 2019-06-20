using System;
using System.ComponentModel;
using System.Reflection;

namespace Concretar.Helper
{
    public class EstadosHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public enum UsuarioDefault : int
        {
            [Description("Administrador General")]
            ADM = 1
        }

        public enum EstadoSolicitudProducto : int
        {
            [Description("No contactado")]
            NO_CONTACTADO = 1,
            [Description("Contactado")]
            CONTACTADO = 2,
            [Description("Imposible de contactar")]
            IMPOSIBLE_CONTACTAR = 3
        }

        public enum TipoPedido : int
        {
            [Description("Baja")]
            BAJA = 1,
            [Description("Pedido de póliza")]
            PEDIDO_POLIZA = 2
        }

        public enum EstadoCuota : int
        {
            PENDIENTE = 0,
            PAGADO = 1,
            ANULADO = 2
        }
    }
}
