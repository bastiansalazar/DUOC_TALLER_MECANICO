using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.NEG
{
    public class ControlRecepcionNEG
    {
        public List<ControlRecepcionVIEW> ListarTodosRecepcion()
        {
            try
            {
                 ControlRecepcionDAL controlRecepcionDAL = new ControlRecepcionDAL();
                return controlRecepcionDAL.ListarTodosRecepcion();
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
                ControlRecepcionDAL controlRecepcionDAL = new ControlRecepcionDAL();
                return controlRecepcionDAL.FiltarControlFechas(desde,hasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertarControlRecepcion(string comentario, DateTime fechaRececpion, int empleado, int estado, int orden, DataTable tabla)
        {
            try
            {
                CONTROL_RECEPCION control = new CONTROL_RECEPCION();
                List<DETALLE_CONTROL_RECEPCION> detalleControl = new List<DETALLE_CONTROL_RECEPCION>();
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                OrdenPedidoVIEW ordenPedidoVIEW = new OrdenPedidoVIEW();
                ControlRecepcionDAL controlRecepcionDAL = new ControlRecepcionDAL();
                if (orden > -1)
                {                    
                    if (empleado > -1)
                    {
                        if (estado > - 1)
                        {
                            if (tabla.Rows.Count > 0)
                            {
                                ordenPedidoVIEW = ordenPedidoDAL.CargarOrdenPedido(orden);

                                control.FECHA_CREACION = DateTime.Now;
                                control.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                control.COMENTARIO = comentario;
                                control.FECHA_APROVACION = ordenPedidoVIEW.FECHA_ULTIMO_UPDATE;
                                control.FECHA_RECEPCION = fechaRececpion;
                                control.EMPLEADO_ID = empleado;
                                control.ESTADO_CONTROL_RECEPCION_ID = estado;
                                control.ORDEN_PEDIDO_ID = orden;

                                foreach (DataRow fila in tabla.Rows)
                                {
                                    DETALLE_CONTROL_RECEPCION detalle = new DETALLE_CONTROL_RECEPCION();
                                    detalle.FECHA_CREACION = DateTime.Now;
                                    detalle.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                    detalle.CANTIDAD_INGRESADA = int.Parse(fila.ItemArray[2].ToString());
                                    detalle.CANTIDAD_TOTAL = int.Parse(fila.ItemArray[2].ToString());
                                    detalle.NOMBRE_PRODUCTO = fila.ItemArray[1].ToString();
                                    detalle.PRECIO_COMPRA = Decimal.Parse(fila.ItemArray[3].ToString());
                                    detalle.ESTADO_RECEPCION_ID = estado;
                                    detalle.MULTI_MONEDA_ID = ordenPedidoVIEW.MULTI_MONEDA_ID;
                                    detalle.PRODUCTO_ID = int.Parse(fila.ItemArray[0].ToString());
                                    detalle.ESTADO_RECEPCION_ID = 1;
                                    detalleControl.Add(detalle);
                                }

                                return controlRecepcionDAL.IgresarControlRececpion(control, detalleControl);
                            }
                            else { return "Debe indicar los productos recepcionados"; }
                        }
                        else { return "Debe indicar el estado de conformmidad"; }
                    }
                    else { return "Debe indicar un empelado responsable"; }
                }
                else { return "Debe indicar un folio de orden de pedido"; }

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
                ControlRecepcionDAL controlRecepcionDAL = new ControlRecepcionDAL();
                return controlRecepcionDAL.CargarControlRecepcion(idControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
