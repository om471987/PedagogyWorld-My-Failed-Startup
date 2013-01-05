using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PedagogyWorld.Data;

namespace PedagogyWorld.Models
{
    public class FileModel
    {
        [Required]
        public string Name { get; set; }

        public int FileType { get; set; }

        public Guid UnitId { get; set; }
    }
}