using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class RegionDAL
    {
        public List<REGION> ListarRegiones( int pais)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _regiones = (from a in con.REGION
                                 where a.PAIS_ID == pais
                                 orderby a.NOMBRE ascending
                                 select a).ToList();
                return _regiones;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _regiones = (from a in con.REGION
                                 orderby a.PAIS_ID ascending
                                 orderby a.NOMBRE ascending
                                 select a).ToList();
                return _regiones;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.REGION
                              where a.ID == id
                              select a).FirstOrDefault();
                return _query;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.REGION
                                  where a.NOMBRE.Contains(valor)
                                  orderby a.PAIS_ID ascending
                                  orderby a.NOMBRE ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearRegion(REGION region)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.REGION
                              where a.NOMBRE == region.NOMBRE
                              && a.PAIS_ID == region.PAIS_ID
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.REGION
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    region.ID = _ultimo + 1;
                    con.REGION.Add(region);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "Ya existe un registro con ese nombre";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarRegion(REGION region)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.REGION
                              where a.ID == region.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = region.NOMBRE;
                query2.PAIS_ID = region.PAIS_ID;
                query2.FECHA_ULTIMO_UPDATE = region.FECHA_ULTIMO_UPDATE;
                con.SaveChanges();
                return "actualizado";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
