using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DE4.Models
{
    public class NoiDung
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> NgayBD { get; set; }
        public Nullable<System.DateTime> NgayKT { get; set; }
        public string TenDonVi { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> DaDuyet { get; set; }
    }
}