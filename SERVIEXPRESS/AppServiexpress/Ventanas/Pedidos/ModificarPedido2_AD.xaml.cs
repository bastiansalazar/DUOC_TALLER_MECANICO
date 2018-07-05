using AppServiexpress.Ventanas.Pedidos.Modal_Interno;
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

namespace AppServiexpress.Ventanas.Pedidos
{
    /// <summary>
    /// Lógica de interacción para ModificarPedido2_AD.xaml
    /// </summary>
    public partial class ModificarPedido2_AD : Window
    {
        public DataRow dataRow;
        public ModificarPedido_AD modificarPedido;
        public OrdenPedidoVIEW ordenPedidoCarga;

        public ModificarPedido2_AD()
        {
            InitializeComponent();
            dpkFechaModificacion.SelectedDate = DateTime.Now;
        }

        public ModificarPedido2_AD(DataRow _dataRow,ModificarPedido_AD _modificarPedido)
        {
            InitializeComponent();
            CargarTablaProductos();
            dpkFechaModificacion.SelectedDate = DateTime.Now;
            CargarOrdenPedido(_dataRow);
            CargarCombos();
            this.dataRow = _dataRow;
            this.modificarPedido = _modificarPedido;
            CargarFormulario(ordenPedidoCarga);
        }

        private void CargarOrdenPedido(DataRow dataRow)
        {
            int idOrden = int.Parse(dataRow["ID"].ToString());
            OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
            ordenPedidoCarga = ordenPedidoNEG.CargarOrdenPedido(idOrden);
            if (ordenPedidoCarga.ESTADO_ORDEN_PEDIDO_ID==3 || ordenPedidoCarga.ESTADO_ORDEN_PEDIDO_ID == 4)
            {
                btnAgregaProducto.IsEnabled = false;
                btnQuitarProducto.IsEnabled = false;
                btnEnviar.IsEnabled = false;
                btnRechazar.IsEnabled = false;
                btnEliminar.IsEnabled = false;                
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

        private void CargarCombos()
        {
            MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
            EmpleadosNEG empleadosNEG = new EmpleadosNEG();

            try
            {
                List<MULTI_MONEDA> listaMultiMoneda = multiMonedaNEG.ListarMultiMoneda();
                if (listaMultiMoneda.Count > 0)
                {
                    cbxMoneda.ItemsSource = listaMultiMoneda;
                    cbxMoneda.DisplayMemberPath = "TIPO_MODONEDA";
                    cbxMoneda.SelectedValuePath = "ID";
                }
                cbxEmpleado.SelectedIndex = -1;
                cbxEmpleado.IsEnabled = false;
                txtNombreEmpleado.Text = "";
                List<EmpleadosVIEW> listaEmpleados = empleadosNEG.ListarTodosEmpleadosSucursal(ordenPedidoCarga.SUCURSAL_ID);
                if (listaEmpleados.Count > 0)
                {
                    cbxEmpleado.ItemsSource = listaEmpleados;
                    cbxEmpleado.DisplayMemberPath = "NUM_ID";
                    cbxEmpleado.SelectedValuePath = "ID";
                    cbxEmpleado.IsEnabled = true;
                }
                SucursalNEG sucursalNEG = new SucursalNEG();
                var datos = sucursalNEG.CargarSucursal(ordenPedidoCarga.SUCURSAL_ID);
                txtDireccion.Text = datos.DIRECCION;
                txtTelFijo.Text = datos.NUMERO_TELEFONO.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        public void AgregarItemTablaProductos(int _idProd, string _nombre, int _cantidad, decimal _preUni, decimal _preTot)
        {
            try
            {
                DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID PRODUCTO"] = _idProd;
                fila["PRODUCTO"] = _nombre;
                fila["CANTIDAD"] = _cantidad;
                fila["PRECIO UNITARIO"] = _preUni;
                fila["PRECIO TOTAL"] = _preTot;
                tabla.Rows.Add(fila);
                dgProductos.ItemsSource = tabla.DefaultView;
                CalcularTotalCLP();
                if (cbxMoneda.SelectedValue != null)
                {
                    int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                    MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
                    var datos = multiMonedaNEG.CargarMultiMoneda(moneda);
                    decimal valorMoneda = Convert.ToDecimal(datos.MONTO);
                    decimal costoCLP = Convert.ToDecimal(txtCostoTotal.Text);
                    txtCostoMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void CargarFormulario(OrdenPedidoVIEW data)
        {
            lblIdOrdenPedido.Content = data.ID.ToString();
            txtFolio.Text = dataRow["FOLIO"].ToString();
            txtproveedor.Text = data.PROVEEDOR;
            txtRutProveedor.Text = dataRow["RUT PROVEEDOR"].ToString();
            dpkFechaCreacion.SelectedDate = data.FECHA_CREACION;
            txtSucursal.Text = data.SUCURSAL;
            SucursalNEG sucursalNEG = new SucursalNEG();
            var datos = sucursalNEG.CargarSucursal(data.SUCURSAL_ID);
            txtDireccion.Text = datos.DIRECCION;
            txtTelFijo.Text = datos.NUMERO_TELEFONO.ToString();
            txtEmailSucursal.Text = data.EMAIL_SUCURSAL.ToString();
            cbxMoneda.SelectedValue = data.MULTI_MONEDA_ID;
            cbxEmpleado.SelectedValue = data.EMPLEADO_ID;
            txtEmailProveedor.Text = data.EMAIL_PROVEEDOR;

            int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
            EmpleadosNEG empleadosNEG = new EmpleadosNEG();
            var datos1 = empleadosNEG.CargarEmpleado(empleado);
            txtNombreEmpleado.Text = datos1.NOMBRE + " " + datos1.APELLIDO;            

            DetalleOrdenPedidoNEG detalleOrdenPedidoNEG = new DetalleOrdenPedidoNEG();
            List<DETALLE_ORDEN_PEDIDO> listaDetalle = detalleOrdenPedidoNEG.CargarlistaDetalleOrden(data.ID);
            foreach (var fila in listaDetalle)
            {
                AgregarItemTablaProductos(fila.PRODUCTO_ID, fila.NOMBRE_PRODUCTO, int.Parse(fila.CANTIDAD.ToString()),Convert.ToDecimal(fila.PRECIO_COMPRA), Convert.ToDecimal(fila.MONTO_TOTAL));
            }

            int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
            MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
            var datos2 = multiMonedaNEG.CargarMultiMoneda(moneda);
            decimal valorMoneda = Convert.ToDecimal(datos2.MONTO);
            decimal costoCLP = Convert.ToDecimal(txtCostoTotal.Text);
            txtCostoMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
        }

        private void cbxEmpleado_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxEmpleado.SelectedValue != null)
            {
                try
                {
                    int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                    EmpleadosNEG empleadosNEG = new EmpleadosNEG();
                    var datos = empleadosNEG.CargarEmpleado(empleado);
                    txtNombreEmpleado.Text = datos.NOMBRE + " " + datos.APELLIDO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }
        private void cbxMoneda_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxMoneda.SelectedValue != null)
            {
                try
                {
                    int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                    MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
                    var datos = multiMonedaNEG.CargarMultiMoneda(moneda);
                    decimal valorMoneda = Convert.ToDecimal(datos.MONTO);
                    decimal costoCLP = Convert.ToDecimal(txtCostoTotal.Text);
                    txtCostoMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
            else
            {
                txtCostoMoneda.Text = "";
            }
        }

        private void btnAgregaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int proveedor = ordenPedidoCarga.PROVEEDOR_ID;
                int sucursal = ordenPedidoCarga.SUCURSAL_ID;
                AgregarProductosPedidos2 reg = new AgregarProductosPedidos2(proveedor, sucursal, this);
                reg.ShowDialog();
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
                    CalcularTotalCLP();
                    if (cbxMoneda.SelectedValue != null)
                    {
                        int moneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                        MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
                        var datos = multiMonedaNEG.CargarMultiMoneda(moneda);
                        decimal valorMoneda = Convert.ToDecimal(datos.MONTO);
                        decimal costoCLP = Convert.ToDecimal(txtCostoTotal.Text);
                        txtCostoMoneda.Text = string.Format("{0:n2}", (Math.Truncate((costoCLP / valorMoneda))));
                    }
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

        private void CalcularTotalCLP()
        {
            try
            {
                if (dgProductos.Items.Count > 1 )
                {
                    int total = 0;

                    int j = 0;
                    for (int i = 0; i < dgProductos.Items.Count - 1; i++)
                    {
                        var Id_Producto = (dgProductos.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var Nombre_Producto = (dgProductos.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var Cantidad = (dgProductos.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var PrecioUni = (dgProductos.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j++;
                        var PrecioTot = (dgProductos.Items[i] as System.Data.DataRowView).Row.ItemArray[j];
                        j = 0;                 
                        
                        total = total + int.Parse(PrecioTot.ToString());                       
                    }
                    txtCostoTotal.Text = string.Format("{0:n2}", total);
                    cbxMoneda.IsEnabled = true;              
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
                if (cbxMoneda.SelectedValue != null)
                {
                    if (cbxEmpleado.SelectedValue != null)
                    {
                        OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                        int proveedor = ordenPedidoCarga.PROVEEDOR_ID;
                        int sucursal = ordenPedidoCarga.SUCURSAL_ID;
                        string emailSucursal = txtEmailSucursal.Text.ToString();
                        int tipoMultimoneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                        int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                        string emailProveedor = txtEmailProveedor.Text.ToString();
                        DateTime fechaActualizacion = Convert.ToDateTime(dpkFechaModificacion.SelectedDate);
                        DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                        string respuesta = ordenPedidoNEG.ActualizarOrdenPedido(ordenPedidoCarga, proveedor,sucursal,emailSucursal,tipoMultimoneda,empleado,emailProveedor,fechaActualizacion,tabla);
                        if (respuesta == "actualizado")
                        {
                            MessageBox.Show("La orden de pedido ha sido actualizada satisfactoriamente");
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                        }
                    }
                    else
                    { MessageBox.Show("Debe seleccionar un empleado"); }
                }
                else
                { MessageBox.Show("Debe seleccionar una moneda"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxMoneda.SelectedValue != null)
                {
                    if (cbxEmpleado.SelectedValue != null)
                    {
                        MessageBox.Show("Espere mientras se realiza en envío de la orden de pedido. Presione 'Aceptar' para comenzar con el envío");

                        OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                        int proveedor = ordenPedidoCarga.PROVEEDOR_ID;
                        int sucursal = ordenPedidoCarga.SUCURSAL_ID;
                        ordenPedidoCarga.ESTADO_ORDEN_PEDIDO_ID = 3;
                        string emailSucursal = txtEmailSucursal.Text.ToString();
                        int tipoMultimoneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                        int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                        string emailProveedor = txtEmailProveedor.Text.ToString();
                        DateTime fechaActualizacion = Convert.ToDateTime(dpkFechaModificacion.SelectedDate);
                        DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                        string respuesta = ordenPedidoNEG.ActualizarOrdenPedido(ordenPedidoCarga, proveedor, sucursal, emailSucursal, tipoMultimoneda, empleado, emailProveedor, fechaActualizacion, tabla);
                        if (respuesta == "actualizado")
                        {
                            ServerCorreo abrir_server = new ServerCorreo();
                            Correo correoM = new Correo();
                            ExportarArchivos pdf = new ExportarArchivos();
                            //parametriza el servidor STMP para enviar el correo
                            SmtpClient server = abrir_server.InstanciaServer();

                            //cargar modelo solicitud
                            PDF_ModeloOrdenPedido modelo = new PDF_ModeloOrdenPedido();
                            modelo.Folio = dataRow["FOLIO"].ToString();
                            modelo.NombreProveedor = ordenPedidoCarga.PROVEEDOR;
                            modelo.RolProveedor = ordenPedidoCarga.NUMID_PROVEEDOR + "-" + ordenPedidoCarga.DIVID_PROVEEDOR;
                            modelo.FechaSolicitud = ordenPedidoCarga.FECHA_CREACION.ToString();
                            modelo.FechaModificacion = dpkFechaModificacion.SelectedDate.ToString();
                            modelo.Sucursal = ordenPedidoCarga.SUCURSAL;
                            modelo.Direccion = txtDireccion.Text;
                            modelo.Telefono = txtTelFijo.Text;
                            modelo.EmailSucursal = txtEmailSucursal.Text;
                            foreach (DataRow fila in tabla.Rows)
                            {
                                DETALLE_ORDEN_PEDIDO deta = new DETALLE_ORDEN_PEDIDO();
                                deta.PRODUCTO_ID = int.Parse(fila.ItemArray[0].ToString());
                                deta.NOMBRE_PRODUCTO = fila.ItemArray[1].ToString();
                                deta.CANTIDAD = int.Parse(fila.ItemArray[2].ToString());
                                deta.PRECIO_COMPRA = decimal.Parse(fila.ItemArray[3].ToString());
                                deta.MONTO_TOTAL = decimal.Parse(fila.ItemArray[4].ToString());
                                modelo.DetalleOrdenPedido.Add(deta);
                            }
                            modelo.Moneda = cbxMoneda.Text;
                            modelo.CostoTotal = txtCostoTotal.Text;
                            modelo.ConstoMoneda = txtCostoMoneda.Text;
                            modelo.CodigoEmpleado = cbxEmpleado.Text;
                            modelo.NombreEMpleado = txtNombreEmpleado.Text;
                            modelo.EmailProveedor = txtEmailProveedor.Text;

                            string nombreRutaDoc = pdf.OrdenPedidoPDF(modelo);

                            //Instancia la libreria que permite armar correo electronico y llama el metodo que lo crea
                            MailMessage email = correoM.CorreoConfirmacionEnvioPedido(nombreRutaDoc, modelo.EmailSucursal, modelo.NombreEMpleado, modelo.EmailProveedor, modelo.NombreProveedor);
                            //envia el correo
                            server.Send(email);
                            ordenPedidoNEG.EnviarOrdenPedido(ordenPedidoCarga.ID);
                            modificarPedido.CargarTablaOrdenesPedido();
                            MessageBox.Show("La orden de pedido ha sido enviada satisfactoriamente");
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                        }
                    }
                    else
                    { MessageBox.Show("Debe seleccionar un empleado"); }
                }
                else
                { MessageBox.Show("Debe seleccionar una moneda"); }

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
                MessageBox.Show("Espere mientras se elimina la orden de pedido. Presione 'Aceptar' para comenzar");
                OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                string respuesta = ordenPedidoNEG.EliminarOrdenPedido(ordenPedidoCarga.ID);
                if (respuesta =="eliminado")
                {
                    modificarPedido.CargarTablaOrdenesPedido();
                    MessageBox.Show("La orden de pedido ha sido eliminada satisfactoriamente");
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

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Espere mientras se rechaza la orden de pedido. Presione 'Aceptar' para comenzar");
                OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                string respuesta = ordenPedidoNEG.RechazarOrdenPedido(ordenPedidoCarga.ID);
                if (respuesta == "rechazada")
                {
                    ordenPedidoCarga.ESTADO_ORDEN_PEDIDO_ID = 2;
                    modificarPedido.CargarTablaOrdenesPedido();
                    MessageBox.Show("La orden de pedido ha sido rechazada satisfactoriamente");
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

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Espere mientras el documento es generado. Para comenzar presione 'Aceptar'");
                DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                ExportarArchivos pdf = new ExportarArchivos();
                //cargar modelo solicitud
                PDF_ModeloOrdenPedido modelo = new PDF_ModeloOrdenPedido();
                modelo.Folio = dataRow["FOLIO"].ToString();
                modelo.NombreProveedor = ordenPedidoCarga.PROVEEDOR;
                modelo.RolProveedor = ordenPedidoCarga.NUMID_PROVEEDOR + "-" + ordenPedidoCarga.DIVID_PROVEEDOR;
                modelo.FechaSolicitud = ordenPedidoCarga.FECHA_CREACION.ToString();
                modelo.FechaModificacion = dpkFechaModificacion.SelectedDate.ToString();
                modelo.Sucursal = ordenPedidoCarga.SUCURSAL;
                modelo.Direccion = txtDireccion.Text;
                modelo.Telefono = txtTelFijo.Text;
                modelo.EmailSucursal = txtEmailSucursal.Text;
                foreach (DataRow fila in tabla.Rows)
                {
                    DETALLE_ORDEN_PEDIDO deta = new DETALLE_ORDEN_PEDIDO();
                    deta.PRODUCTO_ID = int.Parse(fila.ItemArray[0].ToString());
                    deta.NOMBRE_PRODUCTO = fila.ItemArray[1].ToString();
                    deta.CANTIDAD = int.Parse(fila.ItemArray[2].ToString());
                    deta.PRECIO_COMPRA = decimal.Parse(fila.ItemArray[3].ToString());
                    deta.MONTO_TOTAL = decimal.Parse(fila.ItemArray[4].ToString());
                    modelo.DetalleOrdenPedido.Add(deta);
                }
                modelo.Moneda = cbxMoneda.Text;
                modelo.CostoTotal = txtCostoTotal.Text;
                modelo.ConstoMoneda = txtCostoMoneda.Text;
                modelo.CodigoEmpleado = cbxEmpleado.Text;
                modelo.NombreEMpleado = txtNombreEmpleado.Text;
                modelo.EmailProveedor = txtEmailProveedor.Text;

                pdf.OrdenPedidoPDF(modelo);
                MessageBox.Show("El documento se genero correctamente. Puede acceder a él en la carpeta de 'Mis Documentos'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
    }
}
