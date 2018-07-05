using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL.Vistas
{
    public class ProductosVIEW
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<System.DateTime> FECHA_VENCIMIENTO { get; set; }
        public Nullable<decimal> PRECIO_COMPRA { get; set; }
        public Nullable<decimal> PRECIO_VENTA { get; set; }
        public string SKU { get; set; }
        public Nullable<int> STOCK { get; set; }
        public Nullable<int> STOCK_CRITICO { get; set; }
        public int CATEGORIA_ID { get; set; }
        public Nullable<decimal> SUCURSAL_ID { get; set; }
        public int ESTADO_PRODUCTO_ID { get; set; }
        public int MARCA_ID { get; set; }
        public Nullable<decimal> TIPO_PRODUCTO_ID { get; set; }
        public Nullable<decimal> PROVEEDOR_ID { get; set; }
        public string PROVEEDOR { get; set; }
        public string CATEGORIA { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string SUCURSAL { get; set; }
        public string ESTADO_PRODUCTO { get; set; }
        public string MARCA { get; set; }
    }
}
