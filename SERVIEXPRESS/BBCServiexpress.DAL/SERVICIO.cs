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
    
    public partial class SERVICIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVICIO()
        {
            this.DETALLE_VENTAS = new HashSet<DETALLE_VENTAS>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public int ESTADO_SERVICIO_ID { get; set; }
        public int SUCURSAL_ID { get; set; }
        public int TIPO_SERVICIO_ID { get; set; }
        public Nullable<decimal> COSTO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_VENTAS> DETALLE_VENTAS { get; set; }
        public virtual ESTADO_SERVICIO ESTADO_SERVICIO { get; set; }
        public virtual TIPO_SERVICIO TIPO_SERVICIO { get; set; }
        public virtual SUCURSAL SUCURSAL { get; set; }
    }
}
