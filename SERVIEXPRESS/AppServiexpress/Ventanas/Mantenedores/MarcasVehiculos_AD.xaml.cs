using AppServiexpress.Ventanas.Mantenedores.Manual;
using BBCServiexpress.DAL;
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

namespace AppServiexpress.Ventanas.Mantenedores
{
    /// <summary>
    /// Lógica de interacción para MarcasVehiculos_AD.xaml
    /// </summary>
    public partial class MarcasVehiculos_AD : Window
    {
        public MarcasVehiculos_AD()
        {
            InitializeComponent();
            CargarTablaMarcasVehiculos();
            this.Activate();
        }

        public void CargarTablaMarcasVehiculos()
        {
            this.dgMarcaVehiculo.ItemsSource = null;
            DataTable dt = new DataTable();
            Marca_VehiculosNEG marcasVehiculosNEG = new Marca_VehiculosNEG();

            try
            {
                List<MARCA_VEHICULO> lista = marcasVehiculosNEG.ListarTodasMarcas();
                dt.Columns.Add("ID");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ULTIMO_UPDATE");
                dt.Columns.Add("NOMBRE");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE, x.NOMBRE);
                    }
                }
                dgMarcaVehiculo.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        private void dgMarcaVehiculo_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgMarcaVehiculo.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idMarca = Convert.ToInt32(dr1.ItemArray[0]);
            Marca_VehiculosNEG marcaVehiculoNEG = new Marca_VehiculosNEG();
            var datos = marcaVehiculoNEG.CargarMarcaVehiculo(idMarca);
            txtNombre.Text = datos.NOMBRE;
            lbl_idMarcaVehiculo.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lbl_idMarcaVehiculo.Content = "";
            CargarTablaMarcasVehiculos();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgMarcaVehiculo.ItemsSource = null;
                DataTable dt = new DataTable();
                Marca_VehiculosNEG marcaVehiculoNEG = new Marca_VehiculosNEG();
                List<MARCA_VEHICULO> lista = marcaVehiculoNEG.FiltrarMarcasVehiculos(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ULTIMO_UPDATE");
                dt.Columns.Add("NOMBRE");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE, x.NOMBRE);
                    }
                }
                else
                {
                    MessageBox.Show("No existen marcas de vehiculos registradas para los filtros indicados");
                }
                dgMarcaVehiculo.ItemsSource = dt.DefaultView;

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
                Marca_VehiculosNEG marcaVehiculoNEG = new Marca_VehiculosNEG();
                string nombre = txtNombre.Text.ToUpper();
                string respuesta = marcaVehiculoNEG.CrearMarcaVehiculo(nombre);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("La marca vehiculo ha sido ingresada satisfactoriamente a la base de datos");
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
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Marca_VehiculosNEG marcaVehiculoNEG = new Marca_VehiculosNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lbl_idMarcaVehiculo.Content.ToString());
                string respuesta = marcaVehiculoNEG.ActualizarMarcaVehiculo(nombre, id);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("La marca del vehiculo ha sido actualizado satisfactoriamente");
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
            CargarTablaMarcasVehiculos();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_Mantenedores reg = new AYUDA_Mantenedores();
            reg.Show();
        }
    }
}
