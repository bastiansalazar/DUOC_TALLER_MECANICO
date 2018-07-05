using AppServiexpress.Ventanas.Productos.Manual;
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

namespace AppServiexpress.Ventanas.Productos
{
    /// <summary>
    /// Lógica de interacción para ModificarProductos_AD.xaml
    /// </summary>
    public partial class ModificarProductos_AD : Window
    {
        public ModificarProductos_AD()
        {
            InitializeComponent();
            this.Activate();
            dpkFechaVenc.DisplayDateStart = DateTime.Now;
            CargarTablaProductos();
            CargarCombos();
        }

        public void CargarTablaProductos()
        {
            dgProductos.ItemsSource = null;
            DataTable dt = new DataTable();
            ProductosNEG productosNEG = new ProductosNEG();

            try
            {
                List<ProductosVIEW> lista = productosNEG.ListarTodosProductos();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("SKU");
                dt.Columns.Add("PRECIO_COMPRA");
                dt.Columns.Add("PRECIO_VENTA");
                dt.Columns.Add("FECHA_VENCIMIENTO");
                dt.Columns.Add("CATEGORIA");
                dt.Columns.Add("TIPO_PRODUCTO");
                dt.Columns.Add("MARCA");
                dt.Columns.Add("ESTADO_PRODUCTO");
                dt.Columns.Add("SUCURSAL");


                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.SKU, x.PRECIO_COMPRA, x.PRECIO_VENTA, x.FECHA_VENCIMIENTO, x.CATEGORIA, x.TIPO_PRODUCTO, x.MARCA, x.ESTADO_PRODUCTO, x.SUCURSAL);
                    }
                }
                dgProductos.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        public void CargarCombos()
        {
            SucursalNEG sucursalNEG = new SucursalNEG();
            ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
            CategoriaNEG categoriaNEG = new CategoriaNEG();
            Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();
            MarcaNEG marcaNEG = new MarcaNEG();
            cbxTipoProducto.IsEnabled = false;

            try
            {
                List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursal.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursal;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }

                List<ProveedoresVIEW> listaProveedor = proveedoresNEG.ListarTodosProveedores();
                if (listaProveedor.Count > 0)
                {
                    cbxProveedor.ItemsSource = listaProveedor;
                    cbxProveedor.DisplayMemberPath = "NOMBRE_EMPRESA";
                    cbxProveedor.SelectedValuePath = "ID";
                }

                List<CATEGORIA> listaCategoria = categoriaNEG.ListarCategorias();
                if (listaCategoria.Count > 0)
                {
                    cbxCategoria.ItemsSource = listaCategoria;
                    cbxCategoria.DisplayMemberPath = "NOMBRE";
                    cbxCategoria.SelectedValuePath = "ID";
                }

                List<ESTADO_PRODUCTO> listaEProductos = tipos_EstadosNEG.ListarEProductos();
                if (listaEProductos.Count > 0)
                {
                    cbxEstado.ItemsSource = listaEProductos;
                    cbxEstado.DisplayMemberPath = "NOMBRE";
                    cbxEstado.SelectedValuePath = "ID";
                }

                List<MARCA> listaMarca = marcaNEG.ListarMarcas();
                if (listaMarca.Count > 0)
                {
                    cbxMarca.ItemsSource = listaMarca;
                    cbxMarca.DisplayMemberPath = "NOMBRE";
                    cbxMarca.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void cbxCategorias_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxCategoria.SelectedValue != null)
            {
                try
                {
                    int categoria = int.Parse(cbxCategoria.SelectedValue.ToString());
                    TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
                    List<TIPO_PRODUCTO> listaTipo = tipoProductoNEG.ListarTProductosCategoria(categoria);
                    if (listaTipo.Count > 0)
                    {
                        cbxTipoProducto.ItemsSource = listaTipo;
                        cbxTipoProducto.DisplayMemberPath = "NOMBRE";
                        cbxTipoProducto.SelectedValuePath = "ID";
                        cbxTipoProducto.IsEnabled = true;
                    }
                    else
                    {
                        cbxTipoProducto.SelectedValue = -1;
                        cbxTipoProducto.IsEnabled = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void dgProductos_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgProductos.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idProducto = Convert.ToInt32(dr1.ItemArray[0]);
            ProductosNEG productosNEG = new ProductosNEG();
            var datos = productosNEG.CargarProducto(idProducto);
            lblIdProducto.Content = idProducto.ToString();
            cbxSucursal.SelectedValue = datos.SUCURSAL_ID;
            cbxProveedor.SelectedValue = datos.PROVEEDOR_ID;
            txtNombre.Text = datos.NOMBRE;
            txtDescripcion.Text = datos.DESCRIPCION;
            dpkFechaVenc.SelectedDate = datos.FECHA_VENCIMIENTO;
            txtPrecioCompra.Text = datos.PRECIO_COMPRA.ToString();
            txtPrecioVenta.Text = datos.PRECIO_VENTA.ToString();
            txtStock.Text = datos.STOCK.ToString();
            txtStockCritico.Text = datos.STOCK_CRITICO.ToString();
            cbxCategoria.SelectedValue = datos.CATEGORIA_ID;
            cbxEstado.SelectedValue = datos.ESTADO_PRODUCTO_ID;
            cbxMarca.SelectedValue = datos.MARCA_ID;
            cbxTipoProducto.SelectedValue = datos.TIPO_PRODUCTO_ID;
            TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
            List<TIPO_PRODUCTO> lista = tipoProductoNEG.ListarTProductosCategoria(datos.CATEGORIA_ID);
            if (lista.Count > 0)
            {
                cbxTipoProducto.ItemsSource = lista;
                cbxTipoProducto.DisplayMemberPath = "NOMBRE";
                cbxTipoProducto.SelectedValuePath = "ID";
            }
            cbxTipoProducto.IsEnabled = true;
            cbxTipoProducto.SelectedValue = datos.TIPO_PRODUCTO_ID;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();
                dgProductos.ItemsSource = null;

                DataTable dt = new DataTable();
                ProductosNEG productosNEG = new ProductosNEG();
                List<ProductosVIEW> lista = productosNEG.FiltrarProductos(tipo, valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("SKU");
                dt.Columns.Add("PRECIO_COMPRA");
                dt.Columns.Add("PRECIO_VENTA");
                dt.Columns.Add("FECHA_VENCIMIENTO");
                dt.Columns.Add("CATEGORIA");
                dt.Columns.Add("TIPO_PRODUCTO");
                dt.Columns.Add("MARCA");
                dt.Columns.Add("ESTADO_PRODUCTO");
                dt.Columns.Add("SUCURSAL");


                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.SKU, x.PRECIO_COMPRA, x.PRECIO_VENTA, x.FECHA_VENCIMIENTO, x.CATEGORIA, x.TIPO_PRODUCTO, x.MARCA, x.ESTADO_PRODUCTO, x.SUCURSAL);
                    }
                }
                else
                {
                    MessageBox.Show("No existen productos registrados para los filtros indicados");
                }
                dgProductos.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductosNEG productosNEG = new ProductosNEG();
                int _idProd = 0;
                int.TryParse(lblIdProducto.Content.ToString(), out _idProd);
                int idProducto = _idProd;
                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                int proveedor = int.Parse(cbxProveedor.SelectedValue.ToString());
                string nombre = txtNombre.Text.ToUpper();
                string descripcion = txtDescripcion.Text.ToUpper();
                DateTime fecha_vencimiento = default(DateTime);
                if (dpkFechaVenc.SelectedDate!=null)
                {
                    fecha_vencimiento= DateTime.Parse(dpkFechaVenc.SelectedDate.ToString());
                }                 
                decimal precio_compra = decimal.Parse(txtPrecioCompra.Text.ToString());
                decimal precio_venta = decimal.Parse(txtPrecioVenta.Text.ToString());
                int stock = int.Parse(txtStock.Text.ToString());
                int stock_critico = int.Parse(txtStockCritico.Text.ToString());
                int categoria = int.Parse(cbxCategoria.SelectedValue.ToString());
                int estado = int.Parse(cbxEstado.SelectedValue.ToString());
                int marca = int.Parse(cbxMarca.SelectedValue.ToString());
                int tipo = int.Parse(cbxTipoProducto.SelectedValue.ToString());

                string respuesta = productosNEG.ActualizarProducto(idProducto, tipo, descripcion, nombre, fecha_vencimiento, precio_compra, precio_venta, sucursal, proveedor, stock, stock_critico, categoria, estado, marca);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    CargarTablaProductos();
                    MessageBox.Show("El producto ha sido actualziado satisfactoriamente en la base de datos");
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
        private void LimpiarFormulario()
        {
            cbxSucursal.SelectedValue = -1;
            cbxProveedor.SelectedValue = -1;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            lblIdProducto.Content = "";
            dpkFechaVenc.SelectedDate = DateTime.Now;
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtStock.Text = "";
            txtStockCritico.Text = "";
            cbxCategoria.SelectedValue = -1;
            cbxEstado.SelectedValue = -1;
            cbxMarca.SelectedValue = -1;
            cbxTipoProducto.SelectedValue = -1;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarTablaProductos();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_ModificarProducto reg = new AYUDA_ModificarProducto();
            reg.Show();
        }
    }//
}
