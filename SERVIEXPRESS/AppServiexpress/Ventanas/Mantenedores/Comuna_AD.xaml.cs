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
    /// Lógica de interacción para Comuna_AD.xaml
    /// </summary>
    public partial class Comuna_AD : Window
    {
        public Comuna_AD()
        {
            InitializeComponent();
            CargarTabla();
        }
        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            ProvinciaNEG provinciaNEG = new ProvinciaNEG();
            ComunaNEG comunaNEG = new ComunaNEG();

            try
            {
                List<COMUNA> lista = comunaNEG.ListarTodasComunas();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("PROVINCIA");
                dt.Columns.Add("FECHA CREACION");
                dt.Columns.Add("FECHA ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.PROVINCIA.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

                List<PROVINCIA> listaProvincia = provinciaNEG.ListarTodasProvincias();
                if (listaProvincia.Count > 0)
                {
                    cbxProvincia.ItemsSource = listaProvincia;
                    cbxProvincia.DisplayMemberPath = "NOMBRE";
                    cbxProvincia.SelectedValuePath = "ID";
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
            ComunaNEG comunaNEG = new ComunaNEG();
            var datos = comunaNEG.CargarComuna(id);
            txtNombre.Text = datos.NOMBRE;
            cbxProvincia.SelectedValue = datos.PROVINCIA_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lblId.Content = "";
            cbxProvincia.SelectedValue = -1;
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
                ComunaNEG comunaNEG = new ComunaNEG();
                List<COMUNA> lista = comunaNEG.FiltrarComuna(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("PROVINCIA");
                dt.Columns.Add("FECHA CREACION");
                dt.Columns.Add("FECHA ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.PROVINCIA.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
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
                ComunaNEG comunaNEG = new ComunaNEG();
                string nombre = txtNombre.Text.ToUpper();
                int provincia = int.Parse(cbxProvincia.SelectedValue.ToString());
                string respuesta = comunaNEG.CrearComuna(nombre, provincia);
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
                ComunaNEG comunaNEG = new ComunaNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lblId.Content.ToString());
                int provincia = int.Parse(cbxProvincia.SelectedValue.ToString());
                string respuesta = comunaNEG.ActualizarComuna(nombre, id, provincia);
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
