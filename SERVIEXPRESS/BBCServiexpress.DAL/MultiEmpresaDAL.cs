using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class MultiEmpresaDAL
    {
        public MULTI_EMPRESA CargarMultiEmpresa(int idMulti)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var uventa = (from a in con.MULTI_EMPRESA
                              where a.ID == idMulti
                              select a).FirstOrDefault();
                return uventa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MULTI_EMPRESA> ListarEmpresas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.MULTI_EMPRESA
                              orderby a.RAZON_SOCIAL ascending
                              select a).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MULTI_EMPRESA> FiltrarMultiempresa (string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _resultado = (from a in con.MULTI_EMPRESA
                                  where a.RAZON_SOCIAL.Contains(valor)
                                  orderby a.RAZON_SOCIAL ascending
                                  select a).ToList();
                return _resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearMultiempresa(MULTI_EMPRESA multiempresa)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.MULTI_EMPRESA
                              where a.RAZON_SOCIAL == multiempresa.RAZON_SOCIAL
                              && a.RUT == multiempresa.RUT
                              select a).FirstOrDefault();

                if (_query == null)
                {
                    var _ultimo = (from a in con.MULTI_EMPRESA
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    multiempresa.ID = _ultimo + 1;
                    con.MULTI_EMPRESA.Add(multiempresa);
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

        public string ActualizarMultiempresa(MULTI_EMPRESA mutiempresa)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query2 = (from a in con.MULTI_EMPRESA
                              where a.ID == mutiempresa.ID
                              select a).FirstOrDefault();

                query2.DIRECCION = mutiempresa.DIRECCION;
                query2.NUMERO_TELEFONO = mutiempresa.NUMERO_TELEFONO;
                query2.ESTADO_EMPRESA_ID = mutiempresa.ESTADO_EMPRESA_ID;
                query2.FECHA_ULTIMO_UPDATE = mutiempresa.FECHA_ULTIMO_UPDATE;
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

