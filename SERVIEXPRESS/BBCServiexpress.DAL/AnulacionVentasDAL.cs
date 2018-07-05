using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class AnulacionVentasDAL
    {
        public List<ANULACION_VENTA> ListaVentasAnuladas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ANULACION_VENTA
                              orderby a.VENTAS_ID descending
                              select a).ToList();
                return _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidaEstadoVenta(int idVenta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ANULACION_VENTA
                              where a.VENTAS_ID == idVenta
                              select a).FirstOrDefault();
                if (_query != null)
                    return "ANULADA";

                return "VIGENTE";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AnularVenta(int idVenta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                ANULACION_VENTA anular = new ANULACION_VENTA();
                anular.FECHA_CREACION = DateTime.Now;
                anular.FECHA_ULTIMO_UPDATE = DateTime.Now;
                anular.FECHA_ANULACION = DateTime.Now;
                anular.MOTIVO = "SIN ESPECIFICAR";
                anular.CLIENTE_ID = 1;
                anular.EMPLEADO_ID = 1;
                anular.VENTAS_ID = idVenta;

                var _query = (from a in con.ANULACION_VENTA
                              orderby a.ID descending
                              select a).FirstOrDefault();
                int id = 0;
                if (_query != null)
                    id = _query.ID;
                anular.ID = id + 1;
                con.ANULACION_VENTA.Add(anular);
                con.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
