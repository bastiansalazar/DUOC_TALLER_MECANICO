using AppServiexpress.Ventanas.Boletas.Manual;
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

namespace AppServiexpress.Ventanas.Boletas
{
    /// <summary>
    /// Lógica de interacción para RegistroVentas_AD.xaml
    /// </summary>
    public partial class RegistroVentas_AD : Window
    {
        public RegistroVentas_AD()
        {
            InitializeComponent();
            dpkFechaDesde.DisplayDateStart = new DateTime(1900, 01, 01);
            CargarTablaVentas();
        }

        public void CargarTablaVentas()
        {            
            try
            {
                dgVentas.ItemsSource = null;
                DataTable tabla = new DataTable();
                VentasNEG ventasNEG = new VentasNEG();

                List<VentasVIEW> lista = ventasNEG.ListarTodasVentas();
                tabla.Columns.Add("ID");
                tabla.Columns.Add("FOLIO");
                tabla.Columns.Add("TIPO VENTA");
                tabla.Columns.Add("FECHA VENTA");
                tabla.Columns.Add("SUCURSAL");
                tabla.Columns.Add("EMPLEADO");
                tabla.Columns.Add("CANTIDAD ART");
                tabla.Columns.Add("MONTO_TOTAL");
                tabla.Columns.Add("MONEDA");
                tabla.Columns.Add("CLIENTE");        
                tabla.Columns.Add("ESTADO");

                AnulacionVentaNEG anulacionVentaNEG = new AnulacionVentaNEG();
                List<ANULACION_VENTA> ventaAnulada = anulacionVentaNEG.ListaVentasAnuladas();

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

                        string anulacion = "VIGENTE";
                        foreach(var ven in ventaAnulada)
                        {
                            if (ven.VENTAS_ID == x.ID)
                            {
                                anulacion ="ANULADA";
                            }
                        }

                        string cantidad = x.CANTIDAD_TOTAL.ToString();
                        string costo1 = string.Format("{0:n2}", x.MONTO_TOTAL);
                        tabla.Rows.Add(x.ID, folio, x.TIPO_VENTA, x.FECHA_VENTA, x.NOMBRE_SUCURSAL,x.NOMBRE_EMPLEADO+" "+x.APELLIDO_EMPLEADO,
                        cantidad, costo1, x.TIPO_MONEDA, x.NOMBRE_CLIENTE + " " + x.APELLID_CLIENTE,anulacion);


                    }
                }
                dgVentas.ItemsSource = tabla.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void dgVentas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgVentas.SelectedItem != null)
                {
                    DataRowView dataRowView = dgVentas.SelectedItem as DataRowView;
                    DataRow dataRow = dataRowView.Row;
                    ModificarVenta reg = new ModificarVenta(dataRow, this);
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
                if (dpkFechaDesde!=null)
                {
                    if (dpkFechaHasta != null)
                    {
                        if (dpkFechaDesde.SelectedDate <= dpkFechaHasta.SelectedDate)
                        {
                            dgVentas.ItemsSource = null;
                            DataTable tabla = new DataTable();
                            VentasNEG ventasNEG = new VentasNEG();
                            DateTime desde = DateTime.Parse(dpkFechaDesde.SelectedDate.ToString());
                            DateTime _hasta = DateTime.Parse(dpkFechaHasta.SelectedDate.ToString());
                            DateTime hasta = new DateTime(_hasta.Year, _hasta.Month, _hasta.Day, 23, 59,59);

                            List<VentasVIEW> lista = ventasNEG.ListarVentasRangoFecha(desde,hasta);
                            tabla.Columns.Add("ID");
                            tabla.Columns.Add("FOLIO");
                            tabla.Columns.Add("TIPO VENTA");
                            tabla.Columns.Add("FECHA VENTA");
                            tabla.Columns.Add("SUCURSAL");
                            tabla.Columns.Add("EMPLEADO");
                            tabla.Columns.Add("CANTIDAD ART");
                            tabla.Columns.Add("MONTO TOTAL");
                            tabla.Columns.Add("MONEDA");
                            tabla.Columns.Add("CLIENTE");
                            tabla.Columns.Add("ESTADO");

                            AnulacionVentaNEG anulacionVentaNEG = new AnulacionVentaNEG();
                            List<ANULACION_VENTA> ventaAnulada = anulacionVentaNEG.ListaVentasAnuladas();

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

                                    string anulacion = "VIGENTE";
                                    foreach (var ven in ventaAnulada)
                                    {
                                        if (ven.VENTAS_ID == x.ID)
                                        {
                                            anulacion = "ANULADA";
                                        }
                                    }

                                    string cantidad = x.CANTIDAD_TOTAL.ToString();
                                    string costo1 = string.Format("{0:n2}", x.MONTO_TOTAL);
                                    tabla.Rows.Add(x.ID, folio, x.TIPO_VENTA, x.FECHA_VENTA, x.NOMBRE_SUCURSAL, x.NOMBRE_EMPLEADO + " " + x.APELLIDO_EMPLEADO ,
                                    cantidad, costo1, x.TIPO_MONEDA, x.NOMBRE_CLIENTE + " " + x.APELLID_CLIENTE, anulacion);


                                }
                            }
                            dgVentas.ItemsSource = tabla.DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("La fecha de término no puede ser menor a la de inicio");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe indicar una fecha de fin del rango");
                    }
                }
                else
                {
                    MessageBox.Show("Debe indicar una fecha de inicio del rango");
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " VENTAS";
                DataTable tabla = ((DataView)dgVentas.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                string repuesta = exportar.Ventas_TextoPlano(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " VENTAS";
                DataTable tabla = ((DataView)dgVentas.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.Ventas_Word(tabla, nombre);
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
                string nombre = "REPORTE " + _anio + _mes + _dia + _hora + _minuto + _segundos + " VENTAS";
                DataTable tabla = ((DataView)dgVentas.ItemsSource).ToTable();
                ExportarArchivos exportar = new ExportarArchivos();
                MessageBox.Show("Espere mientras el archivo es generado. Presione 'Aceptar' para iniciar la descarga");
                string repuesta = exportar.Ventas_Excel(tabla, nombre);
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
            CargarTablaVentas();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_RegistroVenta reg = new AYUDA_RegistroVenta();
            reg.Show();
        }
    }
}
