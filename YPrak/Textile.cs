//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YPrak
{
    using System;
    using System.Collections.Generic;
    
    public partial class Textile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Textile()
        {
            this.Fabric_Textile = new HashSet<Fabric_Textile>();
            this.Textile_Color = new HashSet<Textile_Color>();
            this.Textile_Consist = new HashSet<Textile_Consist>();
            this.Textile_Product = new HashSet<Textile_Product>();
        }
    
        public string Textile_Id { get; set; }
        public string Name { get; set; }
        public int Picture_Id { get; set; }
        public int Width { get; set; }
        public int Lenght { get; set; }
        public int Cost { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fabric_Textile> Fabric_Textile { get; set; }
        public virtual Picture Picture { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Textile_Color> Textile_Color { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Textile_Consist> Textile_Consist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Textile_Product> Textile_Product { get; set; }
    }
}
