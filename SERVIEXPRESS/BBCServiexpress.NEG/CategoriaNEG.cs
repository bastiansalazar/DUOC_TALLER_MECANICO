using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class CategoriaNEG
    {
        public List<CATEGORIA> ListarCategorias()
        {
            try
            {
                CategoriasDAL categoriasDAL = new CategoriasDAL();
                return categoriasDAL.ListarCategorias();
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
                CategoriasDAL categoriasDAL = new CategoriasDAL();
                return categoriasDAL.CargarCategoria(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATEGORIA> FiltrarCategoria(string valor)
        {
            try
            {
                CategoriasDAL categoriasDAL = new CategoriasDAL();
                return categoriasDAL.FiltrarCategorias(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearCategoria(string nombre)
        {
            try
            {
                CATEGORIA categoria = new CATEGORIA();
                CategoriasDAL categoriasDAL = new CategoriasDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    categoria.NOMBRE = nombre.ToUpper();
                    categoria.FECHA_CREACION = DateTime.Now;
                    categoria.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return categoriasDAL.CrearCategoria(categoria);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarCategoria(string nombre, int id)
        {
            try
            {
                CATEGORIA categoria = new CATEGORIA();
                CategoriasDAL categoriasDAL = new CategoriasDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        categoria.ID = id;
                        categoria.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        categoria.NOMBRE = nombre;
                        return categoriasDAL.ActualizarCategoria(categoria);
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
