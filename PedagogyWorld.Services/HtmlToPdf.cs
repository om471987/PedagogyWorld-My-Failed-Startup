using System.Diagnostics;

namespace PedagogyWorld.Services
{
    public class HtmlToPdf
    {
        public bool Convert(string Url, string filename)
        {
            // assemble destination PDF file name
            //string filename = ConfigurationManager.AppSettings["ExportFilePath"] + "\\" + output + ".pdf";

            // get proj no for header
            //Project project = new Project(int.Parse(output));

            var p = new Process();
            p.StartInfo.FileName = @"C:\Program Files (x86)\wkhtmltopdf\wkhtmltopdf.exe";//ConfigurationManager.AppSettings["HtmlToPdfExePath"];

            string switches = "--print-media-type ";
            switches += "--margin-top 4mm --margin-bottom 4mm --margin-right 0mm --margin-left 0mm ";
            switches += "--page-size A4 ";
            switches += "--no-background ";
            switches += "--redirect-delay 100";

            p.StartInfo.Arguments = switches + " " + Url + " " + filename;

            p.StartInfo.UseShellExecute = false; // needs to be false in order to redirect output
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true; // redirect all 3, as it should be all 3 or none
            //p.StartInfo.WorkingDirectory = @"C:\Users\omkar\Desktop";//StripFilenameFromFullPath(p.StartInfo.FileName);
            p.Start();

            // read the output here...
            string output = p.StandardOutput.ReadToEnd();

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();

            // if 0 or 2, it worked (not sure about other values, I want a better way to confirm this)
            return (returnCode == 0 || returnCode == 2);
        }
    }
}