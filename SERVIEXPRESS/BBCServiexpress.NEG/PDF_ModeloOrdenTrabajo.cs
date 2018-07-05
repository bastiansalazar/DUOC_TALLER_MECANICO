using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class PDF_ModeloOrdenTrabajo
    {
        public string Folio;
        public string FechaIngreso;
        public string FechaReserva;
        public string EstadoDiagnostico;
        public string NombreCliente;
        public string ApellidoCliente;
        public string NumCliente;
        public string DivCliente;
        public string DireccionCliente;
        public string ComunaCliente;
        public string TelCliente;
        public string Tel2Cliente;
        public string TipoVehiculo;
        public string MarcaVehiculo;
        public string PatenteVehiculo;
        public string NombreEmpleado;
        public string NombreSucursal;
        public string DireccionSucursal;
        public string TelefonoSucursal;
        public string Observacion;
        public List<ServiciosXDiagnosticoVIEW> ListaServicios = new List<ServiciosXDiagnosticoVIEW>();
        public List<ProductosXDiagnostico> ListaProductos = new List<ProductosXDiagnostico>();
        public int TotalTrabajo;
        public string CorreroSucursal;
        public string CorreoCliente;
    }
}
