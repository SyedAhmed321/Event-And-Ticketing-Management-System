using QRCoder;

namespace Event___Ticketing_Management_System.Helpers
{
    public static class QRCodeHelper
    {
        public static string GenerateQRCode(string content)
        {
            using var generator = new QRCodeGenerator();
            var qrData = generator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);
            var bytes = qrCode.GetGraphic(10);
            return "data:image/png;base64," + Convert.ToBase64String(bytes);
        }
    }
}
