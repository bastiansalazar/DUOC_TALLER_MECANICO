using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class TipoServicioDAL
    {
        public TIPO_SERVICIO CargarTipoServicio(int idTipoServicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _clientes = (from a in con.TIPO_SERVICIO
                                 where a.ID == idTipoServicio
                                 orderby a.NOMBRE ascending
                                 select a).FirstOrDefault();
                return _clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TIPO_SERVICIO> FiltrarTipoServicios(string nombre)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.TIPO_SERVICIO
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

        public string CrearTipoServicio(TIPO_SERVICIO tipoServicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exTipoServicio = (from a in con.TIPO_SERVICIO
                                       where a.NOMBRE == tipoServicio.NOMBRE
                                       select a).FirstOrDefault();

                if (_exTipoServicio == null)
                {
                    con.TIPO_SERVICIO.Add(tipoServicio);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "El tipo de servicio ya existe en los registros";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarTipoServicio(TIPO_SERVICIO tipoServicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var tipo = (from a in con.TIPO_SERVICIO
                            where a.NOMBRE == tipoServicio.NOMBRE
                            select a).FirstOrDefault();

                if (tipo == null)
                {
                    var tipo2 = (from a in con.TIPO_SERVICIO
                                where a.ID == tipoServicio.ID
                                select a).FirstOrDefault();

                    tipo2.NOMBRE = tipoServicio.NOMBRE;
                    tipo2.FECHA_ULTIMO_UPDATE = tipoServicio.FECHA_ULTIMO_UPDATE;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "El tipo servicio ya existe con ese nombre en los registros, pruebe con otro";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
