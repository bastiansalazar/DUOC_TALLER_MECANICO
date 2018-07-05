using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ProvinciaNEG
    {
        public List<PROVINCIA> ListarProvincias(int region)
        {
            try
            {
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();
                return provinciaDAL.ListarProvincias(region);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PROVINCIA> ListarTodasProvincias()
        {
            try
            {
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();
                return provinciaDAL.ListarTodasProvincias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PROVINCIA CargarProvincia(int id)
        {
            try
            {
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();
                return provinciaDAL.CargarProvincia(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PROVINCIA> FiltrarProvincia(string valor)
        {
            try
            {
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();
                return provinciaDAL.FiltrarProvincia(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearProvincia(string nombre, int region)
        {
            try
            {
                PROVINCIA provincia = new PROVINCIA();
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    provincia.NOMBRE = nombre.ToUpper();
                    provincia.FECHA_CREACION = DateTime.Now;
                    provincia.REGION_ID = region;
                    provincia.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return provinciaDAL.CrearProvincia(provincia);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarProvincia(string nombre, int id, int region)
        {
            try
            {
                PROVINCIA provincia = new PROVINCIA();
                ProvinciaDAL provinciaDAL = new ProvinciaDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        if (region > 0)
                        {
                            provincia.ID = id;
                            provincia.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            provincia.REGION_ID = region;
                            provincia.NOMBRE = nombre;
                            return provinciaDAL.ActualizarProvincia(provincia);
                        }
                        else { return "Seleccione una region"; }
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
