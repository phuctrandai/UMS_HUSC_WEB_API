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
    
    public partial class QUOCGIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUOCGIA()
        {
            this.THANHPHOes = new HashSet<THANHPHO>();
            this.NOISINHs = new HashSet<NOISINH>();
            this.THONGTINCHUNGs = new HashSet<THONGTINCHUNG>();
            this.QUEQUANs = new HashSet<QUEQUAN>();
            this.THONGTINLIENHEs = new HashSet<THONGTINLIENHE>();
            this.THUONGTRUs = new HashSet<THUONGTRU>();
        }
    
        public int MaQuocGia { get; set; }
        public string TenQuocGia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THANHPHO> THANHPHOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOISINH> NOISINHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THONGTINCHUNG> THONGTINCHUNGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUEQUAN> QUEQUANs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THONGTINLIENHE> THONGTINLIENHEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUONGTRU> THUONGTRUs { get; set; }
    }
}
