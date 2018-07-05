using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class MarcasVehiculosDAL
    {
        public List<MARCA_VEHICULO> ListarTodosMarcas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _marcas = (from a in con.MARCA_VEHICULO
                               orderby a.NOMBRE ascending
                               select a).ToList();
                return _marcas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MARCA_VEHICULO CargarMarcaVehiculo(int idMarca)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _Marca = (from a in con.MARCA_VEHICULO
                              where a.ID == idMarca
                              select a).FirstOrDefault();
                return _Marca;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MARCA_VEHICULO> FiltrarMarcaVehiculo(string nombre)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.MARCA_VEHICULO
                                  where a.NOMBRE.Contains(nombre)
                                  orderby a.NOMBRE ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CrearMarcaVehiculo(MARCA_VEHICULO marcaVehiculo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exTipoServicio = (from a in con.MARCA_VEHICULO
                                       where a.NOMBRE == marcaVehiculo.NOMBRE
                                       select a).FirstOrDefault();

                if (_exTipoServicio == null)
                {
                    con.MARCA_VEHICULO.Add(marcaVehiculo);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "La marca ya existe en los registros";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarMarcaVehiculo(MARCA_VEHICULO marcaVehiculo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.MARCA_VEHICULO
                            where a.NOMBRE == marcaVehiculo.NOMBRE
                            select a).FirstOrDefault();

                if (query == null)
                {
                    var query2 = (from a in con.MARCA_VEHICULO
                                 where a.ID == marcaVehiculo.ID
                                 select a).FirstOrDefault();

                    query2.NOMBRE = marcaVehiculo.NOMBRE;
                    query2.FECHA_ULTIMO_UPDATE = marcaVehiculo.FECHA_ULTIMO_UPDATE;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "Esa marca ya existe en los registros, pruebe con otro";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
