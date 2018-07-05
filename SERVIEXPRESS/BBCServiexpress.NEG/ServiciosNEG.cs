using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ServiciosNEG
    {
        public List<ServiciosVIEW> ListarTodosServicios()
        {
            try
            {
                ServiciosDAL servicioDAL = new ServiciosDAL();
                return servicioDAL.ListarTodosServicios();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ServiciosVIEW CargarServicio(int idServicio)
        {
            try
            {
                ServiciosDAL servicioDAL = new ServiciosDAL();
                return servicioDAL.CargarServicio(idServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosVIEW> FiltrarServicios(string tipo, string valor)
        {
            try
            {
                if (tipo != "" & valor != "")
                {
                    ServiciosDAL servicioDAL = new ServiciosDAL();
                    return servicioDAL.FiltrarServicios(tipo, valor);
                }
                else
                {
                    return new List<ServiciosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string CrearServicio(int tipo_servicio, int estado_servicio, int sucursal,int costo)
        {
            try
            {
                SERVICIO servicio = new SERVICIO();
                ServiciosDAL serviciosDAL = new ServiciosDAL();

                if (tipo_servicio > -1)
                {
                    if (estado_servicio > -1)
                    {
                        if (sucursal > -1)
                        {
                            servicio.TIPO_SERVICIO_ID = tipo_servicio;
                            servicio.ESTADO_SERVICIO_ID = estado_servicio;
                            servicio.SUCURSAL_ID = sucursal;
                            servicio.FECHA_CREACION = DateTime.Now;
                            servicio.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            servicio.COSTO = costo;
                            return serviciosDAL.CrearServicio(servicio);
                        }
                        else { return "Indique una sucursal"; }
                    }
                    else { return "Indique un estado de servicio"; }
                }
                else { return "Indique un tipo de servicio"; }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizarServicio(int tipo_servicio, int estado_servicio, int sucursal,int idServicio,int costo)
        {
            try
            {
                SERVICIO servicio = new SERVICIO();
                ServiciosDAL serviciosDAL = new ServiciosDAL();

                if (tipo_servicio > -1)
                {
                    if (estado_servicio > -1)
                    {
                        if (sucursal > -1)
                        {
                            if (idServicio > 0)
                            {
                                servicio.ID = idServicio;
                                servicio.TIPO_SERVICIO_ID = tipo_servicio;
                                servicio.ESTADO_SERVICIO_ID = estado_servicio;
                                servicio.SUCURSAL_ID = sucursal;
                                servicio.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                servicio.COSTO = costo;
                                return serviciosDAL.ActualizarServicio(servicio);
                            }
                            else { return "Seleccione un servicio"; }
                        }
                        else { return "Indique una sucursal"; }
                    }
                    else { return "Indique un estado de servicio"; }
                }
                else { return "Indique un tipo de servicio"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosVIEW> FiltrarServicios(string valor1, int tipoServicio, string id_sucursal)
        {
            try
            {
                ServiciosDAL servicioDAL = new ServiciosDAL();
                return servicioDAL.FiltrarServicios(valor1, tipoServicio, id_sucursal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//
}
