//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BBCServiexpress.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            this.DETALLE_ANULACION_VENTA = new HashSet<DETALLE_ANULACION_VENTA>();
            this.DETALLE_CONTROL_RECEPCION = new HashSet<DETALLE_CONTROL_RECEPCION>();
            this.DETALLE_ORDEN_PEDIDO = new HashSet<DETALLE_ORDEN_PEDIDO>();
            this.DETALLE_VENTAS = new HashSet<DETALLE_VENTAS>();
        }
    
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
        public int ESTADO_PRODUCTO_ID { get; set; }
        public int MARCA_ID { get; set; }
        public Nullable<decimal> ID_SUCURSAL { get; set; }
        public Nullable<decimal> ID_PROVEERDOR { get; set; }
        public Nullable<decimal> TIPO_PRODUCTO { get; set; }
    
        public virtual CATEGORIA CATEGORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_ANULACION_VENTA> DETALLE_ANULACION_VENTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_CONTROL_RECEPCION> DETALLE_CONTROL_RECEPCION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_ORDEN_PEDIDO> DETALLE_ORDEN_PEDIDO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_VENTAS> DETALLE_VENTAS { get; set; }
        public virtual ESTADO_PRODUCTO ESTADO_PRODUCTO { get; set; }
        public virtual MARCA MARCA { get; set; }
    }
}
