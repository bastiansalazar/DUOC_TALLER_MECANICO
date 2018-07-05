using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class DetalleOrdenPedidoNEG
    {
        public List<DETALLE_ORDEN_PEDIDO> CargarlistaDetalleOrden(int idOrden)
        {
            try
            {
                DetalleOrdenPedidoDAL detalleOrdenPedidoDAL = new DetalleOrdenPedidoDAL();
                return detalleOrdenPedidoDAL.ListarDetalleOrdenPedido(idOrden);

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
