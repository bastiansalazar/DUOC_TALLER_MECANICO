using AppServiexpress.Ventanas.Taller.Manual;
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

namespace AppServiexpress.Ventanas.Taller
{
    /// <summary>
    /// Lógica de interacción para AdministrarServicios_AD.xaml
    /// </summary>
    public partial class AdministrarServicios_AD : Window
    {
        public AdministrarServicios_AD()
        {
            InitializeComponent();
            this.Activate();
            CargarTablaServicios();
            CargarCombos();
        }

        public void CargarTablaServicios()
        {
            dgServicios.ItemsSource = null;
            DataTable dt = new DataTable();
            ServiciosNEG serviciosNEG = new ServiciosNEG();

            try
            {
                List<ServiciosVIEW> lista = serviciosNEG.ListarTodosServicios();
                dt.Columns.Add("ID");
                dt.Columns.Add("TIPO_SERVICIO");
                dt.Columns.Add("ESTADO_SERVICIO");
                dt.Columns.Add("SUCURSAL");
                dt.Columns.Add("COSTO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.TIPO_SERVICIO, x.ESTADO_SERVICIO, x.SUCURSAL, x.COSTO);
                    }
                }
                dgServicios.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        public void CargarCombos()
        {
            Tipos_EstadosNEG tiposNEG = new Tipos_EstadosNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();

            try
            {
                List<TIPO_SERVICIO> listaTServicios = tiposNEG.ListarTServicios();
                if (listaTServicios.Count > 0)
                {
                    cbxTipoServicio.ItemsSource = listaTServicios;
                    cbxTipoServicio.DisplayMemberPath = "NOMBRE";
                    cbxTipoServicio.SelectedValuePath = "ID";
                }

                List<ESTADO_SERVICIO> listaEServicios = tiposNEG.ListarEServicios();
                if (listaEServicios.Count > 0)
                {
                    cbxEstadoServicio.ItemsSource = listaEServicios;
                    cbxEstadoServicio.DisplayMemberPath = "NOMBRE";
                    cbxEstadoServicio.SelectedValuePath = "ID";
                }

                List<SUCURSAL> listaSucursales = sucursalNEG.ListarSucuralesActivas();
                if (listaSucursales.Count > 0)
                {
                    cbxSucursal.ItemsSource = listaSucursales;
                    cbxSucursal.DisplayMemberPath = "NOMBRE";
                    cbxSucursal.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        private void dgServicios_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgServicios.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idServicio = Convert.ToInt32(dr1.ItemArray[0]);
            ServiciosNEG serviciosNEG = new ServiciosNEG();
            var datos = serviciosNEG.CargarServicio(idServicio);
            cbxTipoServicio.SelectedValue = datos.Tipo_Servicio_Id;
            cbxEstadoServicio.SelectedValue = datos.Estado_Servicio_Id;
            cbxSucursal.SelectedValue = datos.Sucursal_Id;
            txtCostoBase.Text = datos.COSTO.ToString();
            lbl_IdServicio.Content = idServicio;
        }
        public void LimpiarFormulario()
        {
            txtCostoBase.Text = "";
            lbl_IdServicio.Content = "";
            cbxEstadoServicio.SelectedIndex = -1;
            cbxSucursal.SelectedIndex = -1;
            cbxTipoServicio.SelectedIndex = -1;
            txtBusqueda.Text = "";
            cbxTipoBusqueda.SelectedIndex = -1;
            CargarTablaServicios();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgServicios.ItemsSource = null;
                DataTable dt = new DataTable();
                ServiciosNEG serviciosNEG = new ServiciosNEG();
                List<ServiciosVIEW> lista = serviciosNEG.FiltrarServicios(tipo, valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("TIPO_SERVICIO");
                dt.Columns.Add("ESTADO_SERVICIO");
                dt.Columns.Add("SUCURSAL");
                dt.Columns.Add("COSTO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.TIPO_SERVICIO, x.ESTADO_SERVICIO, x.SUCURSAL, x.COSTO);
                    }
                }
                else
                {
                    MessageBox.Show("No existen servicios registrados para los filtros indicados");
                }
                dgServicios.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiciosNEG serviciosNEG = new ServiciosNEG();
                int tipo_servicio = int.Parse(cbxTipoServicio.SelectedValue.ToString());
                int estado_servicio = int.Parse(cbxEstadoServicio.SelectedValue.ToString());
                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                int costo = int.Parse(txtCostoBase.ToString());
                string respuesta = serviciosNEG.CrearServicio(tipo_servicio,estado_servicio,sucursal,costo);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El servicio ha sido ingresado satisfactoriamente a la base de datos");
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
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiciosNEG serviciosNEG = new ServiciosNEG();
                int tipo_servicio = int.Parse(cbxTipoServicio.SelectedValue.ToString());
                int estado_servicio = int.Parse(cbxEstadoServicio.SelectedValue.ToString());
                int sucursal = int.Parse(cbxSucursal.SelectedValue.ToString());
                int _id = 0;
                string a = lbl_IdServicio.Content.ToString();
                int.TryParse(a,out _id);
                int costo = int.Parse(txtCostoBase.ToString());
                string respuesta = serviciosNEG.ActualizarServicio(tipo_servicio,estado_servicio,sucursal,_id,costo);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El servicio ha sido actualizado satisfactoriamente");
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

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarTablaServicios();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_AdministrarServicios reg = new AYUDA_AdministrarServicios();
            reg.Show();
        }
    }
}
