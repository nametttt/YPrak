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
    
    public partial class Textile_Color
    {
        public string Textile_Id { get; set; }
        public int Color_Id { get; set; }
        public int Textile_Color_Id { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual Textile Textile { get; set; }
    }
}