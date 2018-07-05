using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class RequerimientoNEG
    {
        public string CrearRequerimiento(int sucursal, int empleado, int cliente, int vehiculo, string observacion, DataTable tablaProductos, DataTable tablaServicios)
        {
            try
            {
                if (sucursal > -1)
                {
                    if (empleado > -1)
                    {
                        if (cliente > -1)
                        {
                            if (vehiculo > -1)
                            {
                                if (tablaServicios.Rows.Count > 0)
                                {
                                    RESERVA_HORA reserva = new RESERVA_HORA();
                                    DIAGNOSTICO diagnostico = new DIAGNOSTICO();
                                    List<SERVICIOS_X_DIAGNOSTICO> serviciosDiagnostico = new List<SERVICIOS_X_DIAGNOSTICO>();
                                    List<PRODUCTOS_X_DIAGNOSTICO> productosDiagnostico = new List<PRODUCTOS_X_DIAGNOSTICO>();
                                    RequerimeintoDAL requerimeintoDAL = new RequerimeintoDAL();

                                    reserva.FECHA_CREACION = DateTime.Now;
                                    reserva.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                    reserva.CLIENTE_ID = cliente;
                                    reserva.EMPLEADO_ID = empleado;
                                    reserva.ESTADO_RESERVA_ID = 1;
                                    reserva.SUCURSAL_ID = sucursal;
                                    reserva.TIPO_RESERVA_ID = 1;
                                    reserva.VEHICULO_ID = vehiculo;
                                    reserva.ORSERVACION_FINAL = observacion;
                                    reserva.FECHA_RESERVA = reserva.FECHA_CREACION;

                                    decimal montoTotal = 0;

                                    foreach (DataRow fila in tablaProductos.Rows)
                                    {
                                        PRODUCTOS_X_DIAGNOSTICO detalle = new PRODUCTOS_X_DIAGNOSTICO();
                                        detalle.ID_PRODUCTO = int.Parse(fila.ItemArray[0].ToString());
                                        detalle.CANTIDAD_PROD = int.Parse(fila.ItemArray[2].ToString());
                                        productosDiagnostico.Add(detalle);

                                        montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[4].ToString());
                                    }
                                    foreach (DataRow fila in tablaServicios.Rows)
                                    {
                                        SERVICIOS_X_DIAGNOSTICO detalle = new SERVICIOS_X_DIAGNOSTICO();
                                        detalle.ID_SERVICIO = int.Parse(fila.ItemArray[0].ToString());
                                        detalle.ID_ESTADO = 1;
                                        serviciosDiagnostico.Add(detalle);

                                        montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[3].ToString());
                                    }

                                    diagnostico.FECHA_CREACION = DateTime.Now;
                                    diagnostico.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                    diagnostico.SUCURSAL_ID = sucursal;
                                    diagnostico.VALOR_FINAL = montoTotal;
                                    diagnostico.ESTADO_DIAGNOSTICO = "RESERVADO";

                                    return requerimeintoDAL.CrearRequerimiento(reserva, serviciosDiagnostico,productosDiagnostico,diagnostico);
                                }
                                else { return "Debe agregar servicios a la orden de trabajo"; }
                            }
                            else { return "Debe indicar una vehiculo"; }
                        }
                        else { return "Debe indicar un cliente"; }
                    }
                    else { return "Debe indicar un empleado"; }
                }
                else { return "Debe indicar una sucursal"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarRequerimiento(ReservaVIEW cargaReservaVIEW, DataTable tablaServicios, DataTable tablaProductos)
        {
            try
            {
                if (tablaServicios.Rows.Count > 0)
                {
                    RESERVA_HORA reserva = new RESERVA_HORA();
                    DIAGNOSTICO diagnostico = new DIAGNOSTICO();
                    List<SERVICIOS_X_DIAGNOSTICO> serviciosDiagnostico = new List<SERVICIOS_X_DIAGNOSTICO>();
                    List<PRODUCTOS_X_DIAGNOSTICO> productosDiagnostico = new List<PRODUCTOS_X_DIAGNOSTICO>();
                    RequerimeintoDAL requerimeintoDAL = new RequerimeintoDAL();

                    reserva.ID = cargaReservaVIEW.ID;
                    reserva.FECHA_ULTIMO_UPDATE = cargaReservaVIEW.FECHA_ULTIMO_UPDATE;
                    reserva.ORSERVACION_FINAL = cargaReservaVIEW.ORSERVACION_FINAL;
                   
                    decimal montoTotal = 0;

                    foreach (DataRow fila in tablaProductos.Rows)
                    {
                        PRODUCTOS_X_DIAGNOSTICO detalle = new PRODUCTOS_X_DIAGNOSTICO();
                        detalle.ID_DIAGNOSTICO = cargaReservaVIEW.ID_DIAGNOTICO;
                        detalle.ID_PRODUCTO = int.Parse(fila.ItemArray[0].ToString());
                        detalle.CANTIDAD_PROD = int.Parse(fila.ItemArray[2].ToString());
                        productosDiagnostico.Add(detalle);

                        montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[4].ToString());
                    }
                    foreach (DataRow fila in tablaServicios.Rows)
                    {
                        
                        SERVICIOS_X_DIAGNOSTICO detalle = new SERVICIOS_X_DIAGNOSTICO();
                        detalle.ID_SERVICIO = int.Parse(fila.ItemArray[0].ToString());
                        detalle.ID_DIAGNOSTICO = cargaReservaVIEW.ID_DIAGNOTICO;
                        string estado = fila.ItemArray[2].ToString();
                        if (estado == "ANALIZANDO")
                        {
                            detalle.ID_ESTADO = 1;
                            diagnostico.ESTADO_DIAGNOSTICO = "INICIADO";
                        }
                        if (estado == "EN PROCESO")
                        {
                            detalle.ID_ESTADO = 2;
                            diagnostico.ESTADO_DIAGNOSTICO = "INICIADO";
                        }
                        if (estado == "COMPLETADO")
                        {
                            detalle.ID_ESTADO = 3;
                        }
                        serviciosDiagnostico.Add(detalle);

                        montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[3].ToString());
                    }

                    diagnostico.ID = cargaReservaVIEW.ID_DIAGNOTICO;
                    diagnostico.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    diagnostico.VALOR_FINAL = montoTotal;
                    

                    return requerimeintoDAL.ActualizarRequerimiento(reserva, serviciosDiagnostico, productosDiagnostico, diagnostico);
                }
                else { return "La tabla de servicios no puede estar vacia"; }

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
                RequerimeintoDAL requerimeintoDAL = new RequerimeintoDAL();
                return requerimeintoDAL.EliminarRequerimiento(cargaReservaVIEW);
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
                RequerimeintoDAL requerimeintoDAL = new RequerimeintoDAL();
                return requerimeintoDAL.FinalizarRequerimiento(iDRESERVA,observacion,fechaActualizacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
