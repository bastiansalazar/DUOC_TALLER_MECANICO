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
    /// Lógica de interacción para IngresarRecepcion_AD.xaml
    /// </summary>
    public partial class IngresarRecepcion_AD : Window
    {
        public IngresarRecepcion_AD()
        {
            InitializeComponent();
            CargarTablaRecepcionPedido();
            CargarCombos();
            CargarTablaDetalle();
            dkpFechaRecepcion.DisplayDateStart = new DateTime(1900, 01, 01);
            dkpFechaRecepcion.DisplayDateEnd = DateTime.Now;
        }
        private void CargarTablaRecepcionPedido()
        {
            dgControlRecepcion.ItemsSource = null;
            DataTable dt = new DataTable();
            ControlRecepcionNEG controlRecepcionNEG = new ControlRecepcionNEG();

            try
            {
                List<ControlRecepcionVIEW> lista = controlRecepcionNEG.ListarTodosRecepcion();
                dt.Columns.Add("ID");
                dt.Columns.Add("FOLIO ORDEN");
                dt.Columns.Add("FECHA RECEPCION");
                dt.Columns.Add("ESTADO RECEPCION");
                dt.Columns.Add("EMPLEADO RESPONSABLE");
                dt.Columns.Add("COMENTARIO");

                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        string folio = x.ORDEN_PEDIDO_ID.ToString();
                        for (int i = 0; i < 9; i++)
                        {
                            if (folio.Length < 8)
                                folio = "0" + folio;
                        }
                        dt.Rows.Add(x.ID, folio, x.FECHA_RECEPCION, x.ESTADO_CONTROL_RECEPCION, x.NUM_ID_EMPELADO + " " + x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO, x.COMENTARIO);
                    }
                }
                dgControlRecepcion.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        public void CargarTablaDetalle()
        {
            try
            {
                dgDetalleControlRecepcion.ItemsSource = null;
                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID PRODUCTO");
                tabla.Columns.Add("PRODUCTO");
                tabla.Columns.Add("CANTIDAD");
                tabla.Columns.Add("PRECIO UNITARIO");
                tabla.Columns.Add("PRECIO TOTAL");
                dgDetalleControlRecepcion.ItemsSource = tabla.DefaultView;

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
                SucursalNEG sucursalNEG = new SucursalNEG();
                Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();
                cbxFolioOP.IsEnabled = false;
                cbxEmpleado.IsEnabled = false;
                List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursal.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursal;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }

                List<ESTADO_CONTROL_RECEPCION> listaEstadoRecepcion = tipos_EstadosNEG.ListarEControlRecepcion();
                if (listaEstadoRecepcion.Count > 0)
                {
                    cbxEstadoControl.ItemsSource = listaEstadoRecepcion;
                    cbxEstadoControl.DisplayMemberPath = "NOMBRE";
                    cbxEstadoControl.SelectedValuePath = "ID";
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
                if (dpkFechaDesde != null && dpkFechaHasta != null)
                {
                    DateTime desde = DateTime.Parse(dpkFechaDesde.SelectedDate.ToString());
                    DateTime hasta = DateTime.Parse(dpkFechaHasta.SelectedDate.ToString());
                    dgControlRecepcion.ItemsSource = null;

                    DataTable dt = new DataTable();
                    ControlRecepcionNEG controlRecepcionNEG = new ControlRecepcionNEG();
                    List<ControlRecepcionVIEW> lista = controlRecepcionNEG.FiltarControlFechas(desde,hasta);
                    dt.Columns.Add("ID");
                    dt.Columns.Add("FOLIO ORDEN");
                    dt.Columns.Add("FECHA RECEPCION");
                    dt.Columns.Add("ESTADO RECEPCION");
                    dt.Columns.Add("EMPLEADO RESPONSABLE");
                    dt.Columns.Add("COMENTARIO");

                    if (lista.Count > 0)
                    {
                        foreach (var x in lista)
                        {
                            string folio = x.ORDEN_PEDIDO_ID.ToString();
                            for (int i = 0; i < 9; i++)
                            {
                                if (folio.Length < 8)
                                    folio = "0" + folio;
                            }
                            dt.Rows.Add(x.ID, folio, x.FECHA_RECEPCION, x.ESTADO_CONTROL_RECEPCION, x.NUM_ID_EMPELADO + " " + x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO, x.COMENTARIO);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen productos registrados para los filtros indicados");
                    }
                    dgControlRecepcion.ItemsSource = dt.DefaultView;
                }
                else
                {
                    MessageBox.Show("Debe ingresar un rango de fechas");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " CONTROL RECEPCION";
                DataTable tabla = ((DataView)dgControlRecepcion.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                string repuesta = exportar.ControlRecpecion_TextoPlano(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " CONTROL RECEPCION";
                DataTable tabla = ((DataView)dgControlRecepcion.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.ControlRecepcion_Word(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " CONTROL RECEPCION";
                DataTable tabla = ((DataView)dgControlRecepcion.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.ControlRecepcion_Excel(tabla, nombre);
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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void cbxSucursal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSucursal.SelectedValue != null)
            {
                try
                {
                    int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                    cbxFolioOP.ItemsSource = null;
                    CargarTablaDetalle();
                    OrdenPedidoDAL orden = new OrdenPedidoDAL();
                    List<FolioView> listafolio = orden.ListarFoliosSucursalEnviada(sucursal);
                    if (listafolio.Count > 0)
                    {
                        cbxFolioOP.ItemsSource = listafolio;
                        cbxFolioOP.DisplayMemberPath = "NOMBRE";
                        cbxFolioOP.SelectedValuePath = "ID";
                        cbxFolioOP.IsEnabled = true;
                    }
                    else
                    {
                        cbxFolioOP.SelectedValue = -1;
                        cbxFolioOP.IsEnabled = false;
                    }

                    EmpleadosNEG empleadosNEG = new EmpleadosNEG();
                    List<EmpleadosVIEW> listaEmpleado = empleadosNEG.ListarTodosEmpleadosSucursal(sucursal);
                    if (listaEmpleado.Count > 0)
                    {
                        cbxEmpleado.ItemsSource = listaEmpleado;
                        cbxEmpleado.DisplayMemberPath = "NUM_ID";
                        cbxEmpleado.SelectedValuePath = "ID";
                        cbxEmpleado.IsEnabled = true;
                    }
                    else
                    {
                        cbxEmpleado.SelectedValue = -1;
                        cbxEmpleado.IsEnabled = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void cbxFolioOP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFolioOP.SelectedValue != null)
            {
                try
                {
                    int ordePedido = int.Parse(cbxFolioOP.SelectedValue.ToString());
                    DetalleOrdenPedidoNEG detalleOrdenPedidoNEG = new DetalleOrdenPedidoNEG();
                    CargarTablaDetalle();
                    List<DETALLE_ORDEN_PEDIDO> listaDetalle = detalleOrdenPedidoNEG.CargarlistaDetalleOrden(ordePedido);
                    foreach (var fila in listaDetalle)
                    {
                        AgregarItemTablaDetalle(fila.PRODUCTO_ID, fila.NOMBRE_PRODUCTO, int.Parse(fila.CANTIDAD.ToString()), Convert.ToDecimal(fila.PRECIO_COMPRA), Convert.ToDecimal(fila.MONTO_TOTAL));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        public void AgregarItemTablaDetalle(int _idProd, string _nombre, int _cantidad, decimal _preUni, decimal _preTot)
        {
            try
            {
                DataTable tabla = ((DataView)dgDetalleControlRecepcion.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID PRODUCTO"] = _idProd;
                fila["PRODUCTO"] = _nombre;
                fila["CANTIDAD"] = _cantidad;
                fila["PRECIO UNITARIO"] = _preUni;
                fila["PRECIO TOTAL"] = _preTot;
                tabla.Rows.Add(fila);
                dgDetalleControlRecepcion.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        public void ActualizarItemTablaDetalle(int index,int _idProd, string _nombre, int _cantidad, decimal _preUni, decimal _preTot)
        {
            try
            {
                DataTable tabla = ((DataView)dgDetalleControlRecepcion.ItemsSource).ToTable();
                DataRow fila = tabla.Rows[index];
                fila["ID PRODUCTO"] = _idProd;
                fila["PRODUCTO"] = _nombre;
                fila["CANTIDAD"] = _cantidad;
                fila["PRECIO UNITARIO"] = _preUni;
                fila["PRECIO TOTAL"] = _preTot;
                //tabla.Rows;
                dgDetalleControlRecepcion.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        public void Limpiar()
        {
            dkpFechaRecepcion.SelectedDate = DateTime.Now;
            cbxEmpleado.SelectedValue = -1;
            cbxEstadoControl.SelectedValue = -1;
            cbxFolioOP.SelectedValue = -1;
            cbxSucursal.SelectedValue = -1;
            CargarTablaDetalle();
            txtComentario.Text = "";
        }
        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgControlRecepcion.SelectedItems.Count > 0)
                {                    
                    if (dgControlRecepcion.SelectedItems.Count == 1)
                    {
                        MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar' para comenzar");
                        DataTable tabla = ((DataView)dgControlRecepcion.ItemsSource).ToTable();                        
                        var indice = dgControlRecepcion.SelectedIndex;
                        DataRow dataRow = tabla.Rows[indice];
                        ExportarArchivos pdf = new ExportarArchivos();
                        PDF_ModeloControlRecepcion modelo = new PDF_ModeloControlRecepcion();
                        ControlRecepcionNEG controlRecepcionNEG = new ControlRecepcionNEG();
                        ControlRecepcionVIEW control = new ControlRecepcionVIEW();
                        DetalleControlRecepcionNEG detalleControlRecepcionNEG = new DetalleControlRecepcionNEG();

                        int idControl = int.Parse(dataRow["ID"].ToString());
                        control = controlRecepcionNEG.CargarControlRecepcion(idControl);
                        control.FOLIO_PEDIDO = dataRow["FOLIO ORDEN"].ToString();

                        modelo.Folio = control.FOLIO_PEDIDO;
                        modelo.NombreProveedor = control.PROVEEDOR;
                        modelo.RolProveedor = control.NUM_ID_PROVEEDOR + "-" + control.DIV_ID_PROVEEDOR;
                        modelo.FechaRecepcion = control.FECHA_RECEPCION.ToString();
                        modelo.EstadoControl = control.ESTADO_CONTROL_RECEPCION;
                        modelo.Comentario = control.COMENTARIO;
                        modelo.NombreSucursal = control.SUCURSAL;
                        modelo.CodigoEmpleado = control.NUM_ID_EMPELADO.ToString();
                        modelo.NombreEmpleado = control.NOMBRE_EMPLEADO;
                        modelo.DetalleControlRecepcion = detalleControlRecepcionNEG.ListarDetalleControlRecepcion(idControl);

                        string respuesta = pdf.ControlRecepcionPedidoPDF(modelo);
                        if (respuesta == "creado")
                        {
                            MessageBox.Show("El documento se genero correctamente. Ubíquelo en 'Mis Documentos'");
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar solo una fila");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una fila");
                }  

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void bntIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxFolioOP.SelectedValue != null)
                {
                    if (cbxEmpleado.SelectedValue != null)
                    {
                        if (cbxEstadoControl.SelectedValue != null)
                        {
                            if (dkpFechaRecepcion.SelectedDate != null)
                            {
                                ControlRecepcionNEG controlRecepcionNEG = new ControlRecepcionNEG();
                                string comentario = txtComentario.Text;
                                DateTime fechaRececpion = DateTime.Parse(dkpFechaRecepcion.SelectedDate.ToString());
                                int empleado = int.Parse(cbxEmpleado.SelectedValue.ToString());
                                int estado = int.Parse(cbxEstadoControl.SelectedValue.ToString());
                                int orden = int.Parse(cbxFolioOP.SelectedValue.ToString());
                                DataTable tabla = ((DataView)dgDetalleControlRecepcion.ItemsSource).ToTable();
                                string respuesta = controlRecepcionNEG.InsertarControlRecepcion(comentario,fechaRececpion,empleado,estado,orden,tabla);
                                if (respuesta == "creado")
                                {
                                    Limpiar();
                                    CargarTablaRecepcionPedido();
                                    MessageBox.Show("El control de recepción se almacenó correctamente");                                    
                                }
                                else
                                {
                                    MessageBox.Show(respuesta);
                                }
                            }
                            else
                            { MessageBox.Show("Debe seleccionar una fecha de recepcion"); }
                        }
                        else
                        { MessageBox.Show("Debe seleccionar un estado de conformidad"); }
                    }
                    else
                    { MessageBox.Show("Debe seleccionar un empleado"); }
                }
                else
                { MessageBox.Show("Debe seleccionar una solicitud"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnModificarDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDetalleControlRecepcion.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgDetalleControlRecepcion.ItemsSource).ToTable();
                    if (dgDetalleControlRecepcion.SelectedItems.Count ==1)
                    {
                        var indice = dgDetalleControlRecepcion.SelectedIndex;
                        DataRow fila = tabla.Rows[indice];
                        int idOrden = int.Parse(cbxFolioOP.SelectedValue.ToString());
                        var numero1 = fila.ItemArray[0].ToString().Split(',');
                        var numero2 = fila.ItemArray[2].ToString().Split(',');
                        var numero3 = fila.ItemArray[3].ToString().Split(',');
                        var numero4 = fila.ItemArray[4].ToString().Split(',');

                        AgregarProductosPedidos3 reg = new AgregarProductosPedidos3(idOrden,this,int.Parse(numero1[0].ToString()),int.Parse(numero2[0].ToString()),int.Parse(numero3[0].ToString()),int.Parse(numero4[0].ToString()), indice);
                        reg.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar solo una fila");
                    }                    
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una fila");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnQuitarDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDetalleControlRecepcion.SelectedItems.Count > 0)
                {
                    DataTable tabla = ((DataView)dgDetalleControlRecepcion.ItemsSource).ToTable();
                    for (int i = 0; i < dgDetalleControlRecepcion.SelectedItems.Count; i++)
                    {
                        int indice = dgDetalleControlRecepcion.Items.IndexOf(dgDetalleControlRecepcion.SelectedItems[i]);
                        DataRow fila = tabla.Rows[indice];
                        tabla.Rows.Remove(fila);
                    }
                    dgDetalleControlRecepcion.ItemsSource = null;
                    dgDetalleControlRecepcion.ItemsSource = tabla.DefaultView;
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

        private void btnAgregarDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxFolioOP.SelectedValue != null)
                {
                    int idOrden = int.Parse(cbxFolioOP.SelectedValue.ToString());
                    AgregarProductosPedidos3 reg = new AgregarProductosPedidos3(idOrden, this);
                    reg.ShowDialog();
                }
                else { MessageBox.Show("Debe seleccionar una orden de pedido"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarCombos();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_IngresarRecepcion reg = new AYUDA_IngresarRecepcion();
            reg.Show();
        }
    }
}
