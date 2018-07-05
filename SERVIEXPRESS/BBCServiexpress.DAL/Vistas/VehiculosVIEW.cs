using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class VehiculosVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public string PATENTE { get; set; }
        public int CLIENTE_ID { get; set; }
        public int MARCA_VEHICULO_ID { get; set; }
        public int TIPO_VEHICULO_ID { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public Nullable<int> RUT_CLIENTE { get; set; }
        public string DIV_CLIENTE { get; set; }
        public string TIPO { get; set; }
        public string MARCA { get; set; }

    }
}
