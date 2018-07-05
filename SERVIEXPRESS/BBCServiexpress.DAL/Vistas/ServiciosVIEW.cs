using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ServiciosVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public string ESTADO_SERVICIO { get; set; }
        public string SUCURSAL { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public Nullable<decimal> COSTO { get; set; }
        public string CALCULO_X_PROD { get; set; }
        public double VALOR_CALCULO { get; set; }
        public int Estado_Servicio_Id { get; set; }
        public int Sucursal_Id { get; set; }
        public int Tipo_Servicio_Id { get; set; }
    }
}
