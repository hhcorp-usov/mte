using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mte.Models
{
    public partial class MteWorkShift
    {
        public Employers CurrentDisp { get; set; }
        public Employers CurrentControler { get; set; }
    }
}