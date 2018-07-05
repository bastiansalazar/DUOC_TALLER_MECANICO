using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ProductosXDiagnostico
    {
        public Nullable<Decimal> IdProducto { get; set; }
        public string NombreProdcuto { get; set; }
        public Nullable<Decimal> Cantidad { get; set; }
        public Nullable<Decimal> PrecioUni { get; set; }
        public Nullable<Decimal> PrecioTot { get; set; }
        
    }
}
