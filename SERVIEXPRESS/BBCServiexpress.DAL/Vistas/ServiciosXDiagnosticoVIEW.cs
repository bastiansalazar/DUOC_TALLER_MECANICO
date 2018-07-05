using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ServiciosXDiagnosticoVIEW
    {
        public decimal IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string EstadoServicio { get; set; }
        public Nullable<decimal> CostoServicio { get; set; }

    }
}
