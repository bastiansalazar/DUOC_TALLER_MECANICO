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
    /// Lógica de interacción para TipodeServicio_AD.xaml
    /// </summary>
    public partial class TipodeServicio_AD : Window
    {
        public TipodeServicio_AD()
        {
            InitializeComponent();
            CargarTablaTipoServicio();
            this.Activate();
        }

        public void CargarTablaTipoServicio()
        {
            dgTipoServicios.ItemsSource = null;
            DataTable dt = new DataTable();
            Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();

            try
            {
                List<TIPO_SERVICIO> lista = tipos_EstadosNEG.ListarTServicios();
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
                dgTipoServicios.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }
        private void dgTiposServicios_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgTipoServicios.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idTipoServicio = Convert.ToInt32(dr1.ItemArray[0]);
            TipoServicioNEG tipoServicioNEG = new TipoServicioNEG();
            var datos = tipoServicioNEG.CargarTipoServicio(idTipoServicio);
            txtNombre.Text = datos.NOMBRE;
            lbl_IdTipoServicio.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            lbl_IdTipoServicio.Content = "";
            CargarTablaTipoServicio();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgTipoServicios.ItemsSource = null;
                DataTable dt = new DataTable();
                TipoServicioNEG tipoServicioNEG = new TipoServicioNEG();
                List<TIPO_SERVICIO> lista = tipoServicioNEG.FiltrarTipoServicios(valor);
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
                    MessageBox.Show("No existen tipo de servicios registrados para los filtros indicados");
                }
                dgTipoServicios.ItemsSource = dt.DefaultView;

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
                TipoServicioNEG tipoServicioNEG = new TipoServicioNEG();
                string nombre = txtNombre.Text.ToUpper();
                string respuesta = tipoServicioNEG.CrearTipoServicio(nombre);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El tipo de servicio ha sido ingresado satisfactoriamente a la base de datos");
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
                TipoServicioNEG tipoServicioNEG = new TipoServicioNEG();
                string nombre = txtNombre.Text.ToUpper();
                int id = int.Parse(lbl_IdTipoServicio.Content.ToString());
                string respuesta = tipoServicioNEG.ActualizarServicio(nombre, id);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El tipo de servicio ha sido actualizado satisfactoriamente");
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
            CargarTablaTipoServicio();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_Mantenedores reg = new AYUDA_Mantenedores();
            reg.Show();
        }
    }//
}
