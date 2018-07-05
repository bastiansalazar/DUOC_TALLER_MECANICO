using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class RegionNEG
    {
        public List<REGION> ListarRegiones(int pais)
        {
            try
            {
                RegionDAL regionDAL = new RegionDAL();
                return regionDAL.ListarRegiones(pais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<REGION> ListarTodasRegiones()
        {
            try
            {
                RegionDAL regionDAL = new RegionDAL();
                return regionDAL.ListarTodasRegiones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public REGION CargarRegion(int id)
        {
            try
            {
                RegionDAL regionDAL = new RegionDAL();
                return regionDAL.CargarRegion(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<REGION> FiltrarRegion(string valor)
        {
            try
            {
                RegionDAL regionDAL = new RegionDAL();
                return regionDAL.FiltrarRegion(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearRegion(string nombre,int pais)
        {
            try
            {
                REGION region = new REGION();
                RegionDAL regionDAL = new RegionDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    region.NOMBRE = nombre.ToUpper();
                    region.FECHA_CREACION = DateTime.Now;
                    region.PAIS_ID = pais;
                    region.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return regionDAL.CrearRegion(region);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarRegion(string nombre, int id,int pais)
        {
            try
            {
                REGION region = new REGION();
                RegionDAL regionDAL = new RegionDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        if (pais > 0)
                        {
                            region.ID = id;
                            region.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            region.PAIS_ID = pais;
                            region.NOMBRE = nombre;
                            return regionDAL.ActualizarRegion(region);
                        }
                        else { return "Seleccione un pais"; }
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

