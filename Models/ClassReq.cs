using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeActivityTraking.Models
{
    public class ClassReq
    {
        public IEnumerable<RequestHelp> Requests { get; set; }
        public SelectList Status { get; set; }
    }
}