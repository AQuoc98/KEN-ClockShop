//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClockUniverse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTable
    {
        public int Watch_ID { get; set; }
        public string Watch_Name { get; set; }
        public string Watch_Description { get; set; }
        public int WatchType_ID { get; set; }
        public decimal Original_Price { get; set; }
        public decimal Selling_Price { get; set; }
        public int InStock { get; set; }
    }
}
