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
    /// Lógica de interacción para ModificarVenta.xaml
    /// </summary>
    public partial class ModificarVenta : Window
    {
        private DataRow dataRow;
        private RegistroVentas_AD registroVentas_AD;
        public DatosDocumentoPagoVIEW datosDocumentoPagoVIEW;

        public ModificarVenta()
        {
            InitializeComponent();
        }

        public ModificarVenta(DataRow dataRow, RegistroVentas_AD registroVentas_AD)
        {
            InitializeComponent();
            this.dataRow = dataRow;
            this.registroVentas_AD = registroVentas_AD;
            CargarTablaDetalle();
            CargarBoleta(dataRow);
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

        private void CargarBoleta(DataRow dataRow)
        {
            CargarTablaDetalle();
            int idBoleta = int.Parse(dataRow["ID"].ToString());
            VentasDAL ventas = new VentasDAL();
            AnulacionVentasDAL anulacionVentasDAL = new AnulacionVentasDAL();
            datosDocumentoPagoVIEW = ventas.CargarDatos(idBoleta);
            VENTAS datoVenta = ventas.ObtenerVentaId(idBoleta);
            SucursalNEG sucursalNEG = new SucursalNEG();
            EmpleadosNEG empleadosNEG = new EmpleadosNEG();
            txtCliente.Text = datosDocumentoPagoVIEW.NOMBRE_CLIENTE + " " + datosDocumentoPagoVIEW.APELLIDO_CLIENTE;
            txtSucursal.Text = sucursalNEG.CargarSucursal(datoVenta.SUCURSAL_ID).NOMBRE;
            txtEmpleado.Text = empleadosNEG.CargarEmpleado(datoVenta.EMPLEADO_ID).NOMBRE + " " + empleadosNEG.CargarEmpleado(datoVenta.EMPLEADO_ID).APELLIDO;
            dpkFechaCreacion.SelectedDate = datoVenta.FECHA_VENTA;
            string folio = idBoleta.ToString();
            for (int i = 0; i < 9; i++)
            {
                if (folio.Length < 8)
                    folio = "0" + folio;
            }
            
            txtFolio.Text = folio;
            txtTipoDocumento.Text =  datosDocumentoPagoVIEW.TIPO_DOCUMENTO;
            txtestadoVenta.Text = anulacionVentasDAL.ValidaEstadoVenta(idBoleta);
            txtMoneda.Text = datosDocumentoPagoVIEW.TIPO_MONEDA;
            double neto = Convert.ToDouble(datosDocumentoPagoVIEW.TOTAL) / 1.19;
            txtNeto.Text = string.Format("{0:n2}", (neto));
            double iva = neto * 0.19;
            txtIva.Text = string.Format("{0:n2}", iva);
            txtTotal.Text = string.Format("{0:n2}", (Convert.ToDouble(datosDocumentoPagoVIEW.TOTAL)));
            txtTotalMoneda.Text = string.Format("{0:n2}", (datosDocumentoPagoVIEW.TOTAL* datosDocumentoPagoVIEW.COSTO_MONEDA));
            //ORDEN DE TRABAJO N°"
            List<DETALLE_VENTAS> listaDetalles = ventas.ListarDetalleVenta(idBoleta);
            foreach(var x in listaDetalles)
            {
                if (x.SERVICIO_ID == 1 && x.PRODUCTO_ID==1)
                {//OT
                    string idOrden = x.NOMBRE_PRODUCTO.Split('°')[1];
                    DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                    DataRow fila = tabla.NewRow();
                    fila["CANTIDAD"] = 1;
                    fila["TIPO ITEM"] = "OT";
                    fila["ID ITEM"] = idOrden;
                    fila["NOMBRE ITEM"] = x.NOMBRE_PRODUCTO;
                    fila["P UNITARIO"] = x.PRECIO_VENTA;
                    fila["TOTAL"] = x.PRECIO_VENTA;
                    tabla.Rows.Add(fila);
                    dgDetalleDocumento.ItemsSource = tabla.DefaultView;
                }
                else if (x.PRODUCTO_ID==1 && x.SERVICIO_ID >1)
                {//serv
                    DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                    DataRow fila = tabla.NewRow();
                    fila["CANTIDAD"] = 1;
                    fila["TIPO ITEM"] = "SER";
                    fila["ID ITEM"] = x.SERVICIO_ID;
                    fila["NOMBRE ITEM"] = x.NOMBRE_PRODUCTO;
                    fila["P UNITARIO"] = x.PRECIO_VENTA;
                    fila["TOTAL"] = x.MONTO_TOTAL;
                    tabla.Rows.Add(fila);
                    dgDetalleDocumento.ItemsSource = tabla.DefaultView;
                }
                else if (x.PRODUCTO_ID >1 && x.SERVICIO_ID == 1)
                {//prod
                    DataTable tabla = ((DataView)dgDetalleDocumento.ItemsSource).ToTable();
                    DataRow fila = tabla.NewRow();
                    fila["CANTIDAD"] = x.CANTIDAD;
                    fila["TIPO ITEM"] = "PRO";
                    fila["ID ITEM"] = x.PRODUCTO_ID;
                    fila["NOMBRE ITEM"] = x.NOMBRE_PRODUCTO;
                    fila["P UNITARIO"] = x.PRECIO_VENTA;
                    fila["TOTAL"] = x.MONTO_TOTAL;
                    tabla.Rows.Add(fila);
                    dgDetalleDocumento.ItemsSource = tabla.DefaultView;
                }
            }        
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar para comenzar'");
            int idVenta = int.Parse(dataRow["ID"].ToString());
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
            string rutaDcoc = PDF.DocumentoPagoPDF(datos, folio);
            MessageBox.Show("El documento ha sido generado correctamente. Puede buecar el archivo en 'Mis Documentos'");
        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtestadoVenta.Text =="VIGENTE")
                {
                    MessageBox.Show("Para confirmar la anulación presione aceptar'");
                    int idVenta = int.Parse(dataRow["ID"].ToString());
                    AnulacionVentasDAL ventasDAL = new AnulacionVentasDAL();
                    bool respuesta = ventasDAL.AnularVenta(idVenta);
                    if (respuesta)
                    {
                        MessageBox.Show("La venta fue anulada correctamente");
                       CargarBoleta(dataRow);
                        registroVentas_AD.CargarTablaVentas();
                    }
                    else
                    {
                        MessageBox.Show("La venta no pudo ser anulada"); 
                    }
                }
                else
                {
                    MessageBox.Show("Esta venta ya se encuentra anulada");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }
    }
}
