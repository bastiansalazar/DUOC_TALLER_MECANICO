using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class TipoProductoDAL
    {
        public List<TIPO_PRODUCTO> ListarTProductosCategoria(int categoria)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.TIPO_PRODUCTO
                                 where a.CATEGORIA_ID == categoria
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_PRODUCTO> ListarTProductos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.TIPO_PRODUCTO
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIPO_PRODUCTO CargarTipoProducto(int id)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.TIPO_PRODUCTO
                              where a.ID == id
                              select a).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_PRODUCTO> FiltrarTipoProducto(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.TIPO_PRODUCTO
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

        public string CrearTipoProducto(TIPO_PRODUCTO tipoProducto)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_PRODUCTO
                              where a.NOMBRE == tipoProducto.NOMBRE
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.TIPO_PRODUCTO
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    tipoProducto.ID = _ultimo + 1;
                    con.TIPO_PRODUCTO.Add(tipoProducto);
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

        public string ActualizarTipoProducto(TIPO_PRODUCTO tipoProducto)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.TIPO_PRODUCTO
                              where a.ID == tipoProducto.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = tipoProducto.NOMBRE;
                query2.CATEGORIA_ID = tipoProducto.CATEGORIA_ID;
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
