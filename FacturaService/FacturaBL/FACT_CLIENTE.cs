//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FacturaBL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FACT_CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FACT_CLIENTE()
        {
            this.FACT_FACTURA = new HashSet<FACT_FACTURA>();
        }
    
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int Estregistro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACT_FACTURA> FACT_FACTURA { get; set; }
    }
}
