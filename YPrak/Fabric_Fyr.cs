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
    
    public partial class Fabric_Fyr
    {
        public string Fyr_Id { get; set; }
        public int Count { get; set; }
        public int Party { get; set; }
    
        public virtual Fyr Fyr { get; set; }
    }
}
