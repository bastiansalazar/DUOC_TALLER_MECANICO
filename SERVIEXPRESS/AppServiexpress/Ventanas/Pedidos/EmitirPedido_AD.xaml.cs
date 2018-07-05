using AppServiexpress.Ventanas.Pedidos.Manual;
using AppServiexpress.Ventanas.Pedidos.Modal_Interno;
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

namespace AppServiexpress.Ventanas.Pedidos
{
    /// <summary>
    /// Lógica de interacción para EmitirPedido_AD.xaml
    /// </summary>
    public partial class EmitirPedido_AD : Window
    {

        public EmitirPedido_AD()
        {
            InitializeComponent();
            dpkFechaCreacion.SelectedDate = DateTime.Now;
            CargarTablaProductos();
            CargarCombos();
        }

        private void CargarCombos()
        {
            ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();
            MultiMonedaNEG multiMonedaNEG = new MultiMonedaNEG();
            EmpleadosNEG empleadosNEG = new EmpleadosNEG();
            cbxEmpleado.IsEnabled = false;
            cbxMoneda.IsEnabled = false;

            try
            {
                List<ProveedoresVIEW> listaProveedor = proveedoresNEG.ListarTodosProveedores();
                if (listaProveedor.Count > 0)
                {
                    cbxProveedor.ItemsSource = listaProveedor;
                    cbxProveedor.DisplayMemberPath = "NOMBRE_EMPRESA";
                    cbxProveedor.SelectedValuePath = "ID";
                }

                List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursal.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursal;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }               

                List<MULTI_MONEDA> listaMultiMoneda = multiMonedaNEG.ListarMultiMoneda();
                if (listaMultiMoneda.Count > 0)
                {
                    cbxMoneda.ItemsSource = listaMultiMoneda;
                    cbxMoneda.DisplayMemberPath = "TIPO_MODONEDA";
                    cbxMoneda.SelectedValuePath = "ID";
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }
        private void cbxProveedor_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxProveedor.SelectedValue != null)
            {
                try
                {
                    int proveedor = int.Parse(cbxProveedor.SelectedValue.ToString());
                    ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
                    var datos = proveedoresNEG.CargarProveedor(proveedor);
                    txtRutProveedor.Text = datos.NUM_ID.ToString();
                    CargarTablaProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }
        private void cbxSucursal_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSucursal.SelectedValue != null)
            {
                try
                {
                    int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                    cbxEmpleado.SelectedIndex = -1;
                    cbxEmpleado.IsEnabled = false;
                    CargarTablaProductos();
                    txtNombreEmpleado.Text = "";
                    EmpleadosNEG empleadosNEG = new EmpleadosNEG();
                    List<EmpleadosVIEW> listaEmpleados = empleadosNEG.ListarTodosEmpleadosSucursal(sucursal);
                    if (listaEmpleados.Count > 0)
                    {
                        cbxEmpleado.ItemsSource = listaEmpleados;
                        cbxEmpleado.DisplayMemberPath = "NUM_ID";
                        cbxEmpleado.SelectedValuePath = "ID";
                        cbxEmpleado.IsEnabled = true;
                    }                    
                    SucursalNEG sucursalNEG = new SucursalNEG();
                    var datos = sucursalNEG.CargarSucursal(sucursal);
                    txtDireccion.Text = datos.DIRECCION;
                    txtTelFijo.Text =  datos.NUMERO_TELEFONO.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
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
                    txtNombreEmpleado.Text = datos.NOMBRE+" "+datos.APELLIDO;
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
        public void AgregarItemTablaProductos(int _idProd, string _nombre, int _cantidad, decimal _preUni,decimal _preTot)
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

        private void Limpiar()
        {
            cbxProveedor.SelectedIndex = -1;
            txtRutProveedor.Text = "";
            dpkFechaCreacion.SelectedDate = DateTime.Now;
            cbxSucursal.SelectedIndex = -1;
            txtDireccion.Text = "";
            txtTelFijo.Text = "";
            txtEmailSucursal.Text = "";
            CargarTablaProductos();
            cbxMoneda.SelectedIndex = -1;
            cbxEmpleado.SelectedIndex = -1;
            txtCostoMoneda.Text = "";
            txtCostoTotal.Text = "";
            txtNombreEmpleado.Text = "";
            txtEmailProveedor.Text = "";
            cbxEmpleado.IsEnabled = false;
            cbxMoneda.IsEnabled = false;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAgregaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxProveedor.SelectedValue != null)
                {
                    if (int.Parse(cbxProveedor.SelectedValue.ToString()) > 0)
                    {
                        if (cbxSucursal.SelectedValue != null)
                        {
                            if (int.Parse(cbxSucursal.SelectedValue.ToString()) > 0)
                            {
                                try
                                {
                                    int proveedor = int.Parse(cbxProveedor.SelectedValue.ToString());
                                    int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                                    AgregarProductosPedidos reg = new AgregarProductosPedidos(proveedor, sucursal, this);
                                    reg.ShowDialog();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                                }
                            }
                            else
                            { MessageBox.Show("Debe seleccionar una sucursal"); }
                        }
                        else
                        { MessageBox.Show("Debe seleccionar una sucursal"); }
                    }
                    else
                    { MessageBox.Show("Debe seleccionar un proveedor"); }
                }
                else { MessageBox.Show("Debe seleccionar un proveedor"); }

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
                if (cbxProveedor.SelectedValue != null)
                {
                    if (cbxSucursal.SelectedValue != null)
                    {
                        if (cbxMoneda.SelectedValue != null)
                        {
                            if (cbxEmpleado.SelectedValue != null)
                            {
                                OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                                int proveedor = int.Parse(cbxProveedor.SelectedValue.ToString());
                                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                                string emailSucursal = txtEmailSucursal.Text.ToString();
                                int tipoMultimoneda = int.Parse(cbxMoneda.SelectedValue.ToString());
                                int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                                string emailProveedor = txtEmailProveedor.Text.ToString();
                                DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                                string respuesta = ordenPedidoNEG.CrearOrdenPedido(proveedor, sucursal, emailSucursal, tipoMultimoneda, empleado, emailProveedor, tabla);
                                if (respuesta == "creado")
                                {
                                    Limpiar();
                                    MessageBox.Show("La orden de pedido ha sido ingresada satisfactoriamente. La orden se encuentra en espera de aprobación para ser enviada");
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
                    else
                    { MessageBox.Show("Debe seleccionar una sucursal"); }
                }
                else { MessageBox.Show("Debe seleccionar un proveedor"); }               

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

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_EmitirPedido reg = new AYUDA_EmitirPedido();
            reg.Show();
        }
    }
}
