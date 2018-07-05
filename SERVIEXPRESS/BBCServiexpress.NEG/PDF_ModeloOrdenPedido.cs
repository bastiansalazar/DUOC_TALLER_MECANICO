using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class PDF_ModeloOrdenPedido
    {
        public string Folio;
        public string NombreProveedor;
        public string RolProveedor;
        public string FechaSolicitud;
        public string FechaModificacion;
        public string Sucursal;
        public string Direccion;
        public string Telefono;
        public string EmailSucursal;
        public List<DETALLE_ORDEN_PEDIDO> DetalleOrdenPedido = new List<DETALLE_ORDEN_PEDIDO>();
        public string Moneda;
        public string CostoTotal;
        public string ConstoMoneda;
        public string CodigoEmpleado;
        public string NombreEMpleado;
        public string EmailProveedor;

    }
}
