using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class MarcasDAL
    {
        public List<MARCA> ListarMarcas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.MARCA
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _lista;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.MARCA
                              where a.ID == id
                              select a).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MARCA> FiltrarMarcas(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.MARCA
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

        public string CrearMarca(MARCA marca)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.MARCA
                              where a.NOMBRE == marca.NOMBRE
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.MARCA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    marca.ID = _ultimo + 1;
                    con.MARCA.Add(marca);
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

        public string ActualizarMarca(MARCA marca)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.MARCA
                             where a.NOMBRE == marca.NOMBRE
                             select a).FirstOrDefault();

                if (query == null)
                {
                    var query2 = (from a in con.MARCA
                                  where a.ID == marca.ID
                                  select a).FirstOrDefault();

                    query2.NOMBRE = marca.NOMBRE;
                    query2.FECHA_ULTIMO_UPDATE = marca.FECHA_ULTIMO_UPDATE;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "Ya existe un registro con ese nombre, pruebe con otro";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
