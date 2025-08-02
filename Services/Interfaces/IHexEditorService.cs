namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IHexEditorService
    {
        string HexToText(string hex);
        string TextToHex(string text);
    }
}
