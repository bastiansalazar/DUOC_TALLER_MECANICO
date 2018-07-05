using AppServiexpress.Ventanas.Pedidos.Manual;
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
    /// Lógica de interacción para EmitirDevolucion_AD.xaml
    /// </summary>
    public partial class EmitirDevolucion_AD : Window
    {
        public EmitirDevolucion_AD()
        {
            InitializeComponent();
            CargarCombos();
            cbxFolio.IsEnabled = false;
        }

        private void CargarCombos()
        {
            try
            {
                SucursalNEG sucursalNEG = new SucursalNEG();
                List<SUCURSAL> listaSucursal = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursal.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursal;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cbxSucursal.SelectedValue = -1;
            CargarTablaDetalle();
            txtMotivo.Text = "";
            txtEmailSucursal.Text = "";
            txtEmailProveedor.Text = "";
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxFolio.SelectedValue !=null)
                {
                    string motivo = txtMotivo.Text;
                    string emailPro = txtEmailProveedor.Text;
                    string emailSuc = txtEmailSucursal.Text;
                    if (motivo.Trim().Length>=10)
                    {
                        if (emailPro.Trim().Length >=5)
                        {
                            if (emailSuc.Trim().Length >= 5)
                            {
                                MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar' para comenzar");
                                int idFolio = int.Parse(cbxFolio.SelectedValue.ToString());
                                string folio = idFolio.ToString();
                                for (int i = 0; i < 9; i++)
                                {
                                    if (folio.Length < 8)
                                        folio = "0" + folio;
                                }
                                ExportarArchivos pdf = new ExportarArchivos();
                                PDF_ModeloDevolucion modelo = new PDF_ModeloDevolucion();
                                OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                                OrdenPedidoVIEW orden = new OrdenPedidoVIEW();
                                DetalleOrdenPedidoNEG detalleOrdenPedidoNEG =new DetalleOrdenPedidoNEG();
                                orden = ordenPedidoNEG.CargarOrdenPedido(idFolio);
                                modelo.Sucursal = orden.SUCURSAL;
                                modelo.Folio = folio;
                                modelo.NombreProveedor = orden.PROVEEDOR;
                                modelo.RolProveedor = orden.NUMID_PROVEEDOR + "-" + orden.DIVID_PROVEEDOR;
                                modelo.Motivo = motivo;
                                modelo.EmailProveedor = emailPro;
                                modelo.EmailSucursal = emailSuc;                                
                                modelo.DetalleOrdenPedido = detalleOrdenPedidoNEG.CargarlistaDetalleOrden(idFolio);

                                ServerCorreo abrir_server = new ServerCorreo();
                                Correo correoM = new Correo();
                                //parametriza el servidor STMP para enviar el correo
                                SmtpClient server = abrir_server.InstanciaServer();

                                string respuesta = pdf.DevolucionPedidoPDF(modelo);

                                //Instancia la libreria que permite armar correo electronico y llama el metodo que lo crea
                                MailMessage email = correoM.CorreoDevolucionPedido(respuesta, emailSuc,emailPro,modelo.NombreProveedor,folio,motivo);
                                //envia el correo
                                server.Send(email);


                                MessageBox.Show("El documento se genero correctamente. Ubíquelo en 'Mis Documentos'");
                            }
                            else
                            {
                                MessageBox.Show("Debe indicar un correo de la sucursal");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe indicar un correo del proveedor");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe indicar un motivo de la menos 10 caracteres");
                    }                    
                }
                else
                {
                    MessageBox.Show("Debe seleccionar solo una fila");
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
                if (cbxFolio.SelectedValue != null)
                {
                    string motivo = txtMotivo.Text;
                    string emailPro = txtEmailProveedor.Text;
                    string emailSuc = txtEmailSucursal.Text;
                    if (motivo.Trim().Length >= 10)
                    {
                        if (emailPro.Trim().Length >= 5)
                        {
                            if (emailSuc.Trim().Length >= 5)
                            {
                                MessageBox.Show("Espere mientras el documento es generado. Presione 'Aceptar' para comenzar");
                                int idFolio = int.Parse(cbxFolio.SelectedValue.ToString());
                                string folio = idFolio.ToString();
                                for (int i = 0; i < 9; i++)
                                {
                                    if (folio.Length < 8)
                                        folio = "0" + folio;
                                }
                                ExportarArchivos pdf = new ExportarArchivos();
                                PDF_ModeloDevolucion modelo = new PDF_ModeloDevolucion();
                                OrdenPedidoNEG ordenPedidoNEG = new OrdenPedidoNEG();
                                OrdenPedidoVIEW orden = new OrdenPedidoVIEW();
                                DetalleOrdenPedidoNEG detalleOrdenPedidoNEG = new DetalleOrdenPedidoNEG();
                                orden = ordenPedidoNEG.CargarOrdenPedido(idFolio);
                                modelo.Sucursal = orden.SUCURSAL;
                                modelo.Folio = folio;
                                modelo.NombreProveedor = orden.PROVEEDOR;
                                modelo.RolProveedor = orden.NUMID_PROVEEDOR + "-" + orden.DIVID_PROVEEDOR;
                                modelo.Motivo = motivo;
                                modelo.EmailProveedor = emailPro;
                                modelo.EmailSucursal = emailSuc;
                                modelo.DetalleOrdenPedido = detalleOrdenPedidoNEG.CargarlistaDetalleOrden(idFolio);

                                string respuesta = pdf.DevolucionPedidoPDF(modelo);

                                MessageBox.Show("El documento se genero correctamente. Ubíquelo en 'Mis Documentos'");
                            }
                            else
                            {
                                MessageBox.Show("Debe indicar un correo de la sucursal");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe indicar un correo del proveedor");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe indicar un motivo de la menos 10 caracteres");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar solo una fila");
                }

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
                    cbxFolio.ItemsSource = null;
                    CargarTablaDetalle();
                    OrdenPedidoDAL orden = new OrdenPedidoDAL();
                    List<FolioView> listafolio = orden.ListarFoliosSucursalEnviada(sucursal);
                    if (listafolio.Count > 0)
                    {
                        cbxFolio.ItemsSource = listafolio;
                        cbxFolio.DisplayMemberPath = "NOMBRE";
                        cbxFolio.SelectedValuePath = "ID";
                        cbxFolio.IsEnabled = true;
                    }
                    else
                    {
                        cbxFolio.SelectedValue = -1;
                        cbxFolio.IsEnabled = false;
                    }                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void CargarTablaDetalle()
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

        private void cbxFolio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFolio.SelectedValue != null)
            {
                try
                {
                    int ordePedido = int.Parse(cbxFolio.SelectedValue.ToString());
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
                DataTable tabla = ((DataView)dgProductos.ItemsSource).ToTable();
                DataRow fila = tabla.NewRow();
                fila["ID PRODUCTO"] = _idProd;
                fila["PRODUCTO"] = _nombre;
                fila["CANTIDAD"] = _cantidad;
                fila["PRECIO UNITARIO"] = _preUni;
                fila["PRECIO TOTAL"] = _preTot;
                tabla.Rows.Add(fila);
                dgProductos.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_EmitirDevolucion reg = new AYUDA_EmitirDevolucion();
            reg.Show();
        }
    }
}
