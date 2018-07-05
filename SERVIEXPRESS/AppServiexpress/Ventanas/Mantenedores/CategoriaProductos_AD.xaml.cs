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
    /// Lógica de interacción para CategoriaProductos_AD.xaml
    /// </summary>
    public partial class CategoriaProductos_AD : Window
    {
        public CategoriaProductos_AD()
        {
            InitializeComponent();
            CargarTabla();
        }

        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            CategoriaNEG categoriaNEG = new CategoriaNEG();

            try
            {
                List<CATEGORIA> lista = categoriaNEG.ListarCategorias();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

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
            CategoriaNEG categoriaNEG = new CategoriaNEG();
            var datos = categoriaNEG.CargarCategoria(id);
            txtNombre.Text = datos.NOMBRE;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lblId.Content = "";
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
                CategoriaNEG categoriaNEG = new CategoriaNEG();
                List<CATEGORIA> lista = categoriaNEG.FiltrarCategoria(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("FECHA_CREACION");
                dt.Columns.Add("FECHA_ACTUALIZACION");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.FECHA_CREACION, x.FECHA_ULTIMO_UPDATE);
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
                CategoriaNEG categoriaNEG = new CategoriaNEG();
                string nombre = txtNombre.Text.ToUpper();
                string respuesta = categoriaNEG.CrearCategoria(nombre);
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
                CategoriaNEG categoriaNEG = new CategoriaNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lblId.Content.ToString());
                string respuesta = categoriaNEG.ActualizarCategoria(nombre, id);
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
