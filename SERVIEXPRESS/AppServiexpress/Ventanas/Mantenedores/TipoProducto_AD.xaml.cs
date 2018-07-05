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
    /// Lógica de interacción para TipoProducto_AD.xaml
    /// </summary>
    public partial class TipoProducto_AD : Window
    {
        public TipoProducto_AD()
        {
            InitializeComponent();
            CargarTabla();
        }
        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();

            CategoriaNEG categoriaNEG = new CategoriaNEG();

            try
            {
                List<TIPO_PRODUCTO> lista = tipoProductoNEG.ListarTProductos();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("COD CATEGORIA");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.CATEGORIA_ID);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

                List<CATEGORIA> listaCategoria = categoriaNEG.ListarCategorias();
                if (listaCategoria.Count > 0)
                {
                    cbxCategoria.ItemsSource = listaCategoria;
                    cbxCategoria.DisplayMemberPath = "NOMBRE";
                    cbxCategoria.SelectedValuePath = "ID";
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
            TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
            var datos = tipoProductoNEG.CargarTipoProducto(id);
            txtNombre.Text = datos.NOMBRE;
            cbxCategoria.SelectedValue = datos.CATEGORIA_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            cbxCategoria.SelectedValue = -1;
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
                TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
                List<TIPO_PRODUCTO> lista = tipoProductoNEG.FiltrarTipoProducto(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("COD CATEGORIA");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.CATEGORIA_ID);
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
                TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
                string nombre = txtNombre.Text.ToUpper();
                int categoria = int.Parse(cbxCategoria.SelectedValue.ToString());
                string respuesta = tipoProductoNEG.CrearTipoProducto(nombre,categoria);
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
                TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
                string nombre = txtNombre.Text.ToUpper();
                int categoria = int.Parse(cbxCategoria.SelectedValue.ToString());
                int id = int.Parse(lblId.Content.ToString());
                string respuesta = tipoProductoNEG.ActualizarTipoProducto(id, nombre,categoria);
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
