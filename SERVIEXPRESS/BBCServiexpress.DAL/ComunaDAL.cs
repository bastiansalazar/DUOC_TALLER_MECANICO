using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ComunaDAL
    {
        public List<COMUNA> ListarComunas(int provincia)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _comunas = (from a in con.COMUNA
                                where a.PROVINCIA_ID == provincia
                                orderby a.NOMBRE ascending
                                select a).ToList();
                return _comunas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<COMUNA> ListarTodasComunas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _regiones = (from a in con.COMUNA
                                 orderby a.PROVINCIA_ID ascending
                                 select a).ToList();
                return _regiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public COMUNA CargarComuna(int id)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.COMUNA
                              where a.ID == id
                              select a).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<COMUNA> FiltrarComuna(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.COMUNA
                                  where a.NOMBRE.Contains(valor)
                                  orderby a.PROVINCIA_ID ascending
                                  orderby a.NOMBRE ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearComuna(COMUNA comuna)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.COMUNA
                              where a.NOMBRE == comuna.NOMBRE
                              && a.PROVINCIA_ID == comuna.PROVINCIA_ID
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.COMUNA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    comuna.ID = _ultimo + 1;
                    con.COMUNA.Add(comuna);
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

        public string ActualizarComuna(COMUNA comuna)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.COMUNA
                              where a.ID == comuna.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = comuna.NOMBRE;
                query2.PROVINCIA_ID = comuna.PROVINCIA_ID;
                query2.FECHA_ULTIMO_UPDATE = comuna.FECHA_ULTIMO_UPDATE;
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
