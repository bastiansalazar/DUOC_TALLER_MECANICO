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
    
    public partial class TIPO_RESERVA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_RESERVA()
        {
            this.RESERVA_HORA = new HashSet<RESERVA_HORA>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_UPDATE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESERVA_HORA> RESERVA_HORA { get; set; }
    }
}
