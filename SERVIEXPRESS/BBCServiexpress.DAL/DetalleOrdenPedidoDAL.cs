using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class DetalleOrdenPedidoDAL
    {
        public List<DETALLE_ORDEN_PEDIDO> ListarDetalleOrdenPedido(int orden)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.DETALLE_ORDEN_PEDIDO
                                 where a.ORDEN_PEDIDO_ID == orden
                              orderby a.ID ascending
                              select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
