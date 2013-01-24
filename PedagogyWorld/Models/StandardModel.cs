using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PedagogyWorld.Models
{
    public class StandardModel
    {
        public IEnumerable<SelectListItem> Standards { get; set; }

        public StrandDomain StrandDomain { get; set; }

        public Header Header { get; set; }

        public Guid Id { get; set; }

        public int[] StandardIds { get; set; }
    }

    public class StandardType
    {
        public bool IsChecked { get; set; }
        public string Type { get; set; }
    }
}