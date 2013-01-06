using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;

namespace PedagogyWorld.Models
{
    public class UnitModel
    {
        public UnitModel()
        {
            var db = new Context();
            var ids = (from t in db.Outcomes select t.Id).ToArray();
            OutcomesList = new MultiSelectList(db.Outcomes.ToList(), "Id", "Name", ids);
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int GradeId { get; set; }

        public int SubjectId { get; set; }

        public MultiSelectList OutcomesList { get; private set; }

        public int[] SelectedOutcomes { get; set; }
    }
}