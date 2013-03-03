using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedagogyWorld.CustomAttributes
{
    public class MustSelectAttribute : ValidationAttribute, IClientValidatable
    {
        public MustSelectAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            return value == null ? false : true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            throw new NotImplementedException();
        }
    }
}