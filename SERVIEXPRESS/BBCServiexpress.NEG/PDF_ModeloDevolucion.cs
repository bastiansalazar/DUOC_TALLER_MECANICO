using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class PDF_ModeloDevolucion
    {
        public string Sucursal;
        public string Folio;
        public string NombreProveedor;
        public string RolProveedor;
        public string Motivo;
        public List<DETALLE_ORDEN_PEDIDO> DetalleOrdenPedido = new List<DETALLE_ORDEN_PEDIDO>();
        public string EmailProveedor;
        public string EmailSucursal;
    }
}
