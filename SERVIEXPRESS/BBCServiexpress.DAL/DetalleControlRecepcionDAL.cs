using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class DetalleControlRecepcionDAL
    {
        public List<DETALLE_CONTROL_RECEPCION> ListarDetalleControlRecepcion(int idControl)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.DETALLE_CONTROL_RECEPCION
                              where a.CONTROL_RECEPCION_ID == idControl
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
