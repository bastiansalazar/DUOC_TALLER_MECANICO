using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.NEG
{
    public class VentasNEG
    {
        public string EmitirVenta(VENTAS ventas, List<DETALLE_VENTAS> listaDetalle,string reservas)
        {
            try
            {
                VentasDAL ventasDAL = new VentasDAL();
                return ventasDAL.EmitirVents(ventas, listaDetalle,reservas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerUtlimoIdVenta()
        {
            try
            {
                VentasDAL ventasDAL = new VentasDAL();
                return ventasDAL.ObtenerUtlimoIdVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VentasVIEW> ListarTodasVentas()
        {
            try
            {
                VentasDAL ventasDAL = new VentasDAL();
                return ventasDAL.ListarTodasVentas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VentasVIEW> ListarVentasRangoFecha(DateTime desde, DateTime hasta)
        {
            try
            {
                VentasDAL ventasDAL = new VentasDAL();
                return ventasDAL.ListarVentasRangoFecha(desde,hasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
