using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ReservaVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public int CLIENTE_ID { get; set; }
        public int EMPLEADO_ID { get; set; }
        public int ESTADO_RESERVA_ID { get; set; }
        public int SUCURSAL_ID { get; set; }
        public int TIPO_RESERVA_ID { get; set; }
        public int VEHICULO_ID { get; set; }
        public string ORSERVACION_FINAL { get; set; }
        public Nullable<System.DateTime> FECHA_RESERVA { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string NOMBRE_APELLIDO { get; set; }
        public Nullable<int> NUM_ID_CLIENTE { get; set; }
        public string DIV_CLIENTE { get; set; }
        public Nullable<int> TELEFONO_CLIENTE { get; set; }
        public Nullable<int> TELEFONO_CLIENTE2 { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string COMUNA_CLIENTE { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public string APELLIDO_EMPLEADO { get; set; }
        public string ESTADO_RESERVA { get; set; }
        public string ESTADO_DIAGNOSTICO { get; set; }
        public int ID_DIAGNOTICO { get; set; }
        public string NOMBRE_SUCURSAL { get; set; }
        public string DIRECCION_SUCURSAL { get; set; }
        public Nullable<int> TELEFONO_SUCURSAL { get; set; }
        public string TIPO_RESERVA { get; set; }
        public string TIPO_VEHICULO { get; set; }
        public string MARCA_VEHICULO { get; set; }
        public string PATENTE_VEHICULO { get; set; }
        public string CORREO_SUCURSAL { get; set; }
        public string CORREO_CLIENTE { get; set; }
        public Nullable<Decimal> TOTAL { get; set; }
    }
}
