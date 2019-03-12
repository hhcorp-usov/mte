using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mte.Models
{
    public partial class EnterprisesView
    {
        public IEnumerable<Enterprises> Enterprises { get; set; }
        public Enterprises ModelInfo { get; set; }
        public PagingInfo PageInfo { get; set; }
    }

    public partial class SmenesView
    {
        public IEnumerable<Smenes> DataItems { get; set; }
        public Smenes ModelInfo { get; set; }
        public PagingInfo PageInfo { get; set; }
    }

}