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
    
    public partial class Textile_Consist
    {
        public string Textile_Id { get; set; }
        public int Consist_Id { get; set; }
        public int Textile_Consist_Id { get; set; }
    
        public virtual Consist Consist { get; set; }
        public virtual Textile Textile { get; set; }
    }
}
