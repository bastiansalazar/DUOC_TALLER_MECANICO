using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class CategoriasDAL
    {
        public List<CATEGORIA> ListarCategorias()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.CATEGORIA
                              orderby a.NOMBRE ascending
                               select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CATEGORIA CargarCategoria(int id)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.CATEGORIA
                                 where a.ID == id
                              select a).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATEGORIA> FiltrarCategorias(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.CATEGORIA
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

        public string CrearCategoria(CATEGORIA categoria)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.CATEGORIA
                                       where a.NOMBRE == categoria.NOMBRE
                                       select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.CATEGORIA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    categoria.ID = _ultimo + 1;
                    con.CATEGORIA.Add(categoria);
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

        public string ActualizarCategoria(CATEGORIA categoria)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.CATEGORIA
                              where a.ID == categoria.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = categoria.NOMBRE;
                query2.FECHA_ULTIMO_UPDATE = categoria.FECHA_ULTIMO_UPDATE;
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
