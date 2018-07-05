using AppServiexpress.Ventanas.Requerimientos.Manual;
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

namespace AppServiexpress.Ventanas.Requerimientos
{
    /// <summary>
    /// Lógica de interacción para FinalizarRequerimientos_AD.xaml
    /// </summary>
    public partial class FinalizarRequerimientos_AD : Window
    {

        public int IDRESERVA;
        public string ESTADORESERVA;

        public FinalizarRequerimientos_AD()
        {
            InitializeComponent();
            CargarTablaRequerimientos();
            cbxTextoBusqueda.IsEnabled = false;
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

        public void CargarTablaRequerimientos()
        {
            try
            {
                dgRequerimientos.ItemsSource = null;
                DataTable tabla = new DataTable();
                ReservaNEG reservaNEG = new ReservaNEG();
                List<ReservaVIEW> lista = reservaNEG.ListarTodosRequerimientos();
                tabla.Columns.Add("ID");
                tabla.Columns.Add("FOLIO");
                tabla.Columns.Add("ESTADO");
                tabla.Columns.Add("FECHA RESERVA");
                tabla.Columns.Add("SUCURSAL");
                tabla.Columns.Add("PATENTE");
                tabla.Columns.Add("TIPO VEHICULO");
                tabla.Columns.Add("MARCA");
                tabla.Columns.Add("CLIENTE SOLICITANTE");
                tabla.Columns.Add("EMPLEADO RECEPCIONISTA");
                tabla.Columns.Add("TOTAL PRESUPUESTO");

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
                        string costo1 = string.Format("{0:n2}", x.TOTAL);
                        tabla.Rows.Add(x.ID, folio, x.ESTADO_DIAGNOSTICO, x.FECHA_RESERVA, x.NOMBRE_SUCURSAL,
                            x.PATENTE_VEHICULO, x.TIPO_VEHICULO, x.MARCA_VEHICULO, x.NOMBRE_CLIENTE + " " + x.NOMBRE_APELLIDO, x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO, costo1);
                    }
                }
                dgRequerimientos.ItemsSource = tabla.DefaultView;

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
                    if (tipo == "0")
                    {
                        tipo = "ESTADO";
                    }
                    if (tipo == "1")
                    {
                        tipo = "CLIENTES";
                    }
                    if (tipo == "2")
                    {
                        tipo = "SUCURSAL";
                    }
                    cbxTextoBusqueda.ItemsSource = null;
                    cbxTextoBusqueda.IsEnabled = false;
                    if (tipo == "CLIENTES")
                    {
                        ClientesNEG clientesNEG = new ClientesNEG();
                        List<ClienteVIEW> listaClientes = clientesNEG.ListarTodosClientes();
                        if (listaClientes.Count > 0)
                        {
                            cbxTextoBusqueda.ItemsSource = listaClientes;
                            cbxTextoBusqueda.DisplayMemberPath = "NUM_ID";
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
                        List<string> listaTipoEstados = new List<string>();
                        listaTipoEstados.Add("RESERVADO");
                        listaTipoEstados.Add("INICIADO");
                        listaTipoEstados.Add("COMPLETADO");
                        listaTipoEstados.Add("PAGADO");
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxTipoBusqueda.SelectedValue != null)
                {
                    if (cbxTextoBusqueda.SelectedValue != null)
                    {
                        string tipo = cbxTipoBusqueda.Text;
                        string valor = cbxTextoBusqueda.SelectedValue.ToString();
                        dgRequerimientos.ItemsSource = null;
                        DataTable tabla = new DataTable();
                        ReservaNEG reservaNEG = new ReservaNEG();
                        List<ReservaVIEW> lista = reservaNEG.FiltrarRequerimientos(tipo, valor);
                        tabla.Columns.Add("ID");
                        tabla.Columns.Add("FOLIO");
                        tabla.Columns.Add("ESTADO");
                        tabla.Columns.Add("FECHA RESERVA");
                        tabla.Columns.Add("SUCURSAL");
                        tabla.Columns.Add("PATENTE");
                        tabla.Columns.Add("TIPO VEHICULO");
                        tabla.Columns.Add("MARCA");
                        tabla.Columns.Add("CLIENTE SOLICITANTE");
                        tabla.Columns.Add("EMPLEADO RECEPCIONISTA");
                        tabla.Columns.Add("TOTAL PRESUPUESTO");

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
                                string costo1 = string.Format("{0:n2}", x.TOTAL);
                                tabla.Rows.Add(x.ID, folio, x.ESTADO_DIAGNOSTICO, x.FECHA_RESERVA, x.NOMBRE_SUCURSAL,
                                    x.PATENTE_VEHICULO, x.TIPO_VEHICULO, x.MARCA_VEHICULO, x.NOMBRE_CLIENTE + " " + x.NOMBRE_APELLIDO, x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO, costo1);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen requerimientos registrados para los filtros indicados");
                        }
                        dgRequerimientos.ItemsSource = tabla.DefaultView;

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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " REQUERIMIENTOS";
                DataTable tabla = ((DataView)dgRequerimientos.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.Requerimientos_Word(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " REQUERIMIENTOS";
                DataTable tabla = ((DataView)dgRequerimientos.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                string repuesta = exportar.Requerimientos_TextoPlano(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " REQUERIMIENTOS";
                DataTable tabla = ((DataView)dgRequerimientos.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.Requerimientos_Excel(tabla, nombre);
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

        private void dgRequerimientos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgRequerimientos.SelectedItem != null)
                {
                    DataRowView dataRowView = dgRequerimientos.SelectedItem as DataRowView;
                    DataRow dataRow = dataRowView.Row;
                    int id = int.Parse(dataRow["ID"].ToString());
                    IDRESERVA = id;
                    ReservaDAL reservaDAL = new ReservaDAL();
                    ReservaVIEW reserva = reservaDAL.CargarReservaView(id);
                    string folio = id.ToString();
                    for (int i = 0; i < 9; i++)
                    {
                        if (folio.Length < 8)
                            folio = "0" + folio;
                    }
                    CargarTablaProductos();
                    CargarTablaServicios();
                    txtFolio.Text = folio;
                    txtNombreCliente.Text = reserva.NOMBRE_CLIENTE+" "+reserva.NOMBRE_APELLIDO;
                    txtPatente.Text = reserva.PATENTE_VEHICULO;
                    dpkFechaCreacion.SelectedDate = reserva.FECHA_RESERVA;
                    dpkFechaActualizacion.SelectedDate = reserva.FECHA_ULTIMO_UPDATE;
                    txtSucursal.Text = reserva.NOMBRE_SUCURSAL;
                    txtNombreEmpleado.Text = reserva.NOMBRE_EMPLEADO + " " + reserva.APELLIDO_EMPLEADO;
                    txtObservacion.Text = reserva.ORSERVACION_FINAL;
                    ESTADORESERVA = reserva.ESTADO_DIAGNOSTICO;
                    DiagnosticoDAL diagnosticoDAL = new DiagnosticoDAL();
                    List<ServiciosXDiagnosticoVIEW> listaServicio = diagnosticoDAL.ListarServiciosXDiagnostico(reserva.ID_DIAGNOTICO);
                    foreach (var fila in listaServicio)
                    {
                        if (fila.EstadoServicio!="COMPLETADO")
                        {
                            ESTADORESERVA = "INICIADO";
                        }
                        AgregarItemTablaServicios(int.Parse(fila.IdServicio.ToString()), fila.NombreServicio, fila.EstadoServicio, fila.CostoServicio);
                    }
                    List<ProductosXDiagnostico> listaProductos = diagnosticoDAL.ListarProductosXDiagnostico(reserva.ID_DIAGNOTICO);
                    foreach (var fila in listaProductos)
                    {
                        AgregarItemTablaProductos(int.Parse(fila.IdProducto.ToString()), fila.NombreProdcuto, int.Parse(fila.Cantidad.ToString()), Convert.ToDecimal(fila.PrecioUni), Convert.ToDecimal(fila.PrecioTot));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ESTADORESERVA == "INICIADO")
                {
                    RequerimientoNEG requerimientoNEG = new RequerimientoNEG();
                    DateTime fechaActualizacion = DateTime.Now;
                    if (dpkFechaActualizacion.SelectedDate != null)
                    {
                        fechaActualizacion = Convert.ToDateTime(dpkFechaActualizacion.SelectedDate);
                    }
                    string observacion = txtObservacion.Text;                  

                    string respuesta = requerimientoNEG.FinalizarRequerimiento(IDRESERVA, observacion, fechaActualizacion);
                    if (respuesta == "actualizado")
                    {
                        int idReserva = IDRESERVA;
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
                        modelo.CorreoCliente = reserva.CORREO_CLIENTE;
                        modelo.CorreroSucursal = reserva.CORREO_SUCURSAL;
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
                        string nombreRutaDoc = pdf.OrdenTrabajoPedidoPDF(modelo);
                        ServerCorreo abrir_server = new ServerCorreo();
                        Correo correoM = new Correo();
                        SmtpClient server = abrir_server.InstanciaServer();
                        //Instancia la libreria que permite armar correo electronico y llama el metodo que lo crea
                        MailMessage email = correoM.CorreoFinalizacionRequerimiento(nombreRutaDoc, modelo.CorreoCliente, modelo.NombreCliente+" "+modelo.ApellidoCliente,folio);
                        //envia el correo
                        server.Send(email);

                        CargarTablaRequerimientos();
                        MessageBox.Show("La orden de trabajo ha sido finalizada satisfactoriamente. Ahora se encuantra disponible para ser pagada");
                    }
                    else
                    {
                        MessageBox.Show(respuesta);
                    }
                }
                else
                {
                    MessageBox.Show("Sólo se pueden finalizar requerimientos que con estado 'INICIADO', y servicios 'COMPLETADO'");
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString() + "\n" + ex.InnerException.Message.ToString());
            }
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRequerimientos.SelectedItems.Count > 0)
                {
                    if (dgRequerimientos.SelectedItems.Count == 1)
                    {
                        MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar' para comenzar");
                        DataRowView dataRowView = dgRequerimientos.SelectedItem as DataRowView;
                        DataRow dataRow = dataRowView.Row;
                        int idReserva = int.Parse(dataRow["ID"].ToString());
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

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarTablaRequerimientos();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_FinalizarRequerimiento reg = new AYUDA_FinalizarRequerimiento();
            reg.Show();
        }
    }
}
