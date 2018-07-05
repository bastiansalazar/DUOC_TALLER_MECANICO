using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class TipoServicioNEG
    {
        public TIPO_SERVICIO CargarTipoServicio(int idTipoServicio)
        {
            try
            {
                TipoServicioDAL tipoServicioDAL = new TipoServicioDAL();
                return tipoServicioDAL.CargarTipoServicio(idTipoServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_SERVICIO> FiltrarTipoServicios(string nombre)
        {
            try
            {
                TipoServicioDAL tipoServicioDAL = new TipoServicioDAL();
                return tipoServicioDAL.FiltrarTipoServicios(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearTipoServicio(string nombre)
        {
            try
            {
                TIPO_SERVICIO tipoServicio = new TIPO_SERVICIO();
                TipoServicioDAL tipoServicioDAL = new TipoServicioDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    tipoServicio.NOMBRE = nombre.ToUpper();
                    tipoServicio.FECHA_CREACION = DateTime.Now;
                    tipoServicio.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return tipoServicioDAL.CrearTipoServicio(tipoServicio);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarServicio(string nombre, int id)
        {
            try
            {
                TIPO_SERVICIO tipoServicio = new TIPO_SERVICIO();
                TipoServicioDAL tipoServicioDAL = new TipoServicioDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        tipoServicio.ID = id;
                        tipoServicio.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        return tipoServicioDAL.ActualizarTipoServicio(tipoServicio);
                    }
                    else { return "Seleccione un tipo de servicio"; }
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
