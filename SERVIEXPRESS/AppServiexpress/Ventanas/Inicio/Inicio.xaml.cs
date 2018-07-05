using AppServiexpress.Ventanas.Usuarios;
using AppServiexpress.Ventanas.Boletas;
using AppServiexpress.Ventanas.Productos;
using AppServiexpress.Ventanas.Pedidos;
using AppServiexpress.Ventanas.Requerimientos;
using AppServiexpress.Ventanas.Taller;
using System;
using System.Collections.Generic;
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
using AppServiexpress.Ventanas.Mantenedores;
using BBCServiexpress.NEG;

namespace AppServiexpress
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        private string usuario;

        public Inicio()
        {
            InitializeComponent();            
        }

        public Inicio(string usuario)
        {
            this.usuario = usuario;
            UsuarioNEG usuarioNeg = new UsuarioNEG();
            int tipo1 = usuarioNeg.ObtenerTipoUsuario(usuario);
            InitializeComponent();
            if(tipo1 == 1)
            {
                mMantenedores.IsEnabled=false;
                int tipo2 = usuarioNeg.ObtenerTipoEmpleado(usuario);
                if (tipo2==1)//RECEPCIONISTA
                {
                    iRegistroPersonas.IsEnabled = false;
                    iRegistroProveedor.IsEnabled = false;
                    mProductos.IsEnabled = false;
                    iAdministrarServicios.IsEnabled = false;
                    iAdministrarUsuario.IsEnabled = false;
                }
                else if (tipo2 == 2)//TECNICO
                {
                    mUsuarios.IsEnabled = false;
                    mBoletas.IsEnabled = false;
                    mProductos.IsEnabled = false;
                    iAdministrarServicios.IsEnabled = false;
                }
                else if (tipo2 == 3)//ADMINISTRADOR SUCURSAL
                {
                    iAdministrarUsuario.IsEnabled = false;
                }
            }
        }

        private void Ver_CategoriaProductos(object sender, RoutedEventArgs e)
        {
            CategoriaProductos_AD reg = new CategoriaProductos_AD();
            reg.ShowDialog();
        }

        private void Ver_Comuna(object sender, RoutedEventArgs e)
        {
            Comuna_AD reg = new Comuna_AD();
            reg.ShowDialog();
        }

        private void Ver_Empresas(object sender, RoutedEventArgs e)
        {
            Empresas_AD reg = new Empresas_AD();
            reg.ShowDialog();
        }

        private void Ver_MarcaProductos(object sender, RoutedEventArgs e)
        {
            MarcaProducto_AD reg = new MarcaProducto_AD();
            reg.ShowDialog();
        }

        private void Ver_Moneda(object sender, RoutedEventArgs e)
        {
            Moneda_AD reg = new Moneda_AD();
            reg.ShowDialog();
        }

        private void Ver_Pais(object sender, RoutedEventArgs e)
        {
            Pais_AD reg = new Pais_AD();
            reg.ShowDialog();
        }

        private void Ver_Provincia(object sender, RoutedEventArgs e)
        {
            Provincia reg = new Provincia();
            reg.ShowDialog();
        }

        private void Ver_Region(object sender, RoutedEventArgs e)
        {
            Region_AD reg = new Region_AD();
            reg.ShowDialog();
        }

        private void Ver_Sucursal(object sender, RoutedEventArgs e)
        {
            Sucursal_AD reg = new Sucursal_AD();
            reg.ShowDialog();
        }

        private void Ver_TipoProducto(object sender, RoutedEventArgs e)
        {
            TipoProducto_AD reg = new TipoProducto_AD();
            reg.ShowDialog();
        }

        private void Ver_MarcasVehiculos(object sender, RoutedEventArgs e)
        {
            MarcasVehiculos_AD reg = new MarcasVehiculos_AD();
            reg.ShowDialog();
        }
        private void Ver_AdministrarPersonas(object sender, RoutedEventArgs e)
        {
            AdministrarPersonas_AD reg = new AdministrarPersonas_AD();
            reg.ShowDialog();
        }
        private void Ver_TipodeServicio(object sender, RoutedEventArgs e)
        {
            TipodeServicio_AD reg = new TipodeServicio_AD();
            reg.ShowDialog();
        }

        private void Ver_RegistroClientes(object sender, RoutedEventArgs e)
        {
            ResgistroClientes_AD reg = new ResgistroClientes_AD();
            reg.ShowDialog();
        }
        private void Ver_RegistroEmpleados(object sender, RoutedEventArgs e)
        {
            RegistroEmpleados_AD reg = new RegistroEmpleados_AD();
            reg.ShowDialog();
        }
        private void Ver_RegistroProveedores(object sender, RoutedEventArgs e)
        {
            RegistroProveedores_AD reg = new RegistroProveedores_AD();
            reg.ShowDialog();
        }
        private void Ver_AdministradorUsuarios(object sender, RoutedEventArgs e)
        {
            AdministrarUsuarios_AD reg = new AdministrarUsuarios_AD();
            reg.ShowDialog();
        }

        private void Ver_RegistroVentas(object sender, RoutedEventArgs e)
        {
            RegistroVentas_AD reg = new RegistroVentas_AD();
            reg.ShowDialog();
        }
        private void Ver_EmitirDocumentos(object sender, RoutedEventArgs e)
        {
            EmitirBoleta_AD reg = new EmitirBoleta_AD();
            reg.ShowDialog();
        }
        private void Ver_EmitirDevolucion(object sender, RoutedEventArgs e)
        {
            EmitirDevolucion_AD reg = new EmitirDevolucion_AD();
            reg.ShowDialog();
        }
        private void Ver_EmitirPedido(object sender, RoutedEventArgs e)
        {
            EmitirPedido_AD reg = new EmitirPedido_AD();
            reg.ShowDialog();
        }
        private void Ver_IngresarRecepcion(object sender, RoutedEventArgs e)
        {
            IngresarRecepcion_AD reg = new IngresarRecepcion_AD();
            reg.ShowDialog();
        }
        private void Ver_ModificarPedido(object sender, RoutedEventArgs e)
        {
            ModificarPedido_AD reg = new ModificarPedido_AD();
            reg.ShowDialog();
        }
        private void Ver_IngresarProducto(object sender, RoutedEventArgs e)
        {
            IngresarProductos_AD reg = new IngresarProductos_AD();
            reg.ShowDialog();
        }
        private void Ver_ModificarProducto(object sender, RoutedEventArgs e)
        {
            ModificarProductos_AD reg = new ModificarProductos_AD();
            reg.ShowDialog();
        }
        private void Ver_FinalizarRequerimientos(object sender, RoutedEventArgs e)
        {
            FinalizarRequerimientos_AD reg = new FinalizarRequerimientos_AD();
            reg.ShowDialog();
        }
        private void Ver_IngresarRequerimientos(object sender, RoutedEventArgs e)
        {
            IngresarRequerimientos_AD reg = new IngresarRequerimientos_AD();
            reg.ShowDialog();
        }
        private void Ver_ModificarRequerimientos(object sender, RoutedEventArgs e)
        {
            ModificarRequerimientos_AD reg = new ModificarRequerimientos_AD();
            reg.ShowDialog();
        }
        private void Ver_AdministrarServicios(object sender, RoutedEventArgs e)
        {
            AdministrarServicios_AD reg = new AdministrarServicios_AD();
            reg.ShowDialog();
        }
        private void Ver_AdministrarVehiculos(object sender, RoutedEventArgs e)
        {
            AdministrarVehiculos_AD reg = new AdministrarVehiculos_AD();
            reg.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow inicio = new MainWindow();
                inicio.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
