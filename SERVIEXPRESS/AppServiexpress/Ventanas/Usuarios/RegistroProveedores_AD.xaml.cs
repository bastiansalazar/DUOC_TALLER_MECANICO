using AppServiexpress.Ventanas.Usuarios.Manual;
using AppServiexpress.Ventanas.Usuarios.Modal_Interno;
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

namespace AppServiexpress.Ventanas.Usuarios
{
    /// <summary>
    /// Lógica de interacción para RegistroProveedores_AD.xaml
    /// </summary>
    public partial class RegistroProveedores_AD : Window
    {
        public RegistroProveedores_AD()
        {
            InitializeComponent();
            this.Activate();
            dpkFechaNac.DisplayDateStart = new DateTime(1900, 01, 01);
            dpkFechaNac.DisplayDateEnd = DateTime.Now;
            CargarTablaProveedores();
            CargarCombos();
        }

        public void CargarTablaProveedores()
        {
            dgProveedores.ItemsSource = null;
            DataTable dt = new DataTable();
            ProveedoresNEG proveedoresNEG = new ProveedoresNEG();

            try
            {
                List<ProveedoresVIEW> lista = proveedoresNEG.ListarTodosProveedores();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE_EMPRESA");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("APELLIDO");
                dt.Columns.Add("NUM_ID");
                dt.Columns.Add("DIV_ID");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("COMUNA");
                dt.Columns.Add("TELEFONO_CELULAR");
                dt.Columns.Add("TELEFONO_FIJO");
                dt.Columns.Add("ESTADO_PERSONA");
                dt.Columns.Add("TIPO_PERSONA");
                dt.Columns.Add("ESTADO_PROVEEDOR");
                dt.Columns.Add("TIPO_PROVEEDOR");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID,x.NOMBRE_EMPRESA, x.NOMBRE, x.APELLIDO, x.NUM_ID, x.DIV_ID, x.DIRECCION, x.COMUNA, x.TELEFONO_CELULAR, x.TELEFONO_FIJO, x.ESTADO_PERSONA, x.TIPO_PERSONA, x.ESTADO_PROVEEDOR, x.TIPO_PROVEEDOR);
                    }
                }
                dgProveedores.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        public void CargarCombos()
        {
            PaisNEG paisNEG = new PaisNEG();
            Tipos_EstadosNEG tiposNEG = new Tipos_EstadosNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();
            cbxRegion.IsEnabled = false;
            cbxProvincia.IsEnabled = false;
            cbxComuna.IsEnabled = false;

            try
            {
                List<PAIS> listaPaises = paisNEG.ListarPaises();
                if (listaPaises.Count > 0)
                {
                    cbxPais.ItemsSource = listaPaises;
                    cbxPais.DisplayMemberPath = "NOMBRE";
                    cbxPais.SelectedValuePath = "ID";
                }

                List<TIPO_PERSONA> listaTPersonas = tiposNEG.ListarTPersonas();
                if (listaTPersonas.Count > 0)
                {
                    cbxTipoPersona.ItemsSource = listaTPersonas;
                    cbxTipoPersona.DisplayMemberPath = "NOMBRE";
                    cbxTipoPersona.SelectedValuePath = "ID";
                }

                List<ESTADO_PERSONA> listaEPersonas = tiposNEG.ListarEPersonas();
                if (listaEPersonas.Count > 0)
                {
                    cbxEstadoPersona.ItemsSource = listaEPersonas;
                    cbxEstadoPersona.DisplayMemberPath = "NOMBRE";
                    cbxEstadoPersona.SelectedValuePath = "ID";
                }

                List<TIPO_PROVEEDOR> listaTEmpleados = tiposNEG.ListarTProveedores();
                if (listaTEmpleados.Count > 0)
                {
                    cbxTipoProveedor.ItemsSource = listaTEmpleados;
                    cbxTipoProveedor.DisplayMemberPath = "NOMBRE";
                    cbxTipoProveedor.SelectedValuePath = "ID";
                }

                List<ESTADO_PROVEEDOR> listaEEmpleados = tiposNEG.ListarEProveedor();
                if (listaEEmpleados.Count > 0)
                {
                    cbxEstadoProveedor.ItemsSource = listaEEmpleados;
                    cbxEstadoProveedor.DisplayMemberPath = "NOMBRE";
                    cbxEstadoProveedor.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        private void cbxPais_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPais.SelectedValue != null)
            {
                try
                {
                    int pais = int.Parse(cbxPais.SelectedValue.ToString());
                    RegionNEG regionNEG = new RegionNEG();
                    List<REGION> listaRegion = regionNEG.ListarRegiones(pais);
                    if (listaRegion.Count > 0)
                    {
                        cbxRegion.ItemsSource = listaRegion;
                        cbxRegion.DisplayMemberPath = "NOMBRE";
                        cbxRegion.SelectedValuePath = "ID";
                    }
                    cbxRegion.IsEnabled = true;
                    cbxProvincia.SelectedIndex = -1;
                    cbxProvincia.IsEnabled = false;
                    cbxComuna.SelectedIndex = -1;
                    cbxComuna.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }
            }
        }

        private void cbxRegion_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxRegion.SelectedValue != null)
            {
                try
                {
                    int region = int.Parse(cbxRegion.SelectedValue.ToString());
                    ProvinciaNEG provinciaNEG = new ProvinciaNEG();
                    List<PROVINCIA> listaProvincia = provinciaNEG.ListarProvincias(region);
                    if (listaProvincia.Count > 0)
                    {
                        cbxProvincia.ItemsSource = listaProvincia;
                        cbxProvincia.DisplayMemberPath = "NOMBRE";
                        cbxProvincia.SelectedValuePath = "ID";
                    }
                    cbxProvincia.IsEnabled = true;
                    cbxComuna.SelectedIndex = -1;
                    cbxComuna.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }

            }

        }

        internal void CargarDatosPersona(int idPersona)
        {
            PersonaNEG personaNEG = new PersonaNEG();
            var datos = personaNEG.CargarPersona(idPersona);
            txtNombre.Text = datos.NOMBRE;
            txtApellido.Text = datos.APELLIDO;
            txtRut.Text = datos.NUM_ID.ToString() + "-" + datos.DIV_ID;
            txtDireccion.Text = datos.DIRECCION;
            txtTelFijo.Text = datos.TELEFONO_FIJO.ToString();
            txtTelCelular.Text = datos.TELEFONO_CELULAR.ToString();
            cbxEstadoPersona.SelectedValue = datos.IdEstadoPersona;
            cbxTipoPersona.SelectedValue = datos.IdTipoPersona;
            dpkFechaNac.SelectedDate = datos.FECHA_NACIMIENTO;
            txtEmail.Text = datos.CORREO;
            cbxPais.SelectedValue = datos.IdPais;
            RegionNEG regionNEG = new RegionNEG();
            List<REGION> listaRegion = regionNEG.ListarRegiones(datos.IdPais);
            if (listaRegion.Count > 0)
            {
                cbxRegion.ItemsSource = listaRegion;
                cbxRegion.DisplayMemberPath = "NOMBRE";
                cbxRegion.SelectedValuePath = "ID";
            }
            cbxRegion.IsEnabled = true;
            cbxRegion.SelectedValue = datos.IdRegion;
            ProvinciaNEG provinciaNEG = new ProvinciaNEG();
            List<PROVINCIA> listaProvincia = provinciaNEG.ListarProvincias(datos.IdRegion);
            if (listaProvincia.Count > 0)
            {
                cbxProvincia.ItemsSource = listaProvincia;
                cbxProvincia.DisplayMemberPath = "NOMBRE";
                cbxProvincia.SelectedValuePath = "ID";
            }
            cbxProvincia.IsEnabled = true;
            cbxProvincia.SelectedValue = datos.IdProvincia;
            ComunaNEG comunaNEG = new ComunaNEG();
            List<COMUNA> listaComuna = comunaNEG.ListarComunas(datos.IdProvincia);
            if (listaComuna.Count > 0)
            {
                cbxComuna.ItemsSource = listaComuna;
                cbxComuna.DisplayMemberPath = "NOMBRE";
                cbxComuna.SelectedValuePath = "ID";
            }
            cbxComuna.SelectedValue = datos.IdComuna;
            cbxComuna.IsEnabled = true;
        }

        private void cbxProvincia_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxProvincia.SelectedValue != null)
            {
                try
                {
                    int provincia = int.Parse(cbxProvincia.SelectedValue.ToString());
                    ComunaNEG comunaNEG = new ComunaNEG();
                    List<COMUNA> listaComuna = comunaNEG.ListarComunas(provincia);
                    if (listaComuna.Count > 0)
                    {
                        cbxComuna.ItemsSource = listaComuna;
                        cbxComuna.DisplayMemberPath = "NOMBRE";
                        cbxComuna.SelectedValuePath = "ID";
                    }
                    cbxComuna.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }

            }

        }

        private void dgProveedores_MouseDoubleClick(object sender, EventArgs e)
        {
            DataRowView dr = dgProveedores.SelectedItem as DataRowView;
            DataRow dr1 = dr.Row;
            int idProveedor = Convert.ToInt32(dr1.ItemArray[0]);
            ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
            var datos = proveedoresNEG.CargarProveedor(idProveedor);
            txtNombreEmpresa.Text = datos.NOMBRE_EMPRESA;
            txtNombre.Text = datos.NOMBRE;
            txtApellido.Text = datos.APELLIDO;
            txtRut.Text = datos.NUM_ID.ToString() + "-" + datos.DIV_ID;
            txtDireccion.Text = datos.DIRECCION;
            txtTelFijo.Text = datos.TELEFONO_FIJO.ToString();
            txtTelCelular.Text = datos.TELEFONO_CELULAR.ToString();
            cbxEstadoProveedor.SelectedValue = datos.IdEstadoProveedor;
            cbxTipoProveedor.SelectedValue = datos.IdTipoProveedor;
            cbxEstadoPersona.SelectedValue = datos.IdEstadoPersona;
            cbxTipoPersona.SelectedValue = datos.IdTipoPersona;
            dpkFechaNac.SelectedDate = datos.FECHA_NACIMIENTO;
            txtEmail.Text = datos.CORREO;
            cbxPais.SelectedValue = datos.IdPais;
            RegionNEG regionNEG = new RegionNEG();
            List<REGION> listaRegion = regionNEG.ListarRegiones(datos.IdPais);
            if (listaRegion.Count > 0)
            {
                cbxRegion.ItemsSource = listaRegion;
                cbxRegion.DisplayMemberPath = "NOMBRE";
                cbxRegion.SelectedValuePath = "ID";
            }
            cbxRegion.IsEnabled = true;
            cbxRegion.SelectedValue = datos.IdRegion;
            ProvinciaNEG provinciaNEG = new ProvinciaNEG();
            List<PROVINCIA> listaProvincia = provinciaNEG.ListarProvincias(datos.IdRegion);
            if (listaProvincia.Count > 0)
            {
                cbxProvincia.ItemsSource = listaProvincia;
                cbxProvincia.DisplayMemberPath = "NOMBRE";
                cbxProvincia.SelectedValuePath = "ID";
            }
            cbxProvincia.IsEnabled = true;
            cbxProvincia.SelectedValue = datos.IdProvincia;
            ComunaNEG comunaNEG = new ComunaNEG();
            List<COMUNA> listaComuna = comunaNEG.ListarComunas(datos.IdProvincia);
            if (listaComuna.Count > 0)
            {
                cbxComuna.ItemsSource = listaComuna;
                cbxComuna.DisplayMemberPath = "NOMBRE";
                cbxComuna.SelectedValuePath = "ID";
            }
            cbxComuna.SelectedValue = datos.IdComuna;
            cbxComuna.IsEnabled = true;
        }

        public void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtNombreEmpresa.Text = "";
            txtApellido.Text = "";
            txtRut.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            dpkFechaNac.SelectedDate = DateTime.Now;
            cbxPais.SelectedIndex = -1;
            cbxRegion.SelectedIndex = -1;
            cbxRegion.IsEnabled = false;
            cbxProvincia.SelectedIndex = -1;
            cbxProvincia.IsEnabled = false;
            cbxComuna.SelectedIndex = -1;
            cbxComuna.IsEnabled = false;
            txtTelFijo.Text = "";
            txtTelCelular.Text = "";
            txtBusqueda.Text = "";
            cbxEstadoPersona.SelectedIndex = -1;
            cbxTipoProveedor.SelectedIndex = -1;
            cbxEstadoProveedor.SelectedIndex = -1;
            cbxTipoPersona.SelectedIndex = -1;
            cbxTipoBusqueda.SelectedIndex = -1;
            CargarTablaProveedores();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgProveedores.ItemsSource = null;
                DataTable dt = new DataTable();
                ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
                List<ProveedoresVIEW> lista = proveedoresNEG.FiltrarProveedores(tipo, valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE_EMPRESA");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("APELLIDO");
                dt.Columns.Add("NUM_ID");
                dt.Columns.Add("DIV_ID");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("COMUNA");
                dt.Columns.Add("TELEFONO_CELULAR");
                dt.Columns.Add("TELEFONO_FIJO");
                dt.Columns.Add("ESTADO_PERSONA");
                dt.Columns.Add("TIPO_PERSONA");
                dt.Columns.Add("ESTADO_PROVEEDOR");
                dt.Columns.Add("TIPO_PROVEEDOR");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID,x.NOMBRE_EMPRESA, x.NOMBRE, x.APELLIDO, x.NUM_ID, x.DIV_ID, x.DIRECCION, x.COMUNA, x.TELEFONO_CELULAR, x.TELEFONO_FIJO, x.ESTADO_PERSONA, x.TIPO_PERSONA, x.ESTADO_PROVEEDOR, x.TIPO_PROVEEDOR);
                    }
                }
                else
                {
                    MessageBox.Show("No existen proveedores registrados para los filtros indicados");
                }
                dgProveedores.ItemsSource = dt.DefaultView;


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
                ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
                string nombre = txtNombre.Text.ToUpper();
                string nombreEmpresa = txtNombreEmpresa.Text.ToUpper();
                string apellido = txtApellido.Text.ToUpper();
                string rut = txtRut.Text.ToUpper();
                DateTime fecha_nac = default(DateTime);
                if (dpkFechaNac.SelectedDate != null)
                {
                    fecha_nac = DateTime.Parse(dpkFechaNac.SelectedDate.ToString());
                }
                string direccion = txtDireccion.Text.ToUpper();
                string email = txtEmail.Text;
                int comuna = int.Parse(cbxComuna.SelectedValue.ToString());
                string telefono_fijo = txtTelFijo.Text;
                string celular = txtTelCelular.Text;
                int tipo_persona = int.Parse(cbxTipoPersona.SelectedValue.ToString());
                int estado_persona = int.Parse(cbxEstadoPersona.SelectedValue.ToString());
                int tipo_proveedor = int.Parse(cbxTipoProveedor.SelectedValue.ToString());
                int estado_proveedor = int.Parse(cbxEstadoProveedor.SelectedValue.ToString());

                string respuesta = proveedoresNEG.CrearProveedor(nombreEmpresa, nombre, apellido, rut, fecha_nac, direccion, email, comuna, telefono_fijo, celular, tipo_persona, estado_persona, tipo_proveedor, estado_proveedor);
                if (respuesta == "creado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El proveedor ha sido ingresado satisfactoriamente a la base de datos");
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
                ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
                string nombreEmpresa = txtNombreEmpresa.Text.ToUpper();
                string nombre = txtNombre.Text.ToUpper();
                string apellido = txtApellido.Text.ToUpper();
                string rut = txtRut.Text.ToUpper();
                DateTime fecha_nac = default(DateTime);
                if (dpkFechaNac.SelectedDate != null)
                {
                    fecha_nac = DateTime.Parse(dpkFechaNac.SelectedDate.ToString());
                }
                string direccion = txtDireccion.Text.ToUpper();
                string email = txtEmail.Text;
                int comuna = int.Parse(cbxComuna.SelectedValue.ToString());
                string telefono_fijo = txtTelFijo.Text;
                string celular = txtTelCelular.Text;
                int tipo_persona = int.Parse(cbxTipoPersona.SelectedValue.ToString());
                int estado_persona = int.Parse(cbxEstadoPersona.SelectedValue.ToString());
                int tipo_proveedor = int.Parse(cbxTipoProveedor.SelectedValue.ToString());
                int estado_proveedor = int.Parse(cbxEstadoProveedor.SelectedValue.ToString());
                string respuesta = proveedoresNEG.ActualizarProveedor(nombreEmpresa,nombre, apellido, rut, fecha_nac, direccion, email, comuna, telefono_fijo, celular, tipo_persona, estado_persona, tipo_proveedor, estado_proveedor);
                if (respuesta == "actualizado")
                {
                    LimpiarFormulario();
                    MessageBox.Show("El proveedor ha sido actualizado satisfactoriamente");
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
            CargarTablaProveedores();
        }

        private void btnBuscaPersona_Click(object sender, RoutedEventArgs e)
        {
            CargarPersona reg = new CargarPersona(this, "proveedor");
            reg.ShowDialog();
        }

        private void bntAyuda_Click(object sender, RoutedEventArgs e)
        {
            AYUDA_RegistroProveedores reg = new AYUDA_RegistroProveedores();
            reg.Show();
        }
    }
}
