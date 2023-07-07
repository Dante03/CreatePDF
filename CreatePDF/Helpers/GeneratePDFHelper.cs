using DinkToPdf;

namespace CreatePDF.Helpers
{
    public static class GeneratePDFHelper
    {
        public static HtmlToPdfDocument HtmlToPDF(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 0, Bottom = 4, Left = 0, Right = 0 },
                DocumentTitle = "Test",
                
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = {
                    DefaultEncoding = "utf-8"
                },
                FooterSettings = {
                    FontSize = 7,
                    Left = "Este documento es proporcionado para la verificación de la información ingresada. No es un documento oficial.",
                    Right = "pag [page]/[toPage]",
                    Line = false,
                },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return htmlToPdfDocument;
        }
        public static string Template(Dictionary<string, string> dictionary, string filePath)
        {
            string rootpath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var path = Path.Combine(rootpath, filePath);

            //Read file content
            string htmlContent = File.ReadAllText(path);

            string klibuimage = $"{rootpath}Views/Images/logocolor.jpg";
            Byte[] klibubytes = System.IO.File.ReadAllBytes(klibuimage);
            dictionary.Add("[KLIBU]", $"data:image;base64,{Convert.ToBase64String(klibubytes)}");

            // Set data
            foreach (var v in dictionary)
            {
                htmlContent = htmlContent.Replace(
                    v.Key,
                    v.Value
                );
            }
            return htmlContent;
        }
    }
}
