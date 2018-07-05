using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class VehiculosNEG
    {
        public List<VehiculosVIEW> ListarTodosVehiculos()
        {
            try
            {
                VehiculosDAL vehiculosDAL = new VehiculosDAL();
                return vehiculosDAL.ListarTodosVehiculos();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public VehiculosVIEW CargarVehiculo(int idVehiculo)
        {
            try
            {
                VehiculosDAL vehiculosDAL = new VehiculosDAL();
                return vehiculosDAL.CargarVehiculo(idVehiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VehiculosVIEW> FiltrarVehiculos(string tipo, string valor)
        {
            try
            {
                if (tipo != "" & valor != "")
                {
                    VehiculosDAL vehiculosDAL = new VehiculosDAL();
                    return vehiculosDAL.FiltrarVehiculos(tipo, valor);
                }
                else
                {
                    return new List<VehiculosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string CrearVehiculo(string patente, int id_cliente, int marca_vehiculo,int tipo_vehiculo)
        {
            try
            {
                VEHICULO vehiculo = new VEHICULO();
                VehiculosDAL vehiculosDAL = new VehiculosDAL();

                if(patente != "")
                {
                    if (id_cliente > -1)
                    {
                        if (marca_vehiculo > -1)
                        {
                            if (tipo_vehiculo > -1)
                            {
                                vehiculo.PATENTE = patente;
                                vehiculo.CLIENTE_ID = id_cliente;
                                vehiculo.MARCA_VEHICULO_ID = marca_vehiculo;
                                vehiculo.TIPO_VEHICULO_ID = tipo_vehiculo;
                                vehiculo.FECHA_CREACION = DateTime.Now;
                                vehiculo.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                return vehiculosDAL.CrearVehiculo(vehiculo);
                            }
                            else { return "Indique un tipo de vehiculo"; }
                        }
                        else { return "Indique una marca"; }
                    }
                    else { return "Indique un cliente"; }
                }
                else { return "Indique una patente"; }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizarVehiculo(int id_cliente, int marca_vehiculo, int tipo_vehiculo, int idvehiculo)
        {
            try
            {
                VEHICULO vehiculo = new VEHICULO();
                VehiculosDAL vehiculosDAL = new VehiculosDAL();

                if (idvehiculo > 0)
                {
                    if (id_cliente > -1)
                    {
                        if (marca_vehiculo > -1)
                        {
                            if (tipo_vehiculo > -1)
                            {
                                vehiculo.ID = idvehiculo;
                                vehiculo.CLIENTE_ID = id_cliente;
                                vehiculo.MARCA_VEHICULO_ID = marca_vehiculo;
                                vehiculo.TIPO_VEHICULO_ID = tipo_vehiculo;
                                vehiculo.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                return vehiculosDAL.ActualizarVehiculo(vehiculo);
                            }
                            else { return "Indique un tipo de vehiculo"; }
                        }
                        else { return "Indique una marca"; }
                    }
                    else { return "Indique un cliente"; }
                }
                else { return "Debe seleccionar una vehiculo de la tabla de vahiculos"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
