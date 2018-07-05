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

namespace AppServiexpress.Ventanas.Boletas.Modal_Interno
{
    /// <summary>
    /// Lógica de interacción para AgregarSER.xaml
    /// </summary>
    public partial class AgregarSER : Window
    {
        private int sucursal;
        private EmitirBoleta_AD emitirBoleta_AD;

        public AgregarSER()
        {
            InitializeComponent();
        }

        public AgregarSER(int sucursal, EmitirBoleta_AD emitirBoleta_AD)
        {
            InitializeComponent();
            this.sucursal = sucursal;
            this.emitirBoleta_AD = emitirBoleta_AD;
            CargarCombos();
        }
        private void CargarCombos()
        {
            ServiciosNEG serviciosNEG = new ServiciosNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();
            try
            {
                txtNombreSucursal.Text = sucursalNEG.CargarSucursal(sucursal).NOMBRE;

                List<ServiciosVIEW> listaServicio = serviciosNEG.FiltrarServicios("ID SUCURSAL", sucursal.ToString());
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
                    string _cantidad = "1";
                    string _tipoItem = "SER";
                    string _idItem = cbxSerivioc.SelectedValue.ToString();
                    string _nombreItem = datos.TIPO_SERVICIO;
                    string _precioUni = datos.COSTO.ToString();
                    string _total = _precioUni;
                    emitirBoleta_AD.AgregarItemDetalle(_cantidad,_tipoItem,_idItem,_nombreItem,_precioUni,_total);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un servicio");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
