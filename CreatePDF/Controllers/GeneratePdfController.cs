using CreatePDF.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreatePDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratePdfController : ControllerBase
    {
        // GET: api/<CreatePdfController>

        private IConverter _converter;
        public GeneratePdfController(IConverter converter)
        {
            _converter = converter;
        }
        [HttpPost("{reportname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<string> Post([FromRoute] string reportname)
        {
            string teamplateName = reportname switch
            {
                "ine" => "Views/inepdf.html",
                "cfe" => "Views/cfepdf.html",
                "rfc" => "Views/rfcpdf.html",
                "curp" => "Views/curppdf.html",
                "wh" => "Views/workhisotrypdf.html",
                "legal" => "Views/legalOpinionpdf.html",
                "bgc" => "Views/bgcpdf.html",
                "idi" => "Views/idi.html",
                _ => string.Empty
            };
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string html = GeneratePDFHelper.Template(dic , teamplateName);
            byte[] file = _converter.Convert(GeneratePDFHelper.HtmlToPDF(html));
            return File(file, MediaTypeNames.Application.Pdf, $"{Guid.NewGuid()}.pdf");
        }
    }
}
