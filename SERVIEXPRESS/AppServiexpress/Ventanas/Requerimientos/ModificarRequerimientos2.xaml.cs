using AppServiexpress.Ventanas.Requerimientos.Modal_Interno;
using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using BBCServiexpress.NEG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppServiexpress.Ventanas.Requerimientos
{
    /// <summary>
    /// Lógica de interacción para ModificarRequerimientos2.xaml
    /// </summary>
    public partial class ModificarRequerimientos2 : Window
    {
        private DataRow dataRow;
        private ModificarRequerimientos_AD modificarRequerimientos_AD;
        private ReservaVIEW cargaReservaVIEW;


        public ModificarRequerimientos2()
        {
            InitializeComponent();
        }

        public ModificarRequerimientos2(DataRow dataRow, ModificarRequerimientos_AD modificarRequerimientos_AD)
        {
            InitializeComponent();           
            this.dataRow = dataRow;
            this.modificarRequerimientos_AD = modificarRequerimientos_AD;
            dpkFechaCreacion.IsEnabled = false;
            CargarInfoReserva(dataRow);
            dpkFechaActualizacion.DisplayDateStart = cargaReservaVIEW.FECHA_RESERVA;
            CargarTablaProductos();
            CargarTablaServicios();
            CargarFormulario(cargaReservaVIEW);
        }

        private void CargarFormulario(ReservaVIEW cargaReservaVIEW)
        {
            txtFolio.Text = dataRow["FOLIO"].ToString();
            txtNombreCliente.Text = cargaReservaVIEW.NOMBRE_CLIENTE;
            txtPatente.Text = cargaReservaVIEW.PATENTE_VEHICULO;
            dpkFechaCreacion.SelectedDate = cargaReservaVIEW.FECHA_RESERVA;
            dpkFechaActualizacion.SelectedDate = cargaReservaVIEW.FECHA_ULTIMO_UPDATE;
            txtSucursal.Text = cargaReservaVIEW.NOMBRE_SUCURSAL;
            txtNombreEmpleado.Text = cargaReservaVIEW.NOMBRE_EMPLEADO + " " + cargaReservaVIEW.APELLIDO_EMPLEADO;
            txtObservacion.Text = cargaReservaVIEW.ORSERVACION_FINAL;
            DiagnosticoDAL diagnosticoDAL = new DiagnosticoDAL();
            List<ServiciosXDiagnosticoVIEW> listaServicio = diagnosticoDAL.ListarServiciosXDiagnostico(cargaReservaVIEW.ID_DIAGNOTICO);
            foreach (var fila in listaServicio)
            {
                AgregarItemTablaServicios(int.Parse(fila.IdServicio.ToString()), fila.NombreServicio, fila.EstadoServicio,fila.CostoServicio);
            }
            List<ProductosXDiagnostico> listaProductos = diagnosticoDAL.ListarProductosXDiagnostico(cargaReservaVIEW.ID_DIAGNOTICO);
            foreach (var fila in listaProductos)
            {
                AgregarItemTablaProductos(int.Parse(fila.IdProducto.ToString()),fila.NombreProdcuto,int.Parse(fila.Cantidad.ToString()),Convert.ToDecimal(fila.PrecioUni),Convert.ToDecimal(fila.PrecioTot));
            }
        }

        internal void ActualizarItemTablaServicio(int indice, int servicio, string text, string nuevoEstado, int costoSErvicio)
        {
            try
            {
                DataTable tabla = ((DataView)dgServicios.ItemsSource).ToTable();
                DataRow fila = tabla.Rows[indice];
                fila["ID SERVICIO"] = servicio;
                fila["TIPO SERVICIO"] = text;
                fila["COSTO"] = costoSErvicio;
                fila["ESTADO"] = nuevoEstado;
                //tabla.Rows;
                dgServicios.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void CargarInfoReserva(DataRow dataRow)
        {
            int idReserva = int.Parse(dataRow["ID"].ToString());
            ReservaNEG reservaNEG = new ReservaNEG();
            cargaReservaVIEW = reservaNEG.CargarReserva(idReserva);
            if (cargaReservaVIEW.ESTADO_DIAGNOSTICO == "INICIADO")
            {
                btnEliminar.IsEnabled = false;
            }else if (cargaReservaVIEW.ESTADO_DIAGNOSTICO == "COMPLETADO" || cargaReservaVIEW.ESTADO_DIAGNOSTICO == "PAGADO")
            {
                btnEliminar.IsEnabled = false;
                btnAgregarServicio.IsEnabled = false;
                btnQuitarServicio.IsEnabled = false;
                btnAgregaProducto.IsEnabled = false;
                btnActualizarServicio.IsEnabled = false;
                btnGuadar.IsEnabled = false;
            }
        }

        public void CargarTablaProductos()
        {
            try
            {
                dgProductos.ItemsSource = null;
                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID PRODUCTO");
                tabla.Columns.Add("PRODUCTO");
                tabla.Columns.Add("CANTIDAD");
                tabla.Columns.Add("PRECIO UNITARIO");
                tabla.Columns.Add("PRECIO TOTAL");
                dgProductos.ItemsSource = tabla.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        internal void AgregarItemTablaServicios(int idServicio, string nombre, string estado, decimal? costo)
        {
            try
            {
                DataTable tabla = ((DataView)dgServicios.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID SERVICIO"] = idServicio;
                fila["TIPO SERVICIO"] = nombre;
                fila["COSTO"] = costo;
                fila["ESTADO"] = estado;
                tabla.Rows.Add(fila);
                dgServicios.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        public void CargarTablaServicios()
        {
            try
            {
                dgServicios.ItemsSource = null;
                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID SERVICIO");
                tabla.Columns.Add("TIPO SERVICIO");
                tabla.Columns.Add("ESTADO");
                tabla.Columns.Add("COSTO");
                dgServicios.ItemsSource = tabla.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar' para comenzar");
                int idReserva = cargaReservaVIEW.ID;
                ExportarArchivos pdf = new ExportarArchivos();
                PDF_ModeloOrdenTrabajo modelo = new PDF_ModeloOrdenTrabajo();
                ReservaNEG reservaNEG = new ReservaNEG();
                ReservaVIEW reserva = reservaNEG.CargarReserva(idReserva);
                DiagnosticoDAL diagnostico = new DiagnosticoDAL();
                List<ServiciosXDiagnosticoVIEW> servicios = diagnostico.ListarServiciosXDiagnostico(reserva.ID_DIAGNOTICO);
                List<ProductosXDiagnostico> productos = diagnostico.ListarProductosXDiagnostico(reserva.ID_DIAGNOTICO);
                int idFolio = reserva.ID;
                string folio = idFolio.ToString();
                for (int i = 0; i < 9; i++)
                {
                    if (folio.Length < 8)
                        folio = "0" + folio;
                }
                modelo.Folio = folio;
                modelo.FechaIngreso = reserva.FECHA_CREACION.ToString();
                modelo.FechaReserva = reserva.FECHA_RESERVA.ToString();
                modelo.EstadoDiagnostico = reserva.ESTADO_DIAGNOSTICO;
                modelo.NombreCliente = reserva.NOMBRE_CLIENTE;
                modelo.ApellidoCliente = reserva.NOMBRE_APELLIDO;
                modelo.NumCliente = reserva.NUM_ID_CLIENTE.ToString();
                modelo.DivCliente = reserva.DIV_CLIENTE.ToString();
                modelo.DireccionCliente = reserva.DIRECCION_CLIENTE;
                modelo.ComunaCliente = reserva.COMUNA_CLIENTE;
                modelo.TelCliente = reserva.TELEFONO_CLIENTE.ToString();
                modelo.Tel2Cliente = reserva.TELEFONO_CLIENTE2.ToString();
                modelo.NombreEmpleado = reserva.NOMBRE_EMPLEADO + " " + reserva.APELLIDO_EMPLEADO;
                modelo.NombreSucursal = reserva.NOMBRE_SUCURSAL;
                modelo.MarcaVehiculo = reserva.MARCA_VEHICULO;
                modelo.TipoVehiculo = reserva.TIPO_VEHICULO;
                modelo.DireccionSucursal = reserva.DIRECCION_SUCURSAL;
                modelo.TelefonoSucursal = reserva.TELEFONO_SUCURSAL.ToString();
                modelo.Observacion = reserva.ORSERVACION_FINAL;
                modelo.TotalTrabajo = int.Parse(reserva.TOTAL.ToString());
                modelo.PatenteVehiculo = reserva.PATENTE_VEHICULO;
                modelo.ListaServicios = servicios;
                modelo.ListaProductos = productos;
                string respuesta = pdf.OrdenTrabajoPedidoPDF(modelo);

                MessageBox.Show("El documento se genero correctamente. Ubíquelo en 'Mis Documentos'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnQuitarServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgServicios.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgServicios.ItemsSource).ToTable();
                    for (int i = 0; i < dgServicios.SelectedItems.Count; i++)
                    {
                        int indice = dgServicios.Items.IndexOf(dgServicios.SelectedItems[i]);
                        DataRow fila = tabla.Rows[indice];
                        string estado = fila.ItemArray[3].ToString();
                        if (estado =="ANALIZANDO")
                        {
                            tabla.Rows.Remove(fila);
                        }
                        else
                        {
                            MessageBox.Show("Sólo puede quitar servicios que no se hayan comenzado");
                        }
                    }
                    dgServicios.ItemsSource = null;
                    dgServicios.ItemsSource = tabla.DefaultView;
                }
                else
                {
                    MessageBox.Show("Debe seleccionar por lo menos una fila");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnAgregarServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int sucursal = cargaReservaVIEW.SUCURSAL_ID;
                AgregarServicioRequerimiento2 reg = new AgregarServicioRequerimiento2(sucursal, this);
                reg.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        internal void AgregarItemTablaProductos(int idProd, string nombre, int cantidad, decimal precioUnitario, decimal preTot)
        {
            try
            {
                DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID PRODUCTO"] = idProd;
                fila["PRODUCTO"] = nombre;
                fila["CANTIDAD"] = cantidad;
                fila["PRECIO UNITARIO"] = precioUnitario;
                fila["PRECIO TOTAL"] = preTot;
                tabla.Rows.Add(fila);
                dgProductos.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnAgregaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int sucursal = cargaReservaVIEW.SUCURSAL_ID;
                AgregarProductoReuqerimiento2 reg = new AgregarProductoReuqerimiento2(sucursal, this);
                reg.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }


        private void btnActualizarServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgServicios.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgServicios.ItemsSource).ToTable();
                    if (dgServicios.SelectedItems.Count == 1)
                    {
                        var indice = dgServicios.SelectedIndex;
                        DataRow fila = tabla.Rows[indice];
                        int diagnostico = cargaReservaVIEW.ID_DIAGNOTICO;
                        string numero3 = fila.ItemArray[2].ToString();
                        int servicio = int.Parse(fila.ItemArray[0].ToString());
                        int costo = int.Parse(fila.ItemArray[3].ToString());

                        AgregarServiciosRequerimientos3 reg = new AgregarServiciosRequerimientos3(cargaReservaVIEW.SUCURSAL_ID,diagnostico,this,servicio, numero3,costo ,indice);
                        reg.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar solo una fila de los servicios");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una fila de los servicios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnGuadar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequerimientoNEG requerimientoNEG = new RequerimientoNEG();
                DateTime fechaActualizacion = DateTime.Now;
                if (dpkFechaActualizacion.SelectedDate != null)
                {
                    fechaActualizacion = Convert.ToDateTime(dpkFechaActualizacion.SelectedDate);
                }
                cargaReservaVIEW.FECHA_ULTIMO_UPDATE = fechaActualizacion;
                DataTable tablaServicios = ((DataView)dgServicios.ItemsSource).ToTable();
                DataTable tablaProductos = ((DataView)dgProductos.ItemsSource).ToTable();
                cargaReservaVIEW.ORSERVACION_FINAL = txtObservacion.Text;
                string respuesta = requerimientoNEG.ActualizarRequerimiento(cargaReservaVIEW,tablaServicios,tablaProductos);
                if (respuesta == "actualizado")
                {
                    modificarRequerimientos_AD.CargarTablaRequerimientos();
                    MessageBox.Show("La orden de trabajo ha sido actualizada satisfactoriamente");
                }
                else
                {
                    MessageBox.Show(respuesta);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Para confirmar presione aceptar");
                RequerimientoNEG requerimientoNEG = new RequerimientoNEG();
                string respuesta = requerimientoNEG.EliminarRequerimiento(cargaReservaVIEW);
                if (respuesta == "eliminado")
                {
                    modificarRequerimientos_AD.CargarTablaRequerimientos();
                    MessageBox.Show("La orden de trabajo se eliminó correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(respuesta);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }
        //
    }
}
