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

namespace AppServiexpress.Ventanas.Boletas.Modal_Interno
{
    /// <summary>
    /// Lógica de interacción para AgregarOT.xaml
    /// </summary>
    public partial class AgregarOT : Window
    {
        private int sucursal;
        private int cliente;
        private EmitirBoleta_AD emitirBoleta_AD;
        private DataTable tablaDeta;

        public AgregarOT()
        {
            InitializeComponent();
        }

        public AgregarOT(int sucursal, int cliente, EmitirBoleta_AD emitirBoleta_AD, DataTable tablaDeta)
        {
            InitializeComponent();
            this.sucursal = sucursal;
            this.cliente = cliente;
            this.emitirBoleta_AD = emitirBoleta_AD;
            this.tablaDeta = tablaDeta;
            CargarCombos();
        }

        private void CargarCombos()
        {
            ClientesNEG clientesNEG = new ClientesNEG();
            SucursalNEG sucursalNEG = new SucursalNEG();
            ReservaNEG reservaNEG = new ReservaNEG();
            try
            {
                txtNombreSucursal.Text = sucursalNEG.CargarSucursal(sucursal).NOMBRE;
                txtNombreCliente.Text = clientesNEG.CargarCliente(cliente).NOMBRE+" "+clientesNEG.CargarCliente(cliente).APELLIDO;

                List<ReservaVIEW> listaServicio = reservaNEG.FiltrarRequerimientos3("CLIENTES",sucursal, cliente.ToString());
                if (listaServicio.Count > 0)
                {
                    cbxOrdenTrabajo.ItemsSource = listaServicio;
                    cbxOrdenTrabajo.DisplayMemberPath = "ID";
                    cbxOrdenTrabajo.SelectedValuePath = "ID";
                }
                else
                {
                    cbxOrdenTrabajo.ItemsSource = null;
                    MessageBox.Show("No existen Orden de Trabajo perdientes en pago para este cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void btnAgregarProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxOrdenTrabajo.SelectedValue != null)
                {
                    if (tablaDeta.Rows.Count>0)
                    {
                        foreach (DataRow x in tablaDeta.Rows)
                        {

                            string tipo = x.ItemArray[1].ToString();

                            int idOrdenTrabajo = int.Parse(cbxOrdenTrabajo.SelectedValue.ToString());
                            string folio = idOrdenTrabajo.ToString();
                            for (int i = 0; i < 9; i++)
                            {
                                if (folio.Length < 8)
                                    folio = "0" + folio;
                            }
                            string _cantidad = "1";
                            string _tipoItem = "OT";
                            string _idItem = cbxOrdenTrabajo.SelectedValue.ToString();
                            string _nombreItem = "ORDEN DE TRABAJO N°" + folio;
                            ReservaNEG reservaNEG = new ReservaNEG();
                            string _total = reservaNEG.CargarReserva(idOrdenTrabajo).TOTAL.ToString();
                            string _precioUni = _total;
                            if (tipo == "OT")
                            {
                                int id = int.Parse(x.ItemArray[2].ToString());
                                if (id != idOrdenTrabajo)
                                {
                                    emitirBoleta_AD.AgregarItemDetalle(_cantidad, _tipoItem, _idItem, _nombreItem, _precioUni, _total);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Esa Orden ya esta ingresada en el detalle del documento");
                                }
                            }
                            else
                            {
                                emitirBoleta_AD.AgregarItemDetalle(_cantidad, _tipoItem, _idItem, _nombreItem, _precioUni, _total);
                                this.Close();
                            }

                        }
                    }
                    else
                    {
                        int idOrdenTrabajo = int.Parse(cbxOrdenTrabajo.SelectedValue.ToString());
                        string folio = idOrdenTrabajo.ToString();
                        for (int i = 0; i < 9; i++)
                        {
                            if (folio.Length < 8)
                                folio = "0" + folio;
                        }
                        string _cantidad = "1";
                        string _tipoItem = "OT";
                        string _idItem = cbxOrdenTrabajo.SelectedValue.ToString();
                        string _nombreItem = "ORDEN DE TRABAJO N°" + folio;
                        ReservaNEG reservaNEG = new ReservaNEG();
                        string _total = reservaNEG.CargarReserva(idOrdenTrabajo).TOTAL.ToString();
                        string _precioUni = _total;
                        int id = idOrdenTrabajo;
                        emitirBoleta_AD.AgregarItemDetalle(_cantidad, _tipoItem, _idItem, _nombreItem, _precioUni, _total);
                        this.Close();
                    }
                    
                }
                else
                { MessageBox.Show("Debe seleccionar una Orden de Trabajo"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }
    }
}
