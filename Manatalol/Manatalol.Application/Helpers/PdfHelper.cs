using UglyToad.PdfPig;

namespace Manatalol.Application.Helpers
{

    public static class PdfHelper
    {
        public static string ExtractText(byte[] pdfBytes)
        {
            using var stream = new MemoryStream(pdfBytes);
            using var pdf = PdfDocument.Open(stream);
            var text = new System.Text.StringBuilder();

            foreach (var page in pdf.GetPages())
            {
                text.AppendLine(page.Text);
            }

            return text.ToString();
        }
    }
}
