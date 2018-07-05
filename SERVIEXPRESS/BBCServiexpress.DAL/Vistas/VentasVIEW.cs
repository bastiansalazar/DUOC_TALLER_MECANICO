using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class VentasVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public Nullable<int> CANTIDAD_TOTAL { get; set; }
        public Nullable<System.DateTime> FECHA_VENTA { get; set; }
        public Nullable<decimal> MONTO_TOTAL { get; set; }
        public int CLIENTE_ID { get; set; }
        public int EMPLEADO_ID { get; set; }
        public int MULTI_MONEDA_ID { get; set; }
        public int SUCURSAL_ID { get; set; }
        public int TIPO_VENTA_ID { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string APELLID_CLIENTE { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public string APELLIDO_EMPLEADO { get; set; }
        public string TIPO_MONEDA { get; set; }
        public string NOMBRE_SUCURSAL { get; set; }
        public string TIPO_VENTA { get; set; }
    }
}
