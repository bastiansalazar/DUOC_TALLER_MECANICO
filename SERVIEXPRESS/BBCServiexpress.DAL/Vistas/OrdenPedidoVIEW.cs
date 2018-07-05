using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class OrdenPedidoVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public Nullable<decimal> CANTIDAD_TOTAL { get; set; }
        public Nullable<System.DateTime> FECHA_ENTREGA { get; set; }
        public Nullable<decimal> MONTO_TOTAL { get; set; }
        public int EMPLEADO_ID { get; set; }
        public int ESTADO_ORDEN_PEDIDO_ID { get; set; }
        public int MULTI_MONEDA_ID { get; set; }
        public int PROVEEDOR_ID { get; set; }
        public int SUCURSAL_ID { get; set; }
        public int TIPO_ORDEN_PEDIDO_ID { get; set; }
        public string EMAIL_PROVEEDOR { get; set; }
        public string EMAIL_SUCURSAL { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public string APELLIDO_EMPLEADO { get; set; }
        public Nullable<int> NUMID_PROVEEDOR { get; set; }
        public string DIVID_PROVEEDOR { get; set; }
        public Nullable<int> NUMID_EMPLEADO { get; set; }
        public string DIVID_EMPLEADO { get; set; }
        public string ESTADO_ORDEN_PEDIDO { get; set; }
        public string MULTI_MONEDA { get; set; }
        public string PROVEEDOR { get; set; }
        public string SUCURSAL { get; set; }
        public string TIPO_ORDEN_PEDIDO { get; set; }
        public Nullable<decimal> VALOR_MULTIMONEDA { get; set; }
    }
}
