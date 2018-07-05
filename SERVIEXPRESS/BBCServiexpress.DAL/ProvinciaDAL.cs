using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ProvinciaDAL
    {
        public List<PROVINCIA> ListarProvincias(int region)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _provincias = (from a in con.PROVINCIA
                                   where a.REGION_ID == region
                                   orderby a.NOMBRE ascending
                                   select a).ToList();
                return _provincias;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _regiones = (from a in con.PROVINCIA
                                 orderby a.REGION_ID ascending
                                 select a).ToList();
                return _regiones;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PROVINCIA
                              where a.ID == id
                              select a).FirstOrDefault();
                return _query;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.PROVINCIA
                                  where a.NOMBRE.Contains(valor)
                                  orderby a.REGION_ID ascending
                                  orderby a.NOMBRE ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearProvincia(PROVINCIA provincia)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PROVINCIA
                              where a.NOMBRE == provincia.NOMBRE
                              && a.REGION_ID == provincia.REGION_ID
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.PROVINCIA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    provincia.ID = _ultimo + 1;
                    con.PROVINCIA.Add(provincia);
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

        public string ActualizarProvincia(PROVINCIA provincia)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.PROVINCIA
                              where a.ID == provincia.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = provincia.NOMBRE;
                query2.REGION_ID = provincia.REGION_ID;
                query2.FECHA_ULTIMO_UPDATE = provincia.FECHA_ULTIMO_UPDATE;
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
