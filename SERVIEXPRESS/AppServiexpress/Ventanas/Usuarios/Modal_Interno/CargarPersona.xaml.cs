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

namespace AppServiexpress.Ventanas.Usuarios.Modal_Interno
{
    /// <summary>
    /// Lógica de interacción para CargarPersona.xaml
    /// </summary>
    public partial class CargarPersona : Window
    {
        private ResgistroClientes_AD resgistroClientes_AD;
        private RegistroEmpleados_AD registroEmpleados_AD;
        private RegistroProveedores_AD registroProveedores_AD;
        private AdministrarUsuarios_AD administrarUsuarios_AD;
        private string registro;

        public CargarPersona()
        {
            InitializeComponent();
        }

        public CargarPersona(ResgistroClientes_AD resgistroClientes_AD,string registro)
        {
            InitializeComponent();
            this.resgistroClientes_AD = resgistroClientes_AD;
            this.registro = registro;
            CargarTablaPersonas();
        }
        public CargarPersona(RegistroEmpleados_AD registroEmpleados_AD, string registro)
        {
            InitializeComponent();
            this.registroEmpleados_AD = registroEmpleados_AD;
            this.registro = registro;
            CargarTablaPersonas();
        }
        public CargarPersona(RegistroProveedores_AD registroProveedores_AD, string registro)
        {
            InitializeComponent();
            this.registroProveedores_AD = registroProveedores_AD;
            this.registro = registro;
            CargarTablaPersonas();
        }
        public CargarPersona(AdministrarUsuarios_AD administrarUsuarios_AD, string registro)
        {
            InitializeComponent();
            this.administrarUsuarios_AD = administrarUsuarios_AD;
            this.registro = registro;
            CargarTablaPersonas();
        }
        public void CargarTablaPersonas()
        {
            dgPersonas.ItemsSource = null;
            DataTable dt = new DataTable();
            PersonaNEG personaNEG = new PersonaNEG();

            try
            {
                List<PersonaVIEW> lista = personaNEG.ListarTodosPersonas();
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("APELLIDO");
                dt.Columns.Add("RUT");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("COMUNA");
                dt.Columns.Add("TELEFONO CELULAR");
                dt.Columns.Add("TELEFONO FIJO");
                dt.Columns.Add("FECHA NACIMIENTO");
                dt.Columns.Add("ESTADO PERSONA");
                dt.Columns.Add("TIPO PERSONA");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.APELLIDO, x.NUM_ID + "-" + x.DIV_ID,
                            x.DIRECCION, x.COMUNA, x.TELEFONO_CELULAR, x.TELEFONO_FIJO,
                            x.FECHA_NACIMIENTO, x.ESTADO_PERSONA, x.TIPO_PERSONA);
                    }
                }
                dgPersonas.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }

        }

        
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tipo = cbxTipoBusqueda.Text;
                string valor = txtBusqueda.Text.ToUpper();

                dgPersonas.ItemsSource = null;
                DataTable dt = new DataTable();
                PersonaNEG personaNEG = new PersonaNEG();
                List<PersonaVIEW> lista = personaNEG.FiltrarPersona(tipo, valor);
                dt.Columns.Add("ID");
                dt.Columns.Add("NOMBRE");
                dt.Columns.Add("APELLIDO");
                dt.Columns.Add("RUT");
                dt.Columns.Add("DIRECCION");
                dt.Columns.Add("COMUNA");
                dt.Columns.Add("TELEFONO CELULAR");
                dt.Columns.Add("TELEFONO FIJO");
                dt.Columns.Add("FECHA NACIMIENTO");
                dt.Columns.Add("ESTADO PERSONA");
                dt.Columns.Add("TIPO PERSONA");
                if (lista.Count > 0)
                {
                    foreach (var x in lista)
                    {
                        dt.Rows.Add(x.ID, x.NOMBRE, x.APELLIDO, x.NUM_ID + "-" + x.DIV_ID,
                            x.DIRECCION, x.COMUNA, x.TELEFONO_CELULAR, x.TELEFONO_FIJO,
                            x.FECHA_NACIMIENTO, x.ESTADO_PERSONA, x.TIPO_PERSONA);
                    }
                }
                else
                {
                    MessageBox.Show("No existen personas registradas para los filtros indicados");
                }
                dgPersonas.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btn_Cargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgPersonas.SelectedItems.Count > 0)
                {
                    if (dgPersonas.SelectedItems.Count < 2)
                    {
                        DataTable tabla = ((DataView)dgPersonas.ItemsSource).ToTable();
                        for (int i = 0; i < dgPersonas.SelectedItems.Count; i++)
                        {
                            int indice = dgPersonas.Items.IndexOf(dgPersonas.SelectedItems[i]);
                            DataRow fila = tabla.Rows[indice];
                            int idPersona = int.Parse(fila.ItemArray[0].ToString());
                            if (registro == "cliente")
                            {
                                resgistroClientes_AD.CargarDatosPersona(idPersona);
                            }
                            else if(registro == "empleado")
                            {
                                registroEmpleados_AD.CargarDatosPersona(idPersona);
                            }
                            else if (registro == "proveedor")
                            {
                                registroProveedores_AD.CargarDatosPersona(idPersona);
                            }
                            else if (registro == "usuario")
                            {
                                administrarUsuarios_AD.CargarDatosPersona(idPersona);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar solo una fila");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar por lo menos una fila");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }           
        }

        private void btn_LimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CargarTablaPersonas();
        }
    }
}
