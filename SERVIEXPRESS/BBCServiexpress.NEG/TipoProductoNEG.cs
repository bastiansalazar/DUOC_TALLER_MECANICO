using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class TipoProductoNEG
    {
        public List<TIPO_PRODUCTO> ListarTProductosCategoria(int categoria)
        {
            try
            {
                TipoProductoDAL datos = new TipoProductoDAL();
                return datos.ListarTProductosCategoria(categoria);
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
                TipoProductoDAL datos = new TipoProductoDAL();
                return datos.ListarTProductos();
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
                TipoProductoDAL datos = new TipoProductoDAL();
                return datos.CargarTipoProducto(id);
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
                TipoProductoDAL datos = new TipoProductoDAL();
                return datos.FiltrarTipoProducto(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearTipoProducto(string nombre, int categoria)
        {
            try
            {
                TIPO_PRODUCTO tipoProducto = new TIPO_PRODUCTO();
                TipoProductoDAL tipoProductoDAL = new TipoProductoDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (categoria  > -1)
                    {
                        tipoProducto.NOMBRE = nombre.ToUpper();
                        tipoProducto.CATEGORIA_ID = categoria;
                        return tipoProductoDAL.CrearTipoProducto(tipoProducto);
                    }
                    else { return "La dirección debe tener al menos 2 caracteres"; }
                }
                else { return "La razón social debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarTipoProducto(int id, string nombre, int categoria)
        {
            try
            {
                TIPO_PRODUCTO tipoProducto = new TIPO_PRODUCTO();
                TipoProductoDAL tipoProductoDAL = new TipoProductoDAL();
                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (categoria > -1)
                    {
                        tipoProducto.NOMBRE = nombre.ToUpper();
                        tipoProducto.CATEGORIA_ID = categoria;
                        tipoProducto.ID = id;
                        return tipoProductoDAL.ActualizarTipoProducto(tipoProducto);
                    }
                    else { return "La dirección debe tener al menos 2 caracteres"; }
                }
                else { return "La razón social debe tener al menos 2 caracteres"; }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
