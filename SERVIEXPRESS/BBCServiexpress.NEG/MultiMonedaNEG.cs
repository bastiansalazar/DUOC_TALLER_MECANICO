using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class MultiMonedaNEG
    {
        public List<MULTI_MONEDA> ListarMultiMoneda()
        {
            try
            {
                MultiMonedaDAL multiDAL = new MultiMonedaDAL();
                return multiDAL.ListarMultiMoneda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MULTI_MONEDA CargarMultiMoneda(int idMultiMoneda)
        {
            try
            {
                MultiMonedaDAL multiMonedaDAL = new MultiMonedaDAL();
                return multiMonedaDAL.CargarMultiMoneda(idMultiMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MULTI_MONEDA> FiltrarMultiMoneda(string valor)
        {
            try
            {
                MultiMonedaDAL multiMonedaDAL = new MultiMonedaDAL();
                return multiMonedaDAL.FiltrarMultiMoneda(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearMultiMoneda(string nombre, double valor)
        {
            try
            {
                MULTI_MONEDA multimoneda = new MULTI_MONEDA();
                MultiMonedaDAL multiMonedaDAL = new MultiMonedaDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    multimoneda.TIPO_MODONEDA = nombre.ToUpper();
                    multimoneda.MONTO = Convert.ToDecimal(valor);
                    multimoneda.FECHA_CREACION = DateTime.Now;
                    multimoneda.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return multiMonedaDAL.CrearMultiMoneda(multimoneda);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarMultiMoneda(string nombre, int id, double valor)
        {
            try
            {
                MULTI_MONEDA multimoneda = new MULTI_MONEDA();
                MultiMonedaDAL multiMonedaDAL = new MultiMonedaDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        multimoneda.ID = id;
                        multimoneda.TIPO_MODONEDA = nombre.ToUpper();
                        multimoneda.MONTO = Convert.ToDecimal(valor);
                        multimoneda.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        return multiMonedaDAL.ActualizarMultiMoneda(multimoneda);
                    }
                    else { return "Seleccione un registro de la tabla"; }
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
