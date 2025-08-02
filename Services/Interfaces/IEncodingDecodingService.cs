namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IEncodingDecodingService
    {
        string Encode(string input, string encodingType);
        string Decode(string input, string encodingType);
    }
}
