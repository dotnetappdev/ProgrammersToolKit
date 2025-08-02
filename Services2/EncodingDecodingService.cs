using System;
using System.Text;

namespace ProgrammersToolKit.Services
{
    public class EncodingDecodingService : Interfaces.IEncodingDecodingService
    {
        public string Encode(string input, string encodingType)
        {
            switch (encodingType.ToLower())
            {
                case "base64":
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
                case "url":
                    return Uri.EscapeDataString(input);
                case "hex":
                    var bytes = Encoding.UTF8.GetBytes(input);
                    return BitConverter.ToString(bytes).Replace("-", "");
                default:
                    throw new NotSupportedException($"Encoding '{encodingType}' not supported.");
            }
        }

        public string Decode(string input, string encodingType)
        {
            switch (encodingType.ToLower())
            {
                case "base64":
                    var bytes = Convert.FromBase64String(input);
                    return Encoding.UTF8.GetString(bytes);
                case "url":
                    return Uri.UnescapeDataString(input);
                case "hex":
                    var numberChars = input.Length;
                    var bytesArr = new byte[numberChars / 2];
                    for (int i = 0; i < numberChars; i += 2)
                        bytesArr[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
                    return Encoding.UTF8.GetString(bytesArr);
                default:
                    throw new NotSupportedException($"Decoding '{encodingType}' not supported.");
            }
        }
    }
}
