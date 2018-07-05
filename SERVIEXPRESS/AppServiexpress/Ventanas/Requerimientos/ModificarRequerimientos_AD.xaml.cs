using AppServiexpress.Ventanas.Requerimientos.Manual;
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
    /// Lógica de interacción para ModificarRequerimientos_AD.xaml
    /// </summary>
    public partial class ModificarRequerimientos_AD : Window
    {
        public ModificarRequerimientos_AD()
        {
            InitializeComponent();
            CargarTablaRequerimientos();
            cbxTextoBusqueda.IsEnabled = false;
        }

        public void CargarTablaRequerimientos()
        {
            try
            {
                dgReuqerimientos.ItemsSource = null;
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
                        tabla.Rows.Add(x.ID,folio,x.ESTADO_DIAGNOSTICO,x.FECHA_RESERVA,x.NOMBRE_SUCURSAL,
                            x.PATENTE_VEHICULO,x.TIPO_VEHICULO,x.MARCA_VEHICULO,x.NOMBRE_CLIENTE+" "+x.NOMBRE_APELLIDO,x.NOMBRE_EMPLEADO+" "+x.APELLIDO_EMPLEADO,costo1);
                    }
                }
                dgReuqerimientos.ItemsSource = tabla.DefaultView;

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
                        dgReuqerimientos.ItemsSource = null;
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
                                    x.PATENTE_VEHICULO, x.TIPO_VEHICULO, x.MARCA_VEHICULO, x.NOMBRE_CLIENTE+" "+x.NOMBRE_APELLIDO, x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO, costo1);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen requerimientos registrados para los filtros indicados");
                        }
                        dgReuqerimientos.ItemsSource = tabla.DefaultView;

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
                DataTable tabla = ((DataView)dgReuqerimientos.ItemsSource).ToTable();
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
                DataTable tabla = ((DataView)dgReuqerimientos.ItemsSource).ToTable();
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
                DataTable tabla = ((DataView)dgReuqerimientos.ItemsSource).ToTable();
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

        private void dgReuqerimientos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgReuqerimientos.SelectedItem != null)
                {
                    DataRowView dataRowView = dgReuqerimientos.SelectedItem as DataRowView;
                    DataRow dataRow = dataRowView.Row;
                    ModificarRequerimientos2 reg = new ModificarRequerimientos2(dataRow, this);
                    reg.ShowDialog();
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
            AYUDA_ModificarRequerimiento reg = new AYUDA_ModificarRequerimiento();
            reg.Show();
        }
    }
}
