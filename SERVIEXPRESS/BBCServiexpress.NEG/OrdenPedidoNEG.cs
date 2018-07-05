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
    public class OrdenPedidoNEG
    {
        public string CrearOrdenPedido(int proveedor,int sucursal,string emailSucursal,int tipoMultimoneda,int empleado, string emailProveedor,DataTable tabla)
        {
            try
            {
                ORDEN_PEDIDO orden = new ORDEN_PEDIDO();
                List<DETALLE_ORDEN_PEDIDO> detalleOrden = new List<DETALLE_ORDEN_PEDIDO>();
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();

                if (proveedor > -1)
                {
                    if (sucursal > -1)
                    {
                        if (emailProveedor.Trim().Length >4)
                        {
                            if (tabla.Rows.Count>0)
                            {
                                if (tipoMultimoneda > -1)
                                {
                                    if (empleado > -1)
                                    {
                                        if (emailSucursal.Trim().Length >4)
                                        {
                                            orden.FECHA_CREACION = DateTime.Now;
                                            orden.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                            decimal montoTotal = 0;
                                            int cantidadTotal = 0;

                                            foreach (DataRow fila in tabla.Rows)
                                            {
                                                DETALLE_ORDEN_PEDIDO detalle = new DETALLE_ORDEN_PEDIDO();
                                                detalle.FECHA_CREACION = DateTime.Now;
                                                detalle.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                detalle.CANTIDAD = int.Parse(fila.ItemArray[2].ToString());
                                                detalle.MONTO_TOTAL = Decimal.Parse(fila.ItemArray[4].ToString());
                                                detalle.NOMBRE_PRODUCTO = fila.ItemArray[1].ToString();
                                                detalle.PRECIO_COMPRA = Decimal.Parse(fila.ItemArray[3].ToString());
                                                detalle.MULTI_MONEDA_ID = tipoMultimoneda;
                                                detalle.PRODUCTO_ID = int.Parse(fila.ItemArray[0].ToString());
                                                detalleOrden.Add(detalle);

                                                montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[4].ToString());
                                                cantidadTotal = cantidadTotal + int.Parse(fila.ItemArray[2].ToString());
                                            }

                                            orden.CANTIDAD_TOTAL = cantidadTotal;
                                            orden.MONTO_TOTAL = montoTotal;
                                            orden.ESTADO_ORDEN_PEDIDO_ID = 1;
                                            orden.EMPLEADO_ID = empleado;
                                            orden.MULTI_MONEDA_ID = tipoMultimoneda;
                                            orden.PROVEEDOR_ID = proveedor;
                                            orden.SUCURSAL_ID = sucursal;
                                            orden.TIPO_ORDEN_PEDIDO_ID = 1;
                                            orden.EMAIL_PROVEEDOR = emailProveedor;
                                            orden.EMAIL_SUCURSAL = emailSucursal;                                           
                                            return ordenPedidoDAL.CrearOrdenPedido(orden,detalleOrden);
                                        }
                                        else { return "Debe email de la sucursal para enviar una copia del pedido"; }
                                    }
                                    else { return "Debe indicar un empleado responsable"; }
                                }
                                else { return "Debe indicar un tipo de moneda"; }
                            }
                            else { return "Debe agregar productos para solicitarlos"; }
                        }
                        else { return "Debe indicar email del proveedor para enviar el pedido"; }
                    }
                    else { return "Debe indicar una sucursal"; }
                }
                else { return "Debe indicar un proveedor"; }              

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
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.ListarTodasOrdenesPedidos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdenPedidoVIEW CargarOrdenPedido(int idOrdenPedido)
        {
            try
            {
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.CargarOrdenPedido(idOrdenPedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarOrdenPedido(OrdenPedidoVIEW ordenPedidCarga,int proveedor,int sucursal,string emailSucursal,int tipoMultimoneda,int empleado,string emailProveedor,DateTime fechaActualizacion,DataTable tabla)
        {
            try
            {
                if (emailProveedor.Trim().Length > 4)
                {
                    if (tabla.Rows.Count > 0)
                    {
                        if (tipoMultimoneda > -1)
                        {
                            if (empleado > -1)
                            {
                                if (emailSucursal.Trim().Length > 4)
                                {
                                    ORDEN_PEDIDO orden = new ORDEN_PEDIDO();
                                    List<DETALLE_ORDEN_PEDIDO> detalleOrden = new List<DETALLE_ORDEN_PEDIDO>();
                                    OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();

                                    orden.FECHA_ULTIMO_UPDATE = fechaActualizacion;
                                    decimal montoTotal = 0;
                                    int cantidadTotal = 0;

                                    foreach (DataRow fila in tabla.Rows)
                                    {
                                        DETALLE_ORDEN_PEDIDO detalle = new DETALLE_ORDEN_PEDIDO();
                                        detalle.FECHA_CREACION = DateTime.Now;
                                        detalle.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                        detalle.CANTIDAD = int.Parse(fila.ItemArray[2].ToString());
                                        detalle.MONTO_TOTAL = Decimal.Parse(fila.ItemArray[4].ToString());
                                        detalle.NOMBRE_PRODUCTO = fila.ItemArray[1].ToString();
                                        detalle.PRECIO_COMPRA = Decimal.Parse(fila.ItemArray[3].ToString());
                                        detalle.MULTI_MONEDA_ID = tipoMultimoneda;
                                        detalle.PRODUCTO_ID = int.Parse(fila.ItemArray[0].ToString());
                                        detalleOrden.Add(detalle);

                                        montoTotal = montoTotal + Decimal.Parse(fila.ItemArray[4].ToString());
                                        cantidadTotal = cantidadTotal + int.Parse(fila.ItemArray[2].ToString());
                                    }

                                    orden.ID = ordenPedidCarga.ID;
                                    orden.CANTIDAD_TOTAL = cantidadTotal;
                                    orden.MONTO_TOTAL = montoTotal;
                                    orden.EMPLEADO_ID = empleado;
                                    orden.ESTADO_ORDEN_PEDIDO_ID = ordenPedidCarga.ESTADO_ORDEN_PEDIDO_ID;
                                    orden.MULTI_MONEDA_ID = tipoMultimoneda;
                                    orden.EMAIL_PROVEEDOR = emailProveedor;
                                    orden.EMAIL_SUCURSAL = emailSucursal;
                                    return ordenPedidoDAL.ActualizarOrdenPedido(orden, detalleOrden);

                                }
                                else { return "Debe email de la sucursal para enviar una copia del pedido"; }
                            }
                            else { return "Debe indicar un empleado responsable"; }
                        }
                        else { return "Debe indicar un tipo de moneda"; }
                    }
                    else { return "Debe agregar productos para solicitarlos"; }
                }
                else { return "Debe indicar email del proveedor para enviar el pedido"; }

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
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.RechazarOrdenPedido(idOrden);
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
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.EnviarOrdenPedido(idOrden);
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
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.EliminarOrdenPedido(idOrden);
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
                OrdenPedidoDAL ordenPedidoDAL = new OrdenPedidoDAL();
                return ordenPedidoDAL.FiltrarOrdenesPedidos(tipo, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
