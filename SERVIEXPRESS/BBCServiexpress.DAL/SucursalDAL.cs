using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class SucursalDAL
    {
        public List<SUCURSAL> ListarSusursalesActivas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _sucursales = (from a in con.SUCURSAL
                                   join b in con.ESTADO_SUCURSAL
                                   on a.ESTADO_SUCURSAL_ID equals b.ID
                                   where b.NOMBRE == "VIGENTE"
                                   orderby a.NOMBRE ascending
                                   select a).ToList();
                return _sucursales;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SUCURSAL CargarSucursal(int idSucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _sucursal = (from a in con.SUCURSAL
                              where a.ID == idSucursal
                              select a).FirstOrDefault();
                return _sucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SUCURSAL> ListarTodasSucursales()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _regiones = (from a in con.SUCURSAL
                                 orderby a.NOMBRE ascending
                                 select a).ToList();
                return _regiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SUCURSAL> FiltrarSucursal(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.SUCURSAL
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

        public string CrearSucursal(SUCURSAL sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.SUCURSAL
                              where a.NOMBRE == sucursal.NOMBRE
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.SUCURSAL
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    sucursal.ID = _ultimo + 1;
                    con.SUCURSAL.Add(sucursal);
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

        public string ActualizarSucursal(SUCURSAL sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.SUCURSAL
                              where a.ID == sucursal.ID
                              select a).FirstOrDefault();

                query2.NOMBRE = sucursal.NOMBRE;
                query2.DIRECCION = sucursal.DIRECCION;
                query2.NUMERO_TELEFONO = sucursal.NUMERO_TELEFONO;
                query2.ESTADO_SUCURSAL_ID = sucursal.ESTADO_SUCURSAL_ID;
                query2.MULTI_EMPRESA_ID = sucursal.MULTI_EMPRESA_ID;
                query2.FECHA_ULTIMO_UPDATE = sucursal.FECHA_ULTIMO_UPDATE;
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
