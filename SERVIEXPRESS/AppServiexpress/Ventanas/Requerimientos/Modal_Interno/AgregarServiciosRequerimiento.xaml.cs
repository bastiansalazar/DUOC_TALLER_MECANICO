using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using BBCServiexpress.NEG;
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

namespace AppServiexpress.Ventanas.Requerimientos.Modal_Interno
{
    /// <summary>
    /// Lógica de interacción para AgregarServiciosRequerimiento.xaml
    /// </summary>
    public partial class AgregarServiciosRequerimiento : Window
    {
        private int sucursal;
        private IngresarRequerimientos_AD ingresarRequerimientos_AD;

        public AgregarServiciosRequerimiento()
        {
            InitializeComponent();
        }

        public AgregarServiciosRequerimiento(int sucursal, IngresarRequerimientos_AD ingresarRequerimientos_AD)
        {
            InitializeComponent();
            this.sucursal = sucursal;
            this.ingresarRequerimientos_AD = ingresarRequerimientos_AD;
            CargarCombos();
        }

        private void CargarCombos()
        {
            ServiciosNEG serviciosNEG = new ServiciosNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();
            try
            {
                txtNombreSucursal.Text = sucursalNEG.CargarSucursal(sucursal).NOMBRE;

                List<ServiciosVIEW> listaServicio = serviciosNEG.FiltrarServicios("ID SUCURSAL",sucursal.ToString());
                if (listaServicio.Count > 0)
                {
                    cbxSerivioc.ItemsSource = listaServicio;
                    cbxSerivioc.DisplayMemberPath = "TIPO_SERVICIO";
                    cbxSerivioc.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }
               

        private void btnAgregarServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxSerivioc.SelectedValue != null)
                {
                    int _idServicio = int.Parse(cbxSerivioc.SelectedValue.ToString());
                    ServiciosNEG serviciosNEG = new ServiciosNEG();
                    var datos = serviciosNEG.CargarServicio(_idServicio);
                    ingresarRequerimientos_AD.AgregarItemTablaServicios(_idServicio, datos.TIPO_SERVICIO,datos.COSTO);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un servicio");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Limpiar()
        {
            txtValor.Text = "";
            cbxSerivioc.SelectedIndex = -1;
        }

        private void cbxSerivioc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSerivioc.SelectedValue != null)
            {
                try
                {
                    int idServicio = int.Parse(cbxSerivioc.SelectedValue.ToString());
                    ServiciosNEG serviciosNEG = new ServiciosNEG();
                    int valor = int.Parse(serviciosNEG.CargarServicio(idServicio).COSTO.ToString());
                    txtValor.Text = valor.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }

            }
        }
    }
}
