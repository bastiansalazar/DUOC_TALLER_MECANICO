using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class OrdenPedidoDAL
    {

        public List<FolioView> ListarFoliosSucursal(int sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                List<FolioView> lista = new List<FolioView>();
                var _query = (from a in con.ORDEN_PEDIDO
                              orderby a.ID descending
                              where a.SUCURSAL_ID == sucursal
                              select a).ToList();
                foreach (var x in _query)
                {
                    string folio = x.ID.ToString();
                    for (int i = 0; i < 9; i++)
                    {
                        if (folio.Length < 8)
                            folio = "0" + folio;
                    }
                    FolioView algo = new FolioView();
                    algo.ID = x.ID;
                    algo.NOMBRE = folio;
                    lista.Add(algo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FolioView> ListarFoliosSucursalEnviada(int sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                List<FolioView> lista = new List<FolioView>();
                var _query = (from a in con.ORDEN_PEDIDO
                              orderby a.ID descending
                              where a.SUCURSAL_ID == sucursal
                              && a.ESTADO_ORDEN_PEDIDO_ID == 3
                              select a).ToList();
                foreach (var x in _query)
                {
                    string folio = x.ID.ToString();
                    for (int i = 0; i < 9; i++)
                    {
                        if (folio.Length < 8)
                            folio = "0" + folio;
                    }
                    FolioView algo = new FolioView();
                    algo.ID = x.ID;
                    algo.NOMBRE = folio;
                    lista.Add(algo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearOrdenPedido(ORDEN_PEDIDO orden, List<DETALLE_ORDEN_PEDIDO> listaDetalle)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                con.ORDEN_PEDIDO.Add(orden);
                con.SaveChanges();
                var _query = (from a in con.ORDEN_PEDIDO
                              orderby a.ID descending
                              select a).FirstOrDefault();
                foreach(var fila in listaDetalle)
                {
                    fila.ORDEN_PEDIDO_ID = _query.ID;
                    var ultimo = (from a in con.DETALLE_ORDEN_PEDIDO
                                  orderby a.ID descending
                                  select a.ID).FirstOrDefault();
                    fila.ID = ultimo + 1;
                    con.DETALLE_ORDEN_PEDIDO.Add(fila);
                    con.SaveChanges();
                }
                
                return "creado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarOrdenPedido(ORDEN_PEDIDO orden, List<DETALLE_ORDEN_PEDIDO> listaDetalle)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ORDEN_PEDIDO
                              where a.ID == orden.ID
                              select a).FirstOrDefault();
                if (_query!=null)
                {
                    _query.FECHA_ULTIMO_UPDATE = orden.FECHA_ULTIMO_UPDATE;
                    _query.CANTIDAD_TOTAL = orden.CANTIDAD_TOTAL;
                    _query.MONTO_TOTAL = orden.MONTO_TOTAL;
                    _query.EMPLEADO_ID = orden.EMPLEADO_ID;
                    _query.MULTI_MONEDA_ID = orden.MULTI_MONEDA_ID;
                    _query.EMAIL_PROVEEDOR = orden.EMAIL_PROVEEDOR;
                    _query.EMAIL_SUCURSAL = orden.EMAIL_SUCURSAL;
                    _query.ESTADO_ORDEN_PEDIDO_ID = orden.ESTADO_ORDEN_PEDIDO_ID;
                    con.SaveChanges();

                    var _query2 = (from a in con.DETALLE_ORDEN_PEDIDO
                                   where a.ORDEN_PEDIDO_ID == orden.ID
                                   select a).ToList();

                    foreach (var fila in _query2)
                    {
                        con.DETALLE_ORDEN_PEDIDO.Remove(fila);
                        con.SaveChanges();
                    }

                    foreach (var fila in listaDetalle)
                    {
                        var _id = (from a in con.DETALLE_ORDEN_PEDIDO
                                       orderby a.ID descending
                                       select a.ID).FirstOrDefault();

                        fila.ORDEN_PEDIDO_ID = orden.ID;
                        fila.ID = _id + 1;
                        con.DETALLE_ORDEN_PEDIDO.Add(fila);
                        con.SaveChanges();
                    }

                    return "actualizado";
                }
                else
                {
                    return "La orden de pedido ya no encuentra en la base de datos";
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdenPedidoVIEW> FiltrarOrdenesPedidos(string tipo, int valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "PROVEEDOR")
                {
                    var _query = (from a in con.ORDEN_PEDIDO
                                  join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.ESTADO_ORDEN_PEDIDO on a.ESTADO_ORDEN_PEDIDO_ID equals d.ID
                                  join e in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals e.ID
                                  join f in con.PROVEEDOR on a.PROVEEDOR_ID equals f.ID
                                  join g in con.PERSONA on f.PERSONA_ID equals g.ID
                                  join h in con.SUCURSAL on a.SUCURSAL_ID equals h.ID
                                  where a.PROVEEDOR_ID == valor
                                  orderby a.ID ascending
                                  select new OrdenPedidoVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                                      FECHA_ENTREGA = a.FECHA_ENTREGA,
                                      MONTO_TOTAL = a.MONTO_TOTAL,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_ORDEN_PEDIDO_ID = a.ESTADO_ORDEN_PEDIDO_ID,
                                      MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                                      PROVEEDOR_ID = a.PROVEEDOR_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      EMAIL_PROVEEDOR = a.EMAIL_PROVEEDOR,
                                      EMAIL_SUCURSAL = a.EMAIL_SUCURSAL,
                                      NOMBRE_EMPLEADO = c.NOMBRE,
                                      APELLIDO_EMPLEADO = c.APELLIDO,
                                      NUMID_EMPLEADO = c.NUM_ID,
                                      DIVID_EMPLEADO = c.DIV_ID,
                                      NUMID_PROVEEDOR = g.NUM_ID,
                                      DIVID_PROVEEDOR = g.DIV_ID,
                                      ESTADO_ORDEN_PEDIDO = d.NOMBRE,
                                      MULTI_MONEDA = e.TIPO_MODONEDA,
                                      PROVEEDOR = f.NOMBRE_EMPRESA,
                                      SUCURSAL = h.NOMBRE,
                                      VALOR_MULTIMONEDA = e.MONTO
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "SUCURSAL")
                {
                    var _query = (from a in con.ORDEN_PEDIDO
                                  join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.ESTADO_ORDEN_PEDIDO on a.ESTADO_ORDEN_PEDIDO_ID equals d.ID
                                  join e in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals e.ID
                                  join f in con.PROVEEDOR on a.PROVEEDOR_ID equals f.ID
                                  join g in con.PERSONA on f.PERSONA_ID equals g.ID
                                  join h in con.SUCURSAL on a.SUCURSAL_ID equals h.ID
                                  where a.SUCURSAL_ID == valor
                                  orderby a.ID ascending
                                  select new OrdenPedidoVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                                      FECHA_ENTREGA = a.FECHA_ENTREGA,
                                      MONTO_TOTAL = a.MONTO_TOTAL,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_ORDEN_PEDIDO_ID = a.ESTADO_ORDEN_PEDIDO_ID,
                                      MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                                      PROVEEDOR_ID = a.PROVEEDOR_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      EMAIL_PROVEEDOR = a.EMAIL_PROVEEDOR,
                                      EMAIL_SUCURSAL = a.EMAIL_SUCURSAL,
                                      NOMBRE_EMPLEADO = c.NOMBRE,
                                      APELLIDO_EMPLEADO = c.APELLIDO,
                                      NUMID_EMPLEADO = c.NUM_ID,
                                      DIVID_EMPLEADO = c.DIV_ID,
                                      NUMID_PROVEEDOR = g.NUM_ID,
                                      DIVID_PROVEEDOR = g.DIV_ID,
                                      ESTADO_ORDEN_PEDIDO = d.NOMBRE,
                                      MULTI_MONEDA = e.TIPO_MODONEDA,
                                      PROVEEDOR = f.NOMBRE_EMPRESA,
                                      SUCURSAL = h.NOMBRE,
                                      VALOR_MULTIMONEDA = e.MONTO
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "ESTADO")
                {
                    var _query = (from a in con.ORDEN_PEDIDO
                                  join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.ESTADO_ORDEN_PEDIDO on a.ESTADO_ORDEN_PEDIDO_ID equals d.ID
                                  join e in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals e.ID
                                  join f in con.PROVEEDOR on a.PROVEEDOR_ID equals f.ID
                                  join g in con.PERSONA on f.PERSONA_ID equals g.ID
                                  join h in con.SUCURSAL on a.SUCURSAL_ID equals h.ID
                                  where a.ESTADO_ORDEN_PEDIDO_ID == valor
                                  orderby a.ID ascending
                                  select new OrdenPedidoVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                                      FECHA_ENTREGA = a.FECHA_ENTREGA,
                                      MONTO_TOTAL = a.MONTO_TOTAL,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_ORDEN_PEDIDO_ID = a.ESTADO_ORDEN_PEDIDO_ID,
                                      MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                                      PROVEEDOR_ID = a.PROVEEDOR_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      EMAIL_PROVEEDOR = a.EMAIL_PROVEEDOR,
                                      EMAIL_SUCURSAL = a.EMAIL_SUCURSAL,
                                      NOMBRE_EMPLEADO = c.NOMBRE,
                                      APELLIDO_EMPLEADO = c.APELLIDO,
                                      NUMID_EMPLEADO = c.NUM_ID,
                                      DIVID_EMPLEADO = c.DIV_ID,
                                      NUMID_PROVEEDOR = g.NUM_ID,
                                      DIVID_PROVEEDOR = g.DIV_ID,
                                      ESTADO_ORDEN_PEDIDO = d.NOMBRE,
                                      MULTI_MONEDA = e.TIPO_MODONEDA,
                                      PROVEEDOR = f.NOMBRE_EMPRESA,
                                      SUCURSAL = h.NOMBRE,
                                      VALOR_MULTIMONEDA = e.MONTO
                                  }).ToList();
                    return _query;
                }
                else
                {
                    return new List<OrdenPedidoVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public string EnviarOrdenPedido(int idOrden)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.ORDEN_PEDIDO
                             where a.ID == idOrden
                             select a).FirstOrDefault();

                if (query != null)
                {
                    query.ESTADO_ORDEN_PEDIDO_ID = 3;                   
                    con.SaveChanges();
                    return "eliminado";
                }
                else
                {
                    return "La orden de pedido ya no existe en la base de datos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RechazarOrdenPedido(int idOrden)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.ORDEN_PEDIDO
                             where a.ID == idOrden
                             select a).FirstOrDefault();

                if (query != null)
                {
                    query.ESTADO_ORDEN_PEDIDO_ID = 2;
                    con.SaveChanges();
                    return "rechazada";
                }
                else
                {
                    return "La orden de pedido ya no existe en la base de datos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EliminarOrdenPedido(int idOrden)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var query = (from a in con.ORDEN_PEDIDO
                             where a.ID == idOrden
                             select a).FirstOrDefault();

                if (query != null)
                {
                    var query2 = (from a in con.DETALLE_ORDEN_PEDIDO
                                  where a.ORDEN_PEDIDO_ID == idOrden
                                  select a).ToList();
                    foreach(var x in query2)
                    {
                        con.DETALLE_ORDEN_PEDIDO.Remove(x);
                        con.SaveChanges();
                    }
                    con.ORDEN_PEDIDO.Remove(query);
                    con.SaveChanges();
                    return "eliminado";
                }
                else
                {
                    return "La orden de pedido ya no existe en la base de datos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdenPedidoVIEW> ListarTodasOrdenesPedidos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ORDEN_PEDIDO
                              join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                                 join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                 join d in con.ESTADO_ORDEN_PEDIDO on a.ESTADO_ORDEN_PEDIDO_ID equals d.ID
                                 join e in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals e.ID
                                 join f in con.PROVEEDOR on a.PROVEEDOR_ID equals f.ID
                                 join g in con.PERSONA on f.PERSONA_ID equals g.ID
                                 join h in con.SUCURSAL on a.SUCURSAL_ID equals h.ID
                              orderby a.ID ascending
                              select new OrdenPedidoVIEW
                                 {
                                     ID = a.ID,
                                     FECHA_CREACION = a.FECHA_CREACION,
                                     FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                     CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                                     FECHA_ENTREGA = a.FECHA_ENTREGA,
                                     MONTO_TOTAL = a.MONTO_TOTAL,
                                     EMPLEADO_ID = a.EMPLEADO_ID,
                                     ESTADO_ORDEN_PEDIDO_ID = a.ESTADO_ORDEN_PEDIDO_ID,
                                     MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                                     PROVEEDOR_ID = a.PROVEEDOR_ID,
                                     SUCURSAL_ID = a.SUCURSAL_ID,
                                     EMAIL_PROVEEDOR = a.EMAIL_PROVEEDOR,
                                     EMAIL_SUCURSAL = a.EMAIL_SUCURSAL,
                                     NOMBRE_EMPLEADO = c.NOMBRE,
                                     APELLIDO_EMPLEADO = c.APELLIDO,
                                     NUMID_EMPLEADO = c.NUM_ID,
                                     DIVID_EMPLEADO = c.DIV_ID,
                                     NUMID_PROVEEDOR = g.NUM_ID,
                                     DIVID_PROVEEDOR = g.DIV_ID,
                                     ESTADO_ORDEN_PEDIDO = d.NOMBRE,
                                     MULTI_MONEDA = e.TIPO_MODONEDA,
                                     PROVEEDOR = f.NOMBRE_EMPRESA,
                                     SUCURSAL = h.NOMBRE,
                                     VALOR_MULTIMONEDA = e.MONTO
                                 }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdenPedidoVIEW CargarOrdenPedido(int idOrden)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ORDEN_PEDIDO
                              join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.ESTADO_ORDEN_PEDIDO on a.ESTADO_ORDEN_PEDIDO_ID equals d.ID
                              join e in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals e.ID
                              join f in con.PROVEEDOR on a.PROVEEDOR_ID equals f.ID
                              join g in con.PERSONA on f.PERSONA_ID equals g.ID
                              join h in con.SUCURSAL on a.SUCURSAL_ID equals h.ID
                              where a.ID==idOrden
                              select new OrdenPedidoVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                                  FECHA_ENTREGA = a.FECHA_ENTREGA,
                                  MONTO_TOTAL = a.MONTO_TOTAL,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_ORDEN_PEDIDO_ID = a.ESTADO_ORDEN_PEDIDO_ID,
                                  MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                                  PROVEEDOR_ID = a.PROVEEDOR_ID,
                                  SUCURSAL_ID = a.SUCURSAL_ID,
                                  EMAIL_PROVEEDOR = a.EMAIL_PROVEEDOR,
                                  EMAIL_SUCURSAL = a.EMAIL_SUCURSAL,
                                  NOMBRE_EMPLEADO = c.NOMBRE,
                                  APELLIDO_EMPLEADO = c.APELLIDO,
                                  NUMID_EMPLEADO = c.NUM_ID,
                                  DIVID_EMPLEADO = c.DIV_ID,
                                  NUMID_PROVEEDOR = g.NUM_ID,
                                  DIVID_PROVEEDOR = g.DIV_ID,
                                  ESTADO_ORDEN_PEDIDO = d.NOMBRE,
                                  MULTI_MONEDA = e.TIPO_MODONEDA,
                                  PROVEEDOR = f.NOMBRE_EMPRESA,
                                  SUCURSAL = h.NOMBRE,
                                  VALOR_MULTIMONEDA = e.MONTO
                              }).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
