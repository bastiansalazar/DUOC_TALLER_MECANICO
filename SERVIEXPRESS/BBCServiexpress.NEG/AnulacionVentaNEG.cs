using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL;

namespace BBCServiexpress.NEG
{
    public class AnulacionVentaNEG
    {
        public List<ANULACION_VENTA> ListaVentasAnuladas()
        {
            try
            {
                AnulacionVentasDAL datos = new AnulacionVentasDAL();
                return datos.ListaVentasAnuladas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
