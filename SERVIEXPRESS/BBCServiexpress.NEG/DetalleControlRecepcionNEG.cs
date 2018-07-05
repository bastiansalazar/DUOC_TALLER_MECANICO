using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class DetalleControlRecepcionNEG
    {
        public List<DETALLE_CONTROL_RECEPCION> ListarDetalleControlRecepcion(int idControl)
        {
            try
            {
                DetalleControlRecepcionDAL detalle = new DetalleControlRecepcionDAL();
                return detalle.ListarDetalleControlRecepcion(idControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
