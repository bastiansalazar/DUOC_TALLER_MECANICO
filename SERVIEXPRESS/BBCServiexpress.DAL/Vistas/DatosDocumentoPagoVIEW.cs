using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class DatosDocumentoPagoVIEW
    {
        public string NOMBRE_MULTIEMPRESA;
        public string DIRECCION_MULTIEMPRESA;
        public Nullable<int> TELEFONO_MULTIEMPRESA;
        public string RUT_MULTIEMPRESA;
        public int FOLIO;
        public string NOMBRE_CLIENTE;
        public string APELLIDO_CLIENTE;
        public string DIRECCION_CLIENTE;
        public string COMUNA_CLIENTE;
        public Nullable<DateTime> FECHA_EMISION;
        public Nullable<int> NUM_CLIENTE;
        public string DIV_CLIENTE;
        public string CORREO_CLIENTE;
        public Nullable<int> TELEFONO1_CLIENTE;
        public Nullable<int> TELEFONO_CLIENTE;
        public string TIPO_DOCUMENTO;
        public List<DetalleBoletaVIEW> DETALLE_BOLETA;
        public int TOTAL_NETO;
        public int IVA;
        public Nullable<decimal> TOTAL;
        public Nullable<decimal> COSTO_MONEDA;
        public string TIPO_MONEDA;

    }
}
