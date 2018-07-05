using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ProveedoresVIEW
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }

        public string NOMBRE_EMPRESA { get; set; }
        public Nullable<int> NUM_ID { get; set; }
        public string DIV_ID { get; set; }
        public string DIRECCION { get; set; }
        public string COMUNA { get; set; }
        public Nullable<int> TELEFONO_CELULAR { get; set; }
        public Nullable<int> TELEFONO_FIJO { get; set; }
        public string ESTADO_PERSONA { get; set; }
        public string TIPO_PERSONA { get; set; }
        public string ESTADO_PROVEEDOR { get; set; }
        public string TIPO_PROVEEDOR { get; set; }
        public Nullable<System.DateTime> FECHA_NACIMIENTO { get; set; }
        public string CORREO { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdProvincia { get; set; }
        public int IdComuna { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdEstadoPersona { get; set; }
        public int IdEstadoProveedor { get; set; }
        public int IdTipoProveedor { get; set; }
    }
}
