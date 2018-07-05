using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.DAL
{
    public class ControlRecepcionDAL
    {
        public List<ControlRecepcionVIEW> ListarTodosRecepcion()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.CONTROL_RECEPCION
                              join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.ESTADO_CONTROL_RECEPCION on a.ESTADO_CONTROL_RECEPCION_ID equals d.ID
                              orderby a.ORDEN_PEDIDO_ID ascending
                              select new ControlRecepcionVIEW
                              {
                                 ID = a.ID,
                                 FECHA_CREACION = a.FECHA_CREACION,
                                 FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                 COMENTARIO = a.COMENTARIO,
                                 FECHA_APROVACION = a.FECHA_APROVACION,
                                 FECHA_RECEPCION = a.FECHA_RECEPCION,
                                 EMPLEADO_ID = a.EMPLEADO_ID,
                                 ESTADO_CONTROL_RECEPCION_ID = a.ESTADO_CONTROL_RECEPCION_ID,
                                 ORDEN_PEDIDO_ID = a.ORDEN_PEDIDO_ID,
                                 NOMBRE_EMPLEADO = c.NOMBRE,
                                 APELLIDO_EMPLEADO = c.APELLIDO,
                                 NUM_ID_EMPELADO = c.NUM_ID,
                                 ESTADO_CONTROL_RECEPCION = d.NOMBRE
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControlRecepcionVIEW> FiltarControlFechas(DateTime desde, DateTime hasta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.CONTROL_RECEPCION
                              join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.ESTADO_CONTROL_RECEPCION on a.ESTADO_CONTROL_RECEPCION_ID equals d.ID
                              where a.FECHA_RECEPCION>=desde &&
                              a.FECHA_RECEPCION <=hasta
                              orderby a.ORDEN_PEDIDO_ID ascending
                              select new ControlRecepcionVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  COMENTARIO = a.COMENTARIO,
                                  FECHA_APROVACION = a.FECHA_APROVACION,
                                  FECHA_RECEPCION = a.FECHA_RECEPCION,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_CONTROL_RECEPCION_ID = a.ESTADO_CONTROL_RECEPCION_ID,
                                  ORDEN_PEDIDO_ID = a.ORDEN_PEDIDO_ID,
                                  NOMBRE_EMPLEADO = c.NOMBRE,
                                  APELLIDO_EMPLEADO = c.APELLIDO,
                                  NUM_ID_EMPELADO = c.NUM_ID,
                                  ESTADO_CONTROL_RECEPCION = d.NOMBRE
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IgresarControlRececpion(CONTROL_RECEPCION control, List<DETALLE_CONTROL_RECEPCION> detalleControl)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                //Insertar a COntrold e recepcion
                con.CONTROL_RECEPCION.Add(control);
                con.SaveChanges();
                var orden = (from a in con.ORDEN_PEDIDO
                             where a.ID == control.ORDEN_PEDIDO_ID
                             select a).FirstOrDefault();

                //actualizar estao orden pedido a cursado
                orden.ESTADO_ORDEN_PEDIDO_ID = 4;
                con.SaveChanges();

                //obtinene el id de control de recepcion
                var _query = (from a in con.CONTROL_RECEPCION
                              where a.ORDEN_PEDIDO_ID == control.ORDEN_PEDIDO_ID
                              select a).FirstOrDefault();

                foreach (var fila in detalleControl)
                {
                    //obtiene ultimo id detallo control
                    var _query2 = (from a in con.DETALLE_CONTROL_RECEPCION
                                   orderby a.ID descending
                                   select a).FirstOrDefault();                    
                    int id = 0;
                    if (_query2 != null)
                    {
                        id = _query2.ID;
                    }                    
                    fila.ID = id + 1;                    
                    fila.CONTROL_RECEPCION_ID = _query.ID;
                    con.DETALLE_CONTROL_RECEPCION.Add(fila);
                    con.SaveChanges();

                    //actualiza prostock producto
                    var prod = (from a in con.PRODUCTO
                                where a.ID == fila.PRODUCTO_ID
                                select a).FirstOrDefault();

                    if (prod !=null)
                    {
                        prod.STOCK = prod.STOCK + fila.CANTIDAD_INGRESADA;
                        con.SaveChanges();
                    }
                }

                return "creado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlRecepcionVIEW CargarControlRecepcion(int idControl)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.CONTROL_RECEPCION
                              join b in con.EMPLEADO on a.EMPLEADO_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.ESTADO_CONTROL_RECEPCION on a.ESTADO_CONTROL_RECEPCION_ID equals d.ID
                              join e in con.ORDEN_PEDIDO on a.ORDEN_PEDIDO_ID equals e.ID
                              join f in con.PROVEEDOR on e.PROVEEDOR_ID equals f.ID
                              join g in con.SUCURSAL on e.SUCURSAL_ID equals g.ID
                              join h in con.PERSONA on f.PERSONA_ID equals h.ID
                              where a.ID == idControl
                              select new ControlRecepcionVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  COMENTARIO = a.COMENTARIO,
                                  FECHA_APROVACION = a.FECHA_APROVACION,
                                  FECHA_RECEPCION = a.FECHA_RECEPCION,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_CONTROL_RECEPCION_ID = a.ESTADO_CONTROL_RECEPCION_ID,
                                  ORDEN_PEDIDO_ID = a.ORDEN_PEDIDO_ID,
                                  NOMBRE_EMPLEADO = c.NOMBRE,
                                  APELLIDO_EMPLEADO = c.APELLIDO,
                                  NUM_ID_EMPELADO = c.NUM_ID,
                                  ESTADO_CONTROL_RECEPCION = d.NOMBRE,
                                  PROVEEDOR = f.NOMBRE_EMPRESA,
                                  DIV_ID_PROVEEDOR = h.DIV_ID,
                                  NUM_ID_PROVEEDOR = h.NUM_ID,
                                  SUCURSAL = g.NOMBRE
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
