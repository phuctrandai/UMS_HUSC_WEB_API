//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMS_HUSC_WEB_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QUANHUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUANHUYEN()
        {
            this.PHUONGXAs = new HashSet<PHUONGXA>();
            this.QUEQUANs = new HashSet<QUEQUAN>();
            this.THONGTINLIENHEs = new HashSet<THONGTINLIENHE>();
            this.THUONGTRUs = new HashSet<THUONGTRU>();
        }
    
        public int MaQuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public Nullable<int> MaThanhPho { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHUONGXA> PHUONGXAs { get; set; }
        public virtual THANHPHO THANHPHO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUEQUAN> QUEQUANs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THONGTINLIENHE> THONGTINLIENHEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUONGTRU> THUONGTRUs { get; set; }
    }
}
