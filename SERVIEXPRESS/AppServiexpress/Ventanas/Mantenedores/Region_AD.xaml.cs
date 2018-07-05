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
    /// Lógica de interacción para Region_AD.xaml
    /// </summary>
    public partial class Region_AD : Window
    {
        public Region_AD()
        {
            InitializeComponent();
            CargarTabla();
        }

        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            PaisNEG paisNEG = new PaisNEG();
            RegionNEG regionNEG = new RegionNEG();

            try
            {
                List<REGION> lista = regionNEG.ListarTodasRegiones();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("PAIS");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE,x.PAIS.NOMBRE,x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

                List<PAIS> listaPaises = paisNEG.ListarPaises();
                if (listaPaises.Count > 0)
                {
                    cbxPais.ItemsSource = listaPaises;
                    cbxPais.DisplayMemberPath = "NOMBRE";
                    cbxPais.SelectedValuePath = "ID";
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
            RegionNEG regionNEG = new RegionNEG();
            var datos = regionNEG.CargarRegion(id);
            txtNombre.Text = datos.NOMBRE;
            cbxPais.SelectedValue = datos.PAIS_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lblId.Content = "";
            cbxPais.SelectedValue = -1;
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
                RegionNEG regionNEG = new RegionNEG();
                List<REGION> lista = regionNEG.FiltrarRegion(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("PAIS");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.PAIS.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
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
                RegionNEG regionNEG = new RegionNEG();
                string nombre = txtNombre.Text.ToUpper();
                int pais = int.Parse(cbxPais.SelectedValue.ToString());
                string respuesta = regionNEG.CrearRegion(nombre,pais);
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
                RegionNEG regionNEG = new RegionNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lblId.Content.ToString());
                int pais = int.Parse(cbxPais.SelectedValue.ToString());
                string respuesta = regionNEG.ActualizarRegion(nombre, id,pais);
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
