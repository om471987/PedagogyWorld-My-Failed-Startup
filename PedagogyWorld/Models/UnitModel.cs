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

        public IEnumerable<SelectListItem> Outcomes { get; set; }
        public int[] OutcomeIds { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
        public int[] SubjectIds { get; set; }

        public IEnumerable<SelectListItem> Grades { get; set; }
        public int[] GradeIds { get; set; }
    }
}