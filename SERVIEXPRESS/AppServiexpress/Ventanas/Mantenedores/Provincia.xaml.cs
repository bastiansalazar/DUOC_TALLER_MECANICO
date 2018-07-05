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
    /// Lógica de interacción para Provincia.xaml
    /// </summary>
    public partial class Provincia : Window
    {
        public Provincia()
        {
            InitializeComponent();
            CargarTabla();
        }

        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            RegionNEG regionNEG = new RegionNEG();
            ProvinciaNEG provinciaNEG = new ProvinciaNEG();

            try
            {
                List<PROVINCIA> lista = provinciaNEG.ListarTodasProvincias();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("REGION");
                dt.Columns.Add("FECHA CREACION");
                dt.Columns.Add("FECHA ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.REGION.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;
                

                List<REGION> listaRegion = regionNEG.ListarTodasRegiones();
                if (listaRegion.Count > 0)
                {
                    cbxRegion.ItemsSource = listaRegion;
                    cbxRegion.DisplayMemberPath = "NOMBRE";
                    cbxRegion.SelectedValuePath = "ID";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        private void dgDatos_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgDatos.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int id = Convert.ToInt32(dr1.ItemArray[0]);
            ProvinciaNEG provinciaNEG = new ProvinciaNEG();
            var datos = provinciaNEG.CargarProvincia(id);
            txtNombre.Text = datos.NOMBRE;
            cbxRegion.SelectedValue = datos.REGION_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lblId.Content = "";
            cbxRegion.SelectedValue = -1;
            CargarTabla();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgDatos.ItemsSource = null;
                DataTable dt = new DataTable();
                ProvinciaNEG provinciaNEG = new ProvinciaNEG();
                List<PROVINCIA> lista = provinciaNEG.FiltrarProvincia(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("REGION");
                dt.Columns.Add("FECHA CREACION");
                dt.Columns.Add("FECHA ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.REGION.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                else
                {
                    MessageBox.Show("No existen datos registrados para los filtros indicados");
                }
                dgDatos.ItemsSource = dt.DefaultView;

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
                ProvinciaNEG provinciaNEG = new ProvinciaNEG();
                string nombre = txtNombre.Text.ToUpper();
                int region = int.Parse(cbxRegion.SelectedValue.ToString());
                string respuesta = provinciaNEG.CrearProvincia(nombre, region);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("Los datos fueron ingresados satisfactoriamente");
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
                ProvinciaNEG provinciaNEG = new ProvinciaNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lblId.Content.ToString());
                int region = int.Parse(cbxRegion.SelectedValue.ToString());
                string respuesta = provinciaNEG.ActualizarProvincia(nombre, id, region);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("Los datos fueron actualizados satisfactoriamente");
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
            CargarTabla();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_Mantenedores reg = new AYUDA_Mantenedores();
            reg.Show();
        }
    }
}
