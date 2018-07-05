using AppServiexpress.Ventanas.Requerimientos.Manual;
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
    /// Lógica de interacción para IngresarRequerimientos_AD.xaml
    /// </summary>
    public partial class IngresarRequerimientos_AD : Window
    {
        public IngresarRequerimientos_AD()
        {
            InitializeComponent();
            dpkFechaActualizacion.IsEnabled = false;
            dpkFechaCreacion.SelectedDate = DateTime.Now;
            dpkFechaCreacion.IsEnabled = false;
            cbxVehiculo.IsEnabled = false;
            cbxEmpleadoRecepcion.IsEnabled = false;
            btnExportar.IsEnabled = false;
            CargarCombos();
            CargarTablaProductos();
            CargarTablaServicios();
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

        private void CargarCombos()
        {
            try
            {
                ClientesNEG clientesNEG = new ClientesNEG();
                SucursalNEG sucursalNEG = new SucursalNEG();
              
                List<ClienteVIEW> listaCliente = clientesNEG.ListarTodosClientes();
                if (listaCliente.Count > 0)
                {
                    cbxCliente.ItemsSource = listaCliente;
                    cbxCliente.DisplayMemberPath = "NUM_ID";
                    cbxCliente.SelectedValuePath = "ID";
                }

                List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursal.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursal;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void cbxCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxCliente.SelectedValue != null)
            {
                try
                {
                    string cliente = cbxCliente.SelectedValue.ToString();
                    VehiculosNEG vehiculosNEG = new VehiculosNEG();
                    var datos = vehiculosNEG.FiltrarVehiculos("ID CLIENTE", cliente);
                    cbxVehiculo.ItemsSource = null;
                    ClientesNEG clientesNEG = new ClientesNEG();
                    var datosCliente = clientesNEG.CargarCliente(int.Parse(cliente));
                    txtNombreCliente.Text = datosCliente.NOMBRE + " " + datosCliente.APELLIDO;
                    if (datos.Count > 0)
                    {
                        cbxVehiculo.ItemsSource = datos;
                        cbxVehiculo.DisplayMemberPath = "PATENTE";
                        cbxVehiculo.SelectedValuePath = "ID";
                        cbxVehiculo.IsEnabled = true;
                    }
                    else
                    {
                        cbxVehiculo.IsEnabled = false;
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        public void AgregarItemTablaServicios(int idServicio, string nombre, object costo)
        {
            try
            {
                DataTable tabla = ((DataView)dgServicios.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID SERVICIO"] = idServicio;
                fila["TIPO SERVICIO"] = nombre;
                fila["COSTO"] = costo;
                fila["ESTADO"] = "ANALIZANDO";
                tabla.Rows.Add(fila);
                dgServicios.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void cbxSucursal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSucursal.SelectedValue != null)
            {
                try
                {
                    int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                    EmpleadosNEG empleadosNEG = new EmpleadosNEG();
                    var datos = empleadosNEG.ListarTodosEmpleadosSucursal(sucursal);
                    cbxEmpleadoRecepcion.ItemsSource = null;
                    CargarTablaProductos();
                    CargarTablaServicios();
                    if (datos.Count > 0)
                    {
                        cbxEmpleadoRecepcion.ItemsSource = datos;
                        cbxEmpleadoRecepcion.DisplayMemberPath = "NUM_ID";
                        cbxEmpleadoRecepcion.SelectedValuePath = "ID";
                        cbxEmpleadoRecepcion.IsEnabled = true;
                    }
                    else
                    {
                        cbxEmpleadoRecepcion.IsEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtFolio.Text = "00000000";
            cbxCliente.SelectedIndex = -1;
            cbxSucursal.SelectedIndex = -1;
            dpkFechaCreacion.SelectedDate = DateTime.Now;
            txtNombreCliente.Text = "";
            txtNombreEmpleado.Text = "";
            cbxEmpleadoRecepcion.SelectedIndex = -1;
            CargarTablaProductos();
            CargarTablaServicios();
            txtObservacion.Text = "";
            btnExportar.IsEnabled = false;
        }
        private void btnAgregarServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                        AgregarServiciosRequerimiento reg = new AgregarServiciosRequerimiento(sucursal, this);
                        reg.ShowDialog();
                    }
                    else
                    { MessageBox.Show("Debe seleccionar una sucursal"); }
                }
                else { MessageBox.Show("Debe seleccionar un sucursal"); }

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
                        tabla.Rows.Remove(fila);
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
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                        AgregarProductosRequerimieto reg = new AgregarProductosRequerimieto(sucursal, this);
                        reg.ShowDialog();
                    }
                    else
                    { MessageBox.Show("Debe seleccionar una sucursal"); }
                }
                else
                { MessageBox.Show("Debe seleccionar una sucursal"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnQuitarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgProductos.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                    for (int i = 0; i < dgProductos.SelectedItems.Count; i++)
                    {
                        int indice = dgProductos.Items.IndexOf(dgProductos.SelectedItems[i]);
                        DataRow fila = tabla.Rows[indice];
                        tabla.Rows.Remove(fila);
                    }
                    dgProductos.ItemsSource = null;
                    dgProductos.ItemsSource = tabla.DefaultView;
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

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (cbxVehiculo.SelectedValue != null)
                    {
                        if (cbxEmpleadoRecepcion.SelectedValue != null)
                        {
                            if (cbxCliente.SelectedValue != null)
                            {
                                RequerimientoNEG requerimientoNEG = new RequerimientoNEG();
                                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                                int empleado = int.Parse(cbxEmpleadoRecepcion.SelectedValue.ToString());
                                int cliente = int.Parse(cbxCliente.SelectedValue.ToString());
                                int vehiculo = int.Parse(cbxVehiculo.SelectedValue.ToString());
                                string observacion = txtObservacion.Text;
                                DataTable tablaProductos = ((DataView)dgProductos.ItemsSource).ToTable();
                                DataTable tablaServicios = ((DataView)dgServicios.ItemsSource).ToTable();
                                string respuesta = requerimientoNEG.CrearRequerimiento(sucursal,empleado,cliente,vehiculo,observacion,tablaProductos,tablaServicios);
                                if (respuesta == "creado")
                                {
                                    RequerimeintoDAL req = new RequerimeintoDAL();
                                    RESERVA_HORA obj = req.ObtenerUltimo();
                                    lblidOrdentrabajo.Content = obj.ID;
                                    btnExportar.IsEnabled = true;
                                    MessageBox.Show("La orden de trabajo ha sido ingresada correctamente. Se habilito la descarga del comprobante");
                                }
                                else
                                {
                                    MessageBox.Show(respuesta);
                                }
                            }
                            else
                            { MessageBox.Show("Debe seleccionar un cliente"); }
                        }
                        else
                        { MessageBox.Show("Debe seleccionar un empleado"); }
                    }
                    else
                    { MessageBox.Show("Debe seleccionar una moneda"); }
                }
                else
                { MessageBox.Show("Debe seleccionar una sucursal"); }

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
                int idReserva = int.Parse(lblidOrdentrabajo.Content.ToString());                
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
                modelo.NombreEmpleado = reserva.NOMBRE_EMPLEADO+ " "+reserva.APELLIDO_EMPLEADO;
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

        private void cbxEmpleadoRecepcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxEmpleadoRecepcion.SelectedValue != null)
            {
                try
                {
                    string empleado = cbxEmpleadoRecepcion.SelectedValue.ToString();
                    EmpleadosNEG empleadosNEG = new EmpleadosNEG();
                    var datosCliente = empleadosNEG.CargarEmpleado(int.Parse(empleado));
                    txtNombreEmpleado.Text = datosCliente.NOMBRE + " " + datosCliente.APELLIDO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_IngresarRequerimiento reg = new AYUDA_IngresarRequerimiento();
            reg.Show();
        }
    }
}
