using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class PDF_ModeloControlRecepcion
    {
        public string Folio;
        public string NombreProveedor;
        public string RolProveedor;
        public string FechaRecepcion;
        public string EstadoControl;
        public string Comentario;
        public List<DETALLE_CONTROL_RECEPCION> DetalleControlRecepcion = new List<DETALLE_CONTROL_RECEPCION>();
        public string NombreSucursal;
        public string CodigoEmpleado;
        public string NombreEmpleado;
    }
}
