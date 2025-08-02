using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Text;

namespace ProgrammersToolKit.Services
{
    public class HexEditorService : IHexEditorService
    {
        public string HexToText(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return Encoding.UTF8.GetString(bytes);
        }

        public string TextToHex(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}
