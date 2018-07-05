using AppServiexpress.Ventanas.Pedidos.Manual;
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
    /// Lógica de interacción para ModificarPedido_AD.xaml
    /// </summary>
    public partial class ModificarPedido_AD : Window
    {
        public ModificarPedido_AD()
        {
            InitializeComponent();
            CargarTablaOrdenesPedido();
            cbxTextoBusqueda.IsEnabled = false;
        }
        

        public void CargarTablaOrdenesPedido()
        {
            dgOrdenPedido.ItemsSource = null;
            DataTable tabla = new DataTable();
            OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();

            try
            {
                List<OrdenPedidoVIEW> lista = ordenPedidoNEG.ListarTodasOrdenesPedidos();
                tabla.Columns.Add("ID");
                tabla.Columns.Add("FOLIO");
                tabla.Columns.Add("FECHA CREACION");
                tabla.Columns.Add("SUCURSAL");
                tabla.Columns.Add("RUT PROVEEDOR");
                tabla.Columns.Add("PROVEEDOR");  
                tabla.Columns.Add("MONTO TOTAL");
                tabla.Columns.Add("CANTIDAD ART");
                tabla.Columns.Add("FECHA ENTREGA");
                tabla.Columns.Add("ESTADO");
                tabla.Columns.Add("EMPLEADO RESPONSABLE");
                tabla.Columns.Add("MONEDA");
                tabla.Columns.Add("TOTAL MONEDA");
                tabla.Columns.Add("FECHA ACTUALIZACION");
                
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        string folio = x.ID.ToString();
                        for (int i = 0; i < 9; i++)
                        {
                            if (folio.Length < 8)
                                folio = "0" + folio;
                        }
                        string cantidad = x.CANTIDAD_TOTAL.ToString();
                        string costo1 = string.Format("{0:n2}", x.MONTO_TOTAL);
                        string costo2 = string.Format("{0:n2}", (Math.Truncate((Convert.ToDecimal(x.MONTO_TOTAL) / Convert.ToDecimal(x.VALOR_MULTIMONEDA)))));
                        tabla.Rows.Add(x.ID, folio, x.FECHA_CREACION, x.SUCURSAL, x.NUMID_PROVEEDOR+"-"+x.DIVID_PROVEEDOR,x.PROVEEDOR,
                            costo1,cantidad , x.FECHA_ENTREGA, x.ESTADO_ORDEN_PEDIDO, x.NOMBRE_EMPLEADO+" "+x.APELLIDO_EMPLEADO+" "+x.NUMID_EMPLEADO+"-"+x.DIVID_EMPLEADO,
                            x.MULTI_MONEDA , costo2, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                dgOrdenPedido.ItemsSource = tabla.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void dgOrdenPedido_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgOrdenPedido.SelectedItem!=null)
                {
                    DataRowView dataRowView = dgOrdenPedido.SelectedItem as DataRowView;
                    DataRow dataRow = dataRowView.Row;
                    ModificarPedido2_AD reg = new ModificarPedido2_AD(dataRow, this);
                    reg.ShowDialog();
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxTipoBusqueda.SelectedValue != null)
                {
                    if (cbxTextoBusqueda.SelectedValue != null)
                    {
                        string tipo = cbxTipoBusqueda.Text;
                        int valor = int.Parse(cbxTextoBusqueda.SelectedValue.ToString());
                        dgOrdenPedido.ItemsSource = null;
                        DataTable tabla = new DataTable();
                        OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                        List<OrdenPedidoVIEW> lista = ordenPedidoNEG.FiltrarOrdenesPedidos(tipo,valor);
                        tabla.Columns.Add("ID");
                        tabla.Columns.Add("FOLIO");
                        tabla.Columns.Add("FECHA CREACION");
                        tabla.Columns.Add("SUCURSAL");
                        tabla.Columns.Add("RUT PROVEEDOR");
                        tabla.Columns.Add("PROVEEDOR");
                        tabla.Columns.Add("MONTO TOTAL");
                        tabla.Columns.Add("CANTIDAD ART");
                        tabla.Columns.Add("FECHA ENTREGA");
                        tabla.Columns.Add("ESTADO");
                        tabla.Columns.Add("EMPLEADO RESPONSABLE");
                        tabla.Columns.Add("MONEDA");
                        tabla.Columns.Add("TOTAL MONEDA");
                        tabla.Columns.Add("FECHA ACTUALIZACION");

                        if (lista.Count > 0)
                        {
                            foreach (var x in lista)
                            {
                                string folio = x.ID.ToString();
                                for (int i = 0; i < 9; i++)
                                {
                                    if (folio.Length < 8)
                                        folio = "0" + folio;
                                }
                                string cantidad = x.CANTIDAD_TOTAL.ToString();
                                string costo1 = string.Format("{0:n2}", x.MONTO_TOTAL);
                                string costo2 = string.Format("{0:n2}", (Math.Truncate((Convert.ToDecimal(x.MONTO_TOTAL) / Convert.ToDecimal(x.VALOR_MULTIMONEDA)))));
                                tabla.Rows.Add(x.ID, folio, x.FECHA_CREACION, x.SUCURSAL, x.NUMID_PROVEEDOR + "-" + x.DIVID_PROVEEDOR, x.PROVEEDOR,
                                    costo1, cantidad, x.FECHA_ENTREGA, x.ESTADO_ORDEN_PEDIDO, x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO + " " + x.NUMID_EMPLEADO + "-" + x.DIVID_EMPLEADO,
                                    x.MULTI_MONEDA, costo2, x.FECHA_ULTIMO_UPDATE);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen ordenes de pedidos registrados para los filtros indicados");
                        }
                        dgOrdenPedido.ItemsSource = tabla.DefaultView;

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar el filtro de busqueda");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un tipo de busqueda");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void cbxTipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTipoBusqueda.SelectedValue != null)
            {
                try
                {
                    string tipo = cbxTipoBusqueda.SelectedIndex.ToString();
                    if(tipo == "0")
                    {
                        tipo = "PROVEEDOR";
                    }
                    if (tipo == "1")
                    {
                        tipo = "SUCURSAL";
                    }
                    if (tipo == "2")
                    {
                        tipo = "ESTADO";
                    }
                    cbxTextoBusqueda.ItemsSource = null;
                    cbxTextoBusqueda.IsEnabled = false;

                    if (tipo == "PROVEEDOR")
                    {
                        ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
                        List<ProveedoresVIEW> listaProveedor = proveedoresNEG.ListarTodosProveedores();
                        if (listaProveedor.Count > 0)
                        {
                            cbxTextoBusqueda.ItemsSource = listaProveedor;
                            cbxTextoBusqueda.DisplayMemberPath = "NOMBRE_EMPRESA";
                            cbxTextoBusqueda.SelectedValuePath = "ID";
                        }
                        cbxTextoBusqueda.IsEnabled = true;
                    }
                    else if (tipo == "SUCURSAL")
                    {
                        SucursalNEG sucursalNEG = new SucursalNEG();
                        List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                        if (listaSucursal.Count > 0)
                        {
                            cbxTextoBusqueda.ItemsSource = listaSucursal;
                            cbxTextoBusqueda.DisplayMemberPath = "NOMBRE";
                            cbxTextoBusqueda.SelectedValuePath = "ID";
                        }
                        cbxTextoBusqueda.IsEnabled = true;
                    }
                    else if (tipo == "ESTADO")
                    {
                        Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();
                        List<ESTADO_ORDEN_PEDIDO> listaTipoEstados = tipos_EstadosNEG.ListarEOrdenesPedidos();
                        if (listaTipoEstados.Count > 0)
                        {
                            cbxTextoBusqueda.ItemsSource = listaTipoEstados;
                            cbxTextoBusqueda.DisplayMemberPath = "NOMBRE";
                            cbxTextoBusqueda.SelectedValuePath = "ID";
                        }
                        cbxTextoBusqueda.IsEnabled = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
            else
            {
                cbxTextoBusqueda.SelectedIndex = -1;
                cbxTextoBusqueda.IsEnabled = false;
            }
        }

        private void btnTexto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre ="REPORTE "+ _anio+_mes+_dia+_hora+_minuto+_segundos+" ORDENES DE PEDIDOS";
                DataTable tabla = ((DataView)dgOrdenPedido.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                string repuesta=exportar.OrdenesPedidos_TextoPlano(tabla,nombre);
                if (repuesta == "guardado")
                {
                    MessageBox.Show("El archivo se guardó correctamente. Puede ubicarlo en 'Mis Documentos'");
                }
                else
                {
                    MessageBox.Show("No se ha podido guardar el archivo, intentelo otra vez");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " ORDENES DE PEDIDOS";
                DataTable tabla = ((DataView)dgOrdenPedido.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.OrdenesPedidos_Word(tabla, nombre); 
                if (repuesta == "guardado")
                {
                    MessageBox.Show("El archivo se guardó correctamente. Puede ubicarlo en 'Mis Documentos'");
                }
                else
                {
                    MessageBox.Show("No se ha podido guardar el archivo, intentelo otra vez");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " ORDENES DE PEDIDOS";
                DataTable tabla = ((DataView)dgOrdenPedido.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.OrdenesPedidos_Excel(tabla, nombre);
                if (repuesta == "guardado")
                {
                    MessageBox.Show("El archivo se guardó correctamente. Puede ubicarlo en 'Mis Documentos'");
                }
                else
                {
                    MessageBox.Show("No se ha podido guardar el archivo, intentelo otra vez");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarTablaOrdenesPedido();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_ModificarPedido reg = new AYUDA_ModificarPedido();
            reg.Show();
        }
    }
}
