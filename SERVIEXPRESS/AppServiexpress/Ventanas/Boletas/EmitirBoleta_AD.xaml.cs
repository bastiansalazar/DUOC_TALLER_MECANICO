using AppServiexpress.Ventanas.Boletas.Manual;
using AppServiexpress.Ventanas.Boletas.Modal_Interno;
using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using BBCServiexpress.NEG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
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

namespace AppServiexpress.Ventanas.Boletas
{
    /// <summary>
    /// Lógica de interacción para EmitirBoleta_AD.xaml
    /// </summary>
    public partial class EmitirBoleta_AD : Window
    {
        public EmitirBoleta_AD()
        {
            InitializeComponent();
            CargarCombos();
            CargarTablaDetalle();
            dpkFechaCreacion.SelectedDate = DateTime.Now;
        }

        public void CargarTablaDetalle()
        {
            try
            {
                dgDetalleDocumento.ItemsSource = null;
                DataTable tabla = new DataTable();
                tabla.Columns.Add("CANTIDAD");
                tabla.Columns.Add("TIPO ITEM");
                tabla.Columns.Add("ID ITEM");
                tabla.Columns.Add("NOMBRE ITEM");
                tabla.Columns.Add("P UNITARIO");
                tabla.Columns.Add("TOTAL");
                dgDetalleDocumento.ItemsSource = tabla.DefaultView;

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
                    cbxEmpleado.ItemsSource = null;
                    CargarTablaDetalle();
                    if (datos.Count > 0)
                    {
                        cbxEmpleado.ItemsSource = datos;
                        cbxEmpleado.DisplayMemberPath = "NUM_ID";
                        cbxEmpleado.SelectedValuePath = "ID";
                        cbxEmpleado.IsEnabled = true;
                    }
                    else
                    {
                        cbxEmpleado.IsEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void CargarCombos()
        {
            try
            {
                ClientesNEG clientesNEG = new ClientesNEG();
                SucursalNEG sucursalNEG = new SucursalNEG();
                Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();
                MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();

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

                List<TIPO_VENTA> listaTipoVenta = tipos_EstadosNEG.ListarTVentas();
                if (listaTipoVenta.Count > 0)
                {
                    cbxTipoDocumento.ItemsSource = listaTipoVenta;
                    cbxTipoDocumento.DisplayMemberPath = "NOMBRE";
                    cbxTipoDocumento.SelectedValuePath = "ID";
                }

                List <MULTI_MONEDA> listaMoneda = multiMonedaNEG.ListarMultiMoneda();
                if (listaMoneda.Count > 0)
                {
                    cbxMoneda.ItemsSource = listaMoneda;
                    cbxMoneda.DisplayMemberPath = "TIPO_MODONEDA";
                    cbxMoneda.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        public void Limpiar()
        {
            cbxCliente.SelectedIndex = -1;
            cbxSucursal.SelectedIndex = -1;
            cbxTipoDocumento.SelectedIndex = -1;
            cbxMoneda.SelectedIndex = -1;
            CargarTablaDetalle();
            txtNeto.Text = "";
            txtIva.Text = "";
            txtTotal.Text = "";
            txtTotalMoneda.Text = "";
        }

        internal void AgregarItemDetalle(string cantidad, string tipoItem, string idItem, string nombreItem,string precioUni ,string total)
        {
            try
            {
                DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["CANTIDAD"] = cantidad;
                fila["TIPO ITEM"] = tipoItem;
                fila["ID ITEM"] = idItem;
                fila["NOMBRE ITEM"] = nombreItem;
                fila["P UNITARIO"] = precioUni;
                fila["TOTAL"] = total;
                tabla.Rows.Add(fila);
                dgDetalleDocumento.ItemsSource = tabla.DefaultView;
                CalcularTotalCLP();
                if (cbxMoneda.SelectedValue != null)
                {
                    int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                    MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
                    var datos = multiMonedaNEG.CargarMultiMoneda(moneda);
                    decimal valorMoneda = Convert.ToDecimal(datos.MONTO);
                    decimal costoCLP = Convert.ToDecimal(txtNeto.Text);
                    txtTotalMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void CalcularTotalCLP()
        {
            try
            {
                if (dgDetalleDocumento.Items.Count > 1)
                {
                    int total = 0;

                    int j = 0;
                    for (int i = 0; i < dgDetalleDocumento.Items.Count - 1; i++)
                    {
                        var cantidad = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var tipoItem = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var idItem = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var NombreItem = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var PreUni = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var PrecioTot = (dgDetalleDocumento.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j = 0;

                        total = total + int.Parse(PrecioTot.ToString());
                    }
                    double iva = total * 0.19;
                    txtNeto.Text = string.Format("{0:n2}", total);
                    txtIva.Text = string.Format("{0:n2}", (iva).ToString());
                    txtTotal.Text = string.Format("{0:n2}", (total+iva).ToString());
                    cbxMoneda.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAgregarOT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        if (cbxCliente.SelectedValue != null)
                        {
                            if (int.Parse(cbxCliente.SelectedValue.ToString()) > 0)
                            {
                                DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                                int cliente = int.Parse(cbxCliente.SelectedValue.ToString());
                                AgregarOT reg = new AgregarOT(sucursal,cliente, this, tabla);
                                reg.ShowDialog();
                            }
                            else
                            { MessageBox.Show("Debe seleccionar un cliente"); }
                        }
                        else
                        { MessageBox.Show("Debe seleccionar un cliente"); }
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

        private void btnAgregaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                        int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                        AgregarPRO reg = new AgregarPRO(sucursal, this);
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

        private void btnAgregaServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                        int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                        AgregarSER reg = new AgregarSER(sucursal, this);
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
                if (dgDetalleDocumento.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                    for (int i = 0; i < dgDetalleDocumento.SelectedItems.Count; i++)
                    {
                        int indice = dgDetalleDocumento.Items.IndexOf(dgDetalleDocumento.SelectedItems[i]);
                        DataRow fila = tabla.Rows[indice];
                        tabla.Rows.Remove(fila);
                    }
                    dgDetalleDocumento.ItemsSource = null;
                    dgDetalleDocumento.ItemsSource = tabla.DefaultView;
                    CalcularTotalCLP();
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

        private void cbxMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxMoneda.SelectedValue != null)
            {
                try
                {
                    int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                    MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
                    var datos = multiMonedaNEG.CargarMultiMoneda(moneda);
                    decimal valorMoneda = Convert.ToDecimal(datos.MONTO);
                    decimal costoCLP = Convert.ToDecimal(txtTotal.Text);
                    txtTotalMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
            else
            {
                txtTotalMoneda.Text = "";
            }
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue != null)
                {
                    if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                    {
                        if (cbxCliente.SelectedValue != null)
                        {
                            if (int.Parse(cbxCliente.SelectedValue.ToString()) > 0)
                            {
                                if (cbxTipoDocumento.SelectedValue != null)
                                {
                                    if (int.Parse(cbxTipoDocumento.SelectedValue.ToString()) > 0)
                                    {
                                        if (cbxMoneda.SelectedValue != null)
                                        {
                                            if (int.Parse(cbxTipoDocumento.SelectedValue.ToString()) > 0)
                                            {
                                                if (cbxEmpleado.SelectedValue != null)
                                                {
                                                    if (int.Parse(cbxEmpleado.SelectedValue.ToString()) > 0)
                                                    {
                                                        DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                                                        if (tabla.Rows.Count > 0)
                                                        {
                                                            MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar para comenzar'");
                                                            int _idCliente = int.Parse(cbxCliente.SelectedValue.ToString());
                                                            int _sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                                                            int _tipoDocumento = int.Parse(cbxTipoDocumento.SelectedValue.ToString());
                                                            DateTime _fechaDocumento = DateTime.Parse(dpkFechaCreacion.SelectedDate.ToString());
                                                            double _neto = double.Parse(txtNeto.Text);
                                                            int _idEmpleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                                                            double _iva = double.Parse(txtIva.Text);
                                                            double _total = double.Parse(txtTotal.Text);
                                                            double _totalMoneda = double.Parse(txtTotalMoneda.Text);
                                                            int _idMoneda = int.Parse(cbxTipoDocumento.SelectedValue.ToString());
                                                            List<DETALLE_VENTAS> listaDetalle = new List<DETALLE_VENTAS>();
                                                            int cantidadTotal = 0;
                                                            string reservas = "";
                                                            foreach (DataRow fila in tabla.Rows)
                                                            {
                                                                DETALLE_VENTAS detalle = new DETALLE_VENTAS();
                                                                detalle.FECHA_CREACION = DateTime.Now;
                                                                detalle.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                detalle.CANTIDAD = int.Parse(fila.ItemArray[0].ToString());
                                                                detalle.MONTO_TOTAL = int.Parse(fila.ItemArray[5].ToString());
                                                                detalle.NOMBRE_PRODUCTO = fila.ItemArray[3].ToString();
                                                                detalle.PRECIO_VENTA = Convert.ToDecimal(fila.ItemArray[4].ToString());
                                                                detalle.MULTI_MONEDA_ID = _idMoneda;
                                                                
                                                                if (fila.ItemArray[1].ToString() == "PRO")
                                                                {
                                                                    detalle.PRODUCTO_ID = int.Parse(fila.ItemArray[2].ToString());
                                                                    detalle.SERVICIO_ID = 1;
                                                                }
                                                                if (fila.ItemArray[1].ToString() == "SER")
                                                                {
                                                                    detalle.PRODUCTO_ID = 1;
                                                                    detalle.SERVICIO_ID = int.Parse(fila.ItemArray[2].ToString());
                                                                }
                                                                if (fila.ItemArray[1].ToString() == "OT")
                                                                {
                                                                    detalle.PRODUCTO_ID = 1;
                                                                    detalle.SERVICIO_ID = 1;
                                                                    reservas = reservas + fila.ItemArray[2].ToString()+";";
                                                                }
                                                                listaDetalle.Add(detalle);
                                                                cantidadTotal = cantidadTotal + int.Parse(detalle.CANTIDAD.ToString());
                                                            }
                                                            VENTAS ventas = new VENTAS();
                                                            ventas.FECHA_CREACION = DateTime.Now;
                                                            ventas.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                            ventas.CANTIDAD_TOTAL = cantidadTotal;
                                                            ventas.FECHA_VENTA = DateTime.Now;
                                                            ventas.MONTO_TOTAL = Convert.ToDecimal(_total);
                                                            ventas.CLIENTE_ID = _idCliente;
                                                            ventas.EMPLEADO_ID = _idEmpleado;
                                                            ventas.MULTI_MONEDA_ID = _idMoneda;
                                                            ventas.SUCURSAL_ID = _sucursal;
                                                            ventas.TIPO_VENTA_ID = _tipoDocumento;
                                                            VentasNEG ventasNEG = new VentasNEG();
                                                            string respuesta = ventasNEG.EmitirVenta(ventas,listaDetalle,reservas);
                                                            if (respuesta == "creado")
                                                            {                                        
                                                                int idVenta = ventasNEG.ObtenerUtlimoIdVenta();
                                                                VentasDAL ventasDAL = new VentasDAL();
                                                                DatosDocumentoPagoVIEW datos = ventasDAL.CargarDatos(idVenta);
                                                                datos.DETALLE_BOLETA = ventasDAL.ListarDetalleBoleta(idVenta);
                                                                ExportarArchivos PDF = new ExportarArchivos();
                                                                string folio = datos.FOLIO.ToString();
                                                                for (int i = 0; i < 9; i++)
                                                                {
                                                                    if (folio.Length < 8)
                                                                        folio = "0" + folio;
                                                                }
                                                                
                                                                string rutaDcoc = PDF.DocumentoPagoPDF(datos,folio);
                                                                ServerCorreo abrir_server = new ServerCorreo();
                                                                Correo correoM = new Correo();
                                                                SmtpClient server = abrir_server.InstanciaServer();
                                                                //Instancia la libreria que permite armar correo electronico y llama el metodo que lo crea
                                                                MailMessage email = correoM.CorreoEnvioFactura(rutaDcoc, datos.CORREO_CLIENTE, datos.NOMBRE_CLIENTE + " " + datos.APELLIDO_CLIENTE, folio);
                                                                //envia el correo
                                                                server.Send(email);
                                                                Limpiar();

                                                                MessageBox.Show("El documento ha sido generado correctamente. Se ha enviado una copia por correo al cliente y puede tambien buscar una copia del archivo en 'Mis Documentos'");
                                                            }

                                                        }
                                                        else
                                                        { MessageBox.Show("Deben ingresar productos o servicios al documento"); }
                                                    }
                                                    else
                                                    { MessageBox.Show("Debe seleccionar un empleado"); }
                                                }
                                                else
                                                { MessageBox.Show("Debe seleccionar un empleado"); }
                                            }
                                            else
                                            { MessageBox.Show("Debe seleccionar un tipo de documento"); }
                                        }
                                        else
                                        { MessageBox.Show("Debe seleccionar una moneda"); }
                                    }
                                    else
                                    { MessageBox.Show("Debe seleccionar un tipo de documento"); }
                                }
                                else
                                { MessageBox.Show("Debe seleccionar un tipo de documento"); }
                            }
                            else
                            { MessageBox.Show("Debe seleccionar un cliente"); }
                        }
                        else
                        { MessageBox.Show("Debe seleccionar un cliente"); }
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

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_EmitirBoleta reg = new AYUDA_EmitirBoleta();
            reg.Show();
        }
    }
}
