using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ControlRecepcionVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public string COMENTARIO { get; set; }
        public Nullable<System.DateTime> FECHA_APROVACION { get; set; }
        public Nullable<System.DateTime> FECHA_RECEPCION { get; set; }
        public int EMPLEADO_ID { get; set; }
        public int ESTADO_CONTROL_RECEPCION_ID { get; set; }
        public int ORDEN_PEDIDO_ID { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public string APELLIDO_EMPLEADO { get; set; }
        public Nullable<int> NUM_ID_EMPELADO { get; set; }
        public string ESTADO_CONTROL_RECEPCION { get; set; }
        public string FOLIO_PEDIDO { get; set; }
        public string PROVEEDOR { get; set; }
        public string DIV_ID_PROVEEDOR { get; set; }
        public Nullable<int> NUM_ID_PROVEEDOR { get; set; }
        public string SUCURSAL { get; set; }

    }
}
