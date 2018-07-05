using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class MultiMonedaDAL
    {
        public List<MULTI_MONEDA> ListarMultiMoneda()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.MULTI_MONEDA
                              orderby a.TIPO_MODONEDA ascending
                              select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MULTI_MONEDA CargarMultiMoneda(int idMoneda)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _multiMoneda = (from a in con.MULTI_MONEDA
                                 where a.ID == idMoneda
                                 select a).FirstOrDefault();
                return _multiMoneda;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MULTI_MONEDA> FiltrarMultiMoneda(string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.MULTI_MONEDA
                                  where a.TIPO_MODONEDA.Contains(valor)
                                  orderby a.TIPO_MODONEDA ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearMultiMoneda(MULTI_MONEDA multimoneda)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.MULTI_MONEDA
                              where a.TIPO_MODONEDA == multimoneda.TIPO_MODONEDA
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.MULTI_MONEDA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    multimoneda.ID = _ultimo + 1;
                    con.MULTI_MONEDA.Add(multimoneda);
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

        public string ActualizarMultiMoneda(MULTI_MONEDA multimoneda)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.MULTI_MONEDA
                              where a.ID == multimoneda.ID
                              select a).FirstOrDefault();

                query2.TIPO_MODONEDA = multimoneda.TIPO_MODONEDA;
                query2.MONTO = multimoneda.MONTO;
                query2.FECHA_ULTIMO_UPDATE = multimoneda.FECHA_ULTIMO_UPDATE;
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
