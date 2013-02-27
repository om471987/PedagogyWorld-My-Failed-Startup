using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace PedagogyWorld.Models
{
    public class UnitModel
    {
        public UnitModel()
        {
            Unit = new Unit();
        }

        public Unit Unit { get; set; }

        public IEnumerable<SelectListItem> OutcomeTypes { get; set; }

        public int[] OutcomeIds { get; set; }
    }
}