using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class Marca_VehiculosNEG
    {
        public List<MARCA_VEHICULO> ListarTodasMarcas()
        {
            try
            {
                MarcasVehiculosDAL marcasDAL = new MarcasVehiculosDAL();
                return marcasDAL.ListarTodosMarcas();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public MARCA_VEHICULO CargarMarcaVehiculo(int idMarcaV)
        {
            try
            {
                MarcasVehiculosDAL marcaVehiculoDAL = new MarcasVehiculosDAL();
                return marcaVehiculoDAL.CargarMarcaVehiculo(idMarcaV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MARCA_VEHICULO> FiltrarMarcasVehiculos(string nombre)
        {
            try
            {
                MarcasVehiculosDAL marcaVehiculoDAL = new MarcasVehiculosDAL();
                return marcaVehiculoDAL.FiltrarMarcaVehiculo(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CrearMarcaVehiculo(string nombre)
        {
            try
            {
                MARCA_VEHICULO marcaVehiculo = new MARCA_VEHICULO();
                MarcasVehiculosDAL marcaVehiculoDAL = new MarcasVehiculosDAL();

                if (nombre.Trim().Length > 1)
                {
                    marcaVehiculo.NOMBRE = nombre.ToUpper();
                    marcaVehiculo.FECHA_CREACION = DateTime.Now;
                    marcaVehiculo.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return marcaVehiculoDAL.CrearMarcaVehiculo(marcaVehiculo);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarMarcaVehiculo(string nombre, int id)
        {
            try
            {
                MARCA_VEHICULO marcaVehiculo = new MARCA_VEHICULO();
                MarcasVehiculosDAL marcaVehiculoDAL = new MarcasVehiculosDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        marcaVehiculo.ID = id;
                        marcaVehiculo.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        marcaVehiculo.NOMBRE = nombre;
                        return marcaVehiculoDAL.ActualizarMarcaVehiculo(marcaVehiculo);
                    }
                    else { return "Seleccione una marca de vehiculo desde la tabla"; }
                }
                else { return "Debe ingresar un nombre de marca"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
