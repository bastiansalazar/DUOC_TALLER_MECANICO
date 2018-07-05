using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class VentasDAL
    {
        public string EmitirVents(VENTAS venta, List<DETALLE_VENTAS> listaDetalle,string reservas)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                int uventa = (from a in con.VENTAS
                                  orderby a.ID descending
                                  select a.ID).FirstOrDefault();
                venta.ID = uventa + 1;
                con.VENTAS.Add(venta);
                con.SaveChanges();
                foreach (var fila in listaDetalle)
                {
                    int udeta = (from a in con.DETALLE_VENTAS
                                      orderby a.ID descending
                                      select a.ID).FirstOrDefault();
                    fila.VENTAS_ID = venta.ID;
                    fila.ID = udeta + 1;
                    con.DETALLE_VENTAS.Add(fila);
                    con.SaveChanges();
                }
                if (reservas.Contains(";"))
                {
                    var idReservas = reservas.Split(';');
                    foreach (var x in idReservas)
                    {
                        if (x!="")
                        {
                            int id = int.Parse(x);
                            var diagnostico = (from a in con.DIAGNOSTICO
                                               where a.RESERVA_HORA_ID == id
                                               select a).FirstOrDefault();
                            if (diagnostico != null)
                            {
                                diagnostico.ESTADO_DIAGNOSTICO = "PAGADO";
                                con.SaveChanges();
                            }
                        }                       
                            
                    }
                }             

                
                return "creado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VentasVIEW> ListarVentasRangoFecha(DateTime desde, DateTime hasta)
        {
            EntitiesServiexpress con = new EntitiesServiexpress();
            var _query = (from a in con.VENTAS
                          join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                          join c in con.PERSONA on b.PERSONA_ID equals c.ID
                          join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                          join e in con.PERSONA on d.PERSONA_ID equals e.ID
                          join f in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals f.ID
                          join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                          join h in con.TIPO_VENTA on a.TIPO_VENTA_ID equals h.ID
                          where a.FECHA_VENTA >= desde &&
                          a.FECHA_VENTA <= hasta
                          orderby a.ID descending
                          select new VentasVIEW
                          {
                              ID = a.ID,
                              FECHA_CREACION = a.FECHA_CREACION,
                              FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                              CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                              FECHA_VENTA = a.FECHA_VENTA,
                              MONTO_TOTAL = a.MONTO_TOTAL,
                              CLIENTE_ID = a.CLIENTE_ID,
                              EMPLEADO_ID = a.EMPLEADO_ID,
                              MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                              SUCURSAL_ID = a.SUCURSAL_ID,
                              TIPO_VENTA_ID = a.TIPO_VENTA_ID,
                              NOMBRE_CLIENTE = c.NOMBRE,
                              APELLID_CLIENTE = c.APELLIDO,
                              NOMBRE_EMPLEADO = e.NOMBRE,
                              APELLIDO_EMPLEADO = e.APELLIDO,
                              TIPO_MONEDA = f.TIPO_MODONEDA,
                              NOMBRE_SUCURSAL = g.NOMBRE,
                              TIPO_VENTA = h.NOMBRE
                          }).ToList();
            return _query;
        }

        public List<DETALLE_VENTAS> ListarDetalleVenta(int idBoleta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.DETALLE_VENTAS
                              where a.VENTAS_ID == idBoleta
                              orderby a.ID ascending
                              select a).ToList();
                return _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VentasVIEW> ListarTodasVentas()
        {
            EntitiesServiexpress con = new EntitiesServiexpress();
            var _query = (from a in con.VENTAS
                          join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                          join c in con.PERSONA on b.PERSONA_ID equals c.ID
                          join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                          join e in con.PERSONA on d.PERSONA_ID equals e.ID
                          join f in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals f.ID
                          join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                          join h in con.TIPO_VENTA on a.TIPO_VENTA_ID equals h.ID
                          orderby a.ID descending
                          select new VentasVIEW
                          {
                              ID = a.ID,
                              FECHA_CREACION = a.FECHA_CREACION,
                              FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                              CANTIDAD_TOTAL = a.CANTIDAD_TOTAL,
                              FECHA_VENTA = a.FECHA_VENTA,
                              MONTO_TOTAL = a.MONTO_TOTAL,
                              CLIENTE_ID = a.CLIENTE_ID,
                              EMPLEADO_ID = a.EMPLEADO_ID,
                              MULTI_MONEDA_ID = a.MULTI_MONEDA_ID,
                              SUCURSAL_ID = a.SUCURSAL_ID,
                              TIPO_VENTA_ID = a.TIPO_VENTA_ID,
                              NOMBRE_CLIENTE = c.NOMBRE,
                              APELLID_CLIENTE = c.APELLIDO,
                              NOMBRE_EMPLEADO = e.NOMBRE,
                              APELLIDO_EMPLEADO = e.APELLIDO,
                              TIPO_MONEDA = f.TIPO_MODONEDA,
                              NOMBRE_SUCURSAL = g.NOMBRE,
                              TIPO_VENTA = h.NOMBRE
                          }).ToList();
            return _query;
        }

        public int ObtenerUtlimoIdVenta()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                int uventa = (from a in con.VENTAS
                              orderby a.ID descending
                              select a.ID).FirstOrDefault();
                return uventa;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DatosDocumentoPagoVIEW CargarDatos(int idVenta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.VENTAS
                              join y in con.TIPO_VENTA on a.TIPO_VENTA_ID equals y.ID
                              join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                              join e in con.PERSONA on d.PERSONA_ID equals e.ID
                              join x in con.COMUNA on c.COMUNA_ID equals x.ID
                              join f in con.MULTI_MONEDA on a.MULTI_MONEDA_ID equals f.ID
                              join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                              join z in con.MULTI_EMPRESA on g.MULTI_EMPRESA_ID equals z.ID
                              join h in con.TIPO_VENTA on a.TIPO_VENTA_ID equals h.ID
                              where a.ID == idVenta
                              select new DatosDocumentoPagoVIEW
                              {
                                  NOMBRE_MULTIEMPRESA = z.RAZON_SOCIAL,
                                  DIRECCION_MULTIEMPRESA = z.DIRECCION,
                                  TELEFONO_MULTIEMPRESA = z.NUMERO_TELEFONO,
                                  RUT_MULTIEMPRESA = z.RUT,
                                  FOLIO = a.ID,
                                  NOMBRE_CLIENTE = c.NOMBRE,
                                  APELLIDO_CLIENTE = c.APELLIDO,
                                  DIRECCION_CLIENTE = c.DIRECCION,
                                  COMUNA_CLIENTE = x.NOMBRE,
                                  FECHA_EMISION = a.FECHA_VENTA,
                                  NUM_CLIENTE = c.NUM_ID,
                                  DIV_CLIENTE = c.DIV_ID,
                                  CORREO_CLIENTE = c.CORREO,
                                  TELEFONO1_CLIENTE = c.TELEFONO_CELULAR,
                                  TELEFONO_CLIENTE = c.TELEFONO_FIJO,
                                  TIPO_DOCUMENTO = y.NOMBRE,
                                  TOTAL = a.MONTO_TOTAL,
                                  COSTO_MONEDA = f.MONTO,
                                  TIPO_MONEDA = f.TIPO_MODONEDA

                              }).FirstOrDefault();
                return _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleBoletaVIEW> ListarDetalleBoleta(int idVenta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.DETALLE_VENTAS
                              where a.VENTAS_ID == idVenta
                              orderby a.ID ascending
                              select new DetalleBoletaVIEW
                              {
                                  CANTIDAD = a.CANTIDAD,
                                  DESCRIPCION = a.NOMBRE_PRODUCTO,
                                  PRECIO_UNITARIO = a.PRECIO_VENTA,
                                  TOTAL = a.MONTO_TOTAL

                              }).ToList();
                return _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VENTAS ObtenerVentaId(int venta)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.VENTAS
                              where a.ID == venta
                              select a).FirstOrDefault();
                return _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
