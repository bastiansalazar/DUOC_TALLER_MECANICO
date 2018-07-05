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
    /// Lógica de interacción para AdministrarVehiculos_AD.xaml
    /// </summary>
    public partial class AdministrarVehiculos_AD : Window
    {
        public AdministrarVehiculos_AD()
        {
            InitializeComponent();
            this.Activate();
            CargarTablaVehiculos();
            CargarCombos();
        }

        public void CargarTablaVehiculos()
        {
            dgVehiculos.ItemsSource = null;
            DataTable dt = new DataTable();
            VehiculosNEG vehiculosNEG = new VehiculosNEG();

            try
            {
                List<VehiculosVIEW> lista = vehiculosNEG.ListarTodosVehiculos();
                dt.Columns.Add("ID");
                dt.Columns.Add("PATENTE");
                dt.Columns.Add("MARCA");
                dt.Columns.Add("TIPO");
                dt.Columns.Add("NOMBRE_CLIENTE");
                dt.Columns.Add("RUT_CLIENTE");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.PATENTE, x.MARCA, x.TIPO, x.NOMBRE_CLIENTE, x.RUT_CLIENTE+"-"+x.DIV_CLIENTE);
                    }
                }
                dgVehiculos.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        public void CargarCombos()
        {
            Tipos_EstadosNEG tiposNEG = new Tipos_EstadosNEG();
            Marca_VehiculosNEG marca_VehiculosNEG = new Marca_VehiculosNEG();
            ClientesNEG clientesNEG = new ClientesNEG();

            try
            {
                List<TIPO_VEHICULO> listaTVehiculos = tiposNEG.ListarTVehiculos();
                if (listaTVehiculos.Count > 0)
                {
                    cbxTipoVehiculo.ItemsSource = listaTVehiculos;
                    cbxTipoVehiculo.DisplayMemberPath = "NOMBRE";
                    cbxTipoVehiculo.SelectedValuePath = "ID";
                }

                List<MARCA_VEHICULO> listaMarcaVehiculos = marca_VehiculosNEG.ListarTodasMarcas();
                if (listaMarcaVehiculos.Count > 0)
                {
                    cbxMarcaVehiculo.ItemsSource = listaMarcaVehiculos;
                    cbxMarcaVehiculo.DisplayMemberPath = "NOMBRE";
                    cbxMarcaVehiculo.SelectedValuePath = "ID";
                }

                List<ClienteVIEW> listaClientes = clientesNEG.ListarTodosClientes();
                if (listaClientes.Count > 0)
                {
                    cbxCliente.ItemsSource = listaClientes;
                    cbxCliente.DisplayMemberPath = "NUM_ID";
                    cbxCliente.SelectedValuePath = "ID";//valos del combobox, considerar eso als elecciona cliente
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        private void dgVehiculos_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgVehiculos.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idVehiculo = Convert.ToInt32(dr1.ItemArray[0]);
            VehiculosNEG vehiculosNEG = new VehiculosNEG();
            var datos = vehiculosNEG.CargarVehiculo(idVehiculo);
            cbxTipoVehiculo.SelectedValue = datos.TIPO_VEHICULO_ID;
            cbxMarcaVehiculo.SelectedValue = datos.MARCA_VEHICULO_ID;
            cbxCliente.SelectedValue = datos.CLIENTE_ID;
            txtPatente.Text = datos.PATENTE;
            lbl_IdVehiculo.Content = idVehiculo;
        }
        public void LimpiarFormulario()
        {
            txtPatente.Text = "";
            cbxTipoVehiculo.SelectedIndex = -1;
            cbxMarcaVehiculo.SelectedIndex = -1;
            cbxCliente.SelectedIndex = -1;
            lbl_IdVehiculo.Content = "";
            CargarTablaVehiculos();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgVehiculos.ItemsSource = null;
                DataTable dt = new DataTable();
                VehiculosNEG vehiculosNEG = new VehiculosNEG();
                List<VehiculosVIEW> lista = vehiculosNEG.FiltrarVehiculos(tipo, valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("PATENTE");
                dt.Columns.Add("MARCA");
                dt.Columns.Add("TIPO");
                dt.Columns.Add("NOMBRE_CLIENTE");
                dt.Columns.Add("RUT_CLIENTE");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.PATENTE, x.MARCA, x.TIPO, x.NOMBRE_CLIENTE, x.RUT_CLIENTE);
                    }
                }
                else
                {
                    MessageBox.Show("No existen vehiculos registrados para los filtros indicados");
                }
                dgVehiculos.ItemsSource = dt.DefaultView;

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
                VehiculosNEG vehiculosNEG = new VehiculosNEG();
                int tipoVehiculo = int.Parse(cbxTipoVehiculo.SelectedValue.ToString());
                int marcaVehiculo = int.Parse(cbxMarcaVehiculo.SelectedValue.ToString());
                int cliente = int.Parse(cbxCliente.SelectedValue.ToString());
                string patente = txtPatente.Text.ToUpper();
                string respuesta = vehiculosNEG.CrearVehiculo(patente,cliente, marcaVehiculo, tipoVehiculo);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El vehiculo ha sido ingresado satisfactoriamente a la base de datos");
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
                VehiculosNEG vehiculosNEG = new VehiculosNEG();
                int tipoVehiculo = int.Parse(cbxTipoVehiculo.SelectedValue.ToString());
                int marcaVehiculo = int.Parse(cbxMarcaVehiculo.SelectedValue.ToString());
                int cliente = int.Parse(cbxCliente.SelectedValue.ToString());
                int _id = 0;
                string a = lbl_IdVehiculo.Content.ToString();
                int.TryParse(a, out _id);
                string respuesta = vehiculosNEG.ActualizarVehiculo(cliente, marcaVehiculo, tipoVehiculo, _id);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El vehiculo ha sido actualizado satisfactoriamente");
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
            CargarTablaVehiculos();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_AdministrarVehiculos reg = new AYUDA_AdministrarVehiculos();
            reg.Show();
        }
    }//
}
