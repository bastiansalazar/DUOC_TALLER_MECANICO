using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
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
    /// Lógica de interacción para AgregarServiciosRequerimientos3.xaml
    /// </summary>
    public partial class AgregarServiciosRequerimientos3 : Window
    {
        private int diagnostico;
        private ModificarRequerimientos2 modificarRequerimientos2;
        string estado;
        private int indice;
        int idSucursal;
        int servicio;
        int costoSErvicio;

        public AgregarServiciosRequerimientos3()
        {
            InitializeComponent();
        }

        public AgregarServiciosRequerimientos3(int idSucursal,int diagnostico,ModificarRequerimientos2 modificarRequerimientos2,int servicio, string estado,int costo,int indice)
        {
            InitializeComponent();
            this.diagnostico = diagnostico;
            this.modificarRequerimientos2 = modificarRequerimientos2;
            this.indice = indice;
            this.idSucursal = idSucursal;
            this.estado = estado;
            this.servicio = servicio;
            this.costoSErvicio = costo;
            DiagnosticoDAL diagnosticoDAL = new DiagnosticoDAL();
            ServiciosXDiagnosticoVIEW datos = diagnosticoDAL.CargarServicioXDiagnostico(diagnostico,servicio);
            SucursalDAL sucursalDAL = new SucursalDAL();
            txtNombreSucursal.Text = sucursalDAL.CargarSucursal(idSucursal).NOMBRE;
            txtNombreServicio.Text = datos.NombreServicio;
            txtValor.Text = datos.CostoServicio.ToString();    
            cbxEstado.SelectedValue = estado;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (cbxEstado.SelectedValue != null)
            {
                int nuevoEstado = cbxEstado.SelectedIndex;
                int estadoAntiguo = 0;
                if (estado == "ANALIZANDO")
                {
                    estadoAntiguo = 0;
                }else if (estado == "EN PROCESO")
                {
                    estadoAntiguo = 1;
                }
                else if (estado == "COMPLETADO")
                {
                    estadoAntiguo = 2;
                }

                if (estadoAntiguo < nuevoEstado)
                {
                    if (nuevoEstado == 0)
                    {
                        estado = "ANALIZANDO";
                    }
                    else if (nuevoEstado == 1)
                    {
                        estado = "EN PROCESO";
                    }
                    else if (nuevoEstado == 2)
                    {
                        estado = "COMPLETADO";
                    }
                    modificarRequerimientos2.ActualizarItemTablaServicio(indice, servicio, txtNombreServicio.Text, estado, costoSErvicio);      
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El estado siempre debe ser prorgesivo");
                }           
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar un estado");
            }
        }
    }
}
