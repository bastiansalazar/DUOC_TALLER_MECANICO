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
    /// Lógica de interacción para Empresas_AD.xaml
    /// </summary>
    public partial class Empresas_AD : Window
    {
        public Empresas_AD()
        {
            InitializeComponent();
            CargarTabla();
        }

        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();

            Tipos_EstadosNEG tiposNEG = new Tipos_EstadosNEG();

            try
            {
                List<MULTI_EMPRESA> lista = multiEmpresaNEG.ListarEmpresas();
                dt.Columns.Add("ID");
                dt.Columns.Add("ROL");
                dt.Columns.Add("RAZON SOCIAL");
                dt.Columns.Add("DIRECCIÓN");
                dt.Columns.Add("TELÉFONO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.RUT, x.RAZON_SOCIAL, x.DIRECCION,x.NUMERO_TELEFONO);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

                List<ESTADO_EMPRESA> listaEEmpresa = tiposNEG.ListasEEmpresa();
                if (listaEEmpresa.Count > 0)
                {
                    cbxEstadoEmpresa.ItemsSource = listaEEmpresa;
                    cbxEstadoEmpresa.DisplayMemberPath = "NOMBRE";
                    cbxEstadoEmpresa.SelectedValuePath = "ID";
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
            MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();
            var datos = multiEmpresaNEG.CargarEmpresa(id);
            txtDireccion.Text = datos.DIRECCION;
            txtRazonSocial.Text = datos.RAZON_SOCIAL;
            txtRut.Text = datos.RUT;
            txtTelefono.Text = datos.NUMERO_TELEFONO.ToString();
            cbxEstadoEmpresa.SelectedValue = datos.ESTADO_EMPRESA_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtDireccion.Text = "";
            txtRazonSocial.Text = "";
            txtRut.Text = "";
            txtTelefono.Text = "";
            cbxEstadoEmpresa.SelectedValue = -1;
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
                MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();
                List<MULTI_EMPRESA> lista = multiEmpresaNEG.FiltrarEmpresa(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("ROL");
                dt.Columns.Add("RAZON SOCIAL");
                dt.Columns.Add("DIRECCIÓN");
                dt.Columns.Add("TELÉFONO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.RUT, x.RAZON_SOCIAL, x.DIRECCION, x.NUMERO_TELEFONO);
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
                MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();
                string razon = txtRazonSocial.Text.ToUpper();
                string direccion = txtDireccion.Text.ToUpper();
                int telefono = int.Parse(txtTelefono.Text);
                string rut = txtRut.Text.ToUpper();
                int estado = int.Parse(cbxEstadoEmpresa.SelectedValue.ToString());
                string respuesta = multiEmpresaNEG.CrearEmpresa(razon,direccion,telefono,rut,estado);
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
                MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();
                string direccion = txtDireccion.Text.ToUpper();
                int telefono = int.Parse(txtTelefono.Text);
                int estado = int.Parse(cbxEstadoEmpresa.SelectedValue.ToString());
                int id = int.Parse(lblId.Content.ToString());
                string respuesta = multiEmpresaNEG.ActualizarEmpresa(id,direccion,telefono,estado);
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
