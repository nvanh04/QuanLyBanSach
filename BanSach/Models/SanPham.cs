//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.DonHangCTs = new HashSet<DonHangCT>();
        }
    
        public int IDsp { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> TheLoai { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string HinhAnh { get; set; }
        public string TacGia { get; set; }
        public string NhaXB { get; set; }
        public Nullable<System.DateTime> NamXB { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual DanhMuc DanhMuc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHangCT> DonHangCTs { get; set; }
    }
}
