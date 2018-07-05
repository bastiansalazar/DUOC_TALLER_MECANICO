using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class PaisDAL
    {
        public List<PAIS> ListarPaises()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _paises = (from a in con.PAIS
                               orderby a.NOMBRE ascending
                               select a).ToList();
                return _paises;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PAIS CargarPais(int id)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PAIS
                              where a.ID == id
                              select a).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PAIS> FiltrarPais(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.PAIS
                                  where a.NOMBRE.Contains(valor)
                                  orderby a.NOMBRE ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearPais(PAIS pais)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PAIS
                              where a.NOMBRE == pais.NOMBRE
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.PAIS
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    pais.ID = _ultimo + 1;
                    con.PAIS.Add(pais);
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

        public string ActualizarPais(PAIS pais)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.PAIS
                              where a.ID == pais.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = pais.NOMBRE;
                query2.FECHA_ULTIMO_UPDATE = pais.FECHA_ULTIMO_UPDATE;
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
