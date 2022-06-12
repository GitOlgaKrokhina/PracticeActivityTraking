using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeActivityTraking.Models
{
    public class ClassifiedEvent
    {
        public IEnumerable<Event> Events { get; set; }
        public SelectList Formats { get; set; }
        public SelectList Status { get; set; }
        public SelectList Types { get; set; }
    }
}