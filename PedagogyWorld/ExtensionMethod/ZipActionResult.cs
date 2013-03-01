using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedagogyWorld.ExtensionMethod
{
    public class ZipResult : ActionResult
    {
        private IEnumerable<string> _files;
        public string ZipFileName { get; set; }

        public ZipResult(IEnumerable<string> files, string zipFileName)
        {
            _files = files;
            ZipFileName = zipFileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            using (var zipFile = new ZipFile())
            {
                zipFile.AddFiles(_files, false, "");
                context.HttpContext.Response.ContentType = "application/zip";
                context.HttpContext.Response.AppendHeader("content-disposition", "attachment; filename=" + ZipFileName);
                zipFile.Save(context.HttpContext.Response.OutputStream);
            }
        }

    }
}