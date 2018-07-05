using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class MarcaNEG
    {
        public List<MARCA> ListarMarcas()
        {
            try
            {
                MarcasDAL marcasDAL = new MarcasDAL();
                return marcasDAL.ListarMarcas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MARCA CargarMarca(int id)
        {
            try
            {
                MarcasDAL marcasDAL = new MarcasDAL();
                return marcasDAL.CargarMarca(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MARCA> FiltrarMarca(string valor)
        {
            try
            {
                MarcasDAL marcasDAL = new MarcasDAL();
                return marcasDAL.FiltrarMarcas(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearMarca(string nombre)
        {
            try
            {
                MARCA marca = new MARCA();
                MarcasDAL marcasDAL = new MarcasDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    marca.NOMBRE = nombre.ToUpper();
                    marca.FECHA_CREACION = DateTime.Now;
                    marca.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return marcasDAL.CrearMarca(marca);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarMarca(string nombre, int id)
        {
            try
            {
                MARCA marca = new MARCA();
                MarcasDAL marcasDAL = new MarcasDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        marca.ID = id;
                        marca.NOMBRE = nombre;
                        marca.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        return marcasDAL.ActualizarMarca(marca);
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
