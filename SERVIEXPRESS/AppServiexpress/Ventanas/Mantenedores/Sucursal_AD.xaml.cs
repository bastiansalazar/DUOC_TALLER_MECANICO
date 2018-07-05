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
    /// Lógica de interacción para Sucursal_AD.xaml
    /// </summary>
    public partial class Sucursal_AD : Window
    {
        public Sucursal_AD()
        {
            InitializeComponent();
            CargarTabla();
        }
        public void CargarTabla()
        {
            dgDatos.ItemsSource = null;
            DataTable dt = new DataTable();
            SucursalNEG sucursalNEG = new SucursalNEG();
            MultiEmpresaNEG multiEmpresaNEG = new MultiEmpresaNEG();
            Tipos_EstadosNEG tipos_EstadosNEG = new Tipos_EstadosNEG();

            try
            {
                List<SUCURSAL> lista = sucursalNEG.ListarTodasSucursales();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("NUMERO TEL");
                dt.Columns.Add("EMPRESA");
                dt.Columns.Add("ESTADO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.DIRECCION, x.NUMERO_TELEFONO, x.MULTI_EMPRESA.RAZON_SOCIAL,x.ESTADO_SUCURSAL.NOMBRE);
                    }
                }
                dgDatos.ItemsSource = dt.DefaultView;

                List<MULTI_EMPRESA> listaempresa = multiEmpresaNEG.ListarEmpresas();
                if (listaempresa.Count > 0)
                {
                    cbxEmpresa.ItemsSource = listaempresa;
                    cbxEmpresa.DisplayMemberPath = "RAZON_SOCIAL";
                    cbxEmpresa.SelectedValuePath = "ID";
                }

                List<ESTADO_SUCURSAL> listaestado = tipos_EstadosNEG.ListarESucursal();
                if (listaestado.Count > 0)
                {
                    cbxEstado.ItemsSource = listaestado;
                    cbxEstado.DisplayMemberPath = "NOMBRE";
                    cbxEstado.SelectedValuePath = "ID";
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
            SucursalNEG sucursalNEG = new SucursalNEG();
            var datos = sucursalNEG.CargarSucursal(id);
            txtNombre.Text = datos.NOMBRE;
            txtDireccion.Text = datos.DIRECCION;
            txtTelefono.Text = datos.NUMERO_TELEFONO.ToString();
            cbxEmpresa.SelectedValue = datos.MULTI_EMPRESA_ID;
            cbxEstado.SelectedValue = datos.ESTADO_SUCURSAL_ID;
            lblId.Content = datos.ID;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            lblId.Content = "";
            cbxEstado.SelectedValue = -1;
            cbxEmpresa.SelectedValue = -1;
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
                SucursalNEG sucursalNEG = new SucursalNEG();
                List<SUCURSAL> lista = sucursalNEG.FiltrarSucursal(valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("NUMERO TEL");
                dt.Columns.Add("EMPRESA");
                dt.Columns.Add("ESTADO");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.DIRECCION, x.NUMERO_TELEFONO, x.MULTI_EMPRESA.RAZON_SOCIAL, x.ESTADO_SUCURSAL.NOMBRE);
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
                SucursalNEG sucursalNEG = new SucursalNEG();
                string nombre = txtNombre.Text.ToUpper();
                string direccion = txtDireccion.Text.ToUpper();
                int telefono = int.Parse(txtTelefono.Text);
                int empresa = int.Parse(cbxEmpresa.SelectedValue.ToString());
                int estado = int.Parse(cbxEstado.SelectedValue.ToString());
                string respuesta = sucursalNEG.CrearSucursal(nombre, direccion,telefono,estado,empresa);
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
                SucursalNEG sucursalNEG = new SucursalNEG();
                int id = int.Parse(lblId.Content.ToString());
                string nombre = txtNombre.Text.ToUpper();
                string direccion = txtDireccion.Text.ToUpper();
                int telefono = int.Parse(txtTelefono.Text);
                int empresa = int.Parse(cbxEmpresa.SelectedValue.ToString());
                int estado = int.Parse(cbxEstado.SelectedValue.ToString());
                string respuesta = sucursalNEG.ActualizarSucursal(nombre,direccion,telefono,estado,empresa,id);
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
