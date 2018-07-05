using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.DAL
{
    public class RequerimeintoDAL
    {
        public RESERVA_HORA ObtenerUltimo()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.RESERVA_HORA
                              orderby a.ID descending
                              select a).FirstOrDefault();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearRequerimiento(RESERVA_HORA reserva, List<SERVICIOS_X_DIAGNOSTICO> serviciosDiagnostico, List<PRODUCTOS_X_DIAGNOSTICO> productosDiagnostico, DIAGNOSTICO diagnostico)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                con.RESERVA_HORA.Add(reserva);
                con.SaveChanges();
                var _reserva = (from a in con.RESERVA_HORA
                              orderby a.ID descending
                              select a).FirstOrDefault();

                var _ultidiagnostico = (from a in con.DIAGNOSTICO
                                    orderby a.ID descending
                                    select a.ID).FirstOrDefault();

                diagnostico.RESERVA_HORA_ID = _reserva.ID;
                diagnostico.ID = int.Parse(_ultidiagnostico.ToString()) + 1;
                con.DIAGNOSTICO.Add(diagnostico);
                con.SaveChanges();
                var _diagnostico = (from a in con.DIAGNOSTICO
                                    orderby a.ID descending
                                    select a).FirstOrDefault();
                foreach (var fila in serviciosDiagnostico)
                {
                    decimal ultimo = (from a in con.SERVICIOS_X_DIAGNOSTICO
                                      orderby a.ID_SXD descending
                                      select a.ID_SXD).FirstOrDefault();
                    fila.ID_DIAGNOSTICO = _diagnostico.ID;
                    fila.ID_SXD = ultimo + 1;
                    con.SERVICIOS_X_DIAGNOSTICO.Add(fila);
                    con.SaveChanges();
                }
                foreach (var fila in productosDiagnostico)
                {
                    decimal ultimo = (from a in con.PRODUCTOS_X_DIAGNOSTICO
                                      orderby a.ID_PXD descending
                                      select a.ID_PXD).FirstOrDefault();
                    fila.ID_DIAGNOSTICO = _diagnostico.ID;
                    fila.ID_PXD = ultimo + 1;
                    con.PRODUCTOS_X_DIAGNOSTICO.Add(fila);
                    con.SaveChanges();

                    var producto = (from a in con.PRODUCTO
                                    where a.ID == fila.ID_PRODUCTO
                                    select a).FirstOrDefault();
                    producto.STOCK = int.Parse(producto.STOCK.ToString()) - int.Parse(fila.CANTIDAD_PROD.ToString());
                    con.SaveChanges();
                }
                return "creado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarRequerimiento(RESERVA_HORA reserva, List<SERVICIOS_X_DIAGNOSTICO> serviciosDiagnostico, List<PRODUCTOS_X_DIAGNOSTICO> productosDiagnostico, DIAGNOSTICO diagnostico)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _reserva = (from a in con.RESERVA_HORA
                                where a.ID == reserva.ID
                                select a).FirstOrDefault();

                _reserva.FECHA_ULTIMO_UPDATE = reserva.FECHA_ULTIMO_UPDATE;
                _reserva.ORSERVACION_FINAL = reserva.ORSERVACION_FINAL;
                con.SaveChanges();

                var _diagnostico = (from a in con.DIAGNOSTICO
                                    where a.ID == diagnostico.ID
                                    select a).FirstOrDefault();
                _diagnostico.FECHA_ULTIMO_UPDATE = diagnostico.FECHA_ULTIMO_UPDATE;
                _diagnostico.VALOR_FINAL = diagnostico.VALOR_FINAL;
                _diagnostico.ESTADO_DIAGNOSTICO = diagnostico.ESTADO_DIAGNOSTICO;
                con.SaveChanges();

                var serviciosViejos = (from a in con.SERVICIOS_X_DIAGNOSTICO
                                       where a.ID_DIAGNOSTICO == diagnostico.ID
                                       select a).ToList();
                foreach (var fila in serviciosViejos)
                {
                    con.SERVICIOS_X_DIAGNOSTICO.Remove(fila);
                    con.SaveChanges();
                }

                foreach (var fila in serviciosDiagnostico)
                {
                    decimal ultimo = (from a in con.SERVICIOS_X_DIAGNOSTICO
                                      orderby a.ID_SXD descending
                                      select a.ID_SXD).FirstOrDefault();
                    fila.ID_DIAGNOSTICO = diagnostico.ID;
                    fila.ID_SXD = ultimo + 1;
                    con.SERVICIOS_X_DIAGNOSTICO.Add(fila);
                    con.SaveChanges();
                }
                var productosViejos = (from a in con.PRODUCTOS_X_DIAGNOSTICO
                                       where a.ID_DIAGNOSTICO == diagnostico.ID
                                       select a).ToList();
                foreach (var fila in productosViejos)
                {
                    con.PRODUCTOS_X_DIAGNOSTICO.Remove(fila);
                    con.SaveChanges();
                }
                foreach (var fila in productosDiagnostico)
                {
                    decimal ultimo = (from a in con.PRODUCTOS_X_DIAGNOSTICO
                                      orderby a.ID_PXD descending
                                      select a.ID_PXD).FirstOrDefault();
                    fila.ID_DIAGNOSTICO = diagnostico.ID;
                    fila.ID_PXD = ultimo + 1;
                    con.PRODUCTOS_X_DIAGNOSTICO.Add(fila);
                    con.SaveChanges();

                    var producto = (from a in con.PRODUCTO
                                    where a.ID == fila.ID_PRODUCTO
                                    select a).FirstOrDefault();
                    producto.STOCK = int.Parse(producto.STOCK.ToString()) - int.Parse(fila.CANTIDAD_PROD.ToString());
                    con.SaveChanges();
                }
                return "actualizado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EliminarRequerimiento(ReservaVIEW cargaReservaVIEW)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var serviciosViejos = (from a in con.SERVICIOS_X_DIAGNOSTICO
                                       where a.ID_DIAGNOSTICO == cargaReservaVIEW.ID_DIAGNOTICO
                                       select a).ToList();
                foreach (var fila in serviciosViejos)
                {
                    con.SERVICIOS_X_DIAGNOSTICO.Remove(fila);
                    con.SaveChanges();
                }
                var productosViejos = (from a in con.PRODUCTOS_X_DIAGNOSTICO
                                       where a.ID_DIAGNOSTICO == cargaReservaVIEW.ID_DIAGNOTICO
                                       select a).ToList();
                foreach (var fila in productosViejos)
                {
                    con.PRODUCTOS_X_DIAGNOSTICO.Remove(fila);
                    con.SaveChanges();
                }
                var diagnostico = (from a in con.DIAGNOSTICO
                                   where a.ID == cargaReservaVIEW.ID_DIAGNOTICO
                                   select a).FirstOrDefault();
                con.DIAGNOSTICO.Remove(diagnostico);
                con.SaveChanges();
                var reserva = (from a in con.RESERVA_HORA
                                   where a.ID == cargaReservaVIEW.ID
                                   select a).FirstOrDefault();
                con.RESERVA_HORA.Remove(reserva);
                con.SaveChanges();
                return "eliminado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FinalizarRequerimiento(int iDRESERVA, string observacion, DateTime fechaActualizacion)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var serviciosViejos = (from a in con.RESERVA_HORA
                                       where a.ID == iDRESERVA
                                       select a).FirstOrDefault();
                if (serviciosViejos !=null)
                {
                    serviciosViejos.FECHA_ULTIMO_UPDATE = fechaActualizacion;
                    serviciosViejos.ORSERVACION_FINAL = observacion;                    
                    con.SaveChanges();
                    var diagno = (from a in con.DIAGNOSTICO
                                           where a.RESERVA_HORA_ID == iDRESERVA
                                           select a).FirstOrDefault();
                    diagno.FECHA_ULTIMO_UPDATE = fechaActualizacion;
                    diagno.ESTADO_DIAGNOSTICO = "COMPLETADO";
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "La Orden de Trabajo no se encuentra en la base de datos";
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
